namespace Web.FrontEnd.Modules
{
    using Asp.Provider;
    using Asp.Provider.Cache;
    using Asp.UI;
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;

    public partial class DataSingle : VITModule
    {
        private ArticleBLL _articleBll;

        private int _itemId;
        private string _source;

        protected string RederectComponent { get; set; }
        protected string RederectSendKey { get; set; }

        protected int Width
        {
            get
            {
                return this.GetValueParam<int>("Width");
            }
        }

        protected int Height
        {
            get
            {
                return this.GetValueParam<int>("Height");
            }
        }

        protected DataSimpleModel Data;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._articleBll = new ArticleBLL();

            this._source = this.GetValueParam<string>("Source");

            var sendKey = string.Empty;
            switch (this._source)
            {
                case "PRO": sendKey = SettingsManager.Constants.SendProduct; break;
                case "ART": sendKey = SettingsManager.Constants.SendArticle; break;
                case "CAT": sendKey = SettingsManager.Constants.SendCategory; break;
                case "DOC": sendKey = SettingsManager.Constants.SendArticle; break;
                case "MID": sendKey = SettingsManager.Constants.SendMedia; break;
            }

            this._itemId = this.GetValueParam<bool>("PriorityIdRequest")
                ? this.GetRequestThenParam<int>(sendKey, "Id")
                : this.GetParamThenRequest<int>(sendKey, "Id");

            this.Data = this.GetData(this._source, this._itemId);
            if (this.Data == null)
                if (this.GetValueParam<bool>("ErrorIfNull")) HREF.RedirectComponent("Errors", "PageNotFound");
                else this.Data = new DataSimpleModel();

            // over-write skin title by category title
            if (this.GetValueParam<bool>("OverWriteSkinTitle") && Data != null)
            {
                this.Title = this.Data.Title;
            }
        }

        /// <summary>
        /// Get Data
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="itemId">
        /// The category Id.
        /// </param>
        /// <param name="inChildCategory">
        /// The in Child Category.
        /// </param>
        public DataSimpleModel GetData(string source, int itemId)
        {
            var result = CacheProvider.GetCache<DataSimpleModel>(CacheProvider.Keys.Obj, this.Config.ID, source, this.Config.Language, itemId);
            if (result == null)
            {
                result = new DataSimpleModel();

                switch (source)
                {
                    case "PRO":
                        result = this._articleBll.GetDataSingle(
                            this.Config.ID,
                            this.Config.Language,
                            source,
                            itemId);
                        if(result != null)
                            result.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathProductImage + result.ImagePath;
                        break;
                    case "ART":
                        result = this._articleBll.GetDataSingle(
                            this.Config.ID,
                            this.Config.Language,
                            source,
                            itemId);
                        if (result != null)
                            result.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathArticleImage + result.ImagePath;
                        break;
                    case "CAT":
                        result = this._articleBll.GetDataSingle(
                            this.Config.ID,
                            this.Config.Language,
                            source,
                            itemId);
                        if (result != null)
                            result.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathCategoryImage + result.ImagePath;
                        break;
                    case "MID": result = this._articleBll.GetDataSingle(
                            this.Config.ID,
                            this.Config.Language,
                            source,
                            itemId);
                        if (result != null)
                            result.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathArticleImage + result.ImagePath;
                        break;
                    case "LIN":
                        result = this._articleBll.GetDataSingle(
                            this.Config.ID,
                            this.Config.Language,
                            source,
                            itemId);
                        if (result != null)
                            result.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathArticleImage + result.ImagePath;
                        break;
                }

                CacheProvider.SetCache(result, CacheProvider.Keys.Obj, this.Config.ID, source, this.Config.Language, itemId);
            }

            switch (source)
            {
                case "PRO":
                    this.RederectComponent = "Product";
                    this.RederectSendKey = SettingsManager.Constants.SendProduct;

                    break;
                case "ART":
                    this.RederectComponent = "Article";
                    this.RederectSendKey = SettingsManager.Constants.SendArticle;

                    break;
                case "CAT":
                    var type = CacheProvider.GetCache<string>(CacheProvider.Keys.CatType, this.Config.ID, this.Config.Language, itemId);
                    if (string.IsNullOrEmpty(type))
                    {
                        type = this._articleBll.GetCategoryById(this.Config.ID, this.Config.Language, itemId).TYPEID;
                        CacheProvider.SetCache(type, CacheProvider.Keys.CatType, this.Config.ID, this.Config.Language, itemId);
                    }

                    switch (type)
                    {
                        case "ART":
                            this.RederectComponent = "Articles";
                            this.RederectSendKey = SettingsManager.Constants.SendCategory;
                            break;

                        case "PRO":
                            this.RederectComponent = "Products";
                            this.RederectSendKey = SettingsManager.Constants.SendCategory;
                            break;

                        case "MID":
                            this.RederectComponent = "Album";
                            this.RederectSendKey = SettingsManager.Constants.SendCategory;
                            break;

                        case "DOC":
                            this.RederectComponent = "Documents";
                            this.RederectSendKey = SettingsManager.Constants.SendCategory;
                            break;
                    }

                    break;
                case "LIN":
                    break; 
                case "DOC":
                    this.RederectComponent = "Dcument";
                    this.RederectSendKey = SettingsManager.Constants.SendArticle;

                    break;
                case "MID":
                    this.RederectComponent = "Album";
                    this.RederectSendKey = SettingsManager.Constants.SendArticle;

                    break;
            }

            return result;
        }
    }
}