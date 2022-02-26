namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class WarehouseIODetailViewModel
    {
        public WarehouseModel Warehouse { get; set; }
        public IList<OrderProductModel> WarehouseProducts { get; set; }

        public IList<DataSimpleModel> Products { get; set; }
        public IList<SelectListItem> Suppliers { get; set; }

        public WarehouseIODetailViewModel()
        {
            Warehouse = new WarehouseModel();
            Products = new List<DataSimpleModel>();
            WarehouseProducts = new List<OrderProductModel>();
            Suppliers = new List<SelectListItem>();
        }
    }
}
