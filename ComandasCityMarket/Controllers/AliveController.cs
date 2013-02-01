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
    public class AliveController : Controller
    {
        //
        // GET: /Alive/

        [HttpGet]
        public ActionResult Index()
        {
            return Json(new
            {
                Request = "GET"
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Index(AliveRequest aliv)
        {
            Alive res = new Alive();
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            try
            {
                
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("select * from ff_cat_usuario", myConnection);
                reader = command.ExecuteReader();
                List<Empleado> listaEmpl = new List<Empleado>();
                while (reader.Read())
                {
                    Empleado emp = new Empleado();
                    emp.empl_nom = reader["usr_nombre"].ToString();
                    listaEmpl.Add(emp);
                }//end while
                res.body = listaEmpl;
            }
            catch (SqlException e)
            {
                res.success = false;
                res.message = "ERROR DB " + e.Message;
                return Json(res);
            }
            finally
            {
                myConnection.Close();
            }
          
            if (aliv.request)
            {
                res.success = true;
                res.message = "OK";
            }
            else
            {
                res.success = false;
                res.message = "ERROR 404";
            }
            //return Json(res);
            return Json(res);
        }

    }
}
