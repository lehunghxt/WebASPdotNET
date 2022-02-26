//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        public Customer()
        {
            this.CustomerAddresses = new HashSet<CustomerAddress>();
            this.CustomerPoints = new HashSet<CustomerPoint>();
            this.Items = new HashSet<Item>();
        }
    
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Nullable<decimal> Point { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Password { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
        public virtual ICollection<CustomerPoint> CustomerPoints { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}