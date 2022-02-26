namespace Web.Api.Odata.Modules
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;

    public class GoogleMapController : OdataBaseController<GoogleMapModel, string>
    {
        private CompanyBLL bll;
        public GoogleMapController()
        {
            this.bll = new CompanyBLL();
        }
        [EnableQuery]
        public override IQueryable<GoogleMapModel> Get()
        {
            var config = this.bll.GetConfig(this.Web.ID);
            var data = new GoogleMapModel { ID = config.GoogleApiKey, Address = config.GoogleMapAddress };

            return new List<GoogleMapModel> { data }.AsQueryable();
        }

    }
}
