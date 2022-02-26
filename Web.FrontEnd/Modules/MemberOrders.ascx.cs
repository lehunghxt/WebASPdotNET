namespace Web.FrontEnd.Modules
{
    using System;
    using Asp.UI;
    using Business;
    using Web.Model;
    using System.Collections.Generic;
    using System.Linq;

    public partial class MemberOrders : VITModule
    {
        private ProductBLL productBLL;
        protected List<OrderModel> Data;

        protected void Page_Load(object sender, EventArgs e)
        {
            productBLL = new ProductBLL();
            Data = new List<OrderModel>();
            if (this.UserContext != null)
            {
                Data = productBLL.GetOrders(this.Config.ID, customerId: this.UserContext.UserId).ToList();
            }
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            productBLL.Return(this.Config.ID, 0, this.GetValueRequest<int>("OrderCancleId"), this.GetValueRequest<string>("CustomerNote"));
            if (this.UserContext != null)
            {
                Data = productBLL.GetOrders(this.Config.ID, customerId: this.UserContext.UserId).ToList();
            }
        }
    }   
}