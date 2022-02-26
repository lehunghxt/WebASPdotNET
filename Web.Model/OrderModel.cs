
namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class OrderModel
    {
        public int Id { get; set; }
        public int Status { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerNote { get; set; }
        public decimal CustomerPayDelivery { get; set; }
        public string Note { get; set; }
        public int TotalProduct { get; set; }
        public decimal TotalDue { get; set; }
        public decimal DeliveryFee { get; set; }
        public string ShippingCode { get; set; }
        public int ShippingId { get; set; }
        public bool IsPaid { get; set; }
        public string Voucher { get; set; }
        public decimal Point { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? PaidDate { get; set; }
    }
}  
