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
    using System.Web.Routing;
    using Web.Backend.Models;
    using Web.Business;
    using Web.Model;

    public class FileController : BaseController
    {
        private ArticleBLL articleBLL;
        private CompanyBLL companyBLL;
        private ItemBLL itemBLL;
        private FileBLL fileBLL;
        public FileController()
        {
            articleBLL = new ArticleBLL();
            companyBLL = new CompanyBLL();
            itemBLL = new ItemBLL();
            fileBLL = new FileBLL();
        }

        public ActionResult Index(int catid = 0)
        {
            var model = new CategoryViewModel();
            model.CatId = catid;
            model.Category.TYPEID = "MID";
            model.Category.PUBLISH = true;
            model.Category.ORDERS = 50;
            model.Category.LANGUAGEID = this.LanguageId;

            model.Languages = companyBLL.GetLanguage().ToList();

            var data = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, model.Category.TYPEID, model.CatId).ToList();
            model.Categories = SortTable(data, 0);
            foreach(var category in model.Categories)
            {
                category.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathCategoryImage + category.IMAGE;
            }

            ViewBag.Token = this.User.Token;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(CategoryViewModel model)
        {
            model.Languages = companyBLL.GetLanguage().ToList();

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
                                model.Category.IMAGE = string.Format("{0}.{1}", model.Category.NAME.ConvertToUnSign(), logo.FileName.Split('.')[1]);
                                logo.SaveAs(folder + model.Category.IMAGE);
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Danger = "Gửi file thật bại";
                        }
                    }

                    articleBLL.AddCategory(model.Category, this.User.CompanyId, this.User.UserId);
                    break;
                case "UPDATE":
                    if (Request.Files.Count > 0)
                    {
                        try
                        {
                            var logo = Request.Files["categoryimage"];
                            if (logo.ContentLength > 0)
                            {
                                var oldImage = model.Category.IMAGE;
                                model.Category.IMAGE = string.Format("{0}.{1}", model.Category.NAME.ConvertToUnSign(), logo.FileName.Split('.')[1]);

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
                    break;
                case "REMOVE":
                    articleBLL.RemoveCategory(model.Category.ID, this.User.CompanyId);
                    if (System.IO.File.Exists(folder + model.Category.IMAGE))
                        System.IO.File.Delete(folder + model.Category.IMAGE);
                    break;
            }

            var data = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, model.Category.TYPEID, model.CatId).ToList();
            model.Categories = SortTable(data, 0);
            foreach (var category in model.Categories)
            {
                category.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathCategoryImage + category.IMAGE;
            }

            ViewBag.Token = this.User.Token;
            return View(model);
        }

        public ActionResult Album(int catid = 0)
        {
            var model = new AlbumViewModel();
            model.CatId = catid;
            model.Category.TYPEID = "MID";
            model.Category.PUBLISH = true;
            model.Category.ORDERS = 50;
            model.Category.LANGUAGEID = this.LanguageId;

            var categories = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, "MID", model.CatId)
                                .ToList();
            model.Categories = SortTable(categories, 0).Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.Blank + e.NAME }).ToList();

            model.Languages = companyBLL.GetLanguage()
                               .Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                               .ToList();

            ViewBag.Danger = "";
            return View(model);
        }

        [HttpPost]
        public ActionResult Album(AlbumViewModel model)
        {
            ViewBag.Danger = "";

            var folderCat = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathCategoryImage;
            if (!Directory.Exists(folderCat))
            {
                Directory.CreateDirectory(folderCat);
            }

            var folderMed = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathMediaFile;
            if (!Directory.Exists(folderMed))
            {
                Directory.CreateDirectory(folderMed);
            }

            var fileNames = new Dictionary<string, int>();
                    if (Request.Files.Count > 0)
                    {
                        try
                        {
                            var logo = Request.Files["categoryimage"];
                            if (logo.ContentLength > 0)
                            {
                                model.Category.IMAGE = string.Format("{0}.{1}", model.Category.NAME.ConvertToUnSign(), logo.FileName.Split('.')[1]);
                                logo.SaveAs(folderCat + model.Category.IMAGE);
                            }

                            var files = Request.Files.GetMultiple("mediafile");
                    for(int i = 0; i < files.Count; i++)
                    {
                        if (files[i].ContentLength > 0)
                        {
                            var name = string.Format("{0}_{1}_{2}.{3}", files[i].ContentType.ToLower().Split('/')[0], model.Category.NAME.ConvertToUnSign(), i, files[i].FileName.Split('.')[1]);
                            files[i].SaveAs(folderMed + name);
                            fileNames[name] = files[i].ContentLength;
                        }
                    }

                }
                        catch (Exception ex)
                        {
                            ViewBag.Danger = "Gửi file thật bại. ";
                        }
                    }

                    articleBLL.AddAlbum(model.Category, this.User.CompanyId, this.User.UserId, fileNames);

            if (model.Category.ID > 0) return RedirectToAction("Media", new RouteValueDictionary(new { id = model.Category.ID, catid = model.CatId }));
            else { ViewBag.Danger += "Lưu dữ liệu thật bại"; }
            ViewBag.Token = this.User.Token;
            return View(model);
        }

        public ActionResult Media(int id, int catid = 0)
        {
            var model = new MediaViewModel();
            model.CatId = catid;
            model.Media.CategoryId = id;
            model.Media.LANGUAGEID = this.LanguageId;

            var categories = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, "MID", model.CatId)
                                .ToList();
            model.Categories = SortTable(categories, 0).Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.Blank + e.NAME }).ToList();

            model.Languages = companyBLL.GetLanguage()
                               .Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                               .ToList();

            model.Medias = fileBLL.GetMedias(this.User.CompanyId, this.LanguageId, model.Media.CategoryId).ToList();
            foreach(var file in model.Medias)
            {
                if(string.IsNullOrEmpty(file.FilePath))
                    file.FilePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathMediaFile + file.FileName;
                if (!string.IsNullOrEmpty(file.Poster))
                    file.PosterPath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathMediaFile + file.Poster;
            }

            ViewBag.Danger = "";

            ViewBag.Token = this.User.Token;
            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Media(MediaViewModel model)
        {

            var categories = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, "MID", model.CatId)
                            .ToList();
            model.Categories = SortTable(categories, 0).Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.Blank + e.NAME }).ToList();

            model.Languages = companyBLL.GetLanguage()
                               .Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                               .ToList();

            var folder = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathMediaFile;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            switch (model.Action)
            {
                case "REMOVE":
                    itemBLL.Delete(model.Media.ID, this.User.CompanyId);
                    if (System.IO.File.Exists(folder + model.Media.Poster))
                        System.IO.File.Delete(folder + model.Media.Poster);
                    if (System.IO.File.Exists(folder + model.Media.FileName))
                        System.IO.File.Delete(folder + model.Media.FileName);
                    break;
                case "ADD":
                    if (!string.IsNullOrEmpty(model.Media.TITLE))
                    {

                        var files = Request.Files.GetMultiple("file");
                        if (files.Count > 0 && files.Any(e => e.ContentLength > 0))
                        {
                            try
                            {

                                var poster = Request.Files["poster"];
                                if (poster.ContentLength > 0)
                                {
                                    model.Media.Poster = string.Format("poster_{0}.{1}", model.Media.TITLE.ConvertToUnSign(), poster.FileName.Split('.')[1]);
                                    poster.SaveAs(folder + model.Media.Poster);
                                }

                                for (int i = 0; i < files.Count; i++)
                                {
                                    if (files[i].ContentLength > 0)
                                    {
                                        model.Media.FileName = string.Format("{0}_{1}_{2}.{3}", files[i].ContentType.ToLower().Split('/')[0], model.Media.TITLE.ConvertToUnSign(), i, files[i].FileName.Split('.')[1]);
                                        files[i].SaveAs(folder + model.Media.FileName);
                                        model.Media.Type = files[i].ContentType.ToLower().Split('/')[0];
                                        fileBLL.AddMedia(model.Media, this.User.CompanyId, this.User.UserId);
                                    }
                                }

                                ViewBag.Danger = "";

                            }
                            catch (Exception ex)
                            {
                                ViewBag.Danger = "Gửi file thật bại. ";
                            }
                        }
                        else if (!string.IsNullOrEmpty(model.Media.FileUrl) || !string.IsNullOrEmpty(model.Media.Embed))
                        {
                            fileBLL.AddMedia(model.Media, this.User.CompanyId, this.User.UserId);
                        }

                        fileBLL.SaveChanges();
                    }
                    else
                    {
                        ViewBag.Danger = "Vui lòng nhập tiêu đề. ";
                    }
                    break;
                case "UPDATE":
                    if (Request.Files.Count > 0)
                    {
                        try
                        {
                            var poster = Request.Files["poster"];
                            if (poster.ContentLength > 0)
                            {
                                var oldPoster = model.Media.Poster;
                                model.Media.Poster = string.Format("poster_{0}.{1}", model.Media.TITLE.ConvertToUnSign(), poster.FileName.Split('.')[1]);
                                poster.SaveAs(folder + model.Media.Poster);

                                if (model.Media.Poster != oldPoster && System.IO.File.Exists(folder + oldPoster))
                                    System.IO.File.Delete(folder + oldPoster);
                            }

                            var file = Request.Files["file"];
                            if (file.ContentLength > 0)
                            {
                                model.Media.Type = file.ContentType.ToLower().Split('/')[0];

                                var oldFile = model.Media.FileName;
                                model.Media.FileName = string.Format("{0}_{1}.{2}", file.ContentType.ToLower().Split('/')[0], model.Media.TITLE.ConvertToUnSign(), file.FileName.Split('.')[1]);
                                file.SaveAs(folder + model.Media.FileName);

                                if (model.Media.FileName != oldFile && System.IO.File.Exists(folder + oldFile))
                                    System.IO.File.Delete(folder + oldFile);
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Danger = "Gửi file thật bại. ";
                        }
                    }
                    fileBLL.UpdateMedia(model.Media, this.User.CompanyId, this.User.UserId);

                    break;
            }

            model.Medias = fileBLL.GetMedias(this.User.CompanyId, this.LanguageId, model.Media.CategoryId).ToList();
            foreach (var file in model.Medias)
            {
                if (string.IsNullOrEmpty(file.FilePath))
                    file.FilePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathMediaFile + file.FileName;
                if (!string.IsNullOrEmpty(file.Poster))
                    file.PosterPath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathMediaFile + file.Poster;
            }

            ViewBag.Danger = "";

            ViewBag.Token = this.User.Token;
            return RedirectToAction("Media", new RouteValueDictionary(new { id = model.Media.CategoryId }));
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