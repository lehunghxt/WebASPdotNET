namespace Web.Api.Odata
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;

    public class ModuleController : OdataBaseController<ModuleConfigModel, int>
    {
        private ModuleBLL bll;
        public ModuleController()
        {
            this.bll = new ModuleBLL();
        }
        [EnableQuery]
        public override IQueryable<ModuleConfigModel> Get()
        {
            var param = this.GetParameter();
            var companyId = this.Web.ID;
            if (param.ContainsKey("CompanyId")) companyId = Convert.ToInt32(param["CompanyId"]);
            var data = this.bll.GetAllModuleConfigs(companyId, this.Web.Language).Where(e => e.Publish).OrderBy(e => e.Orders);
            return data;
        }

        protected override ModuleConfigModel GetEntityByKey(int id)
        {
            var param = this.GetParameter();
            var data = this.bll.GetModuleConfig(this.Web.ID, this.Web.Language, id);
            return data;
        }
    }
}
