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
    
    public partial class ItemView
    {
        public int ItemId { get; set; }
        public int Views { get; set; }
    
        public virtual Item Item { get; set; }
    }
}
