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
    
    public partial class ModuleParam
    {
        public string ParamName { get; set; }
        public string ModuleName { get; set; }
        public string Summary { get; set; }
        public string DefaultValue { get; set; }
        public string Type { get; set; }
        public bool UIProperty { get; set; }
    
        public virtual Module Module { get; set; }
    }
}