namespace Web.Api.Odata.Modules
{
    using System.Linq;
    using System.Web.Http.OData;
    using Web.Model;
    using Web.Business;
    using System;
    using System.Collections.Generic;

    public class DataComplexController : OdataBaseController<DataComplexModel, int>
    {
        private ArticleBLL bll;
        public DataComplexController()
        {
            this.bll = new ArticleBLL();
        }
        [EnableQuery]
        public override IQueryable<DataComplexModel> Get()
        {
            var param = this.GetParameter();
            var companyId = this.Web.ID;
            if (param.ContainsKey("CompanyId")) int.TryParse(param["CompanyId"], out companyId);

            var categoryIds = param["CategoryIds"];
            var loadDataIds = param["LoadDataIds"];

            var listCateId = new List<int>();
            var listLoadId = new List<int>();

            if (!string.IsNullOrEmpty(categoryIds)) listCateId = categoryIds.Split(',').Select(s => Convert.ToInt32(s)).ToList();
            if (!string.IsNullOrEmpty(loadDataIds)) listLoadId = loadDataIds.Split(',').Select(s => Convert.ToInt32(s)).ToList();

            string categoryImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathCategoryImage;
            string articleImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathArticleImage;
            string productImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Web.ID) + SettingsManager.Constants.PathProductImage;

            var data = bll.GetDataComplex(companyId, Web.Language, listCateId, listLoadId, categoryImagePath, articleImagePath, productImagePath);
            return data.AsQueryable();
        }

    }
}
