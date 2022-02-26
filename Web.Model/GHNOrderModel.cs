using System;

namespace Web.Model
{
    public class GHNOrderModel
    {
        public int OrderID { get; set; }
        public int PaymentTypeID { get; set; }
        public string OrderCode { get; set; }
        public int CurrentStatus { get; set; }
        public int ExtraFee { get; set; }
        public decimal TotalServiceFee { get; set; }
        public DateTime ExpectedDeliveryTime { get; set; }
        public int ClientHubID { get; set; }
        public string SortCode { get; set; }
    }
}
