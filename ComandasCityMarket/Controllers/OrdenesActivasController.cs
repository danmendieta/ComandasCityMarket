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
    public class OrdenesActivasController : Controller
    {
        //
        // GET: /OrdenesActivas/
        [HttpPost]
        public ActionResult Index(Acceso acceso)
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
                                                        "C.MESA_CVE "+
                                                        "   FROM "+
                                                        "ORDEN A, "+
                                                        "ORDEN_CTRL B, "+
                                                        "MESA C "+
                                                        "   WHERE "	+
                                                        "A.ORDN_STAT = 'INIC' "+
                                                        " AND	A.ORDN_MESE = " +acceso.num_empleado +
                                                        " AND	B.ORDN_ID = A.ORDN_ID"+
                                                        " AND	C.MESA_ID = A.MESA_ID;", myConnection);
                    reader = command.ExecuteReader();
                    while(reader.Read()){
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
                catch(SqlException sqlex){
                    ordenesActivas.success = false;
                    ordenesActivas.message = "ERROR " + sqlex;
                    return Json(ordenesActivas);
                }
                finally{
                    myConnection.Close();
                }
            }
            catch(Exception exc)
            {
                ordenesActivas.success = false;
                ordenesActivas.message = "ERROR " + exc.Message;
                return Json(ordenesActivas);
            }
            ordenesActivas.success = true;
            ordenesActivas.message = "OK";
            return Json(ordenesActivas);
        }//end ActionResult
    }//end class
}
