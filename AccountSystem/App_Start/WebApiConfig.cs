using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Routing;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using WebApiContrib.Selectors;

namespace AccountSystem
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Replace default controller selector by custom
            // Support multiple controller same name but difference namespace
            // Can used to create multiple version for WebAPI
            config.Services.Replace(typeof(IHttpControllerSelector), 
                new NamespaceHttpControllerSelector(config));

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Route 2
            {
                var dataTokens = new RouteValueDictionary();
                dataTokens["_namespace"] = "AccountSystem.Areas.Admin.Controllers";
                var defaults = new RouteValueDictionary();
                defaults["id"] = RouteParameter.Optional;
                defaults["_namespace"] = "AccountSystem.Areas.Admin.Controllers";
                var route = config.Routes.CreateRoute(
                    "api/admin/{controller}/{action}/{id}",
                    defaults,
                    null,
                    dataTokens);

                config.Routes.Add("AdminApi", route);
            }
            // Route 1
            {
                var dataTokens = new RouteValueDictionary();
                dataTokens["_namespace"] = "AccountSystem.Controllers";
                var defaults = new RouteValueDictionary();
                defaults["id"] = RouteParameter.Optional;
                defaults["_namespace"] = "AccountSystem.Controllers";
                var route = config.Routes.CreateRoute(
                    "api/{controller}/{action}/{id}",
                    defaults,
                    null,
                    dataTokens);

                config.Routes.Add("DefaultApi", route);
            }
            

            /*
            var r = config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new {
                    id = RouteParameter.Optional,
                    _namespace = "AccountSystem.Controllers"
                }
            );
            r.DataTokens = new RouteValueDictionary();
            //r.DataTokens = new { _namespace = "AccountSystem.Controllers" };
            */
        }
    }
}
