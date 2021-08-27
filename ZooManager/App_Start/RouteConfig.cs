using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ASPDemo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

        /*    routes.MapRoute(
            name: "About",
            url: "URL/demo/{id}",
            defaults: new { controller = "Details", action = "Details", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Empployee",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Employee", action = "index", id = UrlParameter.Optional }
            );
        */

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }

    }
}
