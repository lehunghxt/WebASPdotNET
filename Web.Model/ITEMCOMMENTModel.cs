
namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class ITEMCOMMENTModel
    {
		public int ID { get; set; }
		public int ITEMID { get; set; }
        public string ItemName { get; set; }
        public string CONTENT { get; set; }
		public string CLIENTID { get; set; }
		public string PHONE { get; set; }
		public string EMAIL { get; set; }
        public string NAME { get; set; }

        public DateTime Date { get; set; }
        public int UserId { get; set; }

        public int Like { get; set; }
        public int DisLike { get; set; }
    }
}  
