namespace Web.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    using log4net;

    public class RewriteUrl : IHttpModule
    {
        #region IHttpModule Members

        public void Dispose()
        {
        }

        protected static readonly ILog log = LogManager.GetLogger(typeof(RewriteUrl));

        public static string RawUrl
        {
            get { return HttpContext.Current.Request.RawUrl.Replace('\'', '/'); }
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += Context_BeginRequest;
        }

        private static void Context_BeginRequest(object sender, EventArgs e)
        {
            var httpApplication = (HttpApplication)sender;
            var rawUrl = RawUrl.ToLower();
            
            if (!rawUrl.Contains("__browserlink") && !rawUrl.Contains("/uploads/") && !rawUrl.Contains("/includes/") && !rawUrl.Contains("/templates/") && !rawUrl.Contains("/modules/"))
            {
            }
        }

        #endregion
    }
}
