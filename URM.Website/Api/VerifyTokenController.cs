using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using URM.Business;
using URM.Model;

namespace URM.Website.Api
{
    public class VerifyTokenController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string token, int appId)
        {
            URMUserModel user = new URMUserModel();
            var ticket = FormsAuthentication.Decrypt(token);
            if (ticket != null && !ticket.Expired)
            {
                var userName = ticket.Name;
                var userBLL = new UserBLL();
                user = userBLL.GetProfileByUserName(userName, appId);
            }

            return Request.CreateResponse(HttpStatusCode.OK, user);
        }
    }
}

