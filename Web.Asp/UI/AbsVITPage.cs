namespace Web.Asp.UI
{
    using System.Text;
    using System.Web;
    using System.Linq;
    using System.Collections.Generic;

    using Library.Web;
    using Web.Asp.Controls;
    using System;
    using Web.Business;
    using Web.Asp.Provider;
    using Web.Model;
    using Security;
    using log4net;
    using Library;

    abstract public class AbsVITPage : System.Web.UI.Page
    {
        private readonly SEOBLL _seoProvider;
        private readonly CompanyBLL _companyProvider;

        protected static readonly ILog log = LogManager.GetLogger(typeof(VITComponent));

        /// <summary>
        /// The http request wrapper.
        /// </summary>
        private readonly HttpContextBase httpContext;

        #region Properties

        private URL _url;
        public URL HREF
        {
            get
            {
                return this._url;
            }
        }

        private LanguageHelper _language;
        public LanguageHelper Language
        {
            get
            {
                return this._language;
            }
        }

        public UserPrincipal UserContext
        {
            get
            {
                // NOTE: add claims identity to support this method
                if (this.httpContext.User.Identity.IsAuthenticated)
                {
                    var principal = this.httpContext.User as UserPrincipal;
                    return principal;
                }

                return null;
            }
        }

        public CompanyConfigModel Config { get; set; }

        public CompanyInfoModel Company { get; set; }
        #endregion

        /// <summary>
        /// hàm add module vào component
        /// </summary>
        /// <param name="position">Vi tri oad module</param>
        /// <param name="moduleName">Tên module</param>
        /// <param name="pars">Param truyền vào module</param>
        /// <param name="title">Tên module</param>
        /// <returns>True: load thành công</returns>
        public bool LoadModule(Position position, string moduleName, Dictionary<string, string> pars, string title = "")
        {
            try
            {
                var pathModule = new StringBuilder();
                pathModule.Append(HREF.TemplatePath);
                pathModule.Append("Skins/");
                pathModule.Append(moduleName);
                pathModule.Append(".ascx");

                var module = LoadControl(pathModule.ToString()) as VITModule;
                if (module != null)
                {
                    module.Title = title;
                        module.Params = pars;
                        position.Controls.Add(module);
                        return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// hàm add module vào component
        /// </summary>
        /// <param name="skinId">Id server của Skin</param>
        /// <param name="moduleName">Tên module</param>
        /// <param name="pars">Param truyền vào module</param>
        /// <param name="title">Tên module</param>
        /// <returns>True: load thành công</returns>
        public bool LoadModule(string skinId, string moduleName, Dictionary<string, string> pars, string title = "")
        {
            try
            {
                var position = this.FindControl(skinId) as Position;
                if (position != null)
                {
                    var pathModule = new StringBuilder();
                    pathModule.Append(HREF.AppPath);
                    pathModule.Append("Skins/");
                    pathModule.Append(moduleName);
                    pathModule.Append(".ascx");
                    
                    var module = LoadControl(pathModule.ToString()) as VITModule;
                    if (module != null)
                    {

                        module.Title = title;
                        module.Params = pars;
                        position.Controls.Add(module);
                            
                            return true;
                    }
                    return false;
                }
                return false;
            }
            catch { return false; }
        }

        public static bool IsIE6
        {
            get
            {
                if (HttpContext.Current.Request.Browser.Browser == "IE" && HttpContext.Current.Request.Browser.Version == "6.0")
                    return true;
                return false;
            }
        }

        protected AbsVITPage()
        {
            this._seoProvider = new SEOBLL();
            this._companyProvider = new CompanyBLL();

            this._url = new URL();
            this._language = new LanguageHelper();

            this.httpContext = new HttpContextWrapper(HttpContext.Current);

            try
            {
                // setconfig
                var config = _companyProvider.GetCompanyByDomain(HREF.Domain);
                if (string.IsNullOrEmpty(config.Language)) config.Language = SettingsManager.Constants.DefauleLanguage;
                this.Config = config;
                //end setconfig

                // set company
                var company = _companyProvider.GetCompany(Config.ID, Config.Language);
                if (company != null)
                {
                    company.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathCompanyImage + company.IMAGE;
                    company.PathWebIcon = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathCompanyImage + company.WebIcon;
                    if (!string.IsNullOrEmpty(company.Background) && !company.Background.StartsWith("#"))
                        company.Background = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathCompanyImage + company.Background;
                }
                this.Company = company;
                // end set company

                var list = HttpContext.Current.Application[SettingsManager.Constants.AppSEOLinkCache + Config.ID] as List<SEOLinkModel>;
                if (list == null || list.Count == 0)
                {
                    list = _seoProvider.GetAll(Config.ID).ToList();
                    HttpContext.Current.Application[SettingsManager.Constants.AppSEOLinkCache + Config.ID] = list;
                }

                if (list.Count > 0)
                {
                    var curent = HttpContext.Current.Request.RawUrl.ToLower();
                    var link = list.FirstOrDefault(l => l.Url.ToLower() == curent || l.SeoUrl.ToLower() == curent);
                    if (link != null)
                    {
                        Title = link.Title;
                        MetaKeywords = link.MetaKeyWork;
                        MetaDescription = link.MetaDescription;
                    }
                    else
                    {
                        Title = company.DISPLAYNAME;
                        MetaKeywords = company.DISPLAYNAME + " - " + company.SLOGAN;
                        MetaDescription = company.DESCRIPTION;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.TraceInformation());
            }
        }

        /// <summary>
        /// Lay gia tri tu Request
        /// </summary>
        /// <typeparam name="T">Kieu du lieu cua Param</typeparam>
        /// <param name="key">Key cua Param</param>
        /// <returns>
        /// Gia tri cua Param voi kieu du lieu cu the
        /// </returns>
        protected T GetValueRequest<T>(string keyRequest)
        {
            if (HttpContext.Current.Request.Params.AllKeys.Select(c => (c ?? string.Empty).ToLower()).Contains(keyRequest.ToLower()))
            {
                try
                {
                    var value = Convert.ChangeType(HttpContext.Current.Request.Params.Get(keyRequest), typeof(T));
                    if (value is T) return (T)value;
                }
                catch
                {
                    return default(T);
                }
            }
            return default(T);
        }

        protected override void OnInit(EventArgs e)
        {
            var action = new ActionValidator(SettingsManager.AppSettings.EnablePreventDDOSSearchEgine, SettingsManager.AppSettings.EnablePreventDDOSHits);
            base.OnInit(e);
            if (!base.IsPostBack)
                if (!action.IsValid(ActionValidator.ActionTypeEnum.FirstVisit)) Response.End();
                else if(!action.IsValid(ActionValidator.ActionTypeEnum.ReVisit)) Response.End();
            else if(!action.IsValid(ActionValidator.ActionTypeEnum.PostBack)) Response.End();
        }
    }
}
