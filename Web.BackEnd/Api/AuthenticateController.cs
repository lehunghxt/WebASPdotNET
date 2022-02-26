namespace Web.BackEnd.Api
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Security;
    using System.Web;
    using Web.Business;
    using Web.Model;
    using System.Web.Http;
    using System;

    public class AuthenticateController : ApiController
    {
        private UserBLL bll;

        public AuthenticateController()
        {
            this.bll = new UserBLL();
        }

        /// <summary>
        /// The login.
        /// </summary>
        public HttpResponseMessage Post(URMUserLoginModel model)
        {
            var userId = this.bll.CheckUser(model.UserName, model.Password, model.ApplicationId);
            if (userId > 0)
            {
                var user = this.bll.GetProfileByUserName(model.UserName, model.ApplicationId);
                if (user == null) return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Lỗi dữ liệu - Vui lòng liên hệ bộ phận kỹ thuật");

                FormsAuthentication.SetAuthCookie(model.UserName, model.CreatePersistentCookie);

                // token
                HttpCookie cookie = FormsAuthentication.GetAuthCookie(model.UserName, model.CreatePersistentCookie);
                user.Token = cookie.Value;

                return this.Request.CreateResponse(HttpStatusCode.Created, user, "application/json");
            }

            return this.Request.CreateErrorResponse(
                HttpStatusCode.BadRequest, "Tài khoản hoặc mật khẩu không đúng");
        }

        public HttpResponseMessage ForgetPassword(URMForgetPasswordModel model)
        {
            var newPass = this.bll.ForgetPassword(model.UserName, model.Email, model.ApplicationId);
            if (string.IsNullOrEmpty(newPass)) return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Tài khoản hoặc Email không đúng");

            var response = this.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(newPass, System.Text.Encoding.Unicode, "text/plain");
            return response;
        }

        public HttpResponseMessage ChangePassword(URMChangePasswordModel model)
        {
            try
            {
                this.bll.ChangePassword(new URMUserAccountModel { UserName = model.Email, NewPassword = model.Password }, model.ApplicationId);

                var response = this.Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(model.Password, System.Text.Encoding.Unicode, "text/plain");
                return response;
            }
            catch (Exception ex)
            {
                var response = this.Request.CreateResponse(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(ex.Message, System.Text.Encoding.Unicode, "text/plain");
                return response;
            }
        }
    }
}
