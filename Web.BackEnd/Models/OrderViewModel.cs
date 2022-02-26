namespace Web.Backend.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class OrderViewModel
    {
        public string Action { get; set; }
        public int OrderId { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public int TotalNew { get; set; }
        public decimal TotalNewDue { get; set; }
        public int TotalConfirm { get; set; }
        public decimal TotalConfirmDue { get; set; }
        public int TotalSend { get; set; }
        public decimal TotalSendDue { get; set; }
        public int TotalReturn { get; set; }
        public decimal TotalReturnDue { get; set; }
        public int TotalRecieved { get; set; }
        public decimal TotalRecievedDue { get; set; }
            
        public IList<OrderModel> Orders { get; set; }
        public IList<OrderModel> NewOrders { get; set; }
        public IList<OrderModel> ConfirmOrders { get; set; }
        public IList<OrderModel> SendOrders { get; set; }
        public IList<OrderModel> RecievedOrders { get; set; }
        public IList<OrderModel> ReturnOrders { get; set; }

        public IList<OrderProductModel> Products { get; set; }

        public OrderViewModel()
        {
            Orders = new List<OrderModel>();
            Products = new List<OrderProductModel>();
        }
    }
}
