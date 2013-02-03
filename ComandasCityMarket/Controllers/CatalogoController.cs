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
    public class CatalogoController : Controller
    {
        //
        // POST: /CategoriasArticulos/
        [HttpPost]
        public ActionResult Index(ReqCatalogo reqCatalogo)
        {
            Catalogo catalogo = new Catalogo();
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();            
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                List<Categoria> categorias= new List<Categoria>();
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
                        if(categoria.agru_tipo == 1){
                            categoria.hasSubCat = true;
                            //SqlCommand commandFindSub = new SqlCommand("SELECT * FROM AGRUPACION_CAT WHERE AGRU_TIPO = 1 AND AGRU_PADRE = "+categoria.agru_padre, myConnection);
                            SqlCommand commandFindSub = new SqlCommand("SELECT * FROM AGRUPACION_CAT WHERE AGRU_PADRE = " + categoria.agru_id, myConnection);
                            SqlDataReader readerSub = commandFindSub.ExecuteReader();
                            List<SubCategoria> listasubCateg = new List<SubCategoria>();                            
                            while(readerSub.Read()){
                                SubCategoria subCat = new SubCategoria();
                                subCat.agru_des = readerSub["agru_des"].ToString();
                                subCat.agru_desc = readerSub["agru_desc"].ToString();
                                subCat.agru_id = Convert.ToInt32(readerSub["agru_id"].ToString());
                                subCat.agru_padre = Convert.ToInt32(readerSub["agru_padre"].ToString());
                                subCat.agru_tipo = Convert.ToInt32(readerSub["agru_tipo"].ToString());
                                SqlCommand commandFindSubArt = new SqlCommand("SELECT A.AGRU_ID, A.ART_DES, A.ART_DESC, A.ART_EAN, A.TIPP_ID, B.ART_PRECIO FROM ARTICULO A, ARTICULO_PRECIO B WHERE  A.AGRU_ID = "+subCat.agru_id +" AND B.ART_EAN = A.ART_EAN AND B.SUCC_ID ="+reqCatalogo.succ_id, myConnection);
                                SqlDataReader readerSubArt = commandFindSubArt.ExecuteReader();
                                List<Articulo> listArticulos = new List<Articulo>();
                                while(readerSubArt.Read()){
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
                                SqlCommand commandFindModificadores = new SqlCommand("SELECT * FROM AGRUPACION_MODIF WHERE AGRU_ID = "+subCat.agru_id, myConnection);
                                SqlDataReader readerModif = commandFindModificadores.ExecuteReader();
                                while(readerModif.Read()){
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
                        }else if(categoria.agru_tipo == 2){
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
                            SqlCommand commandFindModificadores = new SqlCommand("SELECT * FROM AGRUPACION_MODIF WHERE AGRU_ID = "+categoria.agru_id , myConnection);
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
                /*
                //EL SIGUIENTE BLOQUE BUSCA LOS ARTICULOS Y SUS PRECIOS DE ACUERDO A LA SUCURSAL CORRESPONDIENTE
                try {
                    SqlCommand command = new SqlCommand("SELECT *   ", myConnection);// ? <- Query con join
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Articulo articulo = new Articulo();
                        articulo.agru_id = Convert.ToInt32(reader["agru_id"].ToString());
                        articulo.art_des = reader["art_des"].ToString();
                        articulo.art_desc = reader["art_desc"].ToString();
                        articulo.art_ean = Convert.ToDecimal(reader["art_ean"].ToString());
                        articulo.art_precio = Convert.ToDecimal(reader["art_precio"].ToString());
                        articulos.Add(articulo); //A la lista agrega el articulo recuperado durante este bucle del while
                    }//Fin while DataReader buscando Articulo
                    catalogo.articulos = articulos;//Agrega al objeto pre-json el list de los articulos recuperados
                }catch(SqlException exArt){
                    catalogo.success = false;
                    catalogo.message = "ERROR " + exArt.Message;
                    return Json(catalogo);
                }//Fin try-catch buscando Artículo
                //EL SIGUIENTE BLOQUE RECUPERA LOS MODIFICADORES REGISTRADOS EN LA BD
                try {
                    SqlCommand command = new SqlCommand("SELECT *  FROM AGRUPACION_MODIF", myConnection);// ? <- Query con join
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Modificador modificador = new Modificador();
                        modificador.agru_consec = Convert.ToInt32(reader["art_des"].ToString());
                        modificador.agru_des = reader["art_des"].ToString();
                        modificador.agru_desc = reader["art_des"].ToString();
                        modificador.agru_id = Convert.ToInt32(reader["art_des"].ToString());
                        modificadores.Add(modificador); //A la lista agrega el modificador recuperado durante este bucle del while
                    }//fin while DataReader buscando Modificadores
                    catalogo.modificadores = modificadores;//Agregar al objeto pre-json el Listo de Modificadores
                }catch (SqlException exModif) {
                    catalogo.success = false;
                    catalogo.message = "ERROR " + exModif.Message;
                    return Json(catalogo);
                }//fin try-catch buscando Modificadores
                 */
            }catch(Exception e){
                catalogo.success = false;//En caso de caer en exception el estado del boleano se envia en falso y en message el detalle del error
                catalogo.message = "ERROR 168 " + e.Message;
                return Json(catalogo);
            }finally{
                myConnection.Close();//Cerrando dentro del finally la conexión realizada a la base de datos                
            }//fin Bloque completo de Try 
            catalogo.success = true;//Completando el objeto json con estado true y mensaje OK
            catalogo.message = "OK";
            return Json(catalogo);
        }//end Index

    }//end class Controller
}//End namespace
