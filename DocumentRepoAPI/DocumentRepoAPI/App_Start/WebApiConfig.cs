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
            //(MukulAuthorizeFilter)config.DependencyResolver.GetService(typeof(MukulAuthorizeFilter)) - It is added to pass authObj to the dependency in the MukulAuthorizeFilter
            config.Filters.Add((MukulAuthorizeFilter)config.DependencyResolver.GetService(typeof(MukulAuthorizeFilter)));
            //config.MessageHandlers
            //config.Formatters

            config.Filters.Add(new MukulCompressionFilter());

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
