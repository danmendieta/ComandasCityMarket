using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComandasCityMarket.Models;


namespace ComandasCityMarket.Controllers
{
    public class EntrarController : Controller
    {
        //
        // GET: /Entrar/

        [HttpPost]
        public ActionResult Index(Empleado empleado)
        {
            Respuesta respuesta = new Respuesta();
            //Valida Empleado
            if (true)
            {
                respuesta.success = true;
                respuesta.message = "OK";
            }
            else
            {
                respuesta.success = false;
                respuesta.message = "ERROR USUARIO/CONTRASEÑA";
            }
            return Json(respuesta);
        }

    }
}
