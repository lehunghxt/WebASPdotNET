 
 

namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class CustomerProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int CountProducts { get; set; }
    }
}  
