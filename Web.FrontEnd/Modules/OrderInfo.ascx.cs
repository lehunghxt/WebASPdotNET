namespace Web.FrontEnd.Modules
{
    using log4net;
    using System;
    using Library.Web;
    using Library;
    using Web.Asp.UI;
    using Web.Model;
    using Web.Business;
    using Web.Asp.Provider;
    using System.Collections.Generic;
    using System.Linq;

    public partial class OrderInfo : VITModule
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof( OrderInfo));

        private ApiHelper api;

        private ProductBLL productBLL;
        protected OrderModel dto;
        protected List<OrderProductModel> Products;
        protected GHOrderInfo Data { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.api = new ApiHelper();

            this.productBLL = new ProductBLL();

            if (!Page.IsPostBack)
            {
                var orderId = this.GetValueRequest<int>(SettingsManager.Constants.SendOrder);
                if (orderId > 0)
                {
                    txtOrderId.Text = orderId.ToString();
                    dto = this.productBLL.GetOrder(orderId, this.Config.ID);
                    Products = productBLL.GetOrderProducts(new List<int> { orderId }, Config.Language).ToList();
                    if (dto != null)
                    {
                        Data = new GHOrderInfo();
                        if (!string.IsNullOrEmpty(dto.ShippingCode))
                        {
                            var delivery = new Delivery();
                            if (dto.ShippingId == 1) // if ghn
                            {                                
                                var ghnOrder = delivery.GetGHNOrder(Config.ID, dto.ShippingCode);
                                Data.DeliveryTime = Convert.ToDateTime(!string.IsNullOrEmpty(ghnOrder.EndDeliveryTime) ? ghnOrder.EndDeliveryTime : ghnOrder.ExpectedDeliveryTime);
                                Data.Message = ghnOrder.DeliverWarehouseName;
                                Data.Status = ghnOrder.CurrentStatus;
                                Data.ServiceCost = ghnOrder.TotalServiceCost;
                            }
                            else if (dto.ShippingId == 2)
                            {
                                var ghtkOrder = delivery.GetGHTKOrder(Config.ID, dto.ShippingCode);
                                Data.DeliveryTime = ghtkOrder.deliver_date;
                                Data.Message = ghtkOrder.message;
                                Data.Status = ghtkOrder.status_text;
                                Data.ServiceCost = ghtkOrder.ship_money;
                            }
                        }
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            HREF.RedirectComponent("OrderInfo", SettingsManager.Constants.SendOrder + "/" + txtOrderId.Text);
        }

        public class GHOrderInfo
        {
            public string Status { get; set; }
            public string Message { get; set; }
            public DateTime DeliveryTime { get; set; }
            public decimal ServiceCost { get; set; }
        }
    }
}