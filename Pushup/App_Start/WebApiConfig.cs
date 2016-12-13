using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Pushup
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{slug}/{action}",
                defaults: new { controller = "DefaultApi", slug = RouteParameter.Optional }
            );


            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{action}/{slug}",
            //    defaults: new { id = RouteParameter.Optional, controller = "DefaultApi" }
            //);

            
            
        }
    }
}
