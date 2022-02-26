
namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class COMPANYLANGUAGEModel 
    {
		public int ID { get; set; }
        public string EMAIL { get; set; }
        public string PHONE { get; set; }
        public string FAX { get; set; }
        public string IMAGE { get; set; }
        public string PathImage { get; set; }
        public string WARDID { get; set; }
        public string LANGUAGEID { get; set; }
        public string FULLNAME { get; set; }
		public string DISPLAYNAME { get; set; }
		public string DESCRIPTION { get; set; }
		public string ABOUTUS { get; set; }
		public string ADDRESS { get; set; }
		public string SLOGAN { get; set; }
        public string Certificate { get; set; }

        public bool ISDEFAULT { get; set; }

    }
}  
