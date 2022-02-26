
namespace Web.Model
{
	using System;
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class RssModel
    {
		public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}  
