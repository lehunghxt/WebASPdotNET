namespace Web.Model
{
    using System;
    using System.Collections.Generic;

    public class AccessTradeSaleOffModel 
    {
        public string id { get; set; }
        public string content { get; set; }
        public string image { get; set; }
        public string link { get; set; }
        public string merchant { get; set; }
        public string name { get; set; }
        public string aff_link { get; set; }
        public string domain { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public BannerDto[] banners { get; set; }
        public CategoryDto[] categories { get; set; }
        public class BannerDto
        {
            public string link { get; set; }
            public string width { get; set; }
            public string height { get; set; }
        }
        public class CategoryDto
        {
            public int category_no { get; set; }
            public string category_name { get; set; }
            public string category_name_show { get; set; }
        }
    }
}