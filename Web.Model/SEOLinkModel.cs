
namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class SEOLinkModel
    {
        public string SeoUrl { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string MetaKeyWork { get; set; }
        public string MetaDescription { get; set; }
        public int? RefItem { get; set; }
        public string LanguageId { get; set; }
    }
}  
