using System;

namespace Web.Model
{
    public class ATMCommitResponseModel
    {
        public decimal amount { get; set; }
        public string trans_status { get; set; }
        public DateTime response_time { get; set; }
        public string response_message { get; set; }
        public string response_code { get; set; }
        public string order_info { get; set; }
        public int order_id { get; set; }
        public string trans_ref { get; set; }
        public DateTime request_time { get; set; }
        public string order_type { get; set; }
    }
}