using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Api
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("odata/{*pathInfo}");

            routes.MapRoute(
                name: "Position",
                url: "Position/{Position}",
                defaults: new { controller = "Component", action = "Position", Position = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Component",
                url: "{Component}/{Title}/{id}",
                defaults: new { controller = "Component", action = "Index", Component="Home", Title = UrlParameter.Optional, id = UrlParameter.Optional }
            );
        }
    }
}
