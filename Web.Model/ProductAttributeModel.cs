
namespace Web.Model
{
    public partial class ProductAttributeModel
    {
		public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int? CategoryId { get; set; }

        public string Value { get; set; }
        public string ValueName { get; set; }
    }
}  
