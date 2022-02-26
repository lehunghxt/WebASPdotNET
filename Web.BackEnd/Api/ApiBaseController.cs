namespace Web.BackEnd.Api
{
    using Asp.Security;
    using Business;
    using log4net;
    using System;
    using System.Linq;
    using System.Security.Principal;
    using System.Threading;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Security;

    public class ApiBaseController : ApiController
    {
        public new UserPrincipal User;

        protected static readonly ILog log = LogManager.GetLogger(typeof(ApiBaseController));

        public ApiBaseController()
        {
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            var request = controllerContext.Request;
            var authHeaderVal = request.Headers.Authorization;

            // RFC 2617 sec 1.2, "scheme" name is case-insensitive
            if (authHeaderVal.Scheme.Equals("token", StringComparison.OrdinalIgnoreCase) && authHeaderVal.Parameter != null)
            {
                var tokens = authHeaderVal.Parameter.Split('|');
                if (tokens.Length == 3)
                {
                    var token = tokens[0];
                    var userNameInToken = tokens[2];
                    var appIdInToken = Convert.ToInt32(tokens[1]);

                    var ticket = FormsAuthentication.Decrypt(token);
                    if (ticket != null && !ticket.Expired && userNameInToken == ticket.Name)
                    {
                        var userName = ticket.Name;
                        var userBLL = new UserBLL();
                        var user = userBLL.GetProfileByUserName(userName, appIdInToken);
                        if (user != null)
                        {
                            var identity = new GenericIdentity(userName, "token");

                            var roles = user.Roles.Split('|');
                            var principal = new UserPrincipal(identity, roles);
                            principal.FullName = user.FullName;
                            principal.UserName = userName;
                            principal.Roles = roles;
                            principal.UserId = user.ID;
                            principal.AppId = user.AppId;


                            Thread.CurrentPrincipal = principal;
                            HttpContext.Current.User = principal;
                            this.User = principal;
                        }
                        else
                        HttpContext.Current.Response.StatusCode = 401;
                    }
                    else HttpContext.Current.Response.StatusCode = 401;
                }
                else HttpContext.Current.Response.StatusCode = 401;
            }
            else HttpContext.Current.Response.StatusCode = 401;
        }

        protected virtual void CheckRole(string role)
        {
            if (this.User == null) HttpContext.Current.Response.StatusCode = 401;
            else if (!this.User.Roles.Contains(role)) HttpContext.Current.Response.StatusCode = 403;
        }
    }
}
