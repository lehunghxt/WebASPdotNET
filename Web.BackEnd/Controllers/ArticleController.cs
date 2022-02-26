namespace Web.Backend.Controllers
{
    using Library;
    using Library.Web;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using Asp.Provider;
    using Web.Backend.Models;
    using Web.Business;
    using Web.Model;

    public class ArticleController : BaseController
    {
        private ArticleBLL articleBLL;
        private CompanyBLL companyBLL;
        private ItemBLL itemBLL;
        private SEOBLL seoBLL;
        public ArticleController()
        {
            articleBLL = new ArticleBLL();
            companyBLL = new CompanyBLL();
            itemBLL = new ItemBLL();
            seoBLL = new SEOBLL();
        }

        public ActionResult Index(int catid = 0)
        {
            var model = new ArticleViewModel();
            model.ArticleId = 0;
            model.Publish = true;
            model.CatId = catid;
            
            var data = articleBLL.GetAllArticles(this.User.CompanyId, this.LanguageId, model.CatId).OrderByDescending(e => e.ID).ToList();
            model.Articles = data;
            foreach (var article in model.Articles)
            {
                article.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathArticleImage + article.IMAGE;
            }

            var categories = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, "ART", model.CatId).ToList();
            model.Categories = SortTable(categories, 0).Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ArticleViewModel model)
        {
            var categories = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, "ART", model.CatId).ToList();
            model.Categories = SortTable(categories, 0).Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();

            switch (model.Action)
            {
                case "REMOVE":
                    var folder = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathArticleImage;
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    itemBLL.Delete(model.ArticleId, this.User.CompanyId);
                    if (System.IO.File.Exists(folder + model.Image))
                        System.IO.File.Delete(folder + model.Image);

                    seoBLL.Delete(model.ArticleId);
                    break;
                case "PUBLISH":
                    itemBLL.ChangePublish(model.ArticleId, this.User.CompanyId);
                    break;
            }

            var data = articleBLL.GetAllArticles(this.User.CompanyId, this.LanguageId, model.CatId).OrderByDescending(e => e.ID).ToList();
            model.Articles = data;
            foreach (var article in model.Articles)
            {
                article.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathArticleImage + article.IMAGE;
            }

            return View(model);
        }

        public ActionResult Detail(int id = 0, string language = "", int catid = 0)
        {
            var model = new ArticleDetailViewModel();
            model.CatId = catid;

            if (string.IsNullOrEmpty(language)) language = this.LanguageId;

            if (id > 0)
            {
                model.Article = articleBLL.GetArticle(this.User.CompanyId, language, id);
                if (model.Article != null)
                    model.Article.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathArticleImage + model.Article.IMAGE;
                else model.Article = new Model.ARTICLELANGUAGEModel();
            }
            else model.Article = new Model.ARTICLELANGUAGEModel();

            if(model.Article.ID == 0)
            {
                model.Article.ORDERS = 50;
                model.Article.PUBLISH = true;
                model.Article.DISPLAYDATE = DateTime.Now;
                model.Article.HASCOMMENT = true;
                model.Article.LANGUAGEID = language;
                model.Article.TAG = string.Empty;
            }
            var categories = articleBLL.GetCategoryByType(this.User.CompanyId, language, "ART", model.CatId)
                                .ToList();
            model.Categories = SortTable(categories, model.CatId).Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.Blank + e.NAME }).ToList();
            if (model.CatId > 0)
            {
                var cat = categories.FirstOrDefault(e => e.ID == model.CatId);
                model.Categories.Insert(0, new SelectListItem { Value = cat.ID.ToString(), Text = cat.NAME });
                model.Article.CATEGORYID = model.CatId;
            }

            model.Languages = companyBLL.GetLanguage()
                                .Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();
            model.Articles = articleBLL.GetDataSimple(this.User.CompanyId, this.LanguageId, "ART", 0, true);
            foreach (var article in model.Articles)
            {
                article.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathArticleImage + article.ImagePath;
            }
            model.RelatiedArticles = articleBLL.GetRelatiedArticles(model.Article.ID, this.User.CompanyId, this.LanguageId);
            foreach (var article in model.RelatiedArticles)
            {
                article.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathArticleImage + article.ImagePath;
            }

            model.Tags = articleBLL.GetTagByCategoryId(this.User.CompanyId);

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Detail(ArticleDetailViewModel model)
        {
            var categories = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, "ART", model.CatId)
                                .ToList();
            model.Categories = SortTable(categories, 0).Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.Blank + e.NAME }).ToList();
            if (model.CatId > 0)
            {
                var category = categories.FirstOrDefault(e => e.ID == model.CatId);
                model.Categories.Insert(0, new SelectListItem { Value = category.ID.ToString(), Text = category.NAME });
            }

            model.Languages = companyBLL.GetLanguage()
                                .Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();

            var folder = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathArticleImage;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (Request.Files.Count > 0)
            {
                try
                {
                    var logo = Request.Files["articleimage"];
                    if (logo.ContentLength > 0)
                    {
                        var oldImage = model.Article.IMAGE;
                        model.Article.IMAGE = string.Format("{0}_{1}.{2}", model.Article.TITLE.ConvertToUnSign(), DateTime.Now.Ticks, logo.FileName.Split('.')[1]);
                        logo.SaveAs(folder + model.Article.IMAGE);

                        if (model.Article.IMAGE != oldImage && System.IO.File.Exists(folder + oldImage))
                            System.IO.File.Delete(folder + oldImage);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Danger = "Gửi file thật bại";
                }
            }

            if (string.IsNullOrEmpty(model.Article.TAG)) model.Article.TAG = model.Article.TITLE;

            var relatiedArticles = new List<int>();
            if (Request["relatied_id"] != null)
            {
                var ids = Request["relatied_id"].Split(',');
                for (int i = 0; i < ids.Length; i++)
                {
                    var id = Convert.ToInt32(ids[i]);
                    relatiedArticles.Add(id);
                }
            }

            if (model.Article.ID > 0) articleBLL.UpdateArticle(model.Article, relatiedArticles, this.User.CompanyId, this.User.UserId);
            else model.Article.ID = articleBLL.AddArticle(model.Article, relatiedArticles, this.User.CompanyId, this.User.UserId);
            new URL().ClearCache(SettingsManager.Constants.AllDataCache + this.User.CompanyId);

            // SEO link
            var url = new URL();
            //url.CreateSEO(model.Article.ID,
            //                model.Article.TITLE.Trim(),
            //                model.Article.TAG.Trim(),
            //                model.Article.BRIEF.Trim(),
            //                this.User.CompanyId,
            //                "Article",
            //                model.Article.CATEGORYID,
            //                this.LanguageId);

            var seo = new SEOLinkModel();
            seo.RefItem = model.Article.ID;

            seo.Url = url.LinkComponent("Article", SettingsManager.Constants.SendArticle + "/" + model.Article.ID + "/" + model.Article.TITLE.ConvertToUnSign());

            var config = this.companyBLL.GetConfig(this.User.CompanyId);
            if (config.Hierarchy)
            {
                var cat = categories.FirstOrDefault(e => e.ID == model.Article.CATEGORYID);
                if (cat != null) seo.SeoUrl = string.Format("/{0}/{1}", cat.NAME.Trim().ConvertToUnSign(), model.Article.TITLE.Trim().ConvertToUnSign());
                else seo.SeoUrl = "/" + model.Article.TITLE.Trim().ConvertToUnSign();
            }
            else seo.SeoUrl = "/" + model.Article.TITLE.Trim().ConvertToUnSign();

            seo.Title = model.Article.TITLE.Trim();
            seo.MetaKeyWork = model.Article.TAG.Trim();
            if (!string.IsNullOrEmpty(model.Article.BRIEF))
            {
                seo.MetaDescription = model.Article.BRIEF.DeleteHTMLTag().Trim();
                seo.MetaDescription = seo.MetaDescription.Length > 200 ? seo.MetaDescription.Substring(0, 200) : seo.MetaDescription;
            }

            seoBLL.Save(seo, this.User.CompanyId, model.Article.LANGUAGEID);
            new URL().ClearCache(SettingsManager.Constants.AppCompanyDomainMapCache);
            // end SEO link

            model.Article = articleBLL.GetArticle(this.User.CompanyId, this.LanguageId, model.Article.ID);
                if (model.Article != null)
                    model.Article.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathArticleImage + model.Article.IMAGE;

            model.Articles = articleBLL.GetDataSimple(this.User.CompanyId, this.LanguageId, "ART", 0, true);
            foreach (var article in model.Articles)
            {
                article.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathArticleImage + article.ImagePath;
            }

            model.RelatiedArticles = articleBLL.GetRelatiedArticles(model.Article.ID, this.User.CompanyId, this.LanguageId);
            foreach (var article in model.RelatiedArticles)
            {
                article.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathArticleImage + article.ImagePath;
            }

            model.Tags = articleBLL.GetTagByCategoryId(this.User.CompanyId);

            return View(model);
        }

        public ActionResult Comment(int id = 0)
        {
            var model = new ArticleCommentViewModel();
            model.Comments = itemBLL.GetComments(id, this.User.CompanyId).ToList();
            var itemIds = model.Comments.Select(e => e.ITEMID).ToList();
            var articles = articleBLL.GetAllArticles(this.User.CompanyId, this.LanguageId)
                            .Where(e => itemIds.Contains(e.ID))
                            .ToList();

            model.Comments = model.Comments.Join(articles, c => c.ITEMID, a => a.ID, (c, a) => new ITEMCOMMENTModel
            {
                CLIENTID = c.CLIENTID,
                CONTENT = c.CONTENT,
                EMAIL = c.EMAIL,
                ID = c.ID,
                ITEMID = c.ITEMID,
                PHONE = c.PHONE,
                NAME = c.NAME,
                Date = c.Date,
                DisLike = c.DisLike,
                Like = c.Like,
                ItemName = a.TITLE
            }).ToList();

            model.ArticleId = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Comment(ArticleCommentViewModel model)
        {
            if(model.CommentId > 0)
                itemBLL.Delete(model.CommentId, this.User.CompanyId);

            model.Comments = itemBLL.GetComments(model.ArticleId, this.User.CompanyId).ToList();
            var itemIds = model.Comments.Select(e => e.ITEMID).ToList();
            var articles = articleBLL.GetAllArticles(this.User.CompanyId, this.LanguageId)
                            .Where(e => itemIds.Contains(e.ID))
                            .ToList();

            model.Comments = model.Comments.Join(articles, c => c.ITEMID, a => a.ID, (c, a) => new ITEMCOMMENTModel
            {
                CLIENTID = c.CLIENTID,
                CONTENT = c.CONTENT,
                EMAIL = c.EMAIL,
                ID = c.ID,
                ITEMID = c.ITEMID,
                PHONE = c.PHONE,
                ItemName = a.TITLE
            }).ToList();

            return View(model);
        }

        public ActionResult Link(int catid = 0)
        {
            var model = new ArticleURLViewModel();
            model.CatId = catid;
            model.Link.ID = 0;
            model.Link.ORDERS = 50;
            model.Link.PUBLISH = true;
            model.Link.LANGUAGEID = this.LanguageId;

            var data = articleBLL.GetLinks(this.User.CompanyId, this.LanguageId, model.CatId).OrderByDescending(e => e.ID).ToList();
            model.Links = data;
            foreach (var article in model.Links)
            {
                article.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathArticleImage + article.IMAGE;
            }

            var categories = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, "LIN", model.CatId).ToList();
            model.Categories = SortTable(categories, 0).Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();

            model.Languages = companyBLL.GetLanguage()
                                .Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();

            model.Targets = new List<SelectListItem>() {
                new SelectListItem {Value = "_blank" , Text = "_blank"},
                new SelectListItem {Value = "_self" , Text = "_self"},
                new SelectListItem {Value = "_parent" , Text = "_parent"},
                new SelectListItem {Value = "_top" , Text = "_top"},
            };

            ViewBag.Token = this.User.Token;
            return View(model);
        }

        [HttpPost]
        public ActionResult Link(ArticleURLViewModel model)
        {
            var categories = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, "LIN", model.CatId).ToList();
            model.Categories = SortTable(categories, 0).Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();

            model.Languages = companyBLL.GetLanguage()
                                .Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();

            model.Targets = new List<SelectListItem>() {
                new SelectListItem {Value = "_blank" , Text = "_blank"},
                new SelectListItem {Value = "_self" , Text = "_self"},
                new SelectListItem {Value = "_parent" , Text = "_parent"},
                new SelectListItem {Value = "_top" , Text = "_top"},
            };

            var folder = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathArticleImage;
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
                            var logo = Request.Files["linkimage"];
                            if (logo.ContentLength > 0)
                            {
                                model.Link.IMAGE = string.Format("{0}_{1}.{2}", model.Link.TITLE.ConvertToUnSign(), DateTime.Now.Ticks, logo.FileName.Split('.')[1]);
                                logo.SaveAs(folder + model.Link.IMAGE);
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Danger = "Gửi file thật bại";
                        }
                    }

                    articleBLL.AddLink(model.Link, this.User.CompanyId, this.User.UserId);
                    break;
                case "UPDATE":
                    if (Request.Files.Count > 0)
                    {
                        try
                        {
                            var logo = Request.Files["linkimage"];
                            if (logo.ContentLength > 0)
                            {
                                var oldImage = model.Link.IMAGE;
                                model.Link.IMAGE = string.Format("{0}_{1}.{2}", model.Link.TITLE.ConvertToUnSign(), DateTime.Now.Ticks, logo.FileName.Split('.')[1]);

                                logo.SaveAs(folder + model.Link.IMAGE);

                                if (model.Link.IMAGE != oldImage && System.IO.File.Exists(folder + oldImage))
                                    System.IO.File.Delete(folder + oldImage);
                            }
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex.Message, ex);
                            ViewBag.Danger = "Gửi file thật bại";
                        }
                    }
                    articleBLL.UpdateLink(model.Link, this.User.CompanyId, this.User.UserId);
                    break;
                case "REMOVE":
                    itemBLL.Delete(model.Link.ID, this.User.CompanyId);
                    if (System.IO.File.Exists(folder + model.Link.IMAGE))
                        System.IO.File.Delete(folder + model.Link.IMAGE);
                    break;
            }

            var data = articleBLL.GetLinks(this.User.CompanyId, this.LanguageId, model.CatId).OrderByDescending(e => e.ID).ToList();
            model.Links = data;
            foreach (var article in model.Links)
            {
                article.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathArticleImage + article.IMAGE;
            }

            ViewBag.Token = this.User.Token;
            return View(model);
        }

        public ActionResult Documents(int catid = 0)
        {
            var model = new DocumentViewModel();
            model.CatId = catid;

            model.Documents = articleBLL.GetDocuments(this.User.CompanyId, this.LanguageId, model.CatId).OrderByDescending(e => e.ID).ToList();
            foreach (var article in model.Documents)
            {
                article.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathDocumentFile + article.IMAGE;
            }

            var categories = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, "DOC", model.CatId).ToList();
            model.Categories = SortTable(categories, 0).Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();

            ViewBag.Token = this.User.Token;
            return View(model);
        }

        [HttpPost]
        public ActionResult Documents(DocumentViewModel model)
        {
            var categories = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, "DOC", model.CatId).ToList();
            model.Categories = SortTable(categories, 0).Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();

            switch (model.Action)
            {
                case "REMOVE":
                    var folder = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathDocumentFile;
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    itemBLL.Delete(model.DocumentId, this.User.CompanyId);
                    if (System.IO.File.Exists(folder + model.Image))
                        System.IO.File.Delete(folder + model.Image);
                    if (System.IO.File.Exists(folder + model.FileName))
                        System.IO.File.Delete(folder + model.FileName);
                    break;
            }

            model.Documents = articleBLL.GetDocuments(this.User.CompanyId, this.LanguageId, model.CatId).OrderByDescending(e => e.ID).ToList();
            foreach (var article in model.Documents)
            {
                article.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathDocumentFile + article.IMAGE;
            }

            return View(model);
        }

        public ActionResult Document(int id = 0, string language = "", int catid = 0)
        {
            var model = new DocumentDetailViewModel();
            model.CatId = catid;

            if (string.IsNullOrEmpty(language)) language = this.LanguageId;

            if (id > 0)
            {
                model.Document = articleBLL.GetDocument(this.User.CompanyId, language, id);
                if (model.Document != null)
                {
                    model.Document.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathDocumentFile + model.Document.IMAGE;
                    if(string.IsNullOrEmpty(model.Document.FileUrl))
                        model.Document.FileUrl = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathDocumentFile + model.Document.FileName;
                }
                else model.Document = new Model.DocumentModel();
            }
            else model.Document = new Model.DocumentModel();

            if (model.Document.ID == 0)
            {
                model.Document.ORDERS = 50;
                model.Document.PUBLISH = true;
                model.Document.LANGUAGEID = language;
            }
            var categories = articleBLL.GetCategoryByType(this.User.CompanyId, language, "DOC", model.CatId)
                                .ToList();
            model.Categories = SortTable(categories, 0).Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.Blank + e.NAME }).ToList();

            model.Languages = companyBLL.GetLanguage()
                                .Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();

            return View(model);
        }

        [HttpPost, ValidateInput(false)] 
        public ActionResult Document(DocumentDetailViewModel model)
        {
            var categories = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, "DOC", model.CatId)
                                .ToList();
            model.Categories = SortTable(categories, 0).Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.Blank + e.NAME }).ToList();

            model.Languages = companyBLL.GetLanguage()
                                .Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();

            var folder = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathDocumentFile;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var oldImage = model.Document.IMAGE;
            var oldFile = model.Document.FileName;

            if (Request.Files.Count > 0)
            {
                try
                {
                    var logo = Request.Files["articleimage"];
                    if (logo.ContentLength > 0)
                    {
                        model.Document.IMAGE = string.Format("{0}.{1}", model.Document.TITLE.ConvertToUnSign(), logo.FileName.Split('.')[1]);
                        logo.SaveAs(folder + model.Document.IMAGE);

                        if (model.Document.IMAGE != oldImage && System.IO.File.Exists(folder + oldImage))
                            System.IO.File.Delete(folder + oldImage);
                    }

                    var file = Request.Files["documentfile"];
                    if (file.ContentLength > 0)
                    {
                        model.Document.Size = file.ContentLength;
                        model.Document.Type = file.FileName.Split('.')[1];
                        model.Document.FileName = string.Format("{0}.{1}", model.Document.TITLE.ConvertToUnSign(), model.Document.Type);
                        
                        file.SaveAs(folder + model.Document.FileName);

                        if (model.Document.FileName != oldFile && System.IO.File.Exists(folder + oldFile))
                            System.IO.File.Delete(folder + oldFile);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Danger = "Gửi file thật bại";
                }
            }

            if (model.Document.ID > 0) articleBLL.UpdateDocument(model.Document, this.User.CompanyId, this.User.UserId);
            else model.Document.ID = articleBLL.AddDocument(model.Document, this.User.CompanyId, this.User.UserId);

            // SEO link
            var url = new URL();

            var seo = new SEOLinkModel();
            seo.RefItem = model.Document.ID;

            seo.Url = url.LinkComponent("Document", SettingsManager.Constants.SendDocument + "/" + model.Document.ID + "/" + model.Document.TITLE.ConvertToUnSign());

            var config = this.companyBLL.GetConfig(this.User.CompanyId);
            if (config.Hierarchy)
            {
                var cat = categories.FirstOrDefault(e => e.ID == model.Document.CATEGORYID);
                if (cat != null) seo.SeoUrl = string.Format("/{0}/{1}", cat.NAME.Trim().ConvertToUnSign(), model.Document.TITLE.Trim().ConvertToUnSign());
                else seo.SeoUrl = "/" + model.Document.TITLE.Trim().ConvertToUnSign();
            }
            else seo.SeoUrl = "/" + model.Document.TITLE.Trim().ConvertToUnSign();

            seo.Title = model.Document.TITLE.Trim();
            seo.MetaKeyWork = model.Document.TITLE.Trim();
            if (!string.IsNullOrEmpty(model.Document.BRIEF))
            {
                seo.MetaDescription = model.Document.BRIEF.DeleteHTMLTag().Trim();
                seo.MetaDescription = seo.MetaDescription.Length > 200 ? seo.MetaDescription.Substring(0, 200) : seo.MetaDescription;
            }

            seoBLL.Save(seo, this.User.CompanyId, model.Document.LANGUAGEID);
            new URL().ClearCache(SettingsManager.Constants.AppCompanyDomainMapCache);
            // end SEO link

            model.Document = articleBLL.GetDocument(this.User.CompanyId, this.LanguageId, model.Document.ID);
            if (model.Document != null)
            {
                model.Document.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathDocumentFile + model.Document.IMAGE;
                if (string.IsNullOrEmpty(model.Document.FileUrl))
                    model.Document.FileUrl = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathDocumentFile + model.Document.FileName;
            }

            return View(model);
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