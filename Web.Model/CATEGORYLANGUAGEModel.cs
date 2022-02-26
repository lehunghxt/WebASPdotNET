
namespace Web.Model
{
	using System;
	using System.Collections.Generic;

    public partial class CATEGORYLANGUAGEModel
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string LANGUAGEID { get; set; }
        public string LANGUAGENAME { get; set; }
		public string DESCRIPTION { get; set; }
        public string Content { get; set; }
        public int PARENTID { get; set; }
        public string PARENTNAME { get; set; }
        public string TYPEID { get; set; }
        public string IMAGE { get; set; }
        public string PathImage { get; set; }

        public int ORDERS { get; set; }
        public bool PUBLISH { get; set; }

        public string Blank { get; set; }
        public string Languages { get; set; }
    }
}  
