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
    public class MesasController : Controller
    {
        //
        // GET: /Mesas/
        [HttpPost]
        public ActionResult Index()
        {
            RespMesas mesas = new RespMesas();
            try
            {
                SqlDataReader reader = null;
                SqlConnection myConnection = new SqlConnection();
                List<Mesa> listaMesas = new List<Mesa>();
                try {
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                    myConnection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM MESA WHERE MESA_STAT = 'ALTA';", myConnection);
                    reader = command.ExecuteReader();
                    while(reader.Read()){
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
                }catch(SqlException exc){
                    mesas.success = false;
                    mesas.message = "ERROR " + exc.Message;
                }                
            }catch(Exception ex){
                mesas.success = false;
                mesas.message = "ERROR " + ex.Message;
            }
            mesas.success = true;
            mesas.message = "OK";
            return Json(mesas);
        }

    }
}
