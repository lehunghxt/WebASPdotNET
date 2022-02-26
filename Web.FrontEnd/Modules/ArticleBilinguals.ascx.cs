namespace Web.FrontEnd.Modules
{
    using Business;
    using Model;
    using System;
    using System.Collections.Generic;
    using Asp.Provider.Cache;
    using System.Linq;
    using Web.Asp.Provider;

    public partial class ArticleBilinguals : Web.Asp.UI.VITModule
    {
        private ArticleBLL articleBll;
        protected List<ArticleBilingualModel> Data;
        protected int Total;

        protected int CategoryId { get; set; }

        protected string TextTranslate { get; set; }
        protected string TextNoTranslationYet { get; set; }
        protected string TextFixTranslationYet { get; set; }
        protected string TextSendTranslationYet { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTex();

            CategoryId = this.GetRequestThenParam<int>(SettingsManager.Constants.SendCategory, "CategoryId");

            this.articleBll = new ArticleBLL();
            this.Data = this.articleBll.GetArticleBilinguals(
                            Config.ID,
                            CategoryId,
                            Config.Language.ToLower(),
                            this.GetRequestThenParam<string>("sort", "OrderBy"),
                            this.GetRequestThenParam<string>(SettingsManager.Constants.SendTag, "Tag"),
                            out Total, 0, this.GetValueParam<int>("Top")).ToList();
            foreach (var item in this.Data)
            {
                if (!string.IsNullOrEmpty(item.ImagePath))
                    item.ImagePath = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.Config.ID) + SettingsManager.Constants.PathArticleImage + item.ImagePath;
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