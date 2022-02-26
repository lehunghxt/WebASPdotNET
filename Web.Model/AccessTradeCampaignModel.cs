using System;

namespace Web.Model
{
    public class AccessTradeCampaignModel
    {
        public string id { get; set; }
        public string approval { get; set; }
        public string category { get; set; }
        public string conversion_policy { get; set; }
        public string cookie_duration { get; set; }
        public string cookie_policy { get; set; }
        public string description { get; set; }
        public DateTime? end_time { get; set; }
        public string merchant { get; set; }
        public string name { get; set; }
        public string scope { get; set; }
        public DateTime start_time { get; set; }
        public int status { get; set; }
        public string sub_category { get; set; }
        public string type { get; set; }
        public string url { get; set; }
    }
}