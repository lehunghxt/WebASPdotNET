namespace URM.Website.Controllers
{
    using Security;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class BaseController : Controller
    {
        public new UserPrincipal User { get; set; }
        
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            this.User = base.User as UserPrincipal;
            if (User == null) Response.Redirect("/login");
        }

        protected virtual void CheckRole(string role)
        {
            if (this.User == null) Response.Redirect("/login");
            else if (!this.User.Roles.Contains(role)) throw new HttpUnhandledException("Bạn không có quyền thực hiện chức năng này");
        }
    }
}