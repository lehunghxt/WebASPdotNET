namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using Web.Model;

    public class CategoryViewModel
    {
        public int CatId { get; set; }
        public string TypeName { get; set; }
        public string Action { get; set; }
        public CATEGORYLANGUAGEModel Category { get; set; } // dùng để tạo thêm
        public IList<CATEGORYLANGUAGEModel> Categories { get; set; }
        public IList<CATEGORYLANGUAGEModel> Parents { get; set; }
        public IList<LANGUAGEModel> Languages { get; set; }

        public CategoryViewModel()
        {
            Category = new CATEGORYLANGUAGEModel();
            Categories = new List<CATEGORYLANGUAGEModel>();
            Parents = new List<CATEGORYLANGUAGEModel>();
            Languages = new List<LANGUAGEModel>();
        }
    }
}
