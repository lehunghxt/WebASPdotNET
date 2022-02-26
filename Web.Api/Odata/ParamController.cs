namespace Web.Api.Odata
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;

    public class ParamController : OdataBaseController<ModuleConfigParamModel, int>
    {
        private ModuleBLL bll;
        public ParamController()
        {
            this.bll = new ModuleBLL();
        }
        [EnableQuery]
        public override IQueryable<ModuleConfigParamModel> Get()
        {
            var param = this.GetParameter();
            var companyId = this.Web.ID;
            if (param.ContainsKey("CompanyId")) companyId = Convert.ToInt32(param["CompanyId"]);

            var moduleId = 0;
            if (param.ContainsKey("ModuleId"))
                moduleId = Convert.ToInt32(param["ModuleId"]);
            var listModuleId = new List<int>();
            if (moduleId > 0) listModuleId.Add(moduleId);
             var data = this.bll.GetParamConfigs(companyId, listModuleId);
            return data;
        }
    }
}
