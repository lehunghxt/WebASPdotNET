namespace Web.Backend.Controllers
{
    using Library;
    using Library.FileHelpers;
    using Library.Web;
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Web.Asp.Provider;
    using Web.Backend.Models;
    using Web.Business;
    using Web.Model;

    public class ConfigController : BaseController
    {
        private readonly CompanyBLL companyBLL;
        private TemplateBLL templateBLL;
        private ModuleBLL moduleBLL;
        private ItemBLL itemBLL;

        public ConfigController()
        {
            companyBLL = new CompanyBLL();
            templateBLL = new TemplateBLL();
            moduleBLL = new ModuleBLL();
            itemBLL = new ItemBLL();
        }

        public ActionResult Index()
        {
            var model = new ModuleConfigViewModel();
            model.Modules = moduleBLL.GetAllModuleConfigs(this.User.CompanyId, this.LanguageId).ToList();
            var moduleIds = model.Modules.Select(e => e.Id).ToList();
            model.Params = moduleBLL.GetParamConfigs(this.User.CompanyId, moduleIds).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ModuleConfigViewModel model)
        {
            switch(model.Action)
            {
                case "REMOVE": moduleBLL.RemoveModuleConfig(this.User.CompanyId, model.ModuleId);
                    break;
                case "PUBLISH":
                    itemBLL.ChangePublish(model.ModuleId, this.User.CompanyId);
                    break;
                case "ORDER":
                    itemBLL.ChangeOrder(model.ModuleId, this.User.CompanyId, model.ModuleOrder);
                    break;
            }

            model.Modules = moduleBLL.GetAllModuleConfigs(this.User.CompanyId, this.LanguageId).ToList();
            var moduleIds = model.Modules.Select(e => e.Id).ToList();
            model.Params = moduleBLL.GetParamConfigs(this.User.CompanyId, moduleIds).ToList();
            return View(model);
        }

        public ActionResult Module(int id = 0, string language= "", bool? inTemplate = null, string componentName = "", string moduleName = "")
        {
            var model = new ModuleConfigDetailViewModel();
            model.Config = companyBLL.GetConfig(this.User.CompanyId);
            model.CompanyId = this.User.CompanyId;
            if (!string.IsNullOrEmpty(language)) model.Config.DefaultLanguage = language;
            
            model.Languages = companyBLL.GetLanguage()
                                .Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();
            model.Components = templateBLL.GetAllComponents(model.Config.DefaultTemplate)
                                .Select(e => new SelectListItem
                                {
                                    Value = e.ComponentName,
                                    Text = e.ComponentName + " - " + e.Summary
                                }).ToList();
            model.Modules = moduleBLL.GetAllModues()
                                .Select(e => new SelectListItem
                                {
                                    Value = e.ModuleName.ToString(),
                                    Text = e.ModuleName + " - " + e.Summary
                                }).ToList();

            model.ModuleTypes = DataSource.ModuleTyleCollection;

            model.Module = moduleBLL.GetModuleConfig(this.User.CompanyId, model.Config.DefaultLanguage, id);
            if(model.Module == null)
            {
                model.Module = new ModuleConfigModel();
                model.Module.InTemplate = true;
                model.Module.Publish = true;
                model.Module.Orders = 50;
                model.Module.TemplateName = model.Config.DefaultTemplate;
                model.Module.LanguageId = model.Config.DefaultLanguage;
                if (!string.IsNullOrEmpty(moduleName)) model.Module.ModuleName = moduleName;
                else model.Module.ModuleName = model.Modules.FirstOrDefault().Value;
            }

            if (!string.IsNullOrEmpty(componentName)) model.Module.ComponentName = componentName;
            if (inTemplate != null) model.Module.InTemplate = inTemplate ?? true;
            if (model.Module.InTemplate)
            {
                model.Poitions = templateBLL.GetAllPositionTemplates(model.Module.TemplateName)
                                    .Select(e => new SelectListItem
                                    {
                                        Value = e.ID,
                                        Text = e.ID + " - " + e.Summary
                                    }).ToList();
            }
            else
            {
                model.Poitions = templateBLL.GetAllPositionComponents(model.Module.TemplateName, model.Module.ComponentName)
                                    .Select(e => new SelectListItem
                                    {
                                        Value = e.ID,
                                        Text = e.ID + " - " + e.Summary
                                    }).ToList();
            }

            model.Params = moduleBLL.GetParamConfig(model.Module.ModuleName, model.Module.Id).ToList();
            model.Skins = templateBLL.GetAllSkins(model.Module.TemplateName, model.Module.ModuleName)
                                .Select(e => new SelectListItem
                                {
                                    Value = e.ID,
                                    Text = e.ID + " - " + e.Summary
                                }).ToList();

            if (!string.IsNullOrEmpty(language)) model.Module.LanguageId = language;

            return View(model);
        }

        [HttpPost]
        public ActionResult Module(ModuleConfigDetailViewModel model)
        {
            model.CompanyId = this.User.CompanyId;
            model.Params = moduleBLL.GetParamConfig(model.Module.ModuleName, model.Module.Id).ToList();
            foreach(var param in model.Params)
            {
                try
                {
                    var value = Request[param.ID];
                    if (value == null) value = string.Empty;
                    if (value != param.Value) param.Value = value;
                }
                catch
                {
                    param.Value = string.Empty;
                }
            }

            model.Module.Id = moduleBLL.SaveModuleConfig(this.User.CompanyId, this.User.UserId, model.Module, model.Params);

            model.Languages = companyBLL.GetLanguage()
                                .Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();
            model.Components = templateBLL.GetAllComponents(model.Module.TemplateName)
                                .Select(e => new SelectListItem
                                {
                                    Value = e.ComponentName,
                                    Text = e.ComponentName + " - " + e.Summary
                                }).ToList();
            model.Modules = moduleBLL.GetAllModues()
                                .Select(e => new SelectListItem
                                {
                                    Value = e.ModuleName.ToString(),
                                    Text = e.ModuleName + " - " + e.Summary
                                }).ToList();

            model.ModuleTypes = DataSource.ModuleTyleCollection;
            
            if (model.Module.InTemplate)
            {
                model.Poitions = templateBLL.GetAllPositionTemplates(model.Module.TemplateName)
                                    .Select(e => new SelectListItem
                                    {
                                        Value = e.ID,
                                        Text = e.ID + " - " + e.Summary
                                    }).ToList();
            }
            else
            {
                model.Poitions = templateBLL.GetAllPositionComponents(model.Module.TemplateName, model.Module.ComponentName)
                                    .Select(e => new SelectListItem
                                    {
                                        Value = e.ID,
                                        Text = e.ID + " - " + e.Summary
                                    }).ToList();
            }

            model.Skins = templateBLL.GetAllSkins(model.Module.TemplateName, model.Module.ModuleName)
                                .Select(e => new SelectListItem
                                {
                                    Value = e.ID,
                                    Text = e.ID + " - " + e.Summary
                                }).ToList();
            
            return View(model);
        }

        public ActionResult Company(string language = "")
        {
            var model = new CompanyViewModel();

            model.Config = companyBLL.GetConfig(this.User.CompanyId);
            if (string.IsNullOrEmpty(language)) language = model.Config.DefaultLanguage;
            model.Company = companyBLL.GetCompanyInfo(this.User.CompanyId, language);
            model.Languages = companyBLL.GetLanguage().ToList();
            foreach(var lang in model.Languages)
            {
                if (lang.ID == model.Config.DefaultLanguage)
                {
                    lang.ISDEFAULT = true;
                    break;
                }
            }
            model.Domains = companyBLL.GetDomains(this.User.CompanyId).ToList();

            model.PathLogo = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathCompanyImage + model.Company.IMAGE;
            model.PathFavicon = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathCompanyImage + model.Config.WebIcon;

            if (string.IsNullOrEmpty(model.Config.Background)) model.TypeBackground = "none";
            else if (model.Config.Background.StartsWith("#")) model.TypeBackground = "color";
            else
            {
                model.TypeBackground = "image";
                model.PathBackground = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathCompanyImage + model.Config.Background;
            }

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Company(CompanyViewModel model)
        {
            model.Config.DefaultLanguage = Request["lstDefault"];

            if (Request.Files.Count > 0)
            {
                try
                {
                    var folder = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathCompanyImage;
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    var logo = Request.Files["logo"];
                    if (logo.ContentLength > 0)
                    {
                        var oldCompanyImage = model.Company.IMAGE;
                        model.Company.IMAGE = string.Format("{0}{1}.{2}", model.Company.DISPLAYNAME.ConvertToUnSign(), DateTime.Now.Ticks, logo.FileName.Split('.')[1]);
                        logo.SaveAs(folder + model.Company.IMAGE);
                        if (model.Company.IMAGE != oldCompanyImage && System.IO.File.Exists(folder + oldCompanyImage))
                            System.IO.File.Delete(folder + oldCompanyImage);
                    }

                    var favicon = Request.Files["favicon"];
                    if (favicon.ContentLength > 0)
                    {
                        var oldCompanyFav = model.Config.WebIcon;
                        model.Config.WebIcon = string.Format("favicon_{0}.ico", DateTime.Now.Ticks);
                        favicon.SaveAs(folder + model.Config.WebIcon);
                        if (model.Config.WebIcon != oldCompanyFav && System.IO.File.Exists(folder + oldCompanyFav))
                            System.IO.File.Delete(folder + oldCompanyFav);
                    }

                    var folderLang = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathLanguageFile;
                    if (!Directory.Exists(folderLang))
                    {
                        Directory.CreateDirectory(folderLang);
                    }
                    model.Languages = companyBLL.GetLanguage().ToList();
                    foreach (var lang in model.Languages)
                    {
                        var langxml = Request.Files["fup_" + lang.ID];
                        if (langxml.ContentLength > 0)
                        {
                            var langfile = lang.ID + ".xml";
                            langxml.SaveAs(folderLang + langfile);

                            // luu ra web ngoai thong qua ftp
                            var ftp = new FTPHelper();
                            ftp.FTPServerIP = SettingsManager.AppSettings.FTPServerIP;
                            ftp.FTPRootPath = SettingsManager.AppSettings.FTPRootPath + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathLanguageFile;
                            ftp.FTPUserID = SettingsManager.AppSettings.FTPUserID;
                            ftp.FTPassword = SettingsManager.AppSettings.FTPPassword;
                            ftp.Upload(folderLang + langfile);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Danger = "Gửi file thật bại";
                    log.Error("Gửi file thật bại", ex);
                }
            }
            
            if (model.TypeBackground == "none") model.Config.Background = "";
            else if (model.TypeBackground == "color") model.Config.Background = Request["backgroundcolor"];
            else if (model.TypeBackground == "image")
            {
                var folder = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathCompanyImage;
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var background = Request.Files["background"];
                if (background.ContentLength > 0)
                {
                    var oldBackgroundImage = model.Config.Background;
                    model.Config.Background = string.Format("{0}{1}.{2}", model.Company.DISPLAYNAME.ConvertToUnSign(), DateTime.Now.Ticks, background.FileName.Split('.')[1]);
                    background.SaveAs(folder + model.Config.Background);
                    if (model.Config.Background != oldBackgroundImage && System.IO.File.Exists(folder + oldBackgroundImage))
                        System.IO.File.Delete(folder + oldBackgroundImage);
                }
            }

            companyBLL.UpdateInfo(model.Company);
            companyBLL.UpdateConfig(model.Config);

            if (model.AutoUpdateSiteMap)
            {
                var sitemapPath = (new URL()).PhysicalPath(SettingsManager.Constants.PathCompanyImage, model.Company.ID);
                var sitemapFile = sitemapPath + model.Company.ID + "-sitemap.xml";
                IOStream.CreateFolder(sitemapPath);

                if (System.IO.File.Exists(sitemapFile)) System.IO.File.Delete(sitemapFile);

                var HREF = new URL();
                var seoBLL = new SEOBLL();
                var links = seoBLL.GetAll(model.Company.ID);

                var productBLL = new ProductBLL();
                var arrProduct = productBLL.GetTagByCategoryId(model.Company.ID, 0, true);
                foreach (var tag in arrProduct)
                {
                    var tags = tag.Split(',');
                    foreach (var onetag in arrProduct)
                    {
                        var tempURL = "/Products/vit/" + SettingsManager.Constants.SendTag + "/" + onetag.Trim().ConvertToUnSign().Trim();
                        if (!string.IsNullOrEmpty(onetag.Trim()) && !links.Any(e => e.Url == tempURL))
                        {
                            var url = new SEOLinkModel();
                            url.Title = onetag.Trim();
                            url.MetaKeyWork = tag.Trim();
                            url.MetaDescription = tag.Trim();
                            url.Url = tempURL;
                            url.SeoUrl = tempURL.ToLower();
                            links.Add(url);
                        }
                    }
                }

                var artcleBLL = new ArticleBLL();
                var arrArticle = artcleBLL.GetTagByCategoryId(model.Company.ID, 0, true);
                foreach (var tag in arrArticle)
                {
                    var tags = tag.Split(',');
                    foreach (var onetag in tags)
                    {
                        var tempURL = "/Articles/vit/" + SettingsManager.Constants.SendTag + "/" + onetag.Trim().ConvertToUnSign().Trim();                       
                        if (!string.IsNullOrEmpty(onetag.Trim()) && !links.Any(e => e.Url == tempURL))
                        {
                            var url = new SEOLinkModel();
                            url.Title = onetag.Trim();
                            url.MetaKeyWork = tag.Trim();
                            url.MetaDescription = tag.Trim();
                            url.Url = tempURL;
                            url.SeoUrl = tempURL.ToLower();
                            links.Add(url);
                        }
                    }
                }

                var domain = companyBLL.GetDomains(model.Company.ID).FirstOrDefault();

                var sitemap = new SiteMapProcess(domain, sitemapFile, model.Company.ID);
                sitemap.CreateSiteMap(links);

                // luu ra web ngoai thong qua ftp
                var ftp = new FTPHelper();
                ftp.FTPServerIP = SettingsManager.AppSettings.FTPServerIP;
                ftp.FTPRootPath = SettingsManager.AppSettings.FTPRootPath;
                ftp.FTPUserID = SettingsManager.AppSettings.FTPUserID;
                ftp.FTPassword = SettingsManager.AppSettings.FTPPassword;
                ftp.Upload(sitemapFile);
            }

            model.Config = companyBLL.GetConfig(this.User.CompanyId);
            model.Company = companyBLL.GetCompanyInfo(this.User.CompanyId, model.Company.LANGUAGEID);
            model.Languages = companyBLL.GetLanguage().ToList();
            foreach (var language in model.Languages)
            {
                if (language.ID == model.Config.DefaultLanguage)
                {
                    language.ISDEFAULT = true;
                    break;
                }
            }
            model.Domains = companyBLL.GetDomains(this.User.CompanyId).ToList();

            model.PathLogo = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathCompanyImage + model.Company.IMAGE;
            model.PathFavicon = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathCompanyImage + model.Config.WebIcon;

            if (string.IsNullOrEmpty(model.Config.Background)) model.TypeBackground = "none";
            else if (model.Config.Background.StartsWith("#")) model.TypeBackground = "color";
            else
            {
                model.TypeBackground = "image";
                model.PathBackground = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathCompanyImage + model.Config.Background;
            }

            return View(model);
        }

        public ActionResult ThirdParty(string type)
        {
            var model = new ThirdPartyViewModel();
            var config = companyBLL.GetConfig(this.User.CompanyId);
            model.ThirdParty.IsPublished = true;
            model.ThirdParty.TemplateName = config.DefaultTemplate;

            model.Poitions = templateBLL.GetAllPositionTemplates(model.ThirdParty.TemplateName)
                                    .Select(e => new SelectListItem
                                    {
                                        Value = e.ID,
                                        Text = e.ID + " - " + e.Summary
                                    }).ToList();

            model.ThirdParties = companyBLL.GetThirdPartyByWebConfigId(this.User.CompanyId).ToList();
            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ThirdParty(ThirdPartyViewModel model)
        {
            model.Poitions = templateBLL.GetAllPositionTemplates(model.ThirdParty.TemplateName)
                                    .Select(e => new SelectListItem
                                    {
                                        Value = e.ID,
                                        Text = e.ID + " - " + e.Summary
                                    }).ToList();

            switch (model.Action)
            {
                case "SAVE":
                    companyBLL.SaveThirdParty(model.ThirdParty, this.User.CompanyId, this.User.UserId);
                    break;
                case "PUBLISH":
                    itemBLL.ChangePublish(model.ThirdParty.Id, this.User.CompanyId);
                    break;
                case "REMOVE":
                    companyBLL.RemoveThirdParty(model.ThirdParty.Id, this.User.CompanyId);
                    break;
            }

            model.ThirdParties = companyBLL.GetThirdPartyByWebConfigId(this.User.CompanyId).ToList();
            return View(model);
        }
    }
}