namespace Web.Api.Odata
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;

    public class ThirdPartyController : OdataBaseController<ThirdPartyModel, int>
    {
        private CompanyBLL bll;
        public ThirdPartyController()
        {
            this.bll = new CompanyBLL();
        }
        [EnableQuery]
        public override IQueryable<ThirdPartyModel> Get()
        {
            var param = this.GetParameter();
            var companyId = this.Web.ID;
            if (param.ContainsKey("CompanyId")) companyId = Convert.ToInt32(param["CompanyId"]);

             var data = this.bll.GetThirdPartyByWebConfigId(companyId);
            return data;
        }
    }
}
