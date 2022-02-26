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
    
    public partial class WarehouseIO
    {
        public WarehouseIO()
        {
            this.WarehouseIOColections = new HashSet<WarehouseIOColection>();
        }
    
        public int Id { get; set; }
        public string Code { get; set; }
        public int SupplierId { get; set; }
        public int CompanyId { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Type { get; set; }
        public System.DateTime Date { get; set; }
        public string Description { get; set; }
        public int UpdateBy { get; set; }
        public System.DateTime LastUpdate { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<WarehouseIOColection> WarehouseIOColections { get; set; }
    }
}