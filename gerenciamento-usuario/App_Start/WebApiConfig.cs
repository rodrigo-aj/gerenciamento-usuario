using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace gerenciamento_usuario
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApiAction",
                routeTemplate: "api/{controller}/{action}"
            );
        }
    }
}
