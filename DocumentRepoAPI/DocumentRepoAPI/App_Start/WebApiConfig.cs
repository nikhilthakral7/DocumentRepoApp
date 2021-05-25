using DocumentRepoAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DocumentRepoAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            //This will apply this filter to all the actions globally
            config.Filters.Add((MukulAuthorizeFilter)config.DependencyResolver.GetService(typeof(MukulAuthorizeFilter)));

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
