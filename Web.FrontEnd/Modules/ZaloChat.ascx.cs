using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Asp.Provider;
using Web.Asp.Provider.Cache;
using Web.Asp.UI;
using Web.Business;
using Web.Model;

namespace Web.FrontEnd.Modules
{
    public partial class ZaloChat : VITModule
    {
        protected string OfficialAccountId
        {
            get
            {
                    var company = Session[SettingsManager.Constants.SessionCompanyInfo + Config.Language] as CompanyInfoModel;
                    if (company == null)
                    {
                        company = (new CompanyBLL()).GetCompany(Config.ID, Config.Language);
                        Session[SettingsManager.Constants.SessionCompanyInfo + Config.Language] = company;
                    } 

                    if (company != null) return company.GooglePlus;

                    return string.Empty;
            }
        }

        protected int Width
        {
            get
            {
                return this.GetValueParam<int>("Width");
            }
        }

        protected int Height
        {
            get
            {
                return this.GetValueParam<int>("Height");
            }
        }

        protected bool AutoPopup
        {
            get
            {
                return this.GetValueParam<bool>("AutoPopup");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}