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
    
    public partial class CompanyAddress
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Address { get; set; }
        public string WardId { get; set; }
        public string DistrictId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    
        public virtual Company Company { get; set; }
    }
}
