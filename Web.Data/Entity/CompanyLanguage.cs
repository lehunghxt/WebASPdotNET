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
    
    public partial class CompanyLanguage
    {
        public string LanguageId { get; set; }
        public int CompanyId { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public string Slogan { get; set; }
        public string Description { get; set; }
        public string AboutUs { get; set; }
        public string Address { get; set; }
        public string Certificate { get; set; }
    
        public virtual Company Company { get; set; }
    }
}
