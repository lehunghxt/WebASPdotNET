using System;
using System.Collections.Generic;

namespace Web.Model
{
    public class GHNFeeModel
    {
        public DateTime ExpectedDeliveryTime { get; set; }
        public string Name { get; set; }
        public decimal ServiceFee { get; set; }
        public int ServiceID { get; set; }
        public ICollection<ServiceExtrasModel> Extras { get; set; }
    }
    public class ServiceExtrasModel
    {
        public int ServiceID { get; set; }
        public string Name { get; set; }
        public decimal ServiceFee { get; set; }
        public int MaxValue { get; set; }
    }
}