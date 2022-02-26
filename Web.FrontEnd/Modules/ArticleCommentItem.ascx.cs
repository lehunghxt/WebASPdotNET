namespace Web.FrontEnd.Modules
{
    using Asp.Provider;
    using Asp.UI;
    using Business;
    using Model;
    using System;


    using System.Collections.Generic;
    using Web.Asp.Provider.Cache;

    public partial class ArticleCommentItem : VITModule
    {
        private ArticleBLL _bll;
        private ItemBLL _itemBLL;

        protected ARTICLELANGUAGEModel dto;
        protected IList<DataSimpleModel> RelatiedArticles;

        protected bool DisplayDate { get; set; }
        protected bool DisplayTitle { get; set; }
        protected bool DisplayImage { get; set; }
        protected bool DisplayTag { get; set; }
        protected List<string> Tags { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this._bll = new ArticleBLL();
            this._itemBLL = new ItemBLL();

            this.DisplayTitle = this.GetValueParam<bool>("DisplayTitle");
            this.DisplayDate = this.GetValueParam<bool>("DisplayDate");
            this.DisplayImage = this.GetValueParam<bool>("DisplayImage");
            this.DisplayTag = this.GetValueParam<bool>("DisplayTag");

            var id = this.GetRequestThenParam<int>(SettingsManager.Constants.SendArticle, "ArticleId");
            if (id == 0) id = this.GetRequestThenParam<int>(SettingsManager.Constants.SendArticle, "ArticleId");

            this.dto = CacheProvider.GetCache<ARTICLELANGUAGEModel>(CacheProvider.Keys.Art, this.Config.ID, id, this.Config.Language);
            if (this.dto == null)
            {
                this.dto = this._bll.GetArticle(this.Config.ID, this.Config.Language, id);
                if (dto == null)
                {
                    if (this.GetValueParam<bool>("ErrorIfNull")) HREF.RedirectComponent("Errors", "PageNotFound");
                    else dto = new ARTICLELANGUAGEModel();
                }
                else
                {
                    dto.PathImage = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathArticleImage + dto.IMAGE;
                }

                CacheProvider.SetCache(this.dto, CacheProvider.Keys.Art, this.Config.ID, id, this.Config.Language);
            }

            this.Tags = this.HREF.LinkTag("Articles", dto.TAG);

            if (id > 0 && this.GetValueParam<bool>("IsUpdateView")) this._itemBLL.UpView(id, this.Config.ID);

            var param = new Dictionary<string, string>();
            param["ItemId"] = id.ToString();
            param["CaptchaLength"] = this.GetValueParam<string>("CaptchaLength");
            if (dto.HASCOMMENT) this.LoadModule(this.psComment, "ItemComment", param, string.Empty);
            
            // over-write skin title by category title
            if (this.GetValueParam<bool>("IsOverWriteTitle"))
            {
                this.Title = dto.TITLE;
            }

            this.RelatiedArticles = _bll.GetRelatiedArticles(id, this.Config.ID, this.Config.Language);
            foreach (var relatied in RelatiedArticles)
            {
                relatied.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathArticleImage + dto.IMAGE;
            }
        }
    }
}