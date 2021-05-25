using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Routing;

namespace DocumentRepoAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            string logPath = WebConfigurationManager.AppSettings["LogPath"];
            //Configuring Serilog to log in file
            Log.Logger = new LoggerConfiguration()
   .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
   .CreateLogger();
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }


    }
}
