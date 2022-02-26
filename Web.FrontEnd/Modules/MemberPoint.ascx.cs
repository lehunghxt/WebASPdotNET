namespace Web.FrontEnd.Modules
{
    using System;
    using Asp.UI;
    using Business;
    using Web.Model;
    using System.Collections.Generic;

    public partial class MemberPoint : VITModule
    {
        private CustomerBLL customerBLL;
        protected List<OrderPointModel> Data;

        protected void Page_Load(object sender, EventArgs e)
        {
            customerBLL = new CustomerBLL();
            Data = new List<OrderPointModel>();
            var userId = this.UserContext == null ? 0 : this.UserContext.UserId;

            var orderFinishes = customerBLL.GetPointByFinishOrders(userId);
            Data.AddRange(orderFinishes);

            var orderNews = customerBLL.GetPointByNewOrders(userId);
            Data.AddRange(orderNews);
        }
        
    }
}