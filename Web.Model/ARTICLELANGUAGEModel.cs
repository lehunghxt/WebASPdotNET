
namespace Web.Model
{
	using System;
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ARTICLELANGUAGEModel
    {
		public int ID { get; set; }
        public int CATEGORYID { get; set; }
        public string CategoryName { get; set; }
        public string IMAGE { get; set; }
        public string TAG { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DISPLAYDATE { get; set; }
        public bool HASCOMMENT { get; set; }
        public string LANGUAGEID { get; set; }
        public string LANGUAGENAME { get; set; }
        public string TITLE { get; set; }
		public string BRIEF { get; set; }
		public string CONTENT { get; set; }

        public string PathImage { get; set; }

        public int ORDERS { get; set; }
        public bool PUBLISH { get; set; }
        public string Languages { get; set; }
        
        public int Views { get; set; }
        public int Comments { get; set; }
    }
}  
