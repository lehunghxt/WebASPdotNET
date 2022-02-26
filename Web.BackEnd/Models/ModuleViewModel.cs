namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using Web.Model;

    public class ModuleViewModel
    {
        public string Action { get; set; }
        
        public ModuleModel Module { get; set; }
        public ModuleParamModel Param { get; set; }
        public IList<ModuleModel> Modules { get; set; }
        public IList<ModuleParamModel> Params { get; set; }

        public ModuleViewModel()
        {
            Param = new ModuleParamModel();
            Module = new ModuleModel();
            Modules = new List<ModuleModel>();
            Params = new List<ModuleParamModel>();
        }
    }
}
