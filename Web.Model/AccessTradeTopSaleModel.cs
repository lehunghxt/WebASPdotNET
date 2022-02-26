namespace Web.Model
{
    public class AccessTradeTopSaleModel
    {
        public string product_id { get; set; }
        public string name { get; set; }
        public string category_id { get; set; }
        public string category_name { get; set; }
        public string image { get; set; }
        public string desc { get; set; }
        public string link { get; set; }
        public string aff_link { get; set; }
        public decimal price { get; set; }
        public decimal discount { get; set; }
        public string brand { get; set; }
        public string short_desc { get; set; }
        public int product_category { get; set; } 
    }
}