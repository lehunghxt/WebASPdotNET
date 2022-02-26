namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class AlbumViewModel
    {
        public int CatId { get; set; }
        public CATEGORYLANGUAGEModel Category { get; set; } // dùng để tạo thêm
        public IList<SelectListItem> Categories { get; set; }
        public IList<SelectListItem> Languages { get; set; }

        public AlbumViewModel()
        {
            Category = new CATEGORYLANGUAGEModel();
            Categories = new List<SelectListItem>();
            Languages = new List<SelectListItem>();
        }
    }
}
