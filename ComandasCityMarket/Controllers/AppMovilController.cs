using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing.Printing;//PRINT
using System.Drawing;//PRINT
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Serialization.Json;
using ComandasCityMarket.Models;


using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace ComandasCityMarket.Controllers
{
    public class AppMovilController : Controller
    {
        string sFont = "Lucida Console";

        //
        // GET: /AppMovil/
        [HttpPost]
        public ActionResult getCatalogo(ReqCatalogo reqCatalogo)
        {
            Catalogo catalogo = new Catalogo();
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                List<Categoria> categorias = new List<Categoria>();
                List<Articulo> articulos = new List<Articulo>();
                List<Modificador> modificadores = new List<Modificador>();
                try//EL SIGUEINTE BLOQUE ES PARA EXTRAER LAS CATEGORIAS
                {
                    SqlCommand command = new SqlCommand("SELECT A.* FROM AGRUPACION_CAT A, RESTAURANT_AGRUP B WHERE B.REST_ID = " + reqCatalogo.rest_id + " AND A.AGRU_PADRE = 0 AND A.AGRU_ID=B.AGRU_ID", myConnection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Categoria categoria = new Categoria();
                        List<Modificador> listaModificadores = new List<Modificador>();
                        categoria.agru_des = reader["agru_des"].ToString();
                        categoria.agru_desc = reader["agru_desc"].ToString();
                        categoria.agru_id = Convert.ToInt32(reader["agru_id"].ToString());
                        categoria.agru_padre = Convert.ToInt32(reader["agru_padre"].ToString());
                        categoria.agru_tipo = Convert.ToInt32(reader["agru_tipo"].ToString());
                        if (categoria.agru_tipo == 1)
                        {
                            categoria.hasSubCat = true;
                            //SqlCommand commandFindSub = new SqlCommand("SELECT * FROM AGRUPACION_CAT WHERE AGRU_TIPO = 1 AND AGRU_PADRE = "+categoria.agru_padre, myConnection);
                            //SqlCommand commandFindSub = new SqlCommand("SELECT * FROM AGRUPACION_CAT WHERE AGRU_PADRE = " + categoria.agru_id, myConnection);
                            SqlCommand commandFindSub = new SqlCommand("SELECT A.* FROM AGRUPACION_CAT A, RESTAURANT_AGRUP B WHERE B.REST_ID = " + reqCatalogo.rest_id + " AND A.AGRU_PADRE = "+ categoria.agru_id+" AND A.AGRU_ID=B.AGRU_ID" , myConnection);
                            
                            SqlDataReader readerSub = commandFindSub.ExecuteReader();
                            List<SubCategoria> listasubCateg = new List<SubCategoria>();
                            while (readerSub.Read())
                            {
                                SubCategoria subCat = new SubCategoria();
                                subCat.agru_des = readerSub["agru_des"].ToString();
                                subCat.agru_desc = readerSub["agru_desc"].ToString();
                                subCat.agru_id = Convert.ToInt32(readerSub["agru_id"].ToString());
                                subCat.agru_padre = Convert.ToInt32(readerSub["agru_padre"].ToString());
                                subCat.agru_tipo = Convert.ToInt32(readerSub["agru_tipo"].ToString());
                                SqlCommand commandFindSubArt = new SqlCommand("SELECT A.AGRU_ID, A.ART_DES, A.ART_DESC, A.ART_EAN, A.TIPP_ID, B.ART_PRECIO FROM ARTICULO A, ARTICULO_PRECIO B WHERE  A.AGRU_ID = " + subCat.agru_id + " AND B.ART_EAN = A.ART_EAN AND B.SUCC_ID =" + reqCatalogo.succ_id, myConnection);
                                SqlDataReader readerSubArt = commandFindSubArt.ExecuteReader();
                                List<Articulo> listArticulos = new List<Articulo>();
                                while (readerSubArt.Read())
                                {
                                    Articulo articulo = new Articulo();
                                    articulo.agru_id = Convert.ToInt32(readerSubArt["agru_id"].ToString());
                                    articulo.art_des = readerSubArt["art_des"].ToString();
                                    articulo.art_desc = readerSubArt["art_desc"].ToString();
                                    articulo.art_ean = Convert.ToDecimal(readerSubArt["art_ean"].ToString());
                                    articulo.art_precio = Convert.ToDecimal(readerSubArt["art_precio"].ToString());
                                    articulo.tipp_id = Convert.ToInt32(readerSubArt["tipp_id"].ToString());
                                    listArticulos.Add(articulo);
                                }//fin while busqueda Articulos por Casa SubCategoria
                                subCat.articulos = listArticulos;
                                SqlCommand commandFindModificadores = new SqlCommand("SELECT * FROM AGRUPACION_MODIF WHERE AGRU_ID = " + subCat.agru_id, myConnection);
                                SqlDataReader readerModif = commandFindModificadores.ExecuteReader();
                                while (readerModif.Read())
                                {
                                    Modificador modificador = new Modificador();
                                    modificador.agru_consec = Convert.ToInt32(readerModif["agru_consec"].ToString());
                                    modificador.agru_des = readerModif["agru_des"].ToString();
                                    modificador.agru_desc = readerModif["agru_desc"].ToString();
                                    modificador.agru_id = Convert.ToInt32(readerModif["agru_id"].ToString());
                                    listaModificadores.Add(modificador);
                                }
                                subCat.modificadores = listaModificadores;
                                //categoria.modificadores = listaModificadores;
                                listasubCateg.Add(subCat);
                            }//Fin Busqueda SubCategorias
                            categoria.subCat = listasubCateg;
                        }
                        else if (categoria.agru_tipo == 2)
                        {
                            categoria.hasSubCat = false;
                            SqlCommand commandFindArt = new SqlCommand("SELECT A.AGRU_ID, A.ART_DES, A.ART_DESC, A.ART_EAN, A.TIPP_ID, B.ART_PRECIO FROM ARTICULO A, ARTICULO_PRECIO B WHERE  A.AGRU_ID = " + categoria.agru_id + " AND B.ART_EAN = A.ART_EAN AND B.SUCC_ID =" + reqCatalogo.succ_id, myConnection);
                            SqlDataReader readerArt = commandFindArt.ExecuteReader();
                            List<Articulo> listArticulos = new List<Articulo>();
                            while (readerArt.Read())
                            {
                                Articulo articulo = new Articulo();
                                articulo.agru_id = Convert.ToInt32(readerArt["agru_id"].ToString());
                                articulo.art_des = readerArt["art_des"].ToString();
                                articulo.art_desc = readerArt["art_desc"].ToString();
                                articulo.art_ean = Convert.ToDecimal(readerArt["art_ean"].ToString());
                                articulo.art_precio = Convert.ToDecimal(readerArt["art_precio"].ToString());
                                articulo.tipp_id = Convert.ToInt32(readerArt["tipp_id"].ToString());
                                listArticulos.Add(articulo);
                            }//fin while busqueda Articulos por Casa SubCategoria
                            categoria.articulos = listArticulos;
                            SqlCommand commandFindModificadores = new SqlCommand("SELECT * FROM AGRUPACION_MODIF WHERE AGRU_ID = " + categoria.agru_id, myConnection);
                            SqlDataReader readerModif = commandFindModificadores.ExecuteReader();
                            while (readerModif.Read())
                            {
                                Modificador modificador = new Modificador();
                                modificador.agru_consec = Convert.ToInt32(readerModif["agru_consec"].ToString());
                                modificador.agru_des = readerModif["agru_des"].ToString();
                                modificador.agru_desc = readerModif["agru_desc"].ToString();
                                modificador.agru_id = Convert.ToInt32(readerModif["agru_id"].ToString());
                                listaModificadores.Add(modificador);
                            }//FIN while musca modificador
                            categoria.modificadores = listaModificadores;
                        }//end else
                        categorias.Add(categoria);
                    }//end while
                    catalogo.catalogo = categorias;
                }
                catch (SqlException exCat)
                {
                    catalogo.success = false;
                    catalogo.message = "ERROR 122" + exCat.Message;
                    return Json(catalogo);
                }
            }
            catch (Exception e)
            {
                catalogo.success = false;//En caso de caer en exception el estado del boleano se envia en falso y en message el detalle del error
                catalogo.message = "ERROR 168 " + e.Message;
                return Json(catalogo);
            }
            finally
            {
                myConnection.Close();//Cerrando dentro del finally la conexión realizada a la base de datos                
            }//fin Bloque completo de Try 
            catalogo.success = true;//Completando el objeto json con estado true y mensaje OK
            catalogo.message = "OK";
            return Json(catalogo);
        }//end GETCATALOGO
        [HttpPost]
        public ActionResult appEntrar(Acceso login)
        {
            RespAcceso respuesta = new RespAcceso();
            try
            {
                SqlDataReader reader = null;
                SqlConnection myConnection = new SqlConnection();
                try
                {
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                    myConnection.Open();
                    //SqlCommand command = new SqlCommand("select * from RESTAURANT a, EMPLEADO b where b.EMPL_COD = " + login.num_empleado + " and a.REST_ID = " + login.rest_id + " and b.EMPL_STAT ='ALTA'", myConnection);
                    SqlCommand command = new SqlCommand("select (select SUCC_DES from SUCURSAL where SUCC_ID = a.SUCC_ID) as SUCC_DES, * from 	RESTAURANT a, ff_cat_usuario b where b.usr_numempleado = " + login.num_empleado + " and a.REST_ID = " + login.rest_id, myConnection);
                    reader = command.ExecuteReader();
                    Empleado emp = null;
                    RestDetalle rest = null;
                    while (reader.Read())
                    {
                        emp = new Empleado();
                        rest = new RestDetalle();
                        rest.rest_des = reader["rest_des"].ToString();
                        rest.rest_id = Convert.ToInt32(reader["rest_id"].ToString());
                        rest.succ_id = Convert.ToInt32(reader["succ_id"].ToString());
                        rest.succ_des = reader["succ_des"].ToString();
                        /*
                        emp.empl_apm = reader["empl_apm"].ToString();
                        emp.empl_app = reader["empl_app"].ToString();
                        emp.empl_cod = Convert.ToInt32(reader["empl_cod"].ToString());
                        emp.empl_nom = reader["empl_nom"].ToString();
                        emp.empl_stat = reader["empl_stat"].ToString();
                        emp.empl_tipo = reader["empl_tipo"].ToString();
                        emp.succ_id = Convert.ToInt32(reader["succ_id"].ToString());
                        */
                        emp.empl_apm = "";
                        emp.empl_app = "";
                        emp.empl_cod = Convert.ToInt32(reader["usr_numempleado"].ToString());
                        emp.empl_nom = reader["usr_nombre"].ToString();
                        emp.empl_stat = "ALTA";
                        emp.empl_tipo = "MESERO";
                        emp.succ_id = Convert.ToInt32(reader["succ_id"].ToString());
                    }//end while
                    if (emp != null && rest != null)
                    {
                        respuesta.success = true;
                        respuesta.message = "OK";
                        respuesta.restaurant = rest;
                        respuesta.empleado = emp;
                    }
                    else
                    {
                        respuesta.success = false;
                        respuesta.message = "NO EXISTE RELACION EMPLEADO - RESTAURANT";
                        respuesta.restaurant = rest;
                        respuesta.empleado = emp;
                    }

                }
                catch (SqlException sqlExc)
                {
                    respuesta.success = false;
                    respuesta.message = "ERROR " + sqlExc.Message;
                    return Json(respuesta);
                }
                finally
                {
                    myConnection.Close();
                }
                //fin try-catch SQL 
            }
            catch (Exception ex)
            {
                respuesta.success = false;
                respuesta.message = "ERROR " + ex.Message;
                return Json(respuesta);
            }//Fin Try-catch General
            return Json(respuesta);
        }//END ENTRAR
        [HttpPost]
        public ActionResult getMesas(ReqDeleteRestaurant rest)
        {
            RespMesas mesas = new RespMesas();
            try
            {
                SqlDataReader reader = null;
                SqlConnection myConnection = new SqlConnection();
                List<Mesa> listaMesas = new List<Mesa>();
                try
                {
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                    myConnection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM MESA WHERE MESA_STAT = 'ALTA' AND REST_ID =@REST", myConnection);
                    command.Parameters.AddWithValue("@REST", rest.rest_id);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Mesa mesa = new Mesa();
                        mesa.mesa_cve = reader["mesa_cve"].ToString();
                        mesa.mesa_des = reader["mesa_des"].ToString();
                        mesa.mesa_id = Convert.ToInt32(reader["mesa_id"].ToString());
                        mesa.mesa_stat = reader["mesa_stat"].ToString();
                        mesa.rest_id = Convert.ToInt32(reader["rest_id"].ToString());
                        mesa.ubic_consec = Convert.ToInt32(reader["ubic_consec"].ToString());
                        listaMesas.Add(mesa);
                    }
                    mesas.mesas = listaMesas;
                    if (listaMesas.Count>0 )
                    {
                        mesas.success = true;
                        mesas.message = "OK";
                    }
                    else
                    {

                        mesas.success = false;
                        mesas.message = "No hay mesas disponibles";
                    }

                }
                catch (SqlException exc)
                {
                    mesas.success = false;
                    mesas.message = "ERROR " + exc.Message;
                }
            }
            catch (Exception ex)
            {
                mesas.success = false;
                mesas.message = "ERROR " + ex.Message;
            }
            return Json(mesas);
        }//END GETMESAS
        [HttpPost]
        public ActionResult getOrdenesActivas(Acceso acceso)
        {
            RespOrdenesActivas ordenesActivas = new RespOrdenesActivas();
            try
            {
                SqlDataReader reader = null;
                SqlConnection myConnection = new SqlConnection();
                List<OrdenActiva> listaOrdenes = new List<OrdenActiva>();
                try
                {
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                    myConnection.Open();
                    SqlCommand command = new SqlCommand("select a.ORDN_ID,a.ORDN_NPER, a.ORDN_IMPTOT, a.ORDN_STAT, a.MESA_ID, 		(select d.ordn_hmov from ORDEN_CTRL d where d.ORDN_ID = a.ORDN_ID) as ORDN_HMOV,		(select c.MESA_cve from MESA c where c.MESA_ID = a.MESA_ID) as MESA_CVE		  from orden a where a.ORDN_MESE= "+acceso.num_empleado+" and a.ORDN_STAT= 'INIC' or a.ORDN_STAT = 'CAMM'", myConnection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        OrdenActiva orden = new OrdenActiva();
                        orden.mesa_cve = reader["mesa_cve"].ToString();
                        orden.mesa_id = Convert.ToInt32(reader["mesa_id"].ToString());
                        orden.ordn_hmov = Convert.ToInt32(reader["ordn_hmov"].ToString());
                        orden.ordn_id = Convert.ToInt32(reader["ordn_id"].ToString());
                        orden.ordn_imptot = Convert.ToDecimal(reader["ordn_imptot"].ToString());
                        orden.ordn_nper = Convert.ToInt32(reader["ordn_nper"].ToString());
                        orden.ordn_stat = reader["ordn_stat"].ToString();
                        listaOrdenes.Add(orden);
                    }
                    ordenesActivas.total_ordenes = listaOrdenes.Count;
                    ordenesActivas.ordenesActivas = listaOrdenes;
                }
                catch (SqlException sqlex)
                {
                    ordenesActivas.success = false;
                    ordenesActivas.message = "ERROR " + sqlex;
                    return Json(ordenesActivas);
                }
                finally
                {
                    myConnection.Close();
                }
            }
            catch (Exception exc)
            {
                ordenesActivas.success = false;
                ordenesActivas.message = "ERROR " + exc.Message;
                return Json(ordenesActivas);
            }
            ordenesActivas.success = true;
            ordenesActivas.message = "OK";
            return Json(ordenesActivas);
        }//end ORDENESACTIVAS
        [HttpPost]
        public ActionResult getRestaurantes(ReqEmpleado req)
        {
            RespRestaurant respuestaRestaurantes = new RespRestaurant();
            try
            {
                SqlDataReader reader = null;
                SqlConnection myConnection = new SqlConnection();
                try
                {
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                    myConnection.Open();
                    SqlCommand command = new SqlCommand("select a.* from restaurant a where a.succ_id = (select usr_succ_id from ff_cat_usuario where usr_numempleado="+ req.empl_cod+ " )  ", myConnection);
                    reader = command.ExecuteReader();
                    List<Restaurant> listaRestaurantes = new List<Restaurant>();
                    while (reader.Read())
                    {
                        Restaurant restaurante = new Restaurant();
                        restaurante.rest_des = reader["rest_des"].ToString();
                        restaurante.rest_id = Convert.ToInt32(reader["rest_id"].ToString());
                        restaurante.succ_id = Convert.ToInt32(reader["succ_id"].ToString());
                        listaRestaurantes.Add(restaurante);
                    }//fin while
                    if (listaRestaurantes.Count == 0)
                    {
                        respuestaRestaurantes.success = false;
                        respuestaRestaurantes.message = "Mesero Inexistente";
                        return Json(respuestaRestaurantes);
                    }
                    respuestaRestaurantes.restaurantes = listaRestaurantes;
                }
                catch (SqlException exc)
                {
                    respuestaRestaurantes.success = false;
                    respuestaRestaurantes.message += "ERROR " + exc.Message;
                    return Json(respuestaRestaurantes);
                }
                finally
                {
                    myConnection.Close();//cerrando conexion de sql
                }//fin try-catch sql
            }
            catch (Exception ex)
            {
                respuestaRestaurantes.success = false;
                respuestaRestaurantes.message += "ERROR " + ex.Message;
                return Json(respuestaRestaurantes);
            }//fin try-catch general
            respuestaRestaurantes.success = true;
            respuestaRestaurantes.message += "OK";
            return Json(respuestaRestaurantes);
        }//fin GETRESTAURANTES
        [HttpPost]
        public ActionResult newOrden(NuevaOrden newOrden)
        {
            RespNuevaOrden resp = new RespNuevaOrden();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO ORDEN (ORDN_NPER, ORDN_IMPTOT, ORDN_STAT, MESA_ID,ORDN_MESE, ORDN_MESEORIG ) VALUES (@NPER,0, 'INIC',@MESA,@MESE,@MESE);  SELECT CAST(SCOPE_IDENTITY() AS INT )", myConnection);
                command.Parameters.AddWithValue("@NPER", newOrden.ordn_nper);
                command.Parameters.AddWithValue("@MESA", newOrden.mesa_id);
                command.Parameters.AddWithValue("@MESE", newOrden.ordn_mese);
                
                int idorden = (int)command.ExecuteScalar();
                if (idorden != 0)
                {
                    DateTime fecha = DateTime.Now;
                    int iFecha = Convert.ToInt32(""+fecha.Year+""+fecha.Month +""+fecha.Day);
                    int iHora = Convert.ToInt32(fecha.Hour + ""+fecha.Minute);                    
                    command = new SqlCommand("INSERT INTO ORDEN_CTRL (ORDN_ID, ORDN_STAT, EMPL_COD, ORDN_FMOV, ORDN_HMOV, ORDN_OBSV) VALUES (@IDORD,'INIC', @MESE,@FECH,@HORA,@OBSV)", myConnection);
                    resp.ordn_id = idorden;
                    command.Parameters.AddWithValue("@IDORD", idorden);
                    command.Parameters.AddWithValue("@MESE", newOrden.ordn_mese);
                    command.Parameters.AddWithValue("@FECH", iFecha);
                    command.Parameters.AddWithValue("@HORA", iHora);
                    command.Parameters.AddWithValue("@OBSV", newOrden.ordn_obsv);
                    if (0 < command.ExecuteNonQuery())
                    {
                        command = new SqlCommand("UPDATE MESA SET MESA_STAT = 'BAJA' WHERE MESA_STAT ='ALTA' AND MESA_ID = @MESA; ", myConnection);
                        command.Parameters.AddWithValue("@MESA", newOrden.mesa_id);

                        if (0 < command.ExecuteNonQuery())
                        {
                            resp.success = true;
                            resp.message = "OK";
                        }
                        else
                        {
                            resp.success = false;
                            resp.message = "MESA NO ACTUALIZADA";
                        }
                    }
                    else
                    {
                        resp.success = false;
                        resp.message = "ORDEN_CTRL NO ACTUALIZADA";
                    }
                }
                else
                {
                    resp.success = false;
                    resp.message = "NO SE CREO NUEVA ORDEN";
                }

                command.Parameters.Clear();

            }
            catch (SqlException ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            return Json(resp);
        }//END NEWORDEN
        [HttpPost]
        public ActionResult newComanda( newComanda newcomand)
        {
            SqlDataReader reader = null;
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            decimal imptot = 0;
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO COMANDA (ORDN_ID, COMA_STAT) VALUES (@ORDN, 'INIC') SELECT CAST(SCOPE_IDENTITY() AS  INT)", myConnection);
                command.Parameters.AddWithValue("@ORDN", newcomand.ordn_id);

                resp.message += "-1";
                int idComanda = (int)command.ExecuteScalar();
                if (0 < idComanda)
                {
                    resp.message += "-2";
                    DateTime fecha = DateTime.Now;
                    int iFecha = Convert.ToInt32("" + fecha.Year + "" + fecha.Month + "" + fecha.Day);
                    int iHora = Convert.ToInt32(fecha.Hour + "" + fecha.Minute);
                    resp.message += "-3";
                    command = null;
                    resp.message += "-4";
                    command = new SqlCommand("INSERT INTO COMANDA_CTRL (COMA_ID, COMA_STAT, COMA_FMOV, COMA_HMOV, COMA_OBSV) VALUES (@IDCOM, 'INIC', @FECH, @HORA, @OBSV)", myConnection);
                    resp.message += "-5";
                    command.Parameters.AddWithValue("@IDCOM", idComanda);
                    command.Parameters.AddWithValue("@FECH", iFecha);
                    command.Parameters.AddWithValue("@HORA", iHora);
                    command.Parameters.AddWithValue("@OBSV", newcomand.coma_obsv);
                    resp.message += "-6";
                    
                    if (0 < command.ExecuteNonQuery())
                    {
                        resp.message += "-7";

                        foreach (OrdenArticulo artOrd in newcomand.ordenarticulo)
                        {
                            resp.message += "-8";
                            command = new SqlCommand("INSERT INTO ORDEN_ARTICULO (ORDN_ID, COMA_ID, ART_EAN, ORDN_CANT, ORDN_IMPUNI, ORDN_IMPART, ORDN_OBSV) VALUES (@ORDN_ID,@COMA_ID,@ART_EAN,@ORDN_CANT,@ORDN_IMPUNI,@ORDN_IMPART,@OBSV)", myConnection);
                            command.Parameters.AddWithValue("@ORDN_ID", newcomand.ordn_id);
                            command.Parameters.AddWithValue("@COMA_ID", idComanda);
                            command.Parameters.AddWithValue("@ART_EAN", artOrd.art_ean);
                            command.Parameters.AddWithValue("@ORDN_CANT", artOrd.ordn_cant);
                            command.Parameters.AddWithValue("@ORDN_IMPUNI", artOrd.ordn_impuni);
                            command.Parameters.AddWithValue("@ORDN_IMPART", artOrd.ordn_impart);
                            command.Parameters.AddWithValue("@OBSV", artOrd.ordn_obsv);
                            imptot += artOrd.ordn_impart;
                            if (0 < command.ExecuteNonQuery())
                            {
                                resp.message += "-9";
                                if (artOrd.hasModif == true)
                                {
                                    resp.message += "-10";
                                    command = new SqlCommand("INSERT INTO ORDENART_MODIF (ORDN_ID, COMA_ID, ART_EAN, AGRU_ID, AGRU_CONSEC) VALUES (@ORDN_ID,@COMA_ID,@ART_EAN,@AGRU_ID,@AGRU_CONSEC)", myConnection);
                                    command.Parameters.AddWithValue("@ORDN_ID", newcomand.ordn_id);
                                    command.Parameters.AddWithValue("@COMA_ID", idComanda);
                                    command.Parameters.AddWithValue("@ART_EAN", artOrd.art_ean);
                                    command.Parameters.AddWithValue("@AGRU_ID", artOrd.art_ean);
                                    command.Parameters.AddWithValue("@AGRU_CONSEC", artOrd.art_ean);
                                    if (0 < command.ExecuteNonQuery())
                                    {
                                        resp.message += "-11|";

                                        resp.success = true;
                                        resp.message += "OK";
                                    }
                                }
                                else
                                {
                                    resp.success = true;
                                    resp.message += "OK";
                                }
                                
                            }
                            else
                            {
                                resp.success = false;
                                resp.message += "MESA NO ACTUALIZADA";
                            }
                        }//end foreach
                        command = new SqlCommand("select ordn_imptot from ORDEN where ORDN_ID= @IDORD", myConnection);
                        command.Parameters.AddWithValue("@IDORD", newcomand.ordn_id);
                        
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            imptot += Convert.ToDecimal(reader["ordn_imptot"].ToString());
                        }

                        command = new SqlCommand("UPDATE ORDEN SET ordn_imptot =@PRECTOT WHERE ORDN_ID =@IDO", myConnection);
                        command.Parameters.AddWithValue("@IDO", newcomand.ordn_id);
                        command.Parameters.AddWithValue("@PRECTOT", imptot);
                        if (0 < command.ExecuteNonQuery())
                        {
                            resp.success = true;
                            resp.message += " OK";
                         
                        }
                        try { printComanda(newcomand); }catch(Exception ext){}
                    }
                    else
                    {
                        resp.success = false;
                        resp.message += "ORDEN_CTRL NO ACTUALIZADA";
                    }
                }
                else
                {
                    resp.success = false;
                    resp.message += "NO SE CREO NUEVA ORDEN";
                }

                command.Parameters.Clear();

            }
            catch (Exception ex)
            {
                resp.success = false;
                resp.message += "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            return Json(resp);
        }//END COMANDA
        [HttpPost]
        public ActionResult testID()
        {
            Respuesta resp =  new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO TIPO_PRODUCTO (TIPP_DES) VALUES ('BORRAME'); SELECT CAST(SCOPE_IDENTITY() AS  INT)", myConnection);
                int i = (int)command.ExecuteScalar();
                resp.success = true;

                resp.message = "ID = " + i;
            }catch(Exception ex){
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
            }finally{
                myConnection.Close();
            }
            return Json(resp);
        }
        [HttpPost]
        public ActionResult consolidarOrdenes(ConsolidaOrdenes listaOrdenesCons)
        {
            Respuesta resp = new Respuesta();
            SqlDataReader reader = null;
            SqlCommand command = null;
            SqlConnection myConnection = new SqlConnection();
            decimal sumTotal =0;
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                DateTime fecha = DateTime.Now;
                int iFecha = Convert.ToInt32("" + fecha.Year + "" + fecha.Month + "" + fecha.Day);
                int iHora = Convert.ToInt32(fecha.Hour + "" + fecha.Minute);
                resp.message += "-3";
                foreach (OrdenRefer numOrdenaConsolidar in listaOrdenesCons.ordenes)
                {
                    int mesaactua=0;
                    command= new SqlCommand("select ordn_imptot, mesa_id from ORDEN where ORDN_ID= @IDORD", myConnection);
                    command.Parameters.AddWithValue("@IDORD", numOrdenaConsolidar.ordn_id);
                    resp.message += "-5";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        sumTotal += Convert.ToDecimal(reader["ordn_imptot"].ToString());
                        mesaactua = Convert.ToInt32(reader["mesa_id"].ToString());
                    }
                    command = new SqlCommand("UPDATE ORDEN SET ORDN_STAT ='CONS' WHERE ORDN_ID =@IDORDD", myConnection);
                    command.Parameters.AddWithValue("@IDORDD", numOrdenaConsolidar.ordn_id);                    
                    if (0 > command.ExecuteNonQuery()){
                        resp.success = false;
                        resp.message+=" ERROR -633";
                        return Json(resp);
                    }
                    command = new SqlCommand("UPDATE MESA SET MESA_STAT ='ALTA' WHERE MESA_ID ="+mesaactua, myConnection);
                    if (0 > command.ExecuteNonQuery()){
                        resp.success = false;
                        resp.message+=" ERROR -665";
                        return Json(resp);
                    }
                    //""
                    command = new SqlCommand("update ORDENART_MODIF set ORDN_ID = @IDNEW where ORDN_ID =@IDORDD ", myConnection);
                    command.Parameters.AddWithValue("@IDNEW", listaOrdenesCons.ordn_id);                    
                    command.Parameters.AddWithValue("@IDORDD", numOrdenaConsolidar.ordn_id);                    
                    if (0 > command.ExecuteNonQuery())
                    {
                        resp.success = false;
                        resp.message += " ERROR -670";
                        return Json(resp);
                    }

                    command = new SqlCommand("UPDATE ORDEN_ARTICULO SET ORDN_ID = @IDNEW WHERE ORDN_ID =@IDORDD ", myConnection);
                    command.Parameters.AddWithValue("@IDNEW", listaOrdenesCons.ordn_id);                    
                    command.Parameters.AddWithValue("@IDORDD", numOrdenaConsolidar.ordn_id);                    
                    if (0 > command.ExecuteNonQuery())
                    {
                        resp.success = false;
                        resp.message += " ERROR -679";
                        return Json(resp);
                    }

                    command = new SqlCommand("UPDATE COMANDA SET ORDN_ID = @IDNEW WHERE ORDN_ID = @IDORDD ", myConnection);
                    command.Parameters.AddWithValue("@IDNEW", listaOrdenesCons.ordn_id);                    
                    command.Parameters.AddWithValue("@IDORDD", numOrdenaConsolidar.ordn_id);                    
                    if (0 > command.ExecuteNonQuery())
                    {
                        resp.success = false;
                        resp.message += " ERROR -687";
                        return Json(resp);
                    }

                }
                command= new SqlCommand("select ordn_imptot from ORDEN where ORDN_ID= @IDORD", myConnection);
                command.Parameters.AddWithValue("@IDORD", listaOrdenesCons.ordn_id);
                resp.message += "-6";
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sumTotal+= Convert.ToDecimal(reader["ordn_imptot"].ToString());
                }

                command = new SqlCommand("UPDATE ORDEN SET ordn_imptot =@PRECTOT WHERE ORDN_ID =@IDO", myConnection);
                command.Parameters.AddWithValue("@IDO", listaOrdenesCons.ordn_id);
                command.Parameters.AddWithValue("@PRECTOT", sumTotal);
                if (0 > command.ExecuteNonQuery())
                {
                    resp.success = false;
                    resp.message += " ERROR -650";
                    return Json(resp);
                }
                else
                {
                    resp.success = true;
                    resp.message += " OK";
                }
                
            }
            catch(Exception ext){
                resp.success = false;
                resp.message = "ERROR " + ext.Message;
                return Json(resp);
            }
            finally{
                myConnection.Close();
            }
            return Json(resp);
        }
        [HttpPost]
        public ActionResult traspasoCuentas(TraspasoOrrdenesMese trasp)
        {
            Respuesta resp = new Respuesta();
            SqlDataReader reader = null;
            SqlCommand command = null;
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                command = new SqlCommand("UPDATE ORDEN SET ORDN_MESE = @NVO WHERE ORDN_MESE = @ORIG", myConnection);
                command.Parameters.AddWithValue("@NVO", trasp.mese_nuevo );
                command.Parameters.AddWithValue("@ORIG", trasp.mese_orig);
                if (0 > command.ExecuteNonQuery())
                {
                    resp.success = false;
                    resp.message += " ERROR -650";
                    return Json(resp);
                }
                else
                {
                    command = new SqlCommand("UPDATE ORDEN_CTRL SET EMPL_COD = @NVO , ORDN_STAT = 'CAMM' WHERE  ORDN_STAT = 'INIC' AND EMPL_COD =@ORIG", myConnection);
                    command.Parameters.AddWithValue("@NVO", trasp.mese_nuevo);
                    command.Parameters.AddWithValue("@ORIG", trasp.mese_orig);
                    if (0 < command.ExecuteNonQuery())
                    {
                        resp.success = true;
                        resp.message = "OK";
                    }
                }
            }catch(Exception ex){
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
            }finally{
                myConnection.Close();
            }
            return Json(resp);
        }
        [HttpPost]
        public ActionResult getProductosOrden(OrdenRefer orden)
        {
            DetalleOrdenes detalledeOrden = new DetalleOrdenes();
            SqlDataReader reader = null;
            SqlCommand command = null;
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                //command = new SqlCommand("select a.ORDN_CANT, b.ART_DESC, a.ORDN_IMPUNI, a.ORDN_IMPART, c.ORDN_NPER from ORDEN_ARTICULO a, ARTICULO b, ORDEN C where b.ART_EAN = a.ART_EAN and a.ORDN_ID = " + orden.ordn_id + " and c.ORDN_ID= " + orden.ordn_id, myConnection);
                command = new SqlCommand("select SUM(ORDN_CANT)AS ORDN_CANT, SUM(ORDN_IMPART) AS ORDN_IMPART, (select art_desc from ARTICULO where ARTICULO.ART_EAN = ORDEN_ARTICULO.ART_EAN) AS ART_DESC,(SELECT ORDN_NPER FROM ORDEN WHERE ORDN_ID = "+ orden.ordn_id+" ) AS ORDN_NPER, ORDN_IMPUNI from ORDEN_ARTICULO where ORDN_ID = "+ orden.ordn_id +" GROUP BY ART_EAN, ORDN_IMPUNI" , myConnection);
                
                reader = command.ExecuteReader();
                List<DetalleOrdenComanda> listaDetalles = new List<DetalleOrdenComanda>();
                while (reader.Read())
                {
                    DetalleOrdenComanda ordComandDet = new DetalleOrdenComanda();
                    ordComandDet.art_desc = reader["art_desc"].ToString();//
                    ordComandDet.ordn_cant = Convert.ToInt32(reader["ordn_cant"].ToString());//
                    ordComandDet.ordn_impart = Convert.ToDecimal(reader["ordn_impart"].ToString());//
                    ordComandDet.ordn_impuni = Convert.ToDecimal(reader["ordn_impuni"].ToString());
                    ordComandDet.ordn_nper = Convert.ToInt32(reader["ordn_nper"].ToString());//
                    listaDetalles.Add(ordComandDet);
                }
                detalledeOrden.productos = listaDetalles;
                detalledeOrden.success = true;
                detalledeOrden.message = "OK";
            }
            catch (Exception ex)
            {
                detalledeOrden.success = false;
                detalledeOrden.message = "ERROR " + ex.Message;
            }
            finally
            {
                myConnection.Close();
            }
            return Json(detalledeOrden);
        }

        [HttpPost]
        public ActionResult getRestoOrdenes(ReqDeleteRestaurant rest) {
            RespOrdenDescripcionCorta resp = new RespOrdenDescripcionCorta();
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                

                //SqlCommand command = new SqlCommand("select a.ordn_id, b.empl_nom,b.empl_app, c.mesa_cve, a.ordn_imptot, a.ordn_nper from orden a, empleado b, mesa c where a.mesa_id = c.mesa_id and a.ordn_mese = b.empl_cod and c.rest_id = @REST and a.ordn_stat <> 'FINC' ", myConnection);
                //command.Parameters.AddWithValue("@REST", rest.rest_id);

                SqlCommand command = new SqlCommand("select distinct count( ordn_mese) as countt, (select usr_nombre from ff_cat_usuario where usr_numempleado= a.ordn_mese) as nom,a.ordn_mese as codigo 	 from orden a where a.ordn_stat <>'FINC' group by a.ordn_mese", myConnection);
                reader = command.ExecuteReader();
                List<OrdenDescripcionCorta> ordenes = new List<OrdenDescripcionCorta>();
                while (reader.Read())
                {
                    OrdenDescripcionCorta ord = new OrdenDescripcionCorta();
                    ord.empl_nom = reader["nom"].ToString();
                    ord.empl_cod = Convert.ToInt32(reader["codigo"].ToString());
                    ord.ordn_nctas = Convert.ToInt32(reader["countt"].ToString());
                    ordenes.Add(ord);
                }//fin while
                resp.ordenes_rest = ordenes;
                resp.message = "OK";
                resp.success = true;

                if (ordenes.Count == 0)
                {
                    resp.success = false;
                    resp.message = "NO HAY MAS ORDENES EN ESTE RESTAURANTE";
                    return Json(resp);
                }
                
            }
            catch(Exception ex)
            {
                resp.success = false;
                resp.message = "ERROR "+ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            return Json(resp);
        }
        [HttpPost]
        public ActionResult finOrden(RefOrden orden)
        {
            Respuesta resp = new Respuesta();
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("UPDATE ORDEN SET ORDN_STAT = 'FINC' WHERE ORDN_ID = @ORDEN", myConnection);
                command.Parameters.AddWithValue("@ORDEN", orden.ordn_id);
                if (0 < command.ExecuteNonQuery())
                {
                    command = new SqlCommand(" UPDATE ORDEN_CTRL SET ORDN_STAT = 'FINC' WHERE ORDN_ID = @ORDEN", myConnection);
                    command.Parameters.AddWithValue("@ORDEN", orden.ordn_id);
                    if (0 < command.ExecuteNonQuery())
                    {
                        command = new SqlCommand(" UPDATE MESA SET MESA_STAT = 'ALTA' WHERE MESA_ID=@MESAID", myConnection);
                        command.Parameters.AddWithValue("@MESAID", orden.mesa_id);
                        if (0 < command.ExecuteNonQuery())
                        {
                            command = new SqlCommand(" select * from orden_articulo where ordn_id = " + orden.ordn_id, myConnection);
                            reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                command = new SqlCommand("insert into ff_venta_local (vtaloc_fecha, vtaloc_cliente, vtaloc_folio, vtaloc_upc, vtaloc_cantidad, vtaloc_imp) values (GETDATE(),16, 2, @artean, @cant,1)", myConnection);
                                command.Parameters.AddWithValue("@artean", reader["art_ean"].ToString());
                                command.Parameters.AddWithValue("@cant", reader["ordn_cant"].ToString());
                                if (0 < command.ExecuteNonQuery())
                                {
                                    resp.success = true;
                                    resp.message += "OK";
                                    
                                }
                            }                            
                            resp.success = true;
                            resp.message = "OK";
                        }
                        else
                        {
                            resp.success = false;
                            resp.message = "NO SE COMPLETO FINALIZAR ORDEN-3 " + orden.ordn_id;
                            return Json(resp);
                        }
                    }
                    else
                    {
                        resp.success = false;
                        resp.message = "NO SE COMPLETO FINALIZAR ORDEN-2 " + orden.ordn_id;
                        return Json(resp);
                    }
                }
                else
                {
                    resp.success = false;
                    resp.message = "NO SE COMPLETO FINALIZAR ORDEN " + orden.ordn_id;
                    return Json(resp);
                }
            }catch(Exception ex){
                resp.success = false;
                resp.message ="ERROR "+ex.Message; 
            }
            finally
            {
                myConnection.Close();
            }
            try { printOrden(orden); }catch(Exception ex){}
            return Json(resp);
        }

        public void printOrden(RefOrden ordn)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            SqlCommand command = null;
            PrintDocument pdoc = null;
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            string sNombreImpresora = "";
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                command = new SqlCommand("select top 1 IMPR_CONF from impresora", myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sNombreImpresora = reader["IMPR_CONF"].ToString();
                }
            }
            catch (Exception ex) { }
            finally { myConnection.Close(); }
            pdoc.PrinterSettings.PrinterName = sNombreImpresora;
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font(sFont, 11);


            PaperSize psize = new PaperSize("Custom", 1000, 20);
            ps.DefaultPageSettings.PaperSize = psize;

            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            pdoc.PrintPage += (sender, e) => pdoc_PrintPageOrden(e, ordn);

            pdoc.Print();
        }

        
        public void printComanda(newComanda listaOrdenesArts)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            SqlCommand command = null;
            PrintDocument pdoc = null;
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            string sNombreImpresora = "";
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                command = new SqlCommand("select top 1 IMPR_CONF from impresora", myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sNombreImpresora = reader["IMPR_CONF"].ToString();
                }
            }catch (Exception ex){}finally{myConnection.Close();}
            pdoc.PrinterSettings.PrinterName = sNombreImpresora;
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font(sFont, 11);


            PaperSize psize = new PaperSize("Custom", 1000, 20);
            ps.DefaultPageSettings.PaperSize = psize;

            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            pdoc.PrintPage += (sender, e) => pdoc_PrintPage(e, listaOrdenesArts);

            pdoc.Print();

              /*  
            DialogResult result = pd.ShowDialog();
            if (result == DialogResult.OK)
            {
                PrintPreviewDialog pp = new PrintPreviewDialog();
                pp.Document = pdoc;
                result = pp.ShowDialog();
                if (result == DialogResult.OK)
                {
                    pdoc.Print();
                }
            }
            */
                
        }


        private void pdoc_PrintPage(PrintPageEventArgs e, newComanda lista)
        {

            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            SqlCommand command = null;
            Graphics graphics = e.Graphics;
            Font font = new Font(sFont, 9, FontStyle.Bold);
            int iMesaCliente = 0;
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                command = new SqlCommand("select mesa_cve from mesa where mesa_id = (select mesa_id from orden where ordn_id=" + lista.ordn_id + ")", myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    iMesaCliente = Convert.ToInt32(reader["mesa_cve"].ToString());
                }
            }
            catch (Exception extt)
            {
            }
            //float fontHeight = font.GetHeight();
            double fontHeight_d = 8.7;
            float fontHeight = (float)fontHeight_d;
            int startX = 15;
            int startY = 15;
            int Offset = 10;
            //graphics.DrawImage(WindowsFormsApplication1.Properties.Resources.LOGO_CITYMARKET, 38, 0, 230, 100);
            //graphics.DrawImage(Image.FromFile(Application.StartupPath +"\\citymark2.png"), 38, 0, 230, 100);

            try { graphics.DrawImage(Image.FromFile(".\\citymark2.png"), 38, 0, 230, 100); }
            catch (Exception ex) { }

            Offset = Offset + 70;
            String numOrden = "No. de Orden " + lista.ordn_id;
            graphics.DrawString(numOrden, new Font(sFont, (float)8.6, FontStyle.Bold),
                                Brushes.Black, CentrarTexto(graphics, numOrden), startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Cliente / Mesa:" + iMesaCliente,
                        new Font(sFont, (float)8.5, FontStyle.Regular), Brushes.Black, startX - 6, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Cant Articulo",
                        new Font(sFont, (float)8.5, FontStyle.Regular),
                        Brushes.Black, startX - 6, startY + Offset);
            Offset = Offset + 20;
            String underLine = "---------------------------------------";
            graphics.DrawString(underLine, new Font(sFont, (float)8.5, FontStyle.Regular),
                        new SolidBrush(Color.Black), startX - 6, startY + Offset);

            int iCantidadArticulos = 0;
            string sMesero = "";
            string sRestaurante = "";
            foreach (OrdenArticulo artOrd in lista.ordenarticulo)
            {
                List<Categoria> categorias = new List<Categoria>();
                List<Articulo> articulos = new List<Articulo>();
                List<Modificador> modificadores = new List<Modificador>();
                string sArticulo = "";
                string sModificador = "";

                try//EL SIGUEINTE BLOQUE ES PARA EXTRAER LAS CATEGORIAS
                {
                    command = new SqlCommand("select art_des from articulo where art_ean = " + artOrd.art_ean, myConnection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        sArticulo = reader["art_des"].ToString();
                        if (sArticulo.Length > 20)
                        {
                            sArticulo = sArticulo.Substring(0, 20);
                        }

                    }
                    command = new SqlCommand("select agru_des from agrupacion_modif where agru_id = " + artOrd.agru_id + " and agru_consec=" + artOrd.agru_consec, myConnection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        sModificador = reader["agru_des"].ToString();
                        if (sModificador.Length > 18)
                        {
                            sModificador = sModificador.Substring(0,18);
                        }
                    }

                }
                catch (Exception exc)
                {
                }

                Offset = Offset + 10;
                graphics.DrawString(artOrd.ordn_cant.ToString(), new Font(sFont, (float)8.5, FontStyle.Regular),
                        new SolidBrush(Color.Black), startX + 10, startY + Offset);
                
                graphics.DrawString(sArticulo, new Font(sFont, (float)8.5, FontStyle.Regular),
                        new SolidBrush(Color.Black), startX + 30, startY + Offset);
                if (artOrd.ordn_obsv != null && artOrd.ordn_obsv.Length > 3)
                {
                    Offset = Offset + 10;
                    string Obsvc = artOrd.ordn_obsv;
                    /*BLOQUE PARA CONTROLAR LA IMPRESION DE OBSERVACIONES*/
                    string obsvValidado = "";
                    int tammax = 25;
                    int tamobsv = Obsvc.Length;
                    if (tamobsv > tammax)
                    {
                        double oper = (tamobsv / tammax);
                        double dblCant = Math.Ceiling(oper) +1;
                        try
                        {  
                            for (int y = 0; y < dblCant; y++)
                            {
                                double z = y * tammax;
                                int aa = (int)Math.Ceiling(z);
                                int bb = aa + tammax;
                                
                                if(y+1< dblCant){
                                    obsvValidado = Obsvc.Substring(aa, tammax);
                                    //obsvValidado = Environment.NewLine;
                                    graphics.DrawString("O:"+obsvValidado, new Font(sFont, (float)7.8, FontStyle.Regular),
                                                        new SolidBrush(Color.Black), startX+35 , startY + Offset);
                                    Offset += 10;
                                }else
                                {
                                    
                                    obsvValidado = Obsvc.Substring(aa, tamobsv-aa);
                                    graphics.DrawString("O:"  + obsvValidado, new Font(sFont, (float)7.8, FontStyle.Regular),
                                                        new SolidBrush(Color.Black), startX + 35, startY + Offset);
                                    Offset += 10;
                                    break;
                                }

                            }
                        }
                        catch (Exception tribaes)
                        {
                            graphics.DrawString("=( " + tribaes, new Font(sFont, (float)7.8, FontStyle.Regular),
                                                       new SolidBrush(Color.Black), 0, startY + Offset);
                            Offset += 10;
                        }

                    }
                    else
                    {
                        graphics.DrawString("O:+ " + Obsvc, new Font(sFont, (float)7.8, FontStyle.Regular),
                        new SolidBrush(Color.Black), startX + 35, startY + Offset);
                    }

                    
                }
                if (artOrd.hasModif)
                {
                    Offset = Offset + 10;
                    graphics.DrawString("M: " + sModificador, new Font(sFont, (float)7.8, FontStyle.Regular),
                        new SolidBrush(Color.Black), startX + 35, startY + Offset);
                }
                iCantidadArticulos += artOrd.ordn_cant;
            }//end foreach
            try
            {
                command = new SqlCommand("select usr_nombre from ff_cat_usuario where usr_numempleado = (select ordn_mese from orden where ordn_id=" + lista.ordn_id + ")", myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sMesero = reader["usr_nombre"].ToString();
                }

                command = new SqlCommand("select rest_des from restaurant where rest_id = (select rest_id from mesa where mesa_id=(select mesa_id from orden where ordn_id=" + lista.ordn_id + "))", myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sRestaurante = reader["rest_des"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                myConnection.Close();
            }
            Offset = Offset + 20;
            graphics.DrawString(underLine, new Font(sFont, (float)8.5, FontStyle.Regular),
                        new SolidBrush(Color.Black), startX - 6, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("No. de Articulos:" + iCantidadArticulos, new Font(sFont, (float)7.5, FontStyle.Regular),
                        new SolidBrush(Color.Black), startX - 6, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("Atendio:" + sMesero, new Font(sFont, (float)7.5, FontStyle.Regular),
                        new SolidBrush(Color.Black), startX - 6, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("Origen:" + sRestaurante, new Font(sFont, (float)7.5, FontStyle.Regular),
                        new SolidBrush(Color.Black), startX - 6, startY + Offset);
            Offset = Offset + 25;

            string leyendaa = "Ticket No Valido";
            e.Graphics.DrawString(leyendaa, new Font(sFont, (float)8.6, FontStyle.Bold),
                                Brushes.Black, CentrarTexto(graphics, leyendaa), startY + Offset);
            Offset = Offset + 12;
            string leyendab = "Como Comprobante de Pago";
            e.Graphics.DrawString(leyendab, new Font(sFont, (float)8.6, FontStyle.Bold),
                                Brushes.Black, CentrarTexto(graphics, leyendab), startY + Offset);
            Offset = Offset + 20;
            DateTime nw = DateTime.Now;
            string fechahora = "" + nw;
            e.Graphics.DrawString(fechahora, new Font(sFont, (float)8.6, FontStyle.Regular),
                                Brushes.Black, CentrarTexto(graphics, fechahora), startY + Offset);
            Offset = Offset + 20;

        }


        private void pdoc_PrintPageOrden(PrintPageEventArgs e, RefOrden orden)
        {
            
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            SqlCommand command = null;
            Graphics graphics = e.Graphics;
            Font font = new Font(sFont, 9, FontStyle.Bold);
            int iMesaCliente = 0;
            string sTienda = "";
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                command = new SqlCommand("select mesa_cve from mesa where mesa_id = (select mesa_id from orden where ordn_id=" + orden.ordn_id + ")", myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    iMesaCliente = Convert.ToInt32(reader["mesa_cve"].ToString());
                }
                command = new SqlCommand("select succ_des from sucursal where succ_id = (select succ_id from ff_cat_usuario where usr_numempleado =(select ordn_mese from orden where ordn_id = " +orden.ordn_id+") )", myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sTienda = reader["succ_des"].ToString();
                }
            }
            catch (Exception extt)
            {
            }
            //float fontHeight = font.GetHeight();
            double fontHeight_d = 8.7;
            float fontHeight = (float)fontHeight_d;
            int startX = 15;
            int startY = 15;
            int Offset = 10;
            try { graphics.DrawImage(Image.FromFile(".\\citymark2.png"), 38, 0, 230, 100); }
            catch (Exception ex) { }

            Offset = Offset + 80;
            graphics.DrawString(sTienda, new Font(sFont, (float)8.6, FontStyle.Bold),
                                Brushes.Black, CentrarTexto(graphics, sTienda), startY + Offset);
            Offset = Offset + 20;

            String sTitulo = "Tiendas Comercial Mexicana" ;
            graphics.DrawString(sTitulo, new Font(sFont, (float)8.6, FontStyle.Bold),
                                Brushes.Black, CentrarTexto(graphics, sTitulo), startY + Offset);
            Offset = Offset + 20;
            String numOrden = "No. de Orden " + orden.ordn_id;
            graphics.DrawString(numOrden, new Font(sFont, (float)8.6, FontStyle.Bold),
                                Brushes.Black, CentrarTexto(graphics, numOrden), startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Cliente / Mesa:" + iMesaCliente,
                        new Font(sFont, (float)8.5, FontStyle.Regular), Brushes.Black, startX - 6, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Can Articulo",
                        new Font(sFont, (float)8.5, FontStyle.Regular),
                        Brushes.Black, startX - 8, startY + Offset);
            graphics.DrawString("Precio",
                        new Font(sFont, (float)8.5, FontStyle.Regular),
                        Brushes.Black, CentrarImporte(graphics,"Precio"), startY + Offset);
            graphics.DrawString("Total",
                        new Font(sFont, (float)8.5, FontStyle.Regular),
                        Brushes.Black, CentrarTotal(graphics,"Total"), startY + Offset);
            Offset = Offset + 20;
            String underLine = "---------------------------------------";
            graphics.DrawString(underLine, new Font(sFont, (float)8.5, FontStyle.Regular),
                        new SolidBrush(Color.Black), startX - 6, startY + Offset);

            int iCantidadArticulos = 0;
            string sMesero = "";
            string sRestaurante = "";
            List<DetalleOrden> listaDetallesOrden = new List<DetalleOrden>();
            
            try
            {
                command = new SqlCommand("select  art_ean, SUM(ORDN_CANT) as ordn_cant,SUM(ORDN_IMPUNI) as ordn_impuni, SUM(ORDN_IMPART) as ordn_impart from orden_articulo where ordn_id= "+ orden.ordn_id+" group by  ART_EAN" , myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DetalleOrden ordart = new DetalleOrden();
                    ordart.art_ean = Convert.ToDecimal(reader["art_ean"].ToString());
                    ordart.ordn_cant = Convert.ToInt32(reader["ordn_cant"].ToString());
                    //ordart.ordn_id = Convert.ToInt32(reader["ordn_id"].ToString());
                    ordart.ordn_id = orden.ordn_id;
                    ordart.ordn_impart = Convert.ToDecimal(reader["ordn_impart"].ToString());
                    ordart.ordn_impuni = Convert.ToDecimal(reader["ordn_impuni"].ToString());
                    //ordart.ordn_obsv = (reader["ordn_obsv"].ToString());
                    listaDetallesOrden.Add(ordart);
                    iCantidadArticulos+=ordart.ordn_cant;
                }
            }catch(Exception excv){

            }
            decimal totaltotales = 0;
            foreach (DetalleOrden artOrd in listaDetallesOrden )
            {
                
                string sArticulo = "";

                try//EL SIGUEINTE BLOQUE ES PARA EXTRAER LAS CATEGORIAS
                {
                    command = new SqlCommand("select art_des from articulo where art_ean = " + artOrd.art_ean, myConnection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        sArticulo = reader["art_des"].ToString().Replace("\n", " ");
                    }
                    //command = new SqlCommand("select agru_des from agrupacion_modif where agru_id = " + artOrd.agru_id + " and agru_consec=" + artOrd.agru_consec, myConnection);
                    //reader = command.ExecuteReader();
                    //while (reader.Read())
                    //{
                    //    sModificador = reader["agru_des"].ToString();
                    //}

                }
                catch (Exception exc)
                {
                }
                if (sArticulo.Length>18)
                {
                    sArticulo=sArticulo.Substring(0, 18);
                }
                Offset = Offset + 10;
                graphics.DrawString(artOrd.ordn_cant.ToString(), new Font(sFont, (float)8, FontStyle.Regular),
                        new SolidBrush(Color.Black), startX + 5, startY + Offset);
                graphics.DrawString(sArticulo, new Font(sFont, (float)8, FontStyle.Regular),
                        new SolidBrush(Color.Black), startX + 25, startY + Offset);
                string sImportUni = artOrd.ordn_impuni.ToString();
                graphics.DrawString(sImportUni, new Font(sFont, (float)8, FontStyle.Regular),
                        new SolidBrush(Color.Black), CentrarImporte(graphics, sImportUni), startY + Offset);//new SolidBrush(Color.Black), startX + 170, startY + Offset);
                decimal totl = artOrd.ordn_impuni * artOrd.ordn_cant;
                totaltotales += totl;
                string svonvert= Convert.ToString(totl);
                graphics.DrawString(svonvert, new Font(sFont, (float)8, FontStyle.Regular),
                        new SolidBrush(Color.Black), CentrarTotal(graphics, svonvert), startY + Offset);
                //if (artOrd.ordn_obsv != null && artOrd.ordn_obsv.Length > 3)
                //{
                //    Offset = Offset + 10;
                //    graphics.DrawString("O: " + artOrd.ordn_obsv, new Font("Courier New", (float)7.8, FontStyle.Regular),
                //        new SolidBrush(Color.Black), startX + 35, startY + Offset);
                //}
                //if (artOrd.hasModif)
                //{
                //    Offset = Offset + 10;
                //    graphics.DrawString("M: " + sModificador, new Font("Courier New", (float)7.8, FontStyle.Regular),
                //        new SolidBrush(Color.Black), startX + 35, startY + Offset);
                //}
                //iCantidadArticulos += artOrd.ordn_cant;
            }//end foreach
            try
            {
                command = new SqlCommand("select usr_nombre from ff_cat_usuario where usr_numempleado = (select ordn_mese from orden where ordn_id=" + orden.ordn_id + ")", myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sMesero = reader["usr_nombre"].ToString();
                }

                command = new SqlCommand("select rest_des from restaurant where rest_id = (select rest_id from mesa where mesa_id=(select mesa_id from orden where ordn_id=" + orden.ordn_id + "))", myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sRestaurante = reader["rest_des"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                myConnection.Close();
            }
            Offset = Offset + 20;
            graphics.DrawString(underLine, new Font(sFont, (float)8.5, FontStyle.Regular),
                        new SolidBrush(Color.Black), startX - 6, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("Total ", new Font(sFont, (float)7.5, FontStyle.Regular),
                        new SolidBrush(Color.Black), startX +170, startY + Offset);
            string stottales = "" + totaltotales;
            graphics.DrawString("" + totaltotales, new Font(sFont, (float)7.5, FontStyle.Regular),
                        new SolidBrush(Color.Black), CentrarTotal(graphics, stottales), startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("No. de Articulos:" + iCantidadArticulos, new Font(sFont, (float)7.5, FontStyle.Regular),
                        new SolidBrush(Color.Black), startX - 6, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("Atendio:" + sMesero, new Font(sFont, (float)7.5, FontStyle.Regular),
                        new SolidBrush(Color.Black), startX - 6, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("Origen:" + sRestaurante, new Font(sFont, (float)7.5, FontStyle.Regular),
                        new SolidBrush(Color.Black), startX - 6, startY + Offset);
            Offset = Offset + 25;

            string leyendaa = "Ticket No Valido";
            e.Graphics.DrawString(leyendaa, new Font(sFont, (float)8.6, FontStyle.Bold),
                                Brushes.Black, CentrarTexto(graphics, leyendaa), startY + Offset);
            Offset = Offset + 12;
            string leyendab = "Como Comprobante de Pago";
            e.Graphics.DrawString(leyendab, new Font(sFont, (float)8.6, FontStyle.Bold),
                                Brushes.Black, CentrarTexto(graphics, leyendab), startY + Offset);
            Offset = Offset + 20;
            DateTime nw = DateTime.Now;
            string fechahora = "" + nw;
            e.Graphics.DrawString(fechahora, new Font(sFont, (float)8.6, FontStyle.Regular),
                                Brushes.Black, CentrarTexto(graphics, fechahora), startY + Offset);
            Offset = Offset + 20;

        }

        public float CentrarTexto(Graphics graphics, String letter)
        {
            SizeF size = graphics.MeasureString(letter.ToString(), new Font(sFont, (float)8.6, FontStyle.Bold));
            float tamano = (280 - size.Width) / 2;
            return tamano;
        }

        public float CentrarImporte(Graphics graphics, String letter)
        {
            SizeF size = graphics.MeasureString(letter.ToString(), new Font(sFont, (float)8.6, FontStyle.Bold));
            double ttam = 1.24;
            float tamano = (280 - size.Width) / (float)ttam;
            return tamano;
        }

        public float CentrarTotal(Graphics graphics, String letter)
        {
            SizeF size = graphics.MeasureString(letter.ToString(), new Font(sFont, (float)8.6, FontStyle.Bold));
            double ttam= .96;
            float tamano = (280 - size.Width) / (float)ttam;
            return tamano;
        }
        
        
        [HttpPost]
        public ActionResult Alive()
        {
            Respuesta resp = new Respuesta();
            resp.success = true;
            resp.message = "OK";
            return Json(resp);
        }
    

    }//END NAMESPACE


}
