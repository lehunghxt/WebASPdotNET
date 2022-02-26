 
 

namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class CustomerOrderModel
    {
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public decimal TotalDue { get; set; }
        public int CountProducts { get; set; }
        public Nullable<System.DateTime> LastBuyDate { get; set; }
    }
}  
