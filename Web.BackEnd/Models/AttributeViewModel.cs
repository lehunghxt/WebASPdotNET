namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class AttributeViewModel
    {
        public string Action { get; set; }
        public AttributeModel Attribute { get; set; }
        public IList<AttributeModel> Attributes { get; set; }
        public IList<SelectListItem> AttributeCategories { get; set; }

        public AttributeViewModel()
        {
            Attribute = new AttributeModel();
            Attributes = new List<AttributeModel>();
            AttributeCategories = new List<SelectListItem>();
        }
    }
}
