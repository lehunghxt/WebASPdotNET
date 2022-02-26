namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class ArticleDetailViewModel
    {
        public int CatId { get; set; }
        public ARTICLELANGUAGEModel Article { get; set; }
        public IList<SelectListItem> Categories { get; set; }
        public IList<SelectListItem> Languages { get; set; }

        public IList<DataSimpleModel> RelatiedArticles { get; set; }
        public IList<DataSimpleModel> Articles { get; set; }
        public IList<string> Tags { get; set; }

        public ArticleDetailViewModel()
        {
            Categories = new List<SelectListItem>();
            Languages = new List<SelectListItem>();
            Articles = new List<DataSimpleModel>();
            RelatiedArticles = new List<DataSimpleModel>();
            Tags = new List<string>();
        }
    }
}
