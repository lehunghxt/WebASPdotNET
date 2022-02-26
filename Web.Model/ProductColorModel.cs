
namespace Web.Model
{
    public partial class ProductColorModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public decimal? Price { get; set; }
    }
}  
