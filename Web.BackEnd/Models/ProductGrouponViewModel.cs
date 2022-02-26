namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class ProductGrouponViewModel
    {
        public int CompanyId { get; set; }
        public int CatId { get; set; }
        public GrouponModel Groupon { get; set; }
        public ProductModel Product { get; set; }
        public IList<SelectListItem> Languages { get; set; }
        public IList<SelectListItem> Categories { get; set; }
        public IList<ProductPriceModel> Prices { get; set; }
        public IList<ProductColorModel> Colors { get; set; }
        public IList<ProductAttributeModel> Attributes { get; set; }
        public IList<ItemImageModel> Images { get; set; }
        public IList<ProductModel> Products { get; set; }
        public IList<DataSimpleModel> RelatiedProducts { get; set; }
        public IList<ProductAddOnModel> AddOns { get; set; }
        public IList<string> Tags { get; set; }

        public ProductGrouponViewModel()
        {
            Product = new ProductModel();
            Groupon = new GrouponModel();
            Languages = new List<SelectListItem>();
            Categories = new List<SelectListItem>();
            Prices = new List<ProductPriceModel>();
            Colors = new List<ProductColorModel>();
            Attributes = new List<ProductAttributeModel>();
            Images = new List<ItemImageModel>();
            Products = new List<ProductModel>();
            AddOns = new List<ProductAddOnModel>();
            RelatiedProducts = new List<DataSimpleModel>();
            Tags = new List<string>();
        }
    }
}
