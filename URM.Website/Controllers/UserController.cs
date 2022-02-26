namespace URM.Website.Controllers
{
    using Library;
    using Library.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using URM.Business;
    using URM.Model;
    using URM.Website.Models;

    public class UserController : BaseController
    {
        public ActionResult Index()
        {
            var api = new ApiHelper(this.User.Token);
            var model = new UserViewModel();

            var parent = this.User.UserId;
            if (this.User.Roles.Contains("AllAccount")) parent = 0;

            //model.Users = this.userBLL.GetAllChildUser(this.User.AppId, null, parent).ToList();
            var param = new Dictionary<string, object>();
            param["ParentId"] = parent;
            model.Users = api.GetAll<URMUserModel>(string.Format("http://{0}/odata/User", Request.Url.Authority), param).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(UserViewModel model)
        {
            var api = new ApiHelper(this.User.Token);
            switch (model.Action)
            {
                case "ADDUSER":
                    try
                    {
                        //this.userBLL.CreateAccount(model.UserModel, this.User.UserId, this.User.AppId);
                        model.UserModel = api.Insert<URMUserModel>(string.Format("http://{0}/odata/User", Request.Url.Authority), model.UserModel);
                        ViewBag.Error = "Lưu thành công";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }
                    break;
                case "UPDATEUSER":
                    try
                    {
                        //this.userBLL.DeleteAccount(model.AccountId, this.User.AppId);
                        api.Update<URMUserModel>(string.Format("http://{0}/odata/User", Request.Url.Authority), model.UserModel.ID, model.UserModel);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }
                    break;
                case "DELETEUSER":
                    try
                    {
                        //this.userBLL.UpdateAccount(model.UserModel, this.User.AppId);
                        api.Delete(string.Format("http://{0}/odata/User", Request.Url.Authority), model.AccountId);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }
                    break;
            }

            var parent = this.User.UserId;
            if (this.User.Roles.Contains("AllAccount")) parent = 0;
            var param = new Dictionary<string, object>();
            param["ParentId"] = parent;
            model.Users = api.GetAll<URMUserModel>(string.Format("http://{0}/odata/User", Request.Url.Authority), param).ToList();

            return View(model);
        }

        public ActionResult Group()
        {
            base.CheckRole("Group");

            var api = new ApiHelper(this.User.Token);
            var model = new Models.GroupViewModel();

            model.Groups = api.GetAll<URMGroupModel>(string.Format("http://{0}/odata/Group", Request.Url.Authority)).ToList();
            var roles = api.GetAll<URMRoleModel>(string.Format("http://{0}/odata/Role", Request.Url.Authority));
            if (roles != null)
            {
                var groupNames = roles.Select(e => e.RoleGroup).Distinct().ToArray();
                foreach (var name in groupNames)
                {
                    model.Roles[name] = roles.Where(e => e.RoleGroup == name).ToList();
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Group(Models.GroupViewModel model)
        {
            base.CheckRole("Group");

            var api = new ApiHelper(this.User.Token);

            switch (model.Action)
            {
                case "ADDGROUP":
                    try
                    {
                        if (model.GroupName == "Administrator") ViewBag.Error = "Không được thêm nhóm tên Administrator";

                        //this.userBLL.CreateAccount(model.UserModel, this.User.UserId, this.User.AppId);
                        model.Group = api.Insert<Model.URMGroupModel>(string.Format("http://{0}/odata/Group", Request.Url.Authority), (Model.URMGroupModel)model.Group);
                        ViewBag.Error = "Lưu thành công";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }
                    break;
                case "DELETEGROUP":
                    try
                    {
                        if (model.GroupName == "Administrator") ViewBag.Error = "Nhóm Administrator không được xóa";

                        //this.userBLL.UpdateAccount(model.UserModel, this.User.AppId);
                        api.Delete(string.Format("http://{0}/odata/Group", Request.Url.Authority), model.GroupId);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }
                    break;
                case "UPDATEGROUP":
                    try
                    {
                        var vroles = Request["grouprole"];
                        if (!string.IsNullOrEmpty(vroles)) vroles = vroles.Replace(',', '|');
                        var group = new Model.URMGroupModel { ID = model.GroupId, Name = model.GroupName, Roles = vroles };
                        //this.userBLL.DeleteAccount(model.AccountId, this.User.AppId);
                        api.Update<Model.URMGroupModel>(string.Format("http://{0}/odata/Group", Request.Url.Authority), model.GroupId, group);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = ex.Message;
                    }
                    break;
            }

            model.Groups = api.GetAll<URMGroupModel>(string.Format("http://{0}/odata/Group", Request.Url.Authority)).ToList();
            var roles = api.GetAll<URMRoleModel>(string.Format("http://{0}/odata/Role", Request.Url.Authority));
            if (roles != null)
            {
                var groupNames = roles.Select(e => e.RoleGroup).Distinct().ToArray();
                foreach (var name in groupNames)
                {
                    model.Roles[name] = roles.Where(e => e.RoleGroup == name).ToList();
                }
            }

            return View(model);
        }

        public ActionResult SetRole(int userId)
        {

            var api = new ApiHelper(this.User.Token);
            var model = new SetRoleViewModel();

            model.User = api.GetOne<URMUserModel>(string.Format("http://{0}/odata/User", Request.Url.Authority), userId);
            if (model.User != null)
            {
                if (string.IsNullOrEmpty(model.User.Roles)) model.User.Roles = string.Empty;
                var param = new Dictionary<string, object>();
                param["UserId"] = this.User.UserId;
                var roles = api.GetAll<URMRoleModel>(string.Format("http://{0}/odata/Role", Request.Url.Authority), param);
                if (roles != null)
                {
                    var groupNames = roles.Select(e => e.RoleGroup).Distinct().ToArray();
                    foreach (var name in groupNames)
                    {
                        model.Roles[name] = roles.Where(e => e.RoleGroup == name).ToList();
                    }
                }
            }
            else RedirectToAction("Index");

            return View(model);
        }

        [HttpPost]
        public ActionResult SetRole(SetRoleViewModel model)
        {
            var api = new ApiHelper(this.User.Token);

            model.User = api.GetOne<URMUserModel>(string.Format("http://{0}/odata/User", Request.Url.Authority), model.User.ID);
            if (model.User != null)
            {
                switch (model.Action)
                {
                    case "SETROLE":
                        try
                        {
                            var sendModel = new URMUserAccountModel();
                            sendModel.ID = model.User.ID;
                            var vroles = Request["grouprole"];
                            if (!string.IsNullOrEmpty(vroles)) sendModel.Roles = vroles.Replace(',', '|');
                            sendModel = api.Insert(string.Format("http://{0}/odata/UserAccount", Request.Url.Authority), sendModel);
                            model.User.Roles = sendModel.Roles;
                            ViewBag.Error = "Lưu thành công";
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Error = ex.Message;
                        }
                        break;
                }

                if (string.IsNullOrEmpty(model.User.Roles)) model.User.Roles = string.Empty;
                var param = new Dictionary<string, object>();
                param["UserId"] = this.User.UserId;
                var roles = api.GetAll<URMRoleModel>(string.Format("http://{0}/odata/Role", Request.Url.Authority), param);
                if (roles != null)
                {
                    var groupNames = roles.Select(e => e.RoleGroup).Distinct().ToArray();
                    foreach (var name in groupNames)
                    {
                        model.Roles[name] = roles.Where(e => e.RoleGroup == name).ToList();
                    }
                }
            }
            else RedirectToAction("Index");

            return View(model);
        }

        public ActionResult UserGroup(int groupId = 0)
        {
            base.CheckRole("Group");

            var api = new ApiHelper(this.User.Token);
            var model = new Models.UserGroupViewModel();

            model.GroupId = groupId;
            model.Groups = api.GetAll<URMGroupModel>(string.Format("http://{0}/odata/Group", Request.Url.Authority))
                            .Select(e => new SelectListItem {
                                Text = e.Name,
                                Value = e.ID.ToString()
                            })
                            .ToList();

            var parent = this.User.UserId;
            var param = new Dictionary<string, object>();
            param["ParentId"] = parent;
            var users = api.GetAll<URMUserModel>(string.Format("http://{0}/odata/User", Request.Url.Authority), param);
            if (users != null)
            {
                model.UserGroups = users.Where(e => e.GroupId == model.GroupId).ToList();
                model.Users = users.Where(e => e.GroupId == 0).ToList();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult UserGroup(UserGroupViewModel model)
        {
            base.CheckRole("Group");

            var api = new ApiHelper(this.User.Token);

            try
            {
                var sendModel = new URMGroupUsersModel();
                sendModel.ID = model.GroupId;
                sendModel.UserIds = Request["userGroup_id"];
                api.Update(string.Format("http://{0}/odata/UserGroup", Request.Url.Authority), model.GroupId, sendModel);
            }
            catch (Exception ex)
            {

            }

            model.Groups = api.GetAll<URMGroupModel>(string.Format("http://{0}/odata/Group", Request.Url.Authority))
                            .Select(e => new SelectListItem
                            {
                                Text = e.Name,
                                Value = e.ID.ToString()
                            })
                            .ToList();
            var parent = this.User.UserId;
            var param = new Dictionary<string, object>();
            param["ParentId"] = parent;
            var users = api.GetAll<URMUserModel>(string.Format("http://{0}/odata/User", Request.Url.Authority), param);
            if (users != null)
            {
                model.UserGroups = users.Where(e => e.GroupId == model.GroupId).ToList();
                model.Users = users.Where(e => e.GroupId == 0).ToList();
            }

            return View(model);
        }
    }
}