namespace URM.Website.Controllers
{
    using Library.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using URM.Business;
    using URM.Model;
    using URM.Website.Models;

    public class RoleController : BaseController
    {
        AppInfoBLL appBLL = new AppInfoBLL();

        public ActionResult Index()
        {
            var model = new RoleGroupModel();
            var api = new ApiHelper(this.User.Token);

            var param = new Dictionary<string, object>();
            param["$orderby"] = "RoleGroup,RoleName";
            
            if (!this.User.Roles.Contains("Role")) param["UserName"] = this.User.UserName;
            var data = api.GetAll<URMRoleModel>(string.Format("http://{0}/odata/Role", Request.Url.Authority), param);
            if (data != null)
            {
                var groupNames = data.Select(e => e.RoleGroup).Distinct().ToArray();
                foreach (var name in groupNames)
                {
                    model.Groups[name] = data.Where(e => e.RoleGroup == name).ToList();
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(RoleGroupModel model)
        {
            base.CheckRole("Role");

            switch (model.Action)
            {
                case "ADDROLE":
                    try
                    {
                        this.appBLL.InsertRole(model.Role, this.User.AppId);
                        this.appBLL.UpdateFullRoleForAdmin(this.User.UserName, this.User.AppId);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }
                    break;
                case "DELETEROLE":
                    try
                    {
                        this.appBLL.DeleteRole(model.RoleId, this.User.AppId);
                        this.appBLL.UpdateFullRoleForAdmin(this.User.UserName, this.User.AppId);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }
                    break;
            }

            var api = new ApiHelper(this.User.Token);

            var param = new Dictionary<string, object>();
            param["$orderby"] = "RoleGroup,RoleName";

            var data = api.GetAll<URMRoleModel>(string.Format("http://{0}/odata/Role", Request.Url.Authority, param));
            if (data != null)
            {
                var groupNames = data.Select(e => e.RoleGroup).Distinct().ToArray();
                foreach (var name in groupNames)
                {
                    model.Groups[name] = data.Where(e => e.RoleGroup == name).ToList();
                }
            }

            return View(model);
        }
    }
}