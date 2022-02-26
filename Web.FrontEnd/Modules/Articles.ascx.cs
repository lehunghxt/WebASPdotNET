namespace Web.FrontEnd.Modules
{
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using Asp.Provider.Cache;
    using System.Linq;
    using Web.Asp.Provider;

    public partial class Articles : Web.Asp.UI.VITModule
    {
        private ArticleBLL articleBll;
        
        private int _categoryId;
        private bool _inChildCategory;

        protected string Search { get; set; }

        protected List<ArticleModel> Data;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            this.articleBll = new ArticleBLL();
            
            this._categoryId = this.GetValueParam<bool>("PriorityCatRequest")
                ? this.GetRequestThenParam<int>(SettingsManager.Constants.SendCategory, "CategoryId")
                : this.GetParamThenRequest<int>(SettingsManager.Constants.SendCategory, "CategoryId");

            this._inChildCategory = this.GetValueParam<bool>("InChildCategory");

            this.LoadCategory();

            Search = this.GetValueRequest<string>("key");
            if (string.IsNullOrEmpty(Search)) Search = this.GetValueRequest<string>(SettingsManager.Constants.SendTag);
            this.Data = this.articleBll.GetArticles(
                            Config.ID,
                            Config.Language,
                            _categoryId,
                            _inChildCategory,
                            this.GetValueParam<string>("OrderBy"),
                            Search).ToList();
            foreach (var item in this.Data)
            {
                if (!string.IsNullOrEmpty(item.ImagePath))
                    item.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathArticleImage + item.ImagePath;
            }


            if (this.GetValueParam<bool>("OverWriteTitle") && Category != null && !string.IsNullOrEmpty(Category.Title))
            {
                this.Title = Category.Title;
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
                if (this.Category == null)
                {
                    this.Category = new DataSimpleModel();
                    this.Category.Title = this.Category.Description = "Chua co noi dung voi ngon ngu '" + Config.Language + "'";
                }
                CacheProvider.SetCache(this.Category, CacheProvider.Keys.Obj, Config.ID, "Category", Config.Language, this._categoryId);
            }
        }
    }
}