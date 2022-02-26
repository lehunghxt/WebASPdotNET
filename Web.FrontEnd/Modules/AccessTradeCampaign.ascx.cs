namespace Web.FrontEnd.Modules
{
    using Library.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Web.Asp.UI;
    using Web.Business;
    using Web.Model;

    public partial class AccessTradeCampaign : VITModule
    {
        private CompanyBLL companyBLL;

        protected List<AccessTradeCampaignModel> Data;
        protected string DeepLink { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            companyBLL = new CompanyBLL();
            var at = companyBLL.GetConfigAccessTrade(Config.ID);
            var api = new ApiHelper(at.AccessKey);
            DeepLink = at.DeepLink;

            var url = "https://api.accesstrade.vn/v1/campaigns";
            var param = new Dictionary<string, object>();
            param["status"] = 1;
            param["approval"] = "successful";

            try
            {
                var temp = api.GetAll<AccessTradeCampaignModel>(url, param);
                if(temp != null)
                {
                    temp = temp.Where(o => o.status == 1 && o.approval == "successful" && (o.end_time == null || o.end_time >= DateTime.Now));
                    Data = temp.ToList();
                }
            }
            catch
            {
                Data = new List<AccessTradeCampaignModel>();
            }
        }
    }
}