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
    public class AdminwebController : Controller
    {
        //
        // GET: /Adminweb/

        [HttpPost]
        public ActionResult Index()
        {
            RespRestaurant respuestaRestaurantes = new RespRestaurant();
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM RESTAURANT;", myConnection);
                reader = command.ExecuteReader();
                List<Restaurant> listaRestaurantes = new List<Restaurant>();
                while (reader.Read())
                {
                    Restaurant restaurante = new Restaurant();
                    restaurante.rest_des = reader["rest_des"].ToString();
                    restaurante.rest_id = Convert.ToInt32(reader["rest_id"].ToString());
                    restaurante.succ_id = Convert.ToInt32(reader["succ_id"].ToString());
                    listaRestaurantes.Add(restaurante);
                }
                respuestaRestaurantes.restaurantes = listaRestaurantes;
            }
            catch (SqlException ex)
            {
                respuestaRestaurantes.success = false;
                respuestaRestaurantes.message = "ERROR " + ex.Message;
                return Json(respuestaRestaurantes);
            }
            finally
            {
                myConnection.Close();
            }
            respuestaRestaurantes.success = true;
            respuestaRestaurantes.message = "OK";
            return Json(respuestaRestaurantes);
        }

    }
}
