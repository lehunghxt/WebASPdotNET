﻿using Library.Web;
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
using Web.Asp.Provider;
using Web.Asp.Security;

namespace Web.Backend
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

        protected void FormsAuthentication_OnAuthenticate(Object sender, FormsAuthenticationEventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true && !Request.Path.ToLower().StartsWith("/odata"))
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        if (!Request.Path.ToLower().StartsWith("/login"))
                        {
                            //let us take out the username now                
                            string userName = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

                            var key = string.Format(SettingsManager.Constants.CookeiLogin, userName);
                            if (Request.Cookies[key] == null)
                                HttpContext.Current.Response.StatusCode = 401;
                            else
                            {
                                var appId = Convert.ToInt32(Request.Cookies[key][SettingsManager.Constants.CookeiAppIdKey]);
                                var token = Request.Cookies[key][SettingsManager.Constants.CookeiTockenKey];
                                token = string.Format("{0}|{1}|{2}", token, userName, appId);

                                var api = new ApiHelper(token);
                                var param = new Dictionary<string, object>();
                                param["UserName"] = userName;

                                var data = api.GetAll<UserInfoModel>(string.Format("{0}/odata/UserInfo", SettingsManager.AppSettings.URMService), param);
                                var user = data.FirstOrDefault();
                                if (user == null) HttpContext.Current.Response.StatusCode = 401;
                                else
                                {
                                    var dataRole = api.GetAll<RoleModel>(string.Format("{0}/odata/Role", SettingsManager.AppSettings.URMService), param);
                                    var roles = new string[] { };
                                    if (dataRole != null && dataRole.Any(o => o.ID == SettingsManager.Constants.PermissonLoginAdmin)) roles = dataRole.Select(o => o.ID).ToArray();
                                    else
                                    {
                                        log.Warn("Khong co quyen: " + SettingsManager.Constants.PermissonLoginAdmin);
                                        HttpContext.Current.Response.StatusCode = 401;
                                    }

                                    var identity = new GenericIdentity(userName, "Forms");
                                    var principal = new UserPrincipal(identity, roles);
                                    principal.FullName = user.FullName;
                                    principal.UserName = userName;
                                    principal.Roles = roles;
                                    principal.UserId = user.ID;
                                    principal.AppId = appId;
                                    principal.Token = token;
                                    principal.Phone = user.Phone;
                                    principal.Email = user.Email;
                                    principal.Birthday = user.Birthday;
                                    principal.Address = user.Address;

                                    var companyBLL = new CompanyBLL();
                                    var companyId = companyBLL.GetCompanyByUserId(user.ID);
                                    principal.CompanyId = companyId;

                                    e.User = principal;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex.Message, ex);
                        //somehting went wrong
                        Response.Redirect("/login");
                    }
                }
                else if (!Request.Path.ToLower().StartsWith("/login") && !Request.Path.ToLower().StartsWith("/api") && !Request.Path.ToLower().StartsWith("/application")) Response.Redirect("/login");
            }
        }
    }
}
