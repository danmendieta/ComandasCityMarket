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
    public class RestaurantesController : Controller
    {
        //
        // GET: /Restaurantes/
        
        public ActionResult Index(ReqEmpleado req)
        {
            RespRestaurant respuestaRestaurantes = new RespRestaurant();
            try {
                SqlDataReader reader = null;
                SqlConnection myConnection = new SqlConnection();
                try
                {
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                    myConnection.Open();
                    SqlCommand command = new SqlCommand("select a.* from RESTAURANT a, EMPLEADO b where b.EMPL_COD = " + req.num_empleado + " and b.SUCC_ID = a.SUCC_ID and b.EMPL_STAT ='ALTA';  ", myConnection);
                    reader = command.ExecuteReader();
                    List<Restaurant> listaRestaurantes = new List<Restaurant>();
                    while(reader.Read()){
                        Restaurant restaurante = new Restaurant();
                        restaurante.rest_des = reader["rest_des"].ToString();
                        restaurante.rest_id = Convert.ToInt32(reader["rest_id"].ToString());
                        restaurante.succ_id = Convert.ToInt32(reader["succ_id"].ToString());
                        listaRestaurantes.Add(restaurante);
                    }//fin while
                    if(listaRestaurantes.Count==0){
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
            }catch(Exception ex){
                respuestaRestaurantes.success = false;
                respuestaRestaurantes.message += "ERROR " + ex.Message;
                return Json(respuestaRestaurantes);
            }//fin try-catch general
            respuestaRestaurantes.success = true;
            respuestaRestaurantes.message += "OK";
            return Json(respuestaRestaurantes);
        }//fin Index

    }//fin class
}//fin namespace
