using System;

namespace Web.Model
{
    public class AccessTradeDataFeedModel
    {
        public string cate { get; set; }
        public string desc { get; set; }
        public decimal discount { get; set; }
        public string campaign { get; set; }
        public string domain { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string product_id { get; set; }
        public string sku { get; set; }
        public string status_discount { get; set; }
        public decimal discount_amount { get; set; }
        public double discount_rate { get; set; }
        public string update_time { get; set; }
        public string url { get; set; }
        public string aff_link { get; set; }
        public category category_commission { get; set; }
        public class category 
        {
            public string partner_reward { get; set; }
            public string reward_type { get; set; } 
        }
    }
}