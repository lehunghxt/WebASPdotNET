namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class ArticleURLViewModel
    {
        public string Action { get; set; }
        public int CatId { get; set; }
        public ARTICLEURLModel Link { get; set; } // dùng để tạo thêm
        public IList<SelectListItem> Categories { get; set; }
        public IList<SelectListItem> Languages { get; set; }
        public IList<SelectListItem> Targets { get; set; }
        public IList<ARTICLEURLModel> Links { get; set; }

        public ArticleURLViewModel()
        {
            Link = new ARTICLEURLModel();
            Categories = new List<SelectListItem>();
            Languages = new List<SelectListItem>();
            Targets = new List<SelectListItem>();
            Links = new List<ARTICLEURLModel>();
        }
    }
}
