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

    public class ArticlesController : ModuleController
    {
        protected CompanyConfigModel Web { get; set; }
        public ArticlesController(CompanyConfigModel web)
        {
            Web = web;
        }

        public ContentResult Articles(string skinName, Dictionary<string, object> param, string title="")
        {
            var articleBLL = new ArticleBLL();
            var data = articleBLL.GetAllArticles(this.Web.ID, this.Web.Language).Where(e => e.PUBLISH).OrderBy(e => e.ORDERS).AsQueryable();

            var categoryId = this.GetValueParam<int>(param, "CategoryId");
            if (categoryId > 0)
            {
                IList<int> listCategory = new List<int>();
                var inChildCat = this.GetValueParam<bool>(param, "InChildCat");
                if (inChildCat)
                {
                    listCategory = articleBLL.GetAllChildId(categoryId, this.Web.ID);
                }

                listCategory.Add(categoryId);
                data = data.Where(c => listCategory.Contains(c.CATEGORYID));
            }

            var top = this.GetValueParam<int>(param, "Top");
            if (top > 0) data = data.Take(top);

            var model = data.ToList();

            var skinPath = string.Format(SkinFormat, Web.Template, skinName);
            
            ViewBag.Title = title;
            ViewBag.Params = param;

            var html = RenderRazorViewToString(skinPath, model);
            return Content(html);
        }
    }
}