namespace Web.Api.Odata.Modules
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;

    public class CompanyController : OdataBaseController<CompanyInfoModel, int>
    {
        private CompanyBLL bll;
        public CompanyController()
        {
            this.bll = new CompanyBLL();
        }
        [EnableQuery]
        public override IQueryable<CompanyInfoModel> Get()
        {
            var data = this.bll.GetCompany(this.Web.ID, Web.Language);
            data.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathCompanyImage + data.IMAGE;
            data.PathWebIcon = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathCompanyImage + data.WebIcon;
            return new List<CompanyInfoModel>() { data }.AsQueryable();
        }
    }
}
