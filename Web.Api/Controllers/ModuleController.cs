namespace Web.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using Web.Business;
    using Web.Api.Models;
    using Web.Model;

    public class ModuleController : Controller
    {
        protected const string SkinFormat = "~/Views/{0}/Skin/{1}.cshtml";     

        protected string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        /// Lay gia tri tu Params
        /// </summary>
        /// <typeparam name="T">Kieu du lieu cua Param</typeparam>
        /// <param name="key">Key cua Param</param>
        /// <returns>
        /// Gia tri cua Param voi kieu du lieu cu the
        /// </returns>
        protected T GetValueParam<T>(Dictionary<string, object> param, string key)
        {
            if (param != null && param.ContainsKey(key))
            {
                try
                {
                    var value = Convert.ChangeType(param[key], typeof(T));
                    return value is T ? (T)value : default(T);
                }
                catch
                {
                    return default(T);
                }
            }
            return default(T);
        }

        /// <summary>
        /// Lay gia tri tu Params
        /// </summary>
        /// <typeparam name="T">Kieu du lieu cua Param</typeparam>
        /// <param name="key">Key cua Param</param>
        /// <returns>
        /// Gia tri cua Param voi kieu du lieu cu the
        /// </returns>
        protected T GetValueRequest<T>(string keyRequest)
        {
            if (this.Request.Params.AllKeys.Select(c => (c ?? string.Empty).ToLower()).Contains(keyRequest.ToLower()))
            {
                try
                {
                    var value = Convert.ChangeType(this.Request.Params.Get(keyRequest), typeof(T));
                    if (value is T) return (T)value;
                }
                catch
                {
                    return default(T);
                }
            }
            return default(T);
        }

        /// <summary>
        /// Lay gia tri tu request, neu ko co thi lay gia tri Params
        /// </summary>
        /// <typeparam name="T">Kieu du lieu cua Param</typeparam>
        /// <param name="keyRequest">Key cua Request</param>
        /// <param name="keyParam">Key cua Param</param>
        /// <returns>
        /// Gia tri voi kieu du lieu cu the
        /// </returns>
        protected T GetRequestThenParam<T>(Dictionary<string, object> param, string keyRequest, string keyParam)
        {
            try
            {
                if (this.Request.Params.AllKeys.Select(c => (c ?? string.Empty).ToLower()).Contains(keyRequest.ToLower()))
                {
                    var value = Convert.ChangeType(this.Request.Params.Get(keyRequest), typeof(T));
                    if (value is T) return (T)value;
                }

                if (param != null && param.ContainsKey(keyParam))
                {
                    var value = Convert.ChangeType(param[keyParam], typeof(T));
                    return value is T ? (T)value : default(T);
                }
            }
            catch
            {
                return default(T);
            }

            return default(T);
        }

        /// <summary>
        /// Lay gia tri tu param, neu ko co thi lay gia tri request
        /// </summary>
        /// <typeparam name="T">Kieu du lieu cua Param</typeparam>
        /// <param name="keyRequest">Key cua Request</param>
        /// <param name="keyParam">Key cua Param</param>
        /// <returns>
        /// Gia tri voi kieu du lieu cu the
        /// </returns>
        protected T GetParamThenRequest<T>(Dictionary<string, object> param, string keyRequest, string keyParam)
        {
            try
            {
                if (param != null && param.ContainsKey(keyParam))
                {
                    var value = Convert.ChangeType(param[keyParam], typeof(T));
                    if (value is T) return (T)value;
                }

                if (this.Request.Params.AllKeys.Select(c => c.ToLower()).Contains(keyRequest.ToLower()))
                {
                    var valueParam = Convert.ChangeType(this.Request.Params.Get(keyRequest), typeof(T));
                    return valueParam is T ? (T)valueParam : default(T);
                }
            }
            catch
            {
                return default(T);
            }

            return default(T);
        }
    }
}