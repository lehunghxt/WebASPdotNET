using System;
namespace Web.Model
{
    public class OrderTransactionModel
    {
        public string Trans_Ref { get; set; }
        public string Trans_Status { get; set; }
        public string ResponseCode { get; set; }
        public System.DateTime RequestTime { get; set; }
        public System.DateTime ResponseTime { get; set; }
        public string ResponseMessage { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string OrderType { get; set; }
        public string OrderInfo { get; set; }
    }
}