namespace Web.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Web.Business;
    using Web.Model;

    public class ComponentController : Controller
    {
        private CompanyBLL companyBLL;
        private TemplateBLL templateBLL;
        private ModuleBLL moduleBLL;

        public CompanyConfigModel Web { get; set; }
        public string ComponentName { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            companyBLL = new CompanyBLL();
            templateBLL = new TemplateBLL();
            moduleBLL = new ModuleBLL();

            var domain = this.Request.Url.Authority;
            Web = companyBLL.GetCompanyByDomain(domain);
            ComponentName = this.ControllerContext.RouteData.Values["Component"].ToString();
        }

        public ActionResult Index()
        {
            ViewBag.Position = new Dictionary<string, string>();
            
            var TemoaltePath = string.Format("~/Views/{0}/{0}.cshtml", Web.Template);
            var ViewPath = string.Format("~/Views/{0}/Component/{1}.cshtml", Web.Template, ComponentName);

            var positionComponents = templateBLL.GetAllPositionComponents(Web.Template, ComponentName).ToList();
            var modules = moduleBLL.GetAllModuleConfigs(this.Web.ID, this.Web.Language, Web.Template, ComponentName, inTemplate: false).ToList();
            var moduleIds = modules.Select(e => e.Id).ToList();
            var param = moduleBLL.GetParamConfigs(this.Web.ID, moduleIds).ToList();
            foreach (var position in positionComponents)
            {
                ViewBag.Position[position.ID] = "";
                var modulePositons = modules.Where(e => e.Position == position.ID).ToList();
                foreach (var module in modulePositons)
                {
                    var pars = param.Where(e => e.ModuleId == module.Id).ToList();
                    var mopa = GetParam(pars);
                    var html = GetModule(module.ModuleName, module.SkinName, module.Title, mopa);
                    ViewBag.Position[position.ID] += html.Content;
                }
            }

            return View(viewName: ViewPath, masterName: TemoaltePath);
        }

        public ContentResult Position(string PositionName)
        {
            var ComponentName = ControllerContext.ParentActionViewContext.RouteData.Values["action"].ToString();
            var modules = moduleBLL.GetAllModuleConfigs(this.Web.ID, this.Web.Language, Web.Template, ComponentName, PositionName, inTemplate: true).ToList();
            var moduleIds = modules.Select(e => e.Id).ToList();
            var param = moduleBLL.GetParamConfigs(this.Web.ID, moduleIds).ToList();
            var content = string.Empty;
            foreach (var module in modules)
            {
                var pars = param.Where(e => e.ModuleId == module.Id).ToList();
                var mopa = GetParam(pars);
                var html = GetModule(module.ModuleName, module.SkinName, module.Title, mopa);
                content += html.Content;
            }

            return Content(content);
        }

        private ContentResult GetModule(string moduleName, string skinName, string title, Dictionary<string, object> param)
        {
            var assambly = Assembly.GetAssembly(typeof(ModuleController));
            var fullTypeName = string.Format("{0}.{1}Controller", typeof(ModuleController).Namespace, moduleName);
            var type = assambly.GetType(fullTypeName);
            var moduleController = (ModuleController)Activator.CreateInstance(type, new[] { Web });
            moduleController.ControllerContext = ControllerContext;
            var moduleType = moduleController.GetType();
            MethodInfo method = moduleType.GetMethod(moduleName);
            
            var html = method.Invoke(moduleController, new object[] { skinName, param, title });
            return html as ContentResult;
        }

        private Dictionary<string, object> GetParam(List<ModuleConfigParamModel> param)
        {
            var dic = new Dictionary<string, object>();
            foreach (var par in param)
            {
                dic[par.ID] = par.Value;
            }
            return dic;
        }
    }
}