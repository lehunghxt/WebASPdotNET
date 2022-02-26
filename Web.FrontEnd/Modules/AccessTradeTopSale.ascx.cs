namespace Web.FrontEnd.Modules
{
    using Asp.UI;
    using Business;
    using Library.Web;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class AccessTradeTopSale : VITModule
    {
        private CompanyBLL companyBLL; 

        protected List<AccessTradeTopSaleModel> Data;

        protected void Page_Load(object sender, EventArgs e)
        {
            companyBLL = new CompanyBLL();
            var at = companyBLL.GetConfigAccessTrade(Config.ID); 

            var url = "https://api.accesstrade.vn/v1/top_products";
            var param = new Dictionary<string, object>();
            var api = new ApiHelper(at.AccessKey);
            this.Data = api.GetAll<AccessTradeTopSaleModel>(url, param).ToList();
            this.Data = this.Data.Where(o => !string.IsNullOrEmpty(o.image) && !string.IsNullOrEmpty(o.name)).ToList();
        }
    }
}