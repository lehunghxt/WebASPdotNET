
namespace Web.Model
{
	using System;
	using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class SupplierModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}  
