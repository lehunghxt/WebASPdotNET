
namespace Web.Backend.Odata
{ 
    using Web.Business;
    using Web.Model;

    /// <summary>
    /// API dùng đổi và cấp mới mật khẩu
    /// </summary>
    public class UserAccountController : OdataBaseController<URMUserAccountModel, int>
    {
        private UserBLL bll;

        public UserAccountController()
        {
            this.bll = new UserBLL();
        }

        /// <summary>
        /// Reset mật khẩu
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected override URMUserAccountModel GetEntityByKey(int key)
        {
            var newPass =  bll.ResetPassword(key, this.User.AppId);
            return new URMUserAccountModel { ID = key, NewPassword = newPass, UserName = this.User.UserName };
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        protected override URMUserAccountModel Update(int key, URMUserAccountModel model)
        {
            model.ID = key;
            this.bll.ChangePassword(model, this.User.AppId);
            return model;
        }

        /// <summary>
        /// Set Quyền
        /// </summary>
        protected override URMUserAccountModel Insert(URMUserAccountModel model)
        {
            this.bll.UpdateRoleAccount(model.ID, model.Roles, this.User.AppId);
            return model;
        }
    }
}

