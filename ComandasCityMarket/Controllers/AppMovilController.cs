using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Serialization.Json;
using ComandasCityMarket.Models;


namespace ComandasCityMarket.Controllers
{
    public class AppMovilController : Controller
    {
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
                    SqlCommand command = new SqlCommand("SELECT * FROM AGRUPACION_CAT WHERE AGRU_PADRE = 0;", myConnection);
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
                            SqlCommand commandFindSub = new SqlCommand("SELECT * FROM AGRUPACION_CAT WHERE AGRU_PADRE = " + categoria.agru_id, myConnection);
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
                                    modificador.agru_des = readerModif["agru_consec"].ToString();
                                    modificador.agru_desc = readerModif["agru_consec"].ToString();
                                    modificador.agru_id = Convert.ToInt32(readerModif["agru_consec"].ToString());
                                    listaModificadores.Add(modificador);
                                }
                                subCat.modificadores = listaModificadores;
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
                                modificador.agru_des = readerModif["agru_consec"].ToString();
                                modificador.agru_desc = readerModif["agru_consec"].ToString();
                                modificador.agru_id = Convert.ToInt32(readerModif["agru_consec"].ToString());
                                listaModificadores.Add(modificador);
                            }//FIN while musca modificador
                            categoria.modificadores = listaModificadores;
                        }
                        categorias.Add(categoria);
                    }
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
                    SqlCommand command = new SqlCommand("select * from RESTAURANT a, EMPLEADO b where b.EMPL_COD = " + login.num_empleado + " and a.REST_ID = " + login.rest_id + " and b.EMPL_STAT ='ALTA'", myConnection);
                    reader = command.ExecuteReader();
                    Empleado emp = new Empleado();
                    Restaurant rest = new Restaurant();
                    while (reader.Read())
                    {

                        rest.rest_des = reader["rest_des"].ToString();
                        rest.rest_id = Convert.ToInt32(reader["rest_id"].ToString());
                        rest.succ_id = Convert.ToInt32(reader["succ_id"].ToString());
                        emp.empl_apm = reader["empl_apm"].ToString();
                        emp.empl_app = reader["empl_app"].ToString();
                        emp.empl_cod = Convert.ToInt32(reader["empl_cod"].ToString());
                        emp.empl_nom = reader["empl_nom"].ToString();
                        emp.empl_stat = reader["empl_stat"].ToString();
                        emp.empl_tipo = reader["empl_tipo"].ToString();
                        emp.succ_id = Convert.ToInt32(reader["succ_id"].ToString());

                    }//end while
                    respuesta.restaurant = rest;
                    respuesta.empleado = emp;
                }
                catch (SqlException sqlExc)
                {
                    respuesta.success = false;
                    respuesta.message = "ERROR " + sqlExc.Message;
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
            }//Fin Try-catch General
            respuesta.success = true;
            respuesta.message = "OK";
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
                    SqlCommand command = new SqlCommand("SELECT * FROM MESA WHERE MESA_STAT = 'ALTA' WHERE REST_ID =@REST", myConnection);
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
            mesas.success = true;
            mesas.message = "OK";
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
                    SqlCommand command = new SqlCommand("SELECT " +
                                                        "A.ORDN_ID, " +
                                                        "A.ORDN_NPER, " +
                                                        "A.ORDN_IMPTOT," +
                                                        "A.ORDN_STAT, " +
                                                        "A.MESA_ID, " +
                                                        "B.ORDN_HMOV, " +
                                                        "C.MESA_CVE " +
                                                        "   FROM " +
                                                        "ORDEN A, " +
                                                        "ORDEN_CTRL B, " +
                                                        "MESA C " +
                                                        "   WHERE " +
                                                        "A.ORDN_STAT = 'INIC' " +
                                                        " AND	A.ORDN_MESE = " + acceso.num_empleado +
                                                        " AND	B.ORDN_ID = A.ORDN_ID" +
                                                        " AND	C.MESA_ID = A.MESA_ID;", myConnection);
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
                    SqlCommand command = new SqlCommand("select a.* from RESTAURANT a, EMPLEADO b where b.EMPL_COD = " + req.empl_cod + " and b.SUCC_ID = a.SUCC_ID and b.EMPL_STAT ='ALTA';  ", myConnection);
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
                        respuestaRestaurantes.message += "NO EXISTEN EMPLEADOS ASOCIADOS AL NUMERO DE EMPLEADO";
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
    
    




    }//END NAMESPACE
}
