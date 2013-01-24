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
            string sQuery = null;
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                List<Categoria> categorias= new List<Categoria>();
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

                //find articulo & precio
                //find modificadores
            }
            catch(Exception e){
                catalogo.success = false;
                catalogo.message = "ERROR " + e.Message;
            }finally{
                myConnection.Close();
                catalogo.success = true;
                catalogo.message = "OK";
            }
            return Json(catalogo);
        }

    }
}
