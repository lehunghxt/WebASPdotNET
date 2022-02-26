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

    public class CommentController : ModuleController
    {
        protected CompanyConfigModel Web { get; set; }
        public CommentController(CompanyConfigModel web)
        {
            Web = web;
        }

        public ContentResult Comment(string skinName, Dictionary<string, object> param, string title="")
        {
            var id = this.GetValueParam<int>(param, "Id");
            var itemBL = new ItemBLL();
            var data = itemBL.GetComments(id, this.Web.ID).OrderByDescending(e => e.Date);
            var model = data.ToList();

            var skinPath = string.Format(SkinFormat, Web.Template, skinName);
            
            ViewBag.Title = title;
            ViewBag.Params = param;

            var html = RenderRazorViewToString(skinPath, model);
            return Content(html);
        }
        
    }
}