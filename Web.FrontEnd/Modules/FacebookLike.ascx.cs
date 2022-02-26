using System;
using Web.Asp.Provider;
using Web.Asp.UI;
using Web.Business;
using Web.Model;

namespace Web.FrontEnd.Modules
{
    public partial class FacebookLike : VITModule
    {
        protected string YourUrl
        {
            get
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

                    if (company != null) return company.FacebookFanpage;

                    return HREF.BaseUrl + Request.RawUrl.Substring(1);
            }
        }

        protected int Width
        {
            get
            {
                return this.GetValueParam<int>("Width");
            }
        }

        protected string BorderColor
        {
            get
            {
                return this.GetValueParam<string>("BorderColor");
            }
        }

        protected bool ShowFaces
        {
            get
            {
                return this.GetValueParam<bool>("ShowFaces");
            }
        }

        protected bool Box
        {
            get
            {
                return this.GetValueParam<bool>("Box");
            }
        }

        protected bool Stream
        {
            get
            {
                return this.GetValueParam<bool>("Stream");
            }
        }

        protected bool Header
        {
            get
            {
                return this.GetValueParam<bool>("Header");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}