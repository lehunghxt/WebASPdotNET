namespace Web.Backend.Controllers
{
    using Asp.Provider;
    using System;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using Web.Backend.Models;
    using Web.Business;
    using Web.Model;

    public class TemplateController : BaseController
    {
        private TemplateBLL templateBLL;
        private ModuleBLL moduleBLL;

        public TemplateController()
        {
            templateBLL = new TemplateBLL();
            moduleBLL = new ModuleBLL();
        }

        public ActionResult Index()
        {
            var model = new TemplateViewModel();
            model.Templates = templateBLL.GetAllTemplates().ToList();
            model.Positions = templateBLL.GetAllPositionTemplates().ToList();
            model.Skins = templateBLL.GetAllSkins().ToList();
            model.Modules = moduleBLL.GetAllModues().ToList();

            foreach (var template in model.Templates)
            {
                template.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, 0) + SettingsManager.Constants.PathTemplateImage + template.ImageName;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(TemplateViewModel model)
        {
            model.Skin.TemplateName = model.Template.TemplateName;
            model.Position.TemplateName = model.Template.TemplateName;

            var folder = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + string.Format(SettingsManager.AppSettings.FolderUpload, 0) + SettingsManager.Constants.PathTemplateImage;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            switch (model.Action)
            {
                case "ADDTEMP":
                    if (Request.Files.Count > 0)
                    {
                        try
                        {
                            var logo = Request.Files["templateimage"];
                            if (logo.ContentLength > 0)
                            {
                                model.Template.ImageName = string.Format("{0}.{1}", model.Template.TemplateName, logo.FileName.Split('.')[1]);
                                logo.SaveAs(folder + model.Template.ImageName);
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Danger = "Gửi file thật bại";
                        }
                    }
                    templateBLL.AddTemplate(model.Template);
                    break;
                case "UPDATETEMP":
                    if (Request.Files.Count > 0)
                    {
                        try
                        {
                            var logo = Request.Files["templateimage"];
                            if (logo.ContentLength > 0)
                            {
                                var oldImage = model.Template.ImageName;
                                model.Template.ImageName = string.Format("{0}.{1}", model.Template.TemplateName, logo.FileName.Split('.')[1]);

                                logo.SaveAs(folder + model.Template.ImageName);

                                if (model.Template.ImageName != oldImage && System.IO.File.Exists(folder + oldImage))
                                    System.IO.File.Delete(folder + oldImage);
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Danger = "Gửi file thật bại";
                        }
                    }
                    templateBLL.UpdateTemplate(model.Template);
                    break;
                case "REMOVETEMP":
                    templateBLL.RemoveTemplate(model.Template);
                    if (System.IO.File.Exists(folder + model.Template.ImageName))
                        System.IO.File.Delete(folder + model.Template.ImageName);
                    break;
                case "ADDSKIN":
                    templateBLL.AddSkinTemplate(model.Skin);
                    break;
                case "REMOVETSKIN":
                    templateBLL.RemoveSkinTemplate(model.Skin);
                    break;
                case "ADDPOS":
                    templateBLL.AddPositionTemplate(model.Position);
                    break;
                case "REMOVEPOS":
                    templateBLL.RemovePositionTemplate(model.Position);
                    break;
            }

            model.Templates = templateBLL.GetAllTemplates().ToList();
            model.Positions = templateBLL.GetAllPositionTemplates().ToList();
            model.Skins = templateBLL.GetAllSkins().ToList();
            model.Modules = moduleBLL.GetAllModues().ToList();

            foreach (var template in model.Templates)
            {
                template.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, 0) + SettingsManager.Constants.PathTemplateImage + template.ImageName;
            }

            return View(model);
        }

        public ActionResult Components(string template)
        {
            var model = new ComponentViewModel();
            model.Component.TemplateName = template;
            model.Components = templateBLL.GetAllComponents(template).ToList();
            model.Positions = templateBLL.GetAllPositionComponents(template).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Components(ComponentViewModel model)
        {
            switch (model.Action)
            {
                case "ADDCOM":
                    templateBLL.AddComponentemplate(model.Component);
                    break;
                case "REMOVECOM":
                    templateBLL.RemoveComponentTemplate(model.Component);
                    break;
                case "ADDPOS":
                    model.Position.TemplateName = model.Component.TemplateName;
                    model.Position.ComponentName = model.Component.ComponentName;   
                    templateBLL.AddPositionComponent(model.Position);
                    break;
                case "REMOVEPOS":
                    model.Position.TemplateName = model.Component.TemplateName;
                    model.Position.ComponentName = model.Component.ComponentName;
                    templateBLL.RemovePositionComponent(model.Position);
                    break;
            }

            model.Components = templateBLL.GetAllComponents(model.Component.TemplateName).ToList();
            model.Positions = templateBLL.GetAllPositionComponents(model.Component.TemplateName).ToList();

            return View(model);
        }

        public ActionResult Modules()
        {
            var model = new ModuleViewModel();
            model.Modules = moduleBLL.GetAllModues().ToList();
            model.Params = moduleBLL.GetParamInModule().ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Modules(ModuleViewModel model)
        {
            switch (model.Action)
            {
                case "REMOVE":
                    moduleBLL.RemoveModule(model.Module);
                    break;
                case "ADD":
                    moduleBLL.AddModule(model.Module);
                    break;
                case "UPDATE":
                    moduleBLL.UpdateModule(model.Module);
                    break;
                case "REMOVEPAR":
                    model.Param.ModuleName = model.Module.ModuleName;
                    moduleBLL.RemoveParamModule(model.Param);
                    break;
            }

            model.Modules = moduleBLL.GetAllModues().ToList();
            model.Params = moduleBLL.GetParamInModule().ToList();

            return View(model);
        }

        public ActionResult Param(string module, string param = "")
        {
            var model = new ParamViewModel();
            model.Param = moduleBLL.GetParam(module, param);
            if (model.Param == null)
            {
                model.Param = new ModuleParamModel();
                model.Param.ModuleName = module;
            }
            model.Types = DataSource.DataTyleCollection;

            return View(model);
        }

        [HttpPost]
        public ActionResult Param(ParamViewModel model)
        {
            var param = moduleBLL.GetParam(model.Param.ModuleName, model.Param.ID);
            if (param == null) moduleBLL.AddParamModule(model.Param);
            else moduleBLL.UpdateParamModule(model.Param);
            model.Types = DataSource.DataTyleCollection;

            return View(model);
        }
    }
}