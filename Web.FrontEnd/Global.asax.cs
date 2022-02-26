namespace Web.FrontEnd
{
    using System;
    using System.Web;
    using log4net;
    using System.Threading;
    using Library;
    using Web.Asp.Provider;

    //using System.Web;

    //using VIT.Provider;

    public class Global : System.Web.HttpApplication
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(Global));

        public Global()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        void Application_Start(object sender, EventArgs e)
        {
            //RouteTable.Routes.AddCombresRoute("Combres Route");
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            new MemberSecurityProvider().InitializePrincipal();
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is ThreadAbortException) return;

            Exception objErr = Server.GetLastError().GetBaseException();
            log.Error("Error Caught in Application_Error event");
            log.Error("Error in: " + Request.Url);
            log.Error("Error Message:" + objErr.Message);
            log.Error(objErr.TraceInformation());
            HttpContext.Current.Response.Redirect(new URL().BaseUrl + "Error.aspx?msg=" + Server.UrlEncode(ex.Message));
        }

        void Session_Start(object sender, EventArgs e)
        {
            Session["language"] = SettingsManager.Constants.DefauleLanguage;
            Session["CountError"] = 0;

            //Session.Timeout = 150;

            // check FreeDay
            if (SettingsManager.AppSettings.FreeDay < 1 || 30 < SettingsManager.AppSettings.FreeDay)
            {
                HttpContext.Current.Response.Redirect(
                    (new URL()).BaseUrl + "Error.aspx?msg="
                    + Server.UrlEncode("Cấu hình không đúng: 0 < FreeDay < 31 "));
            }
        }

        void Session_End(object sender, EventArgs e)
        {
			try
            {
                if (Session[SettingsManager.Constants.SessionCompanyConfig] != null)
                {
                    Application.Lock();
                    Application["visitors_online" + Session[SettingsManager.Constants.SessionCompanyConfig]] = Convert.ToUInt64(Application["visitors_online" + Session[SettingsManager.Constants.SessionCompanyConfig]]) - 1;
                    Application.UnLock();
                }
			}
            catch 
			{ 
				Application.UnLock();
			}
        }

    }
}
