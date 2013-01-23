using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComandasCityMarket.Models;

namespace ComandasCityMarket.Controllers
{
    public class SalirController : Controller
    {
        //
        // GET: /Salir/
        [HttpPost]
        public ActionResult Index(Empleado empleado)
        {
            Respuesta respuesta = new Respuesta();
            
            if (true)
            {
                respuesta.success = true;
                respuesta.message = "OK";
            }
            return Json(respuesta);
        }

    }
}
