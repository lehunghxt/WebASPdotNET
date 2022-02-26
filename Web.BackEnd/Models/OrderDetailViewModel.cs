namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class OrderDetailViewModel
    {
        public string Action { get; set; }
        public OrderModel Order { get; set; }
        public IList<OrderProductModel> OrderProducts { get; set; }

        public IList<ProductModel> Products { get; set; }

        public GHPostOrderModel Delivery { get; set; }
        public List<DistrictModel> Districts { get; set; }

        public COMPANYLANGUAGEModel Company { get; set; }

        public decimal VoucherFee { get; set; }
        public decimal PointFee { get; set; }
        public decimal TotalFee { get; set; }
        public bool UseCustomerDeliverPay { get; set; }

        public OrderDetailViewModel()
        {
            Order = new OrderModel();
            Delivery = new GHPostOrderModel();
            Districts = new List<DistrictModel>();
            Products = new List<ProductModel>();
            OrderProducts = new List<OrderProductModel>();
        }
    }
}
