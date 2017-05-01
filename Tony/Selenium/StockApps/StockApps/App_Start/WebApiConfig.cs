using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace StockApps
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}/",
                defaults: new { id = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{id}/{action}",
                defaults: new { id = RouteParameter.Optional }
                );
            
            /*
            config.Routes.MapHttpRoute(
                name: "DefaultApiWithExtensions",
                routeTemplate: "{controller}.{ext}/{action}",
                defaults: new { ext = "json", action = "Get", showHelp = true }
            );
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new UriPathExtensionMapping("json", "application/json"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}",
                defaults: new { ext = "json", action = "Get", showHelp = true }
            );
            config.Formatters.JsonFormatter.AddQueryStringMapping("responseContentType", "json", "application/json");
            */
        }
    }
}
