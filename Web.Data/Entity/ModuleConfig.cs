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
    
    public partial class ModuleConfig
    {
        public ModuleConfig()
        {
            this.ModuleConfigLanguages = new HashSet<ModuleConfigLanguage>();
            this.ModuleConfigParams = new HashSet<ModuleConfigParam>();
        }
    
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string TemplateName { get; set; }
        public string ComponentName { get; set; }
        public string SkinName { get; set; }
        public string Position { get; set; }
        public bool InTemplate { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual Module Module { get; set; }
        public virtual ICollection<ModuleConfigLanguage> ModuleConfigLanguages { get; set; }
        public virtual Template Template { get; set; }
        public virtual ICollection<ModuleConfigParam> ModuleConfigParams { get; set; }
    }
}
