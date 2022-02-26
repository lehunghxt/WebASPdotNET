using System.Collections.Generic;

namespace Web.Model
{
    public class GHNCreateOrderModel
    {
        public string token { get; set; }
        public int FromDistrictID { get; set; }
        public int ToDistrictID { get; set; }
        public int Weight { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ServiceID { get; set; }
        public int ClientHubID { get; set; }
        public int CoDAmount { get; set; }
        public string NoteCode { get; set; }
        public string ShippingAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientContactPhone { get; set; }
        public string ClientContactName { get; set; }
        public int PaymentTypeID { get; set; }
        public ICollection<ShippingOrderCostDto> ShippingOrderCosts { get; set; }
    }

    public class ShippingOrderCostDto
    {
        public int ServiceID { get; set; }
        public int ServiceType { get; set; }
    }
}
