//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class TemplateModel
    {
        public TemplateModel()
        {
        }
    
        public string TemplateName { get; set; }
        public string ImageName { get; set; }
        public bool IsPublic { get; set; }
        public bool IsPublished { get; set; }

        public int Components { get; set; }
        public int Positions { get; set; }
        public int Skins { get; set; }
        public int Usings { get; set; }

        public string PathImage { get; set; }
    }
}