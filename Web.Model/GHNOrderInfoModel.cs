using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Model
{
    public class GHNOrderInfoModel
    {
        public string CODTransferDate { get; set; }
        public int CSLostPackageID { get; set; }
        public bool CheckMainBankAccount { get; set; }
        public int ClientHubID { get; set; }
        public decimal CoDAmount { get; set; }
        public string Content { get; set; }
        public string CouponCode { get; set; }
        public string CurrentStatus { get; set; }
        public string CurrentWarehouseName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string DeliverWarehouseName { get; set; }
        public string EndDeliveryTime { get; set; }
        public string EndPickTime { get; set; }
        public string EndReturnTime { get; set; }
        public string ErrorMessage { get; set; }
        public string ExpectedDeliveryTime { get; set; }
        public string ExternalCode { get; set; }
        public string ExternalReturnCode { get; set; }
        public string ExtraFees { get; set; }
        public string FirstDeliveredTime { get; set; }
        public int FromDistrictID { get; set; }
        public int FromLat { get; set; }
        public int FromLng { get; set; }
        public string FromWardCode { get; set; }
        public int Height { get; set; }
        public int InsuranceFee { get; set; }
        public int Length { get; set; }
        public string Note { get; set; }
        public string NoteCode { get; set; }
        public int NumDeliver { get; set; }
        public int NumPick { get; set; }
        public string OrderCode { get; set; }
        public string OriginServiceName { get; set; }
        public int PaymentTypeID { get; set; }
        public string PickWarehouseName { get; set; }
        public string ReturnInfo { get; set; }
        public string SealCode { get; set; }
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ShippingAddress { get; set; }
        public List<ShippingOrderCost> ShippingOrderCosts { get; set; }
        public int ShippingOrderID { get; set; }
        public string StartDeliveryTime { get; set; }
        public string StartPickTime { get; set; }
        public string StartReturnTime { get; set; }
        public int ToDistrictID { get; set; }
        public int ToLatitude { get; set; }
        public int ToLongitude { get; set; }
        public string ToWardCode { get; set; }
        public int TotalServiceCost { get; set; }
        public int Weight { get; set; }
        public int Width { get; set; }
    }

    public class ShippingOrderCost
    {
        public decimal Cost { get; set; }
        public int PaymentChannelID { get; set; }
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public int ServiceType { get; set; }
        public int Number { get; set; }
    }
}
