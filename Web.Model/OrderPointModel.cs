 
 

namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class OrderPointModel
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public decimal Due { get; set; }
        public decimal Subtraction { get; set; }
        public decimal Addition { get; set; }

        public DateTime Date { get; set; }
    }
}  
