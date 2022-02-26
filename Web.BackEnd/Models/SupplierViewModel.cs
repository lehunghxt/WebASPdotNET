namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using Web.Model;

    public class SupplierViewModel
    {
        public string Action { get; set; }
        public SupplierModel Supplier { get; set; } // dùng để tạo thêm
        public IList<SupplierModel> Suppliers { get; set; }

        public SupplierViewModel()
        {
            Supplier = new SupplierModel();
            Suppliers = new List<SupplierModel>();
        }
    }
}
