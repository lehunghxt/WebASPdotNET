
namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class ItemImageModel
    {
		public int ID { get; set; }
		public int ItemId { get; set; }
		public string Image { get; set; }

        public string PathImage { get; set; }
    }
}  
