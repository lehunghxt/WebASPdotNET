using Library.Web;
using Library.Web.Security;
using log4net;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Web.Model;
using Web.Business;

namespace Web.Api
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));

        public MvcApplication()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(config =>
            {
                ODataConfig.Register(config); //this has to be before WebApi
                WebApiConfig.Register(config);

            });

            //WebApiConfig.Register(GlobalConfigur
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is ThreadAbortException) return;

            Exception objErr = Server.GetLastError().GetBaseException();
            log.Error("Error Caught in Application_Error event");
            log.Error("Error in: " + Request.Url.ToString());
            if (objErr.Message != null) log.Error("Error Message:" + objErr.Message.ToString());
            if (objErr.StackTrace != null) log.Error("Stack Trace:" + objErr.StackTrace.ToString());
        }
    }
}
