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
    
    public partial class Menu
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string ComponentName { get; set; }
        public string ViewName { get; set; }
        public string Params { get; set; }
        public string Description { get; set; }
        public Nullable<int> Orders { get; set; }
        public Nullable<bool> Publish { get; set; }
    }
}
