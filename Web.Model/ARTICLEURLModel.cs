
namespace Web.Model
{
    public partial class ARTICLEURLModel
    {
		public int ID { get; set; }
        public int CATEGORYID { get; set; }
        public string CategoryName { get; set; }
        public string LANGUAGEID { get; set; }
        public string IMAGE { get; set; }
        public string PathImage { get; set; }
        public string TITLE { get; set; }
		public string BRIEF { get; set; }
		public string CONTENT { get; set; }
        
        public int ORDERS { get; set; }
        public bool PUBLISH { get; set; }
        public string URL { get; set; }
        public string TARGET { get; set; }

        public string Languages { get; set; }
    }
}  
