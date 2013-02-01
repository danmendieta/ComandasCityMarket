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
    public class EntrarController : Controller
    {
        //
        // GET: /Entrar/

        [HttpPost]
        public ActionResult Index(Acceso login)
        {
            RespAcceso respuesta = new RespAcceso();
            try {   
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
            }catch(Exception ex){
                respuesta.success = false;
                respuesta.message = "ERROR " + ex.Message;
            }//Fin Try-catch General
            respuesta.success = true;
            respuesta.message = "OK";
            return Json(respuesta);
        }

    }
}
