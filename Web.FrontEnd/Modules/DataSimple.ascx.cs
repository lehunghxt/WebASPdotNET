namespace Web.FrontEnd.Modules
{
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using Asp.Provider.Cache;
    using System.Linq;
    using Web.Asp.Provider;

    public partial class DataSimple : Web.Asp.UI.VITModule
    {
        private ArticleBLL articleBll;
        private ProductBLL productBLL;

        private int _top;
        private int _categoryId;
        private bool _inChildCategory;

        protected string Source;

        protected List<DataSimpleModel> Data;
        protected DataSimpleModel Category { get; set; }

        public int Width
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

        protected int ColumnCount
        {
            get
            {
                return this.GetValueParam<int>("ColumnCount");
            }
        }

        protected string RederectComponent { get; set; }
        protected string RederectSendKey { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.articleBll = new ArticleBLL(); productBLL = new ProductBLL();

            this._top = this.GetValueParam<int>("Top");
            this._categoryId = this.GetValueParam<bool>("PriorityCatRequest")
                ? this.GetRequestThenParam<int>(SettingsManager.Constants.SendCategory, "CategoryId")
                : this.GetParamThenRequest<int>(SettingsManager.Constants.SendCategory, "CategoryId");

            this._inChildCategory = this.GetValueParam<bool>("InChildCategory");
            this.Source = this.GetValueParam<string>("Source");

            this.LoadCategory();
            this.GetData(this.Source, this._categoryId, this._inChildCategory, this._top);

            if (this.GetValueParam<bool>("OverWriteTitle") && Category != null && !string.IsNullOrEmpty(Category.Title))
            {
                this.Title = Category.Title;
            }

            if (this.GetValueParam<bool>("SetPageHeader")) 
            {
                this.Page.Title = Category.Title;
                this.Page.MetaKeywords = Category.Title;
                this.Page.MetaDescription = Category.Description;
            }
        }

        private void LoadCategory()
        {
            this.Category = CacheProvider.GetCache<DataSimpleModel>(CacheProvider.Keys.Obj, Config.ID, "Category", Config.Language, this._categoryId);
            if (this.Category == null)
            {
                this.Category = this.articleBll.GetDataSingle(
                            Config.ID,
                            Config.Language,
                            "CAT",
                            this._categoryId);
                if (this.Category == null) {
                    this.Category = new DataSimpleModel();
                    this.Category.Title = this.Category.Description = "Chua co noi dung voi ngon ngu '"+ Config.Language + "'";
                }
                CacheProvider.SetCache(this.Category, CacheProvider.Keys.Obj, Config.ID, "Category", Config.Language, this._categoryId);
            }
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
        public void GetData(string source, int categoryId, bool inChildCategory, int top)
        {
            this.Data = CacheProvider.GetCache<List<DataSimpleModel>>(CacheProvider.Keys.Obj, Config.ID, source, Config.Language, categoryId, inChildCategory, 0, this._top);
            var totalRow = CacheProvider.GetCache<int>(CacheProvider.Keys.ObjCount, Config.ID, source, Config.Language, categoryId, inChildCategory, 0, this._top);
            if (this.Data == null || Data.Count == 0 || totalRow == 0)
            {
                this.Data = new List<DataSimpleModel>();

                switch (source)
                {
                    case "PRO":
                        this.Data = this.articleBll.GetDataSimple(
                            Config.ID,
                            Config.Language,
                            source,
                            categoryId,
                            inChildCategory, top).ToList();
                        foreach(var item in this.Data)
                        {
                            if(!string.IsNullOrEmpty(item.ImagePath))
                            item.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathProductImage + item.ImagePath;
                        }
                        break;
                    case "ATR":
                        this.Data = this.productBLL.GetSimpleAttributes(Config.ID, categoryId).ToList(); 
                        break;
                    case "COR":
                        var colors = productBLL.GetColorByCategoryId(Config.ID, categoryId, inChildCategory).ToList();
                        var datas = colors.Select(e => new DataSimpleModel
                        {
                            ID = e.Id,
                            Title = e.Name,
                            ImagePath = e.ImageName,
                            Description = e.Value,
                            CategoryId = categoryId
                        }).ToList();
                        foreach (var color in datas)
                        {
                            if (!Data.Any(e => e.Description == color.Description))
                            {
                                color.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathColorImage + color.ImagePath;
                                Data.Add(color);
                            }
                            else
                            {
                                var item = Data.First(e => e.Description == color.Description);
                                if(string.IsNullOrEmpty(item.Title) && !string.IsNullOrEmpty(color.Title))
                                {
                                    Data.Remove(item);
                                    Data.Add(color);
                                }
                            }
                        }
                        break;
                    case "PTG":
                        var arrP = productBLL.GetTagByCategoryId(Config.ID, categoryId, inChildCategory).Distinct();

                        foreach (var onetag in arrP)
                        {
                            var item = new DataSimpleModel();
                            item.Title = onetag;
                            item.CategoryId = categoryId;
                            Data.Add(item);
                        }
                        break;
                    case "ATG":
                        var arrA = articleBll.GetTagByCategoryId(Config.ID, categoryId, inChildCategory);
                        foreach (var onetag in arrA)
                        {
                            var item = new DataSimpleModel();
                            item.Title = onetag;
                            item.CategoryId = categoryId;
                            Data.Add(item);
                        }
                        break;
                    case "ART":
                        this.Data = this.articleBll.GetDataSimple(
                            Config.ID,
                            Config.Language,
                            source,
                            categoryId,
                            inChildCategory, top).ToList();
                        foreach (var item in this.Data)
                        {
                            if (!string.IsNullOrEmpty(item.ImagePath))
                                item.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathArticleImage + item.ImagePath;
                        }
                        break;
                    case "CAT":
                        this.Data = this.articleBll.GetDataSimple(
                            Config.ID,
                            Config.Language,
                            source,
                            categoryId,
                            inChildCategory, top).ToList();
                        foreach (var item in this.Data)
                        {
                            if (!string.IsNullOrEmpty(item.ImagePath))
                                item.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathCategoryImage + item.ImagePath;
                        }
                        break;
                    case "LIN":
                        this.Data = this.articleBll.GetDataSimple(
                            Config.ID,
                            Config.Language,
                            source,
                            categoryId,
                            inChildCategory, top).ToList();
                        foreach (var item in this.Data)
                        {
                            if (!string.IsNullOrEmpty(item.ImagePath))
                                item.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathArticleImage + item.ImagePath;
                        }
                        break;
                    case "MID":
                        this.Data = this.articleBll.GetDataSimple(
                            Config.ID,
                            Config.Language,
                            source,
                            categoryId,
                            inChildCategory, top,
                            this.GetRequestThenParam<string>("sort", "OrderBy")).ToList();
                        foreach (var item in this.Data)
                        {
                            if (!string.IsNullOrEmpty(item.ImagePath))
                                item.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathMediaFile + item.ImagePath;
                        }
                        break;
                    case "DOC":
                        this.Data = this.articleBll.GetDataSimple(
                            Config.ID,
                            Config.Language,
                            source,
                            categoryId,
                            inChildCategory, top).ToList();
                        foreach (var item in this.Data)
                        {
                            if (!string.IsNullOrEmpty(item.ImagePath))
                                item.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathDocumentFile + item.ImagePath;
                            if (string.IsNullOrEmpty(item.URL))
                                item.URL = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathDocumentFile + item.TargetTag;
                        }
                        break;
                }

                CacheProvider.SetCache(this.Data, CacheProvider.Keys.Obj, Config.ID, source, Config.Language, categoryId, inChildCategory, 0, this._top);
                CacheProvider.SetCache(totalRow, CacheProvider.Keys.ObjCount, Config.ID, source, Config.Language, categoryId, inChildCategory, 0, this._top);
            }

            switch (source)
            {
                case "PRO":
                    this.RederectComponent = "Product";
                    this.RederectSendKey = SettingsManager.Constants.SendProduct;
                    break;
                case "COR":
                    this.RederectComponent = "Products";
                    this.RederectSendKey = SettingsManager.Constants.SendColor;
                    break;
                case "ART":
                    this.RederectComponent = "Article";
                    this.RederectSendKey = SettingsManager.Constants.SendArticle;
                    break;
                case "PTG":
                    this.RederectComponent = "Products";
                    this.RederectSendKey = SettingsManager.Constants.SendTag;
                    break;
                case "ATG":
                    this.RederectComponent = "Articles";
                    this.RederectSendKey = SettingsManager.Constants.SendTag;
                    break;
                case "CAT":
                    var type = CacheProvider.GetCache<string>(CacheProvider.Keys.CatType, Config.ID, Config.Language, categoryId);
                    if (string.IsNullOrEmpty(type))
                    {
                        var cat = this.articleBll.GetCategoryById(Config.ID, Config.Language, categoryId);
                        if (cat != null)
                        {
                            type = cat.TYPEID;
                            CacheProvider.SetCache(type, CacheProvider.Keys.CatType, Config.ID, Config.Language, categoryId);
                        }
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
                case "MID":
                    this.RederectComponent = "Album";
                    this.RederectSendKey = SettingsManager.Constants.SendMedia;

                    break;
                case "DOC":
                    this.RederectComponent = "Document";
                    this.RederectSendKey = SettingsManager.Constants.SendDocument;

                    break;
            }
        }
    }
}