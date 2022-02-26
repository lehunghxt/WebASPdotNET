

namespace Web.Model
{
    public partial class ProductGrouponModel
    {
		public int ID { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string LanguageId { get; set; }
        public string Languages { get; set; }
        public string Title { get; set; }
        public string Brief { get; set; }
        public string Contents { get; set; }
        public int Orders { get; set; }
        public bool Publish { get; set; }
        public string Image { get; set; }
        public string PathImage { get; set; }
        public string Tag { get; set; }

        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int Quantity { get; set; }

        public decimal Price { get; set; }
        public decimal Sale { get; set; }
        public decimal CampainPrice { get; set; }
        public string Code { get; set; }
        public int SaleMin { get; set; }

        public int VoteRate { get; set; }
        public int VoteNumber { get; set; }
        public int Vote
        {
            get
            {
                return this.VoteNumber == 0 ? 1 : this.VoteRate / this.VoteNumber;
            }
        }
        
        public int ViewNumber { get; set; }
        public int PayNumber { get; set; }

        public string JsonPrices { get; set; }
        public string JsonColors { get; set; }
        public string JsonAttributes { get; set; }
    }
}  
