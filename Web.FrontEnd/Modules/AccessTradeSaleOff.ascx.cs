namespace Web.FrontEnd.Modules
{
    using Asp.UI;
    using Business;
    using Library.Web;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class AccessTradeSaleOff : VITModule
    {
        private CompanyBLL companyBLL;

        protected string Coupon
        {
            get
            {
                return this.GetValueParam<string>("Coupon");
            }
        }  

        protected List<AccessTradeSaleOffModel> Data;

        protected void Page_Load(object sender, EventArgs e)
        {
            companyBLL = new CompanyBLL();
            var at = companyBLL.GetConfigAccessTrade(Config.ID);

            var url = "https://api.accesstrade.vn/v1/offers_informations";
            var param = new Dictionary<string, object>();
            param["status"] = 1;
            if (!string.IsNullOrEmpty(Coupon)) param["coupon"] =  this.Coupon;

            try
            {
                var api = new ApiHelper(at.AccessKey);
                var temp = api.GetAll<AccessTradeSaleOffModel>(url, param);
                if (temp != null)
                { 
                    this.Data = temp.ToList();
                }
            }
            catch (Exception ex){
                this.Data = new List<AccessTradeSaleOffModel>();
            }
        }
    }
}