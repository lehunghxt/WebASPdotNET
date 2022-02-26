namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class ArticleViewModel
    {
        public string Action { get; set; }
        public int CatId { get; set; }
        public int ArticleId { get; set; }
        public bool Publish { get; set; }
        public string Image { get; set; }
        public IList<SelectListItem> Categories { get; set; }
        public IList<ARTICLELANGUAGEModel> Articles { get; set; }

        public ArticleViewModel()
        {
            Categories = new List<SelectListItem>();
            Articles = new List<ARTICLELANGUAGEModel>();
        }
    }
}
