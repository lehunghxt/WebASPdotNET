
namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class ITEMModel
    {
		public int ID { get; set; }
		public int COMPANYID { get; set; }
		public int ORDERS { get; set; }
		public bool ISPUBLISH { get; set; }
		public DateTime MODIFYDATE { get; set; }
		public int MODIFYBYUSER { get; set; }
		
    }
}  
