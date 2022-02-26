
namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class ArticleBilingualModel
    {
        public int ID { get; set; }
        public int CategoryId { get; set; }
        public string TargetTag { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreateDate { get; set; }
        public bool HasComment { get; set; }
        public int Views { get; set; }
        public string Title1 { get; set; }
        public string Description1 { get; set; }
        public string Content1 { get; set; }
        public string Title2 { get; set; }
        public string Description2 { get; set; }
        public string Content2 { get; set; }
        public string Link { get; set; }
    }
}  
