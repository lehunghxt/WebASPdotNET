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
    
    public partial class TB_ThongKe
    {
        public int MaTB { get; set; }
        public System.DateTime ThoiGian { get; set; }
        public long SoTruyCap { get; set; }
        public Nullable<int> CompanyId { get; set; }
    
        public virtual Company Company { get; set; }
    }
}
