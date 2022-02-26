namespace Web.FrontEnd.Modules
{
    using Asp.Provider;
    using Asp.Provider.Cache;
    using Asp.UI;
    using Business;
    using Library;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public partial class ArticleCommentFacebook : VITModule
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
        
        private int _numberPost;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._bll = new ArticleBLL();
            this._itemBLL = new ItemBLL();

            this.DisplayTitle = this.GetValueParam<bool>("DisplayTitle");
            this.DisplayDate = this.GetValueParam<bool>("DisplayDate");
            this.DisplayImage = this.GetValueParam<bool>("DisplayImage");
            this.DisplayTag = this.GetValueParam<bool>("DisplayTag");
            
            var id = this.GetRequestThenParam<int>(SettingsManager.Constants.SendArticle, "ArticleId");

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

            if (dto != null && dto.ID > 0 && this.GetValueParam<bool>("IsUpdateView")) this._itemBLL.UpView(id, this.Config.ID);
            
            this._numberPost = this.GetValueParam<int>("PostNumber");
            var param = new Dictionary<string, string>();
            param["PostNumber"] = this._numberPost.ToString();
            if (dto.HASCOMMENT) this.LoadModule(this.psComment, "FacebookComment", param, string.Empty);
            
            if (this.GetValueParam<bool>("IsOverWriteTitle"))
            {
                this.Title = dto.TITLE;
            }
            
            if (this.GetValueParam<bool>("SetPageHeader"))
            {
                this.Page.Title = dto.TITLE;
                this.Page.MetaKeywords = dto.TAG;
                this.Page.MetaDescription = dto.BRIEF;
            }

            this.RelatiedArticles = _bll.GetRelatiedArticles(id, this.Config.ID, this.Config.Language);
        }

        public string LinkType(string types)
        {
            var result = CacheProvider.GetCache<string>(CacheProvider.Keys.ArtType, this.Config.ID, types);
            if (result == null)
            {
                if (string.IsNullOrEmpty(types)) return "";
                var links = new List<string>();
                var tagList = types.Split(',').ToArray(); 
                foreach (var tag in tagList)
                { 
                    links.Add(string.Format("<a href='{0}' title='{1}'>{1}</a>", HREF.LinkComponent( "Articles", SettingsManager.Constants.SendTag + "/" + tag + "/" + tag.ConvertToUnSign()), tag));
                }

                result = string.Join(", ", links);
                CacheProvider.SetCache<string>(result, CacheProvider.Keys.ArtType, this.Config.ID, types);
            }
            return result;
        }
    }
}