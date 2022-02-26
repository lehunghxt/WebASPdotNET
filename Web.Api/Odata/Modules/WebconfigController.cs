namespace Web.Api.Odata.Modules
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System.Collections.Generic;

    public class WebconfigController : OdataBaseController<WEBCONFIGModel, int>
    {
        private CompanyBLL bll;
        public WebconfigController()
        {
            this.bll = new CompanyBLL();
        }
        [EnableQuery]
        public override IQueryable<WEBCONFIGModel> Get()
        {
            var data = this.bll.GetConfig(this.Web.ID);
            return new List<WEBCONFIGModel>() { data }.AsQueryable();
        }
    
        protected override WEBCONFIGModel GetEntityByKey(int id)
        {
            var data = this.bll.GetConfig(this.Web.ID);
            return data;
        }

    }
}
