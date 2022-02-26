namespace Web.Model
{
    public class VisaChargingModel
    {
        public string access_key { get; set; }
        public int order_id { get; set; }
        public string order_info { get; set; }
        public decimal amount { get; set; }
        public string return_url { get; set; }
        public string signature { get; set; }
        public string cus_fname { get; set; }
        public string cus_email { get; set; }
        public string cus_phone { get; set; }
    }
}