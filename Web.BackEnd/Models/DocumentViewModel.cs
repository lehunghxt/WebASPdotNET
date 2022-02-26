namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class DocumentViewModel
    {
        public string Action { get; set; }
        public int CatId { get; set; }
        public int DocumentId { get; set; }
        public bool Publish { get; set; }
        public string Image { get; set; }
        public string FileName { get; set; }

        public IList<SelectListItem> Categories { get; set; }
        public IList<DocumentModel> Documents { get; set; }

        public DocumentViewModel()
        {
            Categories = new List<SelectListItem>();
            Documents = new List<DocumentModel>();
        }
    }
}
