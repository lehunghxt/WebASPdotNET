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
    
    public partial class TemplateConfig
    {
        public int Id { get; set; }
        public string LanguageId { get; set; }
        public int CompanyId { get; set; }
        public string DomainName { get; set; }
        public string TemplateName { get; set; }
        public string ComponentName { get; set; }
        public string ViewName { get; set; }
        public bool IsMobile { get; set; }
        public Nullable<int> FromDate { get; set; }
        public Nullable<int> ToDate { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreateByUser { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ModifyByUser { get; set; }
        public string Background { get; set; }
        public string BackgroundRepeat { get; set; }
        public string BackgroundPosition { get; set; }
    
        public virtual Company Company { get; set; }
    }
}
