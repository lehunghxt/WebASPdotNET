namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class DocumentDetailViewModel
    {
        public int CatId { get; set; }
        public DocumentModel Document { get; set; }
        public IList<SelectListItem> Categories { get; set; }
        public IList<SelectListItem> Languages { get; set; }

        public DocumentDetailViewModel()
        {
            Categories = new List<SelectListItem>();
            Languages = new List<SelectListItem>();
        }
    }
}
