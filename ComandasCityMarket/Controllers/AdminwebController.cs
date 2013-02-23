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
        public ActionResult updateSucursal(Sucursal sucursal) {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("UPDATE SUCURSAL SET SUCC_DES = @DES WHERE SUCC_ID = @ID ", myConnection);
                command.Parameters.AddWithValue("@ID", sucursal.succ_id);
                command.Parameters.AddWithValue("@DES", sucursal.succ_des);
                if (0 < command.ExecuteNonQuery())
                {
                    resp.success = true;
                    resp.message = "OK";
                }
                else
                {
                    resp.success = false;
                    resp.message = "NO HUBO ACTUALIZACION";
                }
                command.Parameters.Clear();
            }
            catch (SqlException ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            
            return Json(resp);
        }//END UPDATE SUCURSAL

        [HttpPost]
        public ActionResult deleteSucursal(ReqSucursal sucursal)
        {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM SUCURSAL WHERE SUCC_ID = @ID ", myConnection);
                command.Parameters.AddWithValue("@ID", sucursal.succ_id);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            catch (SqlException ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            resp.success = true;
            resp.message = "OK";
            return Json(resp);
        }

        [HttpPost]
        public ActionResult newSucursal(Sucursal newSucursal)
        {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO SUCURSAL (SUCC_ID, SUCC_DES) VALUES (@ID, @DES) ", myConnection);
                command.Parameters.AddWithValue("@ID", newSucursal.succ_id);
                command.Parameters.AddWithValue("@DES", newSucursal.succ_des);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            catch (SqlException ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            resp.success = true;
            resp.message = "OK";
            return Json(resp);
        }

        [HttpPost]
        public ActionResult getSucursales() {
            RespSucursal respSucursales = new RespSucursal();
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("select * from SUCURSAL;", myConnection);
                reader = command.ExecuteReader();
                List<Sucursal> listaSucursales = new List<Sucursal>();
                while (reader.Read())
                {
                    Sucursal suc = new Sucursal();
                    suc.succ_des = reader["succ_des"].ToString();
                    suc.succ_id = Convert.ToInt32(reader["succ_id"].ToString());
                    listaSucursales.Add(suc);
                }
                respSucursales.sucursales = listaSucursales;
            }
            catch (Exception ex)
            {
                respSucursales.success = false;
                respSucursales.message = "ERROR " + ex.Message;
                return Json(respSucursales);
            }
            finally
            {
                myConnection.Close();
            }
            respSucursales.success = true;
            respSucursales.message = "OK";
            return Json(respSucursales);
        }//END

        [HttpPost]
        public ActionResult deleteRestaurante(ReqDeleteRestaurant req) 
        {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM RESTAURANT WHERE REST_ID = @RESTID", myConnection);
                command.Parameters.AddWithValue("@RESTID", req.rest_id);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            resp.success = true;
            resp.message = "OK";
            return Json(resp);
        }

        [HttpPost]
        public ActionResult newRestaurant(ReqNewRestaurant req)
        {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO RESTAURANT (REST_DES, SUCC_ID) VALUES (@RESTDES, @SUC) ", myConnection);
                command.Parameters.AddWithValue("@RESTDES", req.rest_des);
                command.Parameters.AddWithValue("@SUC", req.succ_id);
                command.ExecuteNonQuery();
                command.Parameters.Clear();

            }
            catch (SqlException ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            resp.success = true;
            resp.message = "OK";
            return Json(resp);
        }

        [HttpPost]
        public ActionResult getRestaurantes(ReqGetRestaurant req)
        {
            RespRestaurant respuestaRestaurantes = new RespRestaurant();
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM RESTAURANT WHERE SUCC_ID = " + req.succ_id , myConnection);
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
        }//end public ActionResult()

        [HttpPost]
        public ActionResult updateRestaurante(Restaurant rest)
        {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("UPDATE RESTAURANT SET REST_DES = @DES  WHERE REST_ID = @ID AND SUCC_ID = @SUCC", myConnection);
                command.Parameters.AddWithValue("@ID", rest.rest_id);
                command.Parameters.AddWithValue("@DES", rest.rest_des);
                command.Parameters.AddWithValue("@SUCC", rest.succ_id);

                if (0 < command.ExecuteNonQuery())
                {
                    resp.success = true;
                    resp.message = "OK";
                }
                else
                {
                    resp.success = false;
                    resp.message = "NO HUBO ACTUALIZACION";
                }
                command.Parameters.Clear();
            }
            catch (SqlException ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            return Json(resp);
        }//END UPDATE SUCURSAL

        [HttpPost]
        public ActionResult newEmpleado(NuevoEmpleado emp)
        {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO EMPLEADO (EMPL_NOM, EMPL_APP, EMPL_APM, EMPL_COD, EMPL_STAT, EMPL_TIPO, SUCC_ID) "+
                                                    "VALUES (@NOM, @PAT, @MAT, @COD, 'ALTA', @TIPO, @SUCC )", myConnection);
                command.Parameters.AddWithValue("@NOM", emp.empl_nom);
                command.Parameters.AddWithValue("@PAT", emp.empl_app);
                command.Parameters.AddWithValue("@MAT", emp.empl_apm);
                command.Parameters.AddWithValue("@COD", emp.empl_cod);
                command.Parameters.AddWithValue("@TIPO", emp.empl_tipo);
                command.Parameters.AddWithValue("@SUCC", emp.succ_id);

                if (0 < command.ExecuteNonQuery())
                {
                    resp.success = true;
                    resp.message = "OK";
                }
                else
                {
                    resp.success = false;
                    resp.message = "NO OCURRIO INSERT";
                }

                command.Parameters.Clear();

            }
            catch (SqlException ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            return Json(resp);
        }//END NEW EMPLEADO

        [HttpPost]
        public ActionResult bajaEmpleado(ReqEmpleado emp)
        {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("UPDATE EMPLEADO SET EMPL_STAT = 'BAJA' WHERE EMPL_COD = @COD", myConnection);
                command.Parameters.AddWithValue("@COD", emp.empl_cod);
                if (0 < command.ExecuteNonQuery())
                {
                    resp.success = true;
                    resp.message = "OK";
                }
                else
                {
                    resp.success = false;
                    resp.message = "NO HUBO ACTUALIZACION";
                }
            }
            catch (Exception ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            return Json(resp);
        }//END BAJA EMPLEADO

        [HttpPost]
        public ActionResult getEmpleados(ReqEmpleados emp)
        {
            RespEmpleado empleados = new RespEmpleado();
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("select * from EMPLEADO where SUCC_ID =@SUCC", myConnection);
                command.Parameters.AddWithValue("@SUCC", emp.succ_id);
                reader = command.ExecuteReader();
                List<Empleado> listaEmpleado = new List<Empleado>();
                while (reader.Read())
                {
                    Empleado empleado = new Empleado();
                    empleado.empl_apm = reader["empl_apm"].ToString();
                    empleado.empl_app = reader["empl_app"].ToString();
                    empleado.empl_cod = Convert.ToInt32(reader["empl_cod"].ToString());
                    empleado.empl_nom = reader["empl_nom"].ToString();
                    empleado.empl_stat = reader["empl_stat"].ToString();
                    empleado.empl_tipo = reader["empl_tipo"].ToString();
                    listaEmpleado.Add(empleado);
                }
                empleados.listaEmpleados = listaEmpleado;
            }
            catch (Exception ex)
            {
                empleados.success = false;
                empleados.message = "ERROR " + ex.Message;
                return Json(empleados);
            }
            finally
            {
                myConnection.Close();
            }
            empleados.success = true;
            empleados.message = "OK";
            return Json(empleados);
        }

        [HttpPost]
        public ActionResult updateEmpleado(NuevoEmpleado emp)
        {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("UPDATE EMPLEADO SET EMPL_APM = @APM, EMPL_APP =@APP, EMPL_NOM =@NOM, EMPL_TIPO =@TIPO WHERE EMPL_COD = @COD", myConnection);
                command.Parameters.AddWithValue("@APM", emp.empl_apm);
                command.Parameters.AddWithValue("@COD", emp.empl_cod);
                command.Parameters.AddWithValue("@NOM", emp.empl_nom);
                command.Parameters.AddWithValue("@TIPO", emp.empl_tipo);
                if (0 < command.ExecuteNonQuery())
                {
                    resp.success = true;
                    resp.message = "OK";
                }
                else
                {
                    resp.success = false;
                    resp.message = "NO HUBO ACTUALIZACION";
                }
            }
            catch (Exception ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            return Json(resp);
        }//END 

        [HttpPost]
        public ActionResult newUbicacion(NewUbicacion ubic)
        {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO UBICACION(REST_ID, UBIC_DES) VALUES(@REST, @DESC);", myConnection);
                command.Parameters.AddWithValue("@REST", ubic.rest_id);
                command.Parameters.AddWithValue("@DESC", ubic.ubic_des);
                if (0 < command.ExecuteNonQuery())
                {
                    resp.success = true;
                    resp.message = "OK";
                }
                else
                {
                    resp.success = false;
                    resp.message = "NO SE CREO LA UBICACION";
                }
            }
            catch (Exception ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
            }
            return Json(resp);
        }//END NEW UBICACION

        [HttpPost]
        public ActionResult getUbicaciones(ReqUbicacion ubic)
        {
            Ubicaciones respUbicaciones = new Ubicaciones();
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM UBICACION WHERE REST_ID = @REST", myConnection);
                command.Parameters.AddWithValue("@REST", ubic.rest_id);
                reader = command.ExecuteReader();
                List<Ubicacion> listaUbic = new List<Ubicacion>();
                while (reader.Read())
                {
                    Ubicacion ubicacion = new Ubicacion();
                    ubicacion.rest_id = Convert.ToInt32(reader["rest_id"].ToString());
                    ubicacion.ubic_consec = Convert.ToInt32(reader["ubic_consec"].ToString());
                    ubicacion.ubic_des = reader["ubic_des"].ToString();
                    listaUbic.Add(ubicacion);
                }
                respUbicaciones.ubicaciones = listaUbic;
            }
            catch (Exception ex)
            {
                respUbicaciones.success = false;
                respUbicaciones.message = "ERROR " + ex.Message;
                return Json(respUbicaciones);
            }
            finally
            {
                myConnection.Close();
            }
            respUbicaciones.success = true;
            respUbicaciones.message ="OK";
            return Json(respUbicaciones);
        }//END GET UBICACIONES

        [HttpPost]
        public ActionResult updateUbicacion(Ubicacion ubic)
        {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("UPDATE UBICACION SET UBIC_DES =@DESC WHERE UBIC_CONSEC = @CONSEC AND REST_ID =@REST", myConnection);
                command.Parameters.AddWithValue("@REST", ubic.rest_id);
                command.Parameters.AddWithValue("@CONSEC", ubic.ubic_consec);
                command.Parameters.AddWithValue("@DESC", ubic.ubic_des);
                if (0 < command.ExecuteNonQuery())
                {
                    resp.success = true;
                    resp.message = "OK";
                }
                else
                {
                    resp.success = false;
                    resp.message = "NO SE COMPLETO LA ACTUALIZACION";
                }
            }
            catch (Exception ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            return Json(resp);
        }//END UPDATEUBICACION

        [HttpPost]
        public ActionResult deleteUbicacion(ReferenceUbicacion ubicacion)
        {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM UBICACION WHERE REST_ID = @REST AND UBIC_CONSEC = @CONSEC", myConnection);
                command.Parameters.AddWithValue("@REST", ubicacion.rest_id);
                command.Parameters.AddWithValue("@CONSEC", ubicacion.ubic_consec);

                if (0 < command.ExecuteNonQuery())
                {
                    resp.success = true;
                    resp.message = "OK";
                }
                else
                {
                    resp.success = false;
                    resp.message = "NO SE ELIMINO LA UBICACION CORRECTAMENTE";
                }
            }
            catch (Exception ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            return Json(resp);
        }//END DELETEUBICACION

        [HttpPost]
        public ActionResult newMesa(NewMesa mesa)
        {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO MESA (MESA_CVE, MESA_DES, MESA_STAT, REST_ID, UBIC_CONSEC)	VALUES (@CVE, @DES, 'ALTA', @REST, @UBIC);", myConnection);
                command.Parameters.AddWithValue("@CVE", mesa.mesa_cve);
                command.Parameters.AddWithValue("@DES", mesa.mesa_des);
                command.Parameters.AddWithValue("@REST", mesa.rest_id);
                command.Parameters.AddWithValue("@UBIC", mesa.ubic_consec);
                if (0 < command.ExecuteNonQuery())
                {
                    resp.success = true;
                    resp.message = "OK";
                }
                else
                {
                    resp.success = false;
                    resp.message = "NO SE CREO LA MESA";
                }
            }
            catch (Exception ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            return Json(resp);
        }//END NEWMESA

        [HttpPost]
        public ActionResult deleteMesa(IdentifMesa mesa)
        {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            { 
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM MESA WHERE MESA_ID = @MESA", myConnection);
                command.Parameters.AddWithValue("@MESA", mesa.mesa_id);
                if (0 < command.ExecuteNonQuery())
                {
                    resp.success = true;
                    resp.message = "OK";
                }
                else
                {
                    resp.success = false;
                    resp.message = "NO SE CREO LA UBICACION";
                }
            }
            catch (Exception ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            return Json(resp);
        }//END DELETEMESA

        [HttpPost]
        public ActionResult updateMesa(Mesa mesa)
        {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("UPDATE MESA SET MESA_CVE = @MESACVE, MESA_DES =@MESADES, MESA_STAT ='ALTA', REST_ID = @RESTID, UBIC_CONSEC= @UBIC WHERE MESA_ID = @MESAID", myConnection);
                command.Parameters.AddWithValue("@MESACVE", mesa.mesa_cve);
                command.Parameters.AddWithValue("@MESADES", mesa.mesa_des);
                command.Parameters.AddWithValue("@RESTID", mesa.rest_id);
                command.Parameters.AddWithValue("@UBIC", mesa.ubic_consec);
                command.Parameters.AddWithValue("@MESAID", mesa.mesa_id);
                if (0 < command.ExecuteNonQuery())
                {
                    resp.success = true;
                    resp.message = "OK";
                }
                else
                {
                    resp.success = false;
                    resp.message = "NO SE CREO LA UBICACION";
                    return Json(resp);
                }
            }
            catch (Exception ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            return Json(resp);
        }//END UPDATE MESA

        [HttpPost]
        public ActionResult getMesas(ReqMesa reqMesa)
        {
            RespMesas mesas = new RespMesas();
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM MESA WHERE REST_ID =@REST AND UBIC_CONSEC = @CONSEC", myConnection);
                command.Parameters.AddWithValue("@REST", reqMesa.rest_id);
                command.Parameters.AddWithValue("@CONSEC", reqMesa.ubic_consec);
                reader = command.ExecuteReader();
                List<Mesa> listaMesas = new List<Mesa>();
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
                mesas.success = true;
                mesas.message = "OK ";

            }
            catch (Exception ex)
            {
                mesas.success = false;
                mesas.message = "ERROR " + ex.Message;
                return Json(mesas);
            }
            finally
            {
                myConnection.Close();
            }
            return Json(mesas);
        }//END GET MESAS

        [HttpPost]
        public ActionResult getTipoProducto()
        {
            RespTipoProducto tipoProductos = new RespTipoProducto();
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM TIPO_PRODUCTO", myConnection);
                reader = command.ExecuteReader();
                List<TipoProducto> listaTipoProducto = new List<TipoProducto>();
                while (reader.Read())
                {
                    TipoProducto tipoProducto = new TipoProducto();
                    tipoProducto.tipp_id = Convert.ToInt32(reader["tipp_id"].ToString());
                    tipoProducto.tipp_des = reader["tipp_des"].ToString();
                    listaTipoProducto.Add(tipoProducto);
                }
                tipoProductos.tipoProductos = listaTipoProducto;
            }
            catch (Exception ex)
            {
                tipoProductos.success = false;
                tipoProductos.message = "ERROR " + ex.Message;
                return Json(tipoProductos);
            }
            finally
            {
                myConnection.Close();
            }
            tipoProductos.success = true;
            tipoProductos.message = "OK";
            return Json(tipoProductos);
        }//END GETTIPOPRODUCTOS

        [HttpPost]
        public ActionResult newImpresora(NewImpresora impr)
        {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO IMPRESORA (IMPR_CONF, IMPR_DES, IMPR_STAT, REST_ID, TIPP_ID, UBIC_CONSEC) VALUES (@CONF , @DES , 'ALTA' , @REST , @TIPP , @CONSEC)", myConnection);
                command.Parameters.AddWithValue("@CONF", impr.impr_conf);
                command.Parameters.AddWithValue("@DES", impr.impr_des);
                command.Parameters.AddWithValue("@REST", impr.rest_id);
                command.Parameters.AddWithValue("@TIPP", impr.tipp_id);
                command.Parameters.AddWithValue("@CONSEC", impr.ubic_consec);
                if (0 < command.ExecuteNonQuery())
                {
                    resp.success = true;
                    resp.message = "OK";
                }
                else
                {
                    resp.success = false;
                    resp.message = "NO SE CREO MESA";
                }
            }
            catch (Exception ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            return Json(resp);
        }

        [HttpPost]
        public ActionResult deleteImpresora(RefImpresora refImpresora)
        {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM IMPRESORA WHERE IMPR_ID = @IMPR", myConnection);
                command.Parameters.AddWithValue("@IMPR", refImpresora.impr_id);
                if (0 < command.ExecuteNonQuery())
                {
                    resp.success = true;
                    resp.message = "OK";
                }
                else
                {
                    resp.success = false;
                    resp.message = "NO SE ELIMINO IMPRESORA";
                }
                
            }
            catch (Exception ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            return Json(resp);
        }

        [HttpPost]
        public ActionResult updateImpresora(Impresora impresora)
        {
            Respuesta resp = new Respuesta();
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("UPDATE IMPRESORA SET IMPR_CONF =@CONF, IMPR_DES =@DES, IMPR_STAT='ALTA', REST_ID=@REST, TIPP_ID=@TIPP, UBIC_CONSEC= @UBIC  WHERE IMPR_ID = @IMPRID", myConnection);
                command.Parameters.AddWithValue("@CONF", impresora.impr_conf);
                command.Parameters.AddWithValue("@DES", impresora.impr_des);
                command.Parameters.AddWithValue("@IMPRID", impresora.impr_id);
                command.Parameters.AddWithValue("@REST", impresora.rest_id);
                command.Parameters.AddWithValue("@TIPP", impresora.tipp_id);
                command.Parameters.AddWithValue("@UBIC", impresora.ubic_consec);
                if (0 < command.ExecuteNonQuery())
                {
                    resp.success = true;
                    resp.message = "OK";
                }
                else
                {
                    resp.success = false;
                    resp.message = "NO SE ACTUALIZO IMPRESORA";
                }
            }
            catch (Exception ex)
            {
                resp.success = false;
                resp.message = "ERROR " + ex.Message;
                return Json(resp);
            }
            finally
            {
                myConnection.Close();
            }
            return Json(resp);
        }

        [HttpPost]
        public ActionResult getImpresoras()
        {
            RespImpresora impresoras = new RespImpresora();
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            try
            {
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["BaseComercial"].ConnectionString;
                myConnection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM IMPRESORA", myConnection);
                reader = command.ExecuteReader();
                List<Impresora> listaImpr = new List<Impresora>();
                while (reader.Read())
                {
                    Impresora impresora = new Impresora();
                    impresora.impr_conf = reader["impr_conf"].ToString();
                    impresora.impr_des = reader["impr_des"].ToString();
                    impresora.impr_id = Convert.ToInt32(reader["impr_id"].ToString());
                    impresora.impr_stat = reader["impr_stat"].ToString();
                    impresora.rest_id = Convert.ToInt32(reader["rest_id"].ToString());
                    impresora.tipp_id = Convert.ToInt32(reader["tipp_id"].ToString());
                    impresora.ubic_consec = Convert.ToInt32(reader["ubic_consec"].ToString());
                    listaImpr.Add(impresora);
                }
                impresoras.impresoras = listaImpr;
            }
            catch (Exception ex)
            {
                impresoras.success = false;
                impresoras.message = "ERROR " + ex.Message;
                return Json(impresoras);
            }
            finally
            {
                myConnection.Close();
            }
            impresoras.success = true;
            impresoras.message = "OK";
            return Json(impresoras);
        }//END GET IMPRESORAS



    }
}
