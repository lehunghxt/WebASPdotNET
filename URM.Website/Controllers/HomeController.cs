namespace URM.Website.Controllers
{
    using Library;
    using Library.Web;
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using URM.Business;
    using URM.Model;
    using URM.Website.Models;

    public class HomeController : BaseController
    {
        private AppInfoBLL appBLL = new AppInfoBLL();

        public ActionResult Index()
        {
            var api = new ApiHelper(this.User.Token);
           

            var model = new ProfileModel();
            model.UserAccount.Account.ID = this.User.UserId;

            model.AppId = this.User.AppId;
            model.AppName = this.appBLL.GetAppName(this.User.AppId);
            model.Info = api.GetOne<URMUserInfoModel>(string.Format("http://{0}/odata/UserInfo", Request.Url.Authority), model.UserAccount.Account.ID);

            model.IsAdministrator = this.User.Roles.Contains("ReNameApp");

            ViewBag.Error = "";
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ProfileModel model)
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
                        api.Update(string.Format("http://{0}/odata/UserAccount", Request.Url.Authority), model.UserAccount.Account.ID, model.UserAccount.Account);
                        //this.userBLL.ChangePassword(model.UserAccount, this.User.AppId);
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
                        api.Update(string.Format("http://{0}/odata/UserInfo", Request.Url.Authority), model.Info.ID, model.Info);
                        //this.userBLL.ChangePassword(model.UserAccount, this.User.AppId);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorInfo = ex.Message;
                    }
                    break;
                case "RENAMEAPP":
                    try
                    {
                        if (string.IsNullOrEmpty(model.AppName)) throw new BusinessException("Tên ứng dụng không được rỗng");

                        appBLL.ReName(this.User.AppId, model.AppName);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorReName = ex.Message;
                    }
                    break;
            }


            return View(model);
        }

        public ActionResult Api()
        {
            CheckRole("Api");

            return View();
        }
    }
}