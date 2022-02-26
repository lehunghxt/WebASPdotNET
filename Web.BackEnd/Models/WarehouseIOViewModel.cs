namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class WarehouseIOViewModel
    {
        public int IOId { get; set; }
        public IList<WarehouseModel> Warehouses { get; set; }
        public IList<OrderProductModel> Products { get; set; }

        public WarehouseIOViewModel()
        {
            Warehouses = new List<WarehouseModel>();
            Products = new List<OrderProductModel>();
        }
    }
}
