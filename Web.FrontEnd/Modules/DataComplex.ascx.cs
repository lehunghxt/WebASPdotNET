namespace Web.FrontEnd.Modules
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Asp.UI;
    using Business;
    using Model;
    using Asp.Provider.Cache;
    using Asp.Provider;
    using Library;

    public partial class DataComplex : VITModule
    {
        private ArticleBLL _articleBll;
        private ProductBLL _productBll;
        private FileBLL _fileBll;

        public IList<DataComplexModel> Model;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._articleBll = new ArticleBLL();
            this._productBll = new ProductBLL();
            this._fileBll = new FileBLL();

            var categoryIds = this.GetValueParam<string>("CategoryIds");
            var loadDataIds = this.GetValueParam<string>("LoadDataIds");

            var listCateId = new List<int>();
            var listLoadId = new List<int>();

            if(!string.IsNullOrEmpty(categoryIds)) listCateId = categoryIds.Split(',').Select(s => Convert.ToInt32(s)).ToList();
            if (!string.IsNullOrEmpty(loadDataIds)) listLoadId = loadDataIds.Split(',').Select(s => Convert.ToInt32(s)).ToList();
            
            this.GetData(listCateId, listLoadId);
        }

        protected string CreateLink(string catType, bool item, int id, string title)
        {
            var view = string.Empty;
            var send = string.Empty;
            switch (catType)
            {
                case "PRO":
                    view = item ? "Product" : "Products";
                    send = item ? SettingsManager.Constants.SendProduct : SettingsManager.Constants.SendCategory;
                    break;
                case "ART":
                    view = item ? "Article" : "Articles";
                    send = item ? SettingsManager.Constants.SendArticle : SettingsManager.Constants.SendCategory;
                    break;
                case "LIN":
                    return "/";
                    break;
            }
            title = title.ConvertToUnSign();
            var param = string.Format("{0}/{1}/{2}", send, id, title);
            var link = HREF.LinkComponent(view, param);
            return link;
        }

        /// <summary>
        /// Get Data
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="categoryId">
        /// The category Id.
        /// </param>
        /// <param name="inChildCategory">
        /// The in Child Category.
        /// </param>
        public void GetData(List<int> categoryIds, List<int> loadDataIds)
        {
            this.Model = CacheProvider.GetCache<List<DataComplexModel>>(CacheProvider.Keys.Obj, Config.ID, Config.Language, string.Join("", categoryIds));
            if (this.Model == null || Model.Count == 0)
            {
                string categoryImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathCategoryImage;
                string articleImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathArticleImage;
                string productImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathProductImage;

                this.Model = _articleBll.GetDataComplex(Config.ID, Config.Language, categoryIds, loadDataIds, categoryImagePath, articleImagePath, productImagePath);
                
                CacheProvider.SetCache(this.Model, CacheProvider.Keys.Obj, Config.ID, Config.Language, string.Join("", categoryIds));
            }
        }       
    }
}