namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class ModuleConfigDetailViewModel
    {
        public string Action { get; set; }
        public int CompanyId { get; set; }

        public WEBCONFIGModel Config { get; set; }
        public ModuleConfigModel Module { get; set; }
        public IList<SelectListItem> ModuleTypes { get; set; }
        public IList<SelectListItem> Poitions { get; set; }
        public IList<SelectListItem> Components { get; set; }
        public IList<SelectListItem> Modules { get; set; }
        public IList<SelectListItem> Skins { get; set; }
        public IList<SelectListItem> Languages { get; set; }
        public IList<ModuleConfigParamModel> Params { get; set; }

        public ModuleConfigDetailViewModel()
        {
            Module = new ModuleConfigModel();
            ModuleTypes = new List<SelectListItem>();
            Components = new List<SelectListItem>();
            Poitions = new List<SelectListItem>();
            Modules = new List<SelectListItem>();
            Skins = new List<SelectListItem>();
            Params = new List<ModuleConfigParamModel>();
            Languages = new List<SelectListItem>();
        }
    }
}
