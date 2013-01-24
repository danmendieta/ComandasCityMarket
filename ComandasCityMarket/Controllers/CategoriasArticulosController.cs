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
    public class CategoriasArticulosController : Controller
    {
        //
        // POST: /CategoriasArticulos/
        [HttpPost]
        public ActionResult Index()
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
                    SqlCommand command = new SqlCommand("SELECT * FROM AGRUPACION_CAT", myConnection);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Categoria categoria = new Categoria();
                        categoria.agru_des = reader["agru_des"].ToString();
                        categoria.agru_desc = reader["agru_desc"].ToString();
                        categoria.agru_id = Convert.ToInt32(reader["agru_id"].ToString());
                        categoria.agru_padre = reader["agru_padre"].ToString();
                        categoria.agru_tipo = reader["agru_tipo"].ToString();
                        categorias.Add(categoria);
                    }
                    catalogo.categorias = categorias;
                }
                catch (SqlException exCat)
                {   
                    catalogo.success = false;
                    catalogo.message = "ERROR " + exCat.Message;
                    return Json(catalogo);
                }
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
            }catch(Exception e){
                catalogo.success = false;//En caso de caer en exception el estado del boleano se envia en falso y en message el detalle del error
                catalogo.message = "ERROR " + e.Message;
                return Json(catalogo);
            }finally{
                myConnection.Close();//Cerrando dentro del finally la conexión realizada a la base de datos
                catalogo.success = true;//Completando el objeto json con estado true y mensaje OK
                catalogo.message = "OK";
            }//fin Bloque completo de Try 
            return Json(catalogo);
        }//end Index
    }//end class Controller
}//End namespace
