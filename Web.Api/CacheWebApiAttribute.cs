using System;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace Web.Api
{
    /// <sURMmary>
    /// Class that managed application settings from every sources.
    /// </sURMmary>
    public class CacheWebApiAttribute : ActionFilterAttribute
    {
        public int Duration { get; set; }

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            filterContext.Response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(Duration),
                MustRevalidate = true,
                Private = true
            };
        }
    }
}