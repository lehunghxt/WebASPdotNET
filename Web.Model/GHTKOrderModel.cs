using System;

namespace Web.Model
{
    public class GHTKOrderModel
    {
        public decimal insurance_fee { get; set; }
        public string partner_id { get; set; }
        public decimal fee { get; set; }
        public string label { get; set; }
        public string estimated_pick_time { get; set; }
        public string estimated_deliver_time { get; set; }
    }
}
