namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class ParamViewModel
    {        
        public ModuleParamModel Param { get; set; }
        public IList<SelectListItem> Types { get; set; }

        public ParamViewModel()
        {
            Param = new ModuleParamModel();
            Types = new List<SelectListItem>();
        }
    }
}
