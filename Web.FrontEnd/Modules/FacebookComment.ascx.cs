using System;
using Web.Asp.Provider;
using Web.Asp.UI;
using Web.Business;
using Web.Model;

namespace Web.FrontEnd.Modules
{
    public partial class FacebookComment :  VITModule
    {  
        protected string YourUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(this.GetValueParam<string>("YourUrl"))) return this.GetValueParam<string>("YourUrl");
                else return HREF.BaseUrl + Request.RawUrl.Substring(1);
            }
        }
         
        protected int Width
        {
            get
            {
                return this.GetValueParam<int>("Width");
            }
        }

        protected int NumberPost
        {
            get 
            {
                return this.GetValueParam<int>("PostNumber");
            }
        }

        protected String FacebookAppId { get; set; }
 
        protected void Page_Load(object sender, EventArgs e)
        {
            var company = Session[SettingsManager.Constants.SessionCompanyInfo + Config.Language] as CompanyInfoModel;
            if (company == null)
            {
                company = (new CompanyBLL()).GetCompany(Config.ID, Config.Language);
                if (company != null)
                {
                    company.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathCompanyImage + company.IMAGE;
                    company.PathWebIcon = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathCompanyImage + company.WebIcon;
                    company.Background = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathCompanyImage + company.Background;
                }
                Session[SettingsManager.Constants.SessionCompanyInfo + Config.Language] = company;
            }
            FacebookAppId = company.FacebookAppId;
        }
    }
}