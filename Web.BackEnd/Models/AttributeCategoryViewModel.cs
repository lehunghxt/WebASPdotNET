namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class AttributeCategoryViewModel
    {
        public string Action { get; set; }
        public AttributeCategoryModel Category { get; set; }
        public IList<AttributeCategoryModel> Categories { get; set; }
        public AttributeValueModel Value { get; set; }
        public IList<AttributeValueModel> Values { get; set; }
        

        public AttributeCategoryViewModel()
        {
            Category = new AttributeCategoryModel();
            Categories = new List<AttributeCategoryModel>();
            Value = new AttributeValueModel();
            Values = new List<AttributeValueModel>();
        }
    }
}
