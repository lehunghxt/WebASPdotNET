namespace Web.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using Web.Business;
    using Web.Api.Models;
    using Web.Model;

    public class ArticleController : ModuleController
    {
        protected CompanyConfigModel Web { get; set; }
        public ArticleController(CompanyConfigModel web)
        {
            Web = web;
        }

        public ContentResult Article(string skinName, Dictionary<string, object> param, string title = "")
        {
            var id = this.GetValueParam<int>(param, "ArticleId");

            var articleBLL = new ArticleBLL();
            var data = articleBLL.GetArticle(this.Web.ID, this.Web.Language, id);

            var model = data;

            var skinPath = string.Format(SkinFormat, Web.Template, skinName);

            ViewBag.Title = title;
            ViewBag.Params = param;

            var html = RenderRazorViewToString(skinPath, model);
            return Content(html);
        }
    }
}