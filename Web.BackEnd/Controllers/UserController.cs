using System.Collections.Generic;
using System.Web.Mvc;
using Web.Backend.Controllers;
using Web.Backend.Models;
using Web.Business;
using Web.Model;
using System.Linq;
using System;
using Library.Web;
using Web.Asp.Provider;

namespace Web.BackEnd.Controllers
{
    public class UserController : BaseController
    {
        private ArticleBLL articleBLL;
        private CompanyBLL companyBLL;

        public UserController()
        {
            articleBLL = new ArticleBLL();
            companyBLL = new CompanyBLL();
        }

        public ActionResult Info()
        {
            var api = new ApiHelper(this.User.Token);


            var model = new ProfileModel();
            model.UserAccount.Account.ID = this.User.UserId;

            model.Info = api.GetOne<UserInfoModel>(string.Format("{0}/odata/UserInfo", SettingsManager.AppSettings.URMService), model.UserAccount.Account.ID);

            ViewBag.Error = "";
            return View(model);
        }

        [HttpPost]
        public ActionResult Info(ProfileModel model)
        {
            switch (model.Action)
            {
                case "CHANGEPASS":
                    try
                    {
                        if (string.IsNullOrEmpty(model.UserAccount.Account.Password)) throw new BusinessException("Mật khẩu cũ không được rỗng");
                        if (string.IsNullOrEmpty(model.UserAccount.Account.NewPassword)) throw new BusinessException("Mật khẩu mới không được rỗng");
                        if (string.IsNullOrEmpty(model.UserAccount.ConfirmNewPassword)) throw new BusinessException("Vui lòng nhập lại mật khẩu mới");
                        if (model.UserAccount.Account.NewPassword.Trim() != model.UserAccount.ConfirmNewPassword.Trim()) throw new BusinessException("Nhập lại mật khẩu không khớp");
                        if (model.UserAccount.Account.Password.Trim() == model.UserAccount.Account.NewPassword.Trim()) throw new BusinessException("Mật khẩu mới phải khác mật khẩu cũ");

                        var api = new ApiHelper(this.User.Token);
                        model.UserAccount.Account.ID = this.User.UserId;
                        api.Update(string.Format("{0}/odata/UserAccount", SettingsManager.AppSettings.URMService), model.UserAccount.Account.ID, model.UserAccount.Account);
                        //this.userBLL.ChangePassword(model.UserAccount, this.User.AppId);
                        ViewBag.ErrorChangePass = "Cập nhật thành công";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorChangePass = ex.Message;
                    }
                    break;
                case "UPDATEPROFILE":
                    try
                    {
                        if (string.IsNullOrEmpty(model.Info.FullName)) throw new BusinessException("Họ tên không được rỗng");
                        if (string.IsNullOrEmpty(model.Info.Phone)) throw new BusinessException("Số điện thoại không được rỗng");
                        if (string.IsNullOrEmpty(model.Info.Email)) throw new BusinessException("Email không được rỗng");
                        var api = new ApiHelper(this.User.Token);
                        model.Info.ID = this.User.UserId;
                        api.Update(string.Format("{0}/odata/UserInfo", SettingsManager.AppSettings.URMService), model.Info.ID, model.Info);
                        //this.userBLL.ChangePassword(model.UserAccount, this.User.AppId);
                        ViewBag.ErrorInfo = "Cập nhật thành công";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorInfo = ex.Message;
                    }
                    break;
            }


            return View(model);
        }

        // GET: Shared
        public ActionResult MenuShortcuts()
        {
            var model = new MenuShortcutViewModel();

            var dataCats = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, "", 0).ToList();
            model.Categories = SortTable(dataCats, 0);
            var dataShorts = companyBLL.GetMenuShortcut(this.User.CompanyId).ToList();
            model.Shortcuts = SortTable(dataShorts, 0);
            return View(model);
        }

        [HttpPost]
        public ActionResult MenuShortcuts(MenuShortcutViewModel model)
        {
            switch (model.Action)
            {
                case "ADD":
                case "UPDATE":
                    companyBLL.SaveMenuShortcut(model.Shortcut, this.User.CompanyId);
                    break;
                case "REMOVE":
                    companyBLL.RemoveMenuShortcut(model.Shortcut.Id, this.User.CompanyId);
                    break;
            }

            var dataCats = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, "", 0).ToList();
            model.Categories = SortTable(dataCats, 0);
            var dataShorts = companyBLL.GetMenuShortcut(this.User.CompanyId).ToList();
            model.Shortcuts = SortTable(dataShorts, 0);
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

        private IList<MenuShortcutModel> SortTable(IList<MenuShortcutModel> table, int? parentId, string space = "", string distance = "....")
        {
            var rows = table.Where(dto => dto.ParentId == parentId).ToList();
            if (!rows.Any()) return new List<MenuShortcutModel>();
            var sortData = new List<MenuShortcutModel>();
            foreach (var row in rows)
            {
                var spaceNext = space + distance;
                var dt = SortTable(table, row.Id, spaceNext, distance);
                row.Blank = space;
                //row.Title = space + row.Title;
                sortData.Add(row);

                if (dt.Count > 0) sortData.AddRange(dt);
            }
            return sortData;
        }
    }
}