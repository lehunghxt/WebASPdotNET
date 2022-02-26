namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class ProductViewModel
    {
        public string Action { get; set; }
        public int CatId { get; set; }
        public int ProductId { get; set; }
        public int ProductOrders { get; set; }
        public bool Publish { get; set; }
        public string Image { get; set; }
        public IList<ProductModel> Products { get; set; }
        public IList<ProductGrouponModel> Groupons { get; set; }

        public ProductViewModel()
        {
            Products = new List<ProductModel>();
            Groupons = new List<ProductGrouponModel>();
        }
    }
}
