namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class MediaViewModel
    {
        public string Action { get; set; }
        public int CatId { get; set; }
        public MediaModel Media { get; set; } 
        public IList<SelectListItem> Categories { get; set; }
        public IList<SelectListItem> Languages { get; set; }

        public IList<MediaModel> Medias { get; set; }

        public MediaViewModel()
        {
            Media = new MediaModel();
            Categories = new List<SelectListItem>();
            Languages = new List<SelectListItem>();
            Medias = new List<MediaModel>();
        }
    }
}
