using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ComandasCityMarket
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Alive", // Route name
                "{controller}/{action}", // URL with parameters
                new { controller = "Alive", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Catalogo",
                "{controller}",
                new { controller = "Catalogo", action = "Index" }
            );

            routes.MapRoute(
                "CerrarOrden",
                "{controller}",
                new { controller = "CerrarOrden", action = "Index" }
            );

            routes.MapRoute(
                "ConsolidarOrden",
                "{controller}",
                new { controller = "ConsolidarOrden", action = "Index" }
            );
            
            routes.MapRoute(
                "Entrar",
                "{controller}",
                new { controller = "Entrar", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Mesas",
                "{controller}/{action}",
                new { controller = "Mesas", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "NuevaComanda",
                "{controller}/{action}",
                new { controller = "NuevaComanda", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "NuevaOrden",
                "{controller}/{action}",
                new { controller = "NuevaOrden", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "OrdenesActivas",
                "{controller}/{action}",
                new { controller = "OrdenesActivas", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Restaurantes",
                "{controller}/{action}",
                new { controller = "Restaurantes", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "Salir",
                "{controller}/{action}",
                new { controller = "Salir", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "TransferirOrden",
                "{controller}/{action}",
                new { controller = "TransferirOrden", action = "Index" } // Parameter defaults
            );

            /*
            routes.MapRoute(
                "Adminweb",
                "AdminwebRestaurante",
                new { controller = "Alive", action = "Index" } // Parameter defaults
            );
            */


        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            System.Web.Mvc.ModelBinders.Binders.DefaultBinder = new ComandasCityMarket.ModelBinders.ApiDefaultModelBinder();
            RegisterRoutes(RouteTable.Routes);
        }
    }
}