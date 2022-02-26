namespace Web.Backend.Models
{
    using System;
    using System.Collections.Generic;
    using Web.Model;

    public class CustomerViewModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public IList<CustomerOrderModel> Customers { get; set; }

        public CustomerViewModel()
        {
            Customers = new List<CustomerOrderModel>();
        }
    }
}
