namespace Web.FrontEnd.Modules
{
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using Asp.Provider.Cache;
    using System.Linq;
    using Web.Asp.Provider;

    public partial class ArticleBilingual : Web.Asp.UI.VITModule
    {
        private ArticleBLL articleBll;
        private ItemBLL itemBLL;

        protected ArticleBilingualModel Data;
        protected List<string> Tags { get; set; }

        protected string TextTranslate { get; set; }
        protected string TextNoTranslationYet { get; set; }
        protected string TextFixTranslationYet { get; set; }
        protected string TextSendTranslationYet { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.articleBll = new ArticleBLL();
            this.itemBLL = new ItemBLL();

            LoadTex();

            var id = this.GetValueParam<int>("ArticleId");
            if (id == 0) id = this.GetRequestThenParam<int>(SettingsManager.Constants.SendArticle, "ArticleId");

            this.Data = this.articleBll.GetArticleBilingual(id, Config.ID, Config.Language.ToLower());

            if (Data != null)
            {
                Tags = this.HREF.LinkTag("Articles", this.Data.TargetTag);
                if (!string.IsNullOrEmpty(Data.ImagePath)) Data.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathArticleImage + Data.ImagePath;
                this.itemBLL.UpView(id, this.Config.ID);
            }
            else
            {
                Data = new ArticleBilingualModel();
                Data.CreateDate = new DateTime();
                Data.Title1 = Data.Content1 = Data.Description1 = Data.Title2 = Data.Content2 = Data.Description2 = Language["NoTranslationYet"];
                Tags = new List<string>();
            }

        }

        private void LoadTex()
        {
            TextTranslate = Language["Translate"];
            TextNoTranslationYet = Language["NoTranslationYet"];
            TextFixTranslationYet = Language["FixTranslationYet"];
            TextSendTranslationYet = Language["SendTranslationYet"];
        }
    }
}