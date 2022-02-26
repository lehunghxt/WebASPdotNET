namespace Web.Backend.Models
{
    using System;
    using System.Collections.Generic;
    using Web.Model;

    public class CustomerDetailViewModel
    {
        public CustomerModel Customer { get; set; }
        public IList<OrderModel> Orders { get; set; }

        public IList<OrderProductModel> Products { get; set; }

        public IList<CustomerProductModel> CusromerProducts { get; set; }

        public CustomerDetailViewModel()
        {
            Customer = new CustomerModel();
            Orders = new List<OrderModel>();
            Products = new List<OrderProductModel>();
            CusromerProducts = new List<CustomerProductModel>();
        }
    }
}
