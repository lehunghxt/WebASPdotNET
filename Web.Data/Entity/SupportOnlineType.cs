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
    
    public partial class SupportOnlineType
    {
        public SupportOnlineType()
        {
            this.SupportOnlines = new HashSet<SupportOnline>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string CreateByUser { get; set; }
        public string ModifyByUser { get; set; }
    
        public virtual ICollection<SupportOnline> SupportOnlines { get; set; }
    }
}
