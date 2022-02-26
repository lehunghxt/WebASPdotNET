namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using Web.Model;

    public class ModuleConfigViewModel
    {
        public string Action { get; set; }
        public int ModuleId { get; set; }
        public int ModuleOrder { get; set; }
        public IList<ModuleConfigModel> Modules { get; set; }
        public IList<ModuleConfigParamModel> Params { get; set; }

        public ModuleConfigViewModel()
        {
            Modules = new List<ModuleConfigModel>();
            Params = new List<ModuleConfigParamModel>();
        }
    }
}
