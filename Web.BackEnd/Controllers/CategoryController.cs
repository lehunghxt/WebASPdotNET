namespace Web.Backend.Controllers
{
    using Asp.Provider;
    using Library;
    using Library.Web;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using Web.Backend.Models;
    using Web.Business;
    using Web.Model;

    public class CategoryController : BaseController
    {
        private ArticleBLL articleBLL;
        private CompanyBLL companyBLL;
        private ItemBLL itemBLL;
        private SEOBLL seoBLL;

        public CategoryController()
        {
            articleBLL = new ArticleBLL();
            companyBLL = new CompanyBLL();
            itemBLL = new ItemBLL();
            seoBLL = new SEOBLL();
        }

        public ActionResult Index(string type, int catid = 0)
        {
            var model = new CategoryViewModel();
            model.CatId = catid;
            if(catid > 0 && string.IsNullOrEmpty(type))
            {
                var cat = articleBLL.GetCategoryById(this.User.CompanyId, this.LanguageId, catid);
                if (cat != null) type = cat.TYPEID;
            }

            model.Category.TYPEID = type;
            model.Category.PUBLISH = true;
            model.Category.ORDERS = 50;
            model.Category.LANGUAGEID = this.LanguageId;
            model.TypeName = GetCategoryName(type);
            

            model.Languages = companyBLL.GetLanguage().ToList();

            var data = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, type, catid).ToList();
            model.Categories = SortTable(data, 0);
            foreach(var category in model.Categories)
            {
                category.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathCategoryImage + category.IMAGE;
            }

            ViewBag.Token = this.User.Token;
            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Index(CategoryViewModel model)
        {
            var seo = new SEOLinkModel();
            var url = new URL();

            model.TypeName = GetCategoryName(model.Category.TYPEID);

            model.Languages = companyBLL.GetLanguage().ToList();
            var data = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, model.Category.TYPEID, model.CatId).OrderByDescending(e => e.ID).ToList();

            var folder = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathCategoryImage;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            switch (model.Action)
            {
                case "ADD":
                    if (Request.Files.Count > 0)
                    {
                        try
                        {
                            var logo = Request.Files["categoryimage"];
                            if (logo.ContentLength > 0)
                            {
                                model.Category.IMAGE = string.Format("{0}{1}.{2}", model.Category.NAME.ConvertToUnSign(), DateTime.Now.Ticks , logo.FileName.Split('.')[1]);
                                logo.SaveAs(folder + model.Category.IMAGE);
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Danger = "Gửi file thật bại";
                        }
                    }

                    articleBLL.AddCategory(model.Category, this.User.CompanyId, this.User.UserId);
                    new URL().ClearCache(SettingsManager.Constants.AllDataCache + this.User.CompanyId);

                    // SEO link
                    if ((model.Category.TYPEID == "ATR" && model.Category.PARENTID > 0) || model.Category.TYPEID == "PRO" || model.Category.TYPEID == "ART" || model.Category.TYPEID == "DOC")
                    {
                        //var component = string.Empty;
                        //if (model.Category.TYPEID == "ART")
                        //    component = "Articles";
                        //else if (model.Category.TYPEID == "PRO")
                        //    component = "Products";
                        //else if (model.Category.TYPEID == "ATR")
                        //    component = "Attributes";

                        //url.CreateSEO(model.Category.ID,
                        //                model.Category.NAME.Trim(),
                        //                model.Category.NAME.Trim(),
                        //                model.Category.DESCRIPTION.Trim(),
                        //                this.User.CompanyId,
                        //                component,
                        //                model.Category.PARENTID ?? 0,
                        //                this.LanguageId);

                        seo.RefItem = model.Category.ID;
                        if (model.Category.TYPEID == "ART")
                            seo.Url = url.LinkComponent("Articles", SettingsManager.Constants.SendCategory + "/" + model.Category.ID + "/" + model.Category.NAME.ConvertToUnSign());
                        else if (model.Category.TYPEID == "PRO")
                            seo.Url = url.LinkComponent("Products", SettingsManager.Constants.SendCategory + "/" + model.Category.ID + "/" + model.Category.NAME.ConvertToUnSign());
                        else if (model.Category.TYPEID == "DOC")
                            seo.Url = url.LinkComponent("Documents", SettingsManager.Constants.SendCategory + "/" + model.Category.ID + "/" + model.Category.NAME.ConvertToUnSign());

                        //if (model.Category.PARENTID != null && model.Category.PARENTID != 0)
                        //{
                        //    var cat = articleBLL.GetCategoryById(this.User.CompanyId, this.LanguageId, model.Category.PARENTID ?? 0);
                        //    seo.SeoUrl = string.Format("/{0}/{1}", cat.NAME.Trim().ConvertToUnSign(), model.Category.NAME.Trim().ConvertToUnSign());
                        //}
                        //else
                        seo.SeoUrl = "/" + model.Category.NAME.Trim().ConvertToUnSign();
                        seo.Title = model.Category.NAME.Trim();
                        seo.MetaKeyWork = model.Category.NAME.Trim();
                        if (string.IsNullOrEmpty(model.Category.DESCRIPTION))
                            seo.MetaDescription = model.Category.NAME.Trim();
                        else seo.MetaDescription = model.Category.DESCRIPTION.DeleteHTMLTag().Trim();
                        seo.MetaDescription = seo.MetaDescription.Length > 200 ? seo.MetaDescription.Substring(0, 200) : seo.MetaDescription;

                        seoBLL.Save(seo, this.User.CompanyId, model.Category.LANGUAGEID);
                        new URL().ClearCache(SettingsManager.Constants.AppCompanyDomainMapCache);
                    }
                    // end SEO link

                    break;
                case "UPDATE":
                    if (model.Category.ID == model.Category.PARENTID)
                    { ViewBag.Danger = "Danh mục không thể là cha của chính nó"; }
                    else if(data.Any(e => e.PARENTID == model.Category.ID && e.ID == model.Category.PARENTID))
                    { ViewBag.Danger = "Danh mục không thể con của con nó"; }
                    else
                    {
                        var childrenIds = articleBLL.GetAllChildId(model.Category.ID);
                        if (childrenIds.Contains(model.Category.PARENTID))
                        { ViewBag.Danger = "Danh mục bị lặp vòng"; }
                        else
                        {
                            if (Request.Files.Count > 0)
                            {
                                try
                                {
                                    var logo = Request.Files["categoryimage"];
                                    if (logo.ContentLength > 0)
                                    {
                                        var oldImage = model.Category.IMAGE;
                                        model.Category.IMAGE = string.Format("{0}{1}.{2}", model.Category.NAME.ConvertToUnSign(), DateTime.Now.Ticks, logo.FileName.Split('.')[1]);

                                        logo.SaveAs(folder + model.Category.IMAGE);

                                        if (model.Category.IMAGE != oldImage && System.IO.File.Exists(folder + oldImage))
                                            System.IO.File.Delete(folder + oldImage);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    ViewBag.Danger = "Gửi file thật bại";
                                }
                            }
                            articleBLL.UpdateCategory(model.Category, this.User.CompanyId, this.User.UserId);
                            new URL().ClearCache(SettingsManager.Constants.AllDataCache + this.User.CompanyId);

                            // SEO link
                            if ((model.Category.TYPEID == "ATR" && model.Category.PARENTID > 0) || model.Category.TYPEID == "PRO" || model.Category.TYPEID == "ART" || model.Category.TYPEID == "DOC")
                            {
                                //var component = string.Empty;
                                //if (model.Category.TYPEID == "ART")
                                //    component = "Articles";
                                //else if (model.Category.TYPEID == "PRO")
                                //    component = "Products";
                                //else if (model.Category.TYPEID == "ATR")
                                //    component = "Attributes";

                                //url.CreateSEO(model.Category.ID,
                                //                model.Category.NAME.Trim(),
                                //                model.Category.NAME.Trim(),
                                //                model.Category.DESCRIPTION.Trim(),
                                //                this.User.CompanyId,
                                //                component,
                                //                model.Category.PARENTID ?? 0,
                                //                this.LanguageId);

                                seo.RefItem = model.Category.ID;
                                if (model.Category.TYPEID == "ART")
                                    seo.Url = url.LinkComponent("Articles", SettingsManager.Constants.SendCategory + "/" + model.Category.ID + "/" + model.Category.NAME.ConvertToUnSign());
                                else if (model.Category.TYPEID == "PRO")
                                    seo.Url = url.LinkComponent("Products", SettingsManager.Constants.SendCategory + "/" + model.Category.ID + "/" + model.Category.NAME.ConvertToUnSign());
                                else if (model.Category.TYPEID == "DOC")
                                    seo.Url = url.LinkComponent("Documents", SettingsManager.Constants.SendCategory + "/" + model.Category.ID + "/" + model.Category.NAME.ConvertToUnSign());

                                //if (model.Category.PARENTID != null && model.Category.PARENTID != 0)
                                //{
                                //    var cat = articleBLL.GetCategoryById(this.User.CompanyId, this.LanguageId, model.Category.PARENTID ?? 0);
                                //    seo.SeoUrl = string.Format("/{0}/{1}", cat.NAME.Trim().ConvertToUnSign(), model.Category.NAME.Trim().ConvertToUnSign());
                                //}
                                //else 
                                seo.SeoUrl = "/" + model.Category.NAME.Trim().ConvertToUnSign();
                                seo.Title = model.Category.NAME.Trim();
                                seo.MetaKeyWork = model.Category.NAME.Trim();
                                seo.MetaDescription = model.Category.DESCRIPTION.DeleteHTMLTag().Trim();
                                seo.MetaDescription = seo.MetaDescription.Length > 200 ? seo.MetaDescription.Substring(0, 200) : seo.MetaDescription;

                                seoBLL.Save(seo, this.User.CompanyId, model.Category.LANGUAGEID);
                                new URL().ClearCache(SettingsManager.Constants.AppCompanyDomainMapCache);
                            }
                            // end SEO link
                        }
                    }
                    break;
                case "REMOVE":
                    articleBLL.RemoveCategory(model.Category.ID, this.User.CompanyId);
                    if (System.IO.File.Exists(folder + model.Category.IMAGE))
                        System.IO.File.Delete(folder + model.Category.IMAGE);

                    seoBLL.Delete(model.Category.ID);
                    new URL().ClearCache(SettingsManager.Constants.AppCompanyDomainMapCache);
                    new URL().ClearCache(SettingsManager.Constants.AllDataCache + this.User.CompanyId);
                    break;
            }

            data = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, model.Category.TYPEID, model.CatId).ToList();
            model.Categories = SortTable(data, 0);
            foreach (var category in model.Categories)
            {
                category.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathCategoryImage + category.IMAGE;
            }

            ViewBag.Token = this.User.Token;
            return View(model);
        }

        private string GetCategoryName(string typeID)
        {
            switch (typeID.ToUpper())
            {
                case "ART": return "bài viết";
                case "PRO": return "sản phẩm";
                case "WH": return "kho hàng";
                case "SPO": return "hỗ trợ";
                case "LIN": return "liên kết";
                case "DOC": return "tài liệu";
                case "MID": return "media";
                case "ATR": return "thuộc tính sản phẩm";
                default: return string.Empty;
            }
        }

        private IList<CATEGORYLANGUAGEModel> SortTable(IList<CATEGORYLANGUAGEModel> table, int parentId, string space = "", string distance = "....")
        {
            var rows = table.Where(dto => dto.PARENTID == parentId).ToList();
            if (!rows.Any()) return new List<CATEGORYLANGUAGEModel>();
            var sortData = new List<CATEGORYLANGUAGEModel>();
            foreach (var row in rows)
            {
                var spaceNext = space + distance;
                var dt = SortTable(table, row.ID, spaceNext, distance);
                row.Blank = space;
                //row.Title = space + row.Title;
                sortData.Add(row);

                if (dt.Count > 0) sortData.AddRange(dt);
            }
            return sortData;
        }
    }
}