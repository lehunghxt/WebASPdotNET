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

    public class CategoriesController : ModuleController
    {
        protected CompanyConfigModel Web { get; set; }
        public CategoriesController(CompanyConfigModel web)
        {
            Web = web;
        }

        public ContentResult Categories(string skinName, Dictionary<string, object> param, string title="")
        {
            var articleBLL = new ArticleBLL();
            var data = articleBLL.GetCategoryByType(this.Web.ID, this.Web.Language).Where(e => e.PUBLISH).OrderBy(e => e.ORDERS).AsQueryable();

            var type = this.GetValueParam<string>(param, "CategoryType");
            if (!string.IsNullOrEmpty(type)) data = data.Where(e => e.TYPEID == type);

            var parentId = this.GetValueParam<int>(param, "CategoryId");
            if (parentId > 0)
            {
                IList<int> listCategory = new List<int>();
                var inChildCat = this.GetValueParam<bool>(param, "InChildCat");
                if (inChildCat)
                {
                    listCategory = articleBLL.GetAllChildId(parentId, this.Web.ID);
                }

                listCategory.Add(parentId);
                data = data.Where(c => listCategory.Contains(c.ID));
            }
            
            var model = data.ToList();

            var skinPath = string.Format(SkinFormat, Web.Template, skinName);
            
            ViewBag.Title = title;
            ViewBag.Params = param;

            var html = RenderRazorViewToString(skinPath, model);
            return Content(html);
        }
    }
}