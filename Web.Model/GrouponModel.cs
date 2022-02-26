

namespace Web.Model
{
    public partial class GrouponModel
    {
		public int ID { get; set; }

        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}  
