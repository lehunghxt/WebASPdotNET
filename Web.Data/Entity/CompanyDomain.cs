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
    
    public partial class CompanyDomain
    {
        public int CompanyId { get; set; }
        public string Domain { get; set; }
        public string LanguageId { get; set; }
    
        public virtual Company Company { get; set; }
    }
}
