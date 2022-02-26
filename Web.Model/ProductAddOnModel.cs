
namespace Web.Model
{
    public partial class ProductAddOnModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public decimal Sale { get; set; }
        public int Quantity { get; set; }
    }
}  
