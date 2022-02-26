
namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class ArticleModel
    {
        public int ID { get; set; }
        public int CategoryId { get; set; }
        public string TargetTag { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public bool HasComment { get; set; }
        public string LanguageId { get; set; }
    }
}  
