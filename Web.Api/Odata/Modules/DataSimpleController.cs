namespace Web.Api.Odata.Modules
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;

    public class DataSimpleController : OdataBaseController<DataSimpleModel, int>
    {
        private ArticleBLL bll;
        public DataSimpleController()
        {
            this.bll = new ArticleBLL();
        }
        [EnableQuery]
        public override IQueryable<DataSimpleModel> Get()
        {
            var param = this.GetParameter();
            var datasource = param["DataSource"];
            var categoryId = 0;
            if (param.ContainsKey("CategoryId")) int.TryParse(param["CategoryId"], out categoryId);
            var companyId = this.Web.ID;
            if (param.ContainsKey("CompanyId")) int.TryParse(param["CompanyId"], out companyId);

            var data = this.bll.GetDataSimple(companyId, Web.Language, datasource, categoryId);
            foreach (var item in data)
            {
                switch (datasource)
                {
                    case "CAT":
                        item.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathCategoryImage + item.ImagePath;
                        break;
                    case "ART":
                        item.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathArticleImage + item.ImagePath;
                        break;
                    case "PRO":
                        item.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathArticleImage + item.ImagePath;
                        break;
                }
            }

            return data.AsQueryable();
        }

    }
}
