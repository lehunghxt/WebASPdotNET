namespace Web.Asp.UI
{
    using Library;
    using Security;
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Security;
    using Web.Asp.Provider;
    using Web.Business;
    using Web.Model;

    abstract public class VITTemplate : System.Web.UI.MasterPage, IWebUI
    {
        private readonly CompanyBLL _companyProvider;

        #region Properties

        public List<OrderProductModel> GioHang
        {
            get
            {
                var giohang = Session[SettingsManager.Constants.SessionGioHang + this.Config.ID] as List<OrderProductModel>;
                if (giohang == null) giohang = new List<OrderProductModel>();
                return giohang;
            }
        }

        public List<ProductModel> DaXem
        {
            get
            {
                var daxem = Session[SettingsManager.Constants.SessionDaXem + this.Config.ID] as List<ProductModel>;
                if (daxem == null) daxem = new List<ProductModel>();
                return daxem;
            }
        }
        #endregion

        protected VITTemplate()
        {
            this._companyProvider = new CompanyBLL();

            this._url = new URL();
            this._language = new LanguageHelper();

            if (!String.IsNullOrEmpty(HttpContext.Current.Request["logout"]))
            {
                FormsAuthentication.SignOut();
                HREF.RedirectComponent("Home");
            }
        }

        #region inherit interface
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
                return this.Page.User as UserPrincipal;
            }
        }

        public CompanyConfigModel Config
        {
            get
            {
                var config = (new CompanyBLL()).GetCompanyByDomain(HREF.Domain);
                var language = HttpContext.Current.Session[SettingsManager.Constants.SessionLanguage] as string;
                if (!string.IsNullOrEmpty(language)) config.Language = language;
                if (string.IsNullOrEmpty(config.Language)) config.Language = SettingsManager.Constants.DefauleLanguage;

                return config;
            }
        }

        public WEBCONFIGModel FullConfig
        {
            get
            {
                var config = Session[SettingsManager.Constants.SessionCompanyFullConfig] as WEBCONFIGModel;
                if (config == null)
                {
                    config = _companyProvider.GetConfig(this.Config.ID);
                    Session[SettingsManager.Constants.SessionCompanyFullConfig] = config;
                }
                return config;
            }
        }

        public CompanyInfoModel Company
        {
            get
            {

                var company = Session[SettingsManager.Constants.SessionCompanyInfo + Config.Language] as CompanyInfoModel;
                if (company == null)
                {
                    company = _companyProvider.GetCompany(Config.ID, Config.Language);
                    if(company != null)
                    {
                        company.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathCompanyImage + company.IMAGE;
                        company.PathWebIcon = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathCompanyImage + company.WebIcon;
                        if(!string.IsNullOrEmpty(company.Background) && !company.Background.StartsWith("#"))
                            company.Background = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathCompanyImage + company.Background;
                    }
                    Session[SettingsManager.Constants.SessionCompanyInfo + Config.Language] = company;
                }
                return company;
            }
        }
        #endregion
    }
}
