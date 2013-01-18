using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return Json(res);
        }

    }
}
