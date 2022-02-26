namespace Web.FrontEnd.Modules
{
    using Asp.UI;
    using Business;
    using Library.Web;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public partial class AccessTradeDataFeed : VITModule
    { 
        private CompanyBLL companyBLL;
        protected int PageNumber
        {
            get
            {
                return this.GetValueRequest<int>("pagenumber");
            }
        }

        protected int Limit
        {
            get
            {
                return this.GetValueParam<int>("Limit");
            }
        }

        protected bool Discount  
        {
            get
            {
                return this.GetValueParam<bool>("Discount");
            }
        } 

        protected List<AccessTradeDataFeedModel> Data;

        protected void Page_Load(object sender, EventArgs e)
        {
            companyBLL = new CompanyBLL();
            var at = companyBLL.GetConfigAccessTrade(Config.ID);
            var api = new ApiHelper(at.AccessKey);

            var merchants = new List<string>();
            try
            {
                var url = "https://api.accesstrade.vn/v1/offers_informations";
                var param = new Dictionary<string, object>();
                param["status"] = 1;
                var temp = api.GetAll<AccessTradeSaleOffModel>(url, param);
                if (temp != null)
                {
                    merchants = temp.Select(o => o.merchant).Where(o => !string.IsNullOrEmpty(o)).Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                merchants = new List<string>();
            }

            

            this.Data = new List<AccessTradeDataFeedModel>();
            if (merchants.Count == 0)
            {
                try
                {

                    var url = "https://api.accesstrade.vn/v1/datafeeds";
                    var param = new Dictionary<string, object>();
                    param["page"] = this.PageNumber;
                    param["limit"] = this.Limit;
                    param["status_discount"] = this.Discount ? 1 : 0;

                    var temp = api.GetAll<AccessTradeDataFeedModel>(url, param);
                    if (temp != null)
                    {
                        this.Data = temp.ToList();
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                var workerThreads = new List<Thread>();
                foreach (var merchant in merchants)
                {
                    var thread = new Thread(() =>
                    {
                        var url = "https://api.accesstrade.vn/v1/datafeeds";
                        var param = new Dictionary<string, object>();
                        param["page"] = this.PageNumber;
                        param["limit"] = this.Limit;
                        param["campaign"] = merchant;

                        var temp = api.GetAll<AccessTradeDataFeedModel>(url, param);
                        if (temp != null)
                        {
                            this.Data.AddRange(temp.ToList());
                        }
                    });
                    workerThreads.Add(thread);
                    thread.Start();
                }

                foreach (Thread thread in workerThreads)
                {
                    thread.Join();
                }
            }
        }
    }
}