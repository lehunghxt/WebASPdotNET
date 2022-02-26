namespace Web.Asp.Security
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;
    using System.Web;

    /// <summary>
    /// The basic authorization.
    /// </summary>
    public class BasicAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        ///     The authorization header separator.
        /// </summary>
        public const char AuthorizationHeaderSeparator = ':';

        /// <summary>
        ///     The basic scheme.
        /// </summary>
        public const string BasicScheme = "Basic";

        /// <summary>
        ///     The challenge authentication header name.
        /// </summary>
        public const string ChallengeAuthenticationHeaderName = "WWW-Authenticate";

        /// <summary>
        /// The on authorization.
        /// </summary>
        /// <param name="actionContext">
        /// The action context.
        /// </param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            var authHeader = request.Headers.Authorization;
            if (authHeader != null)
            {
                if (string.Compare(authHeader.Scheme, BasicScheme, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    string encodedCredentials = authHeader.Parameter;
                    byte[] credentialBytes = Convert.FromBase64String(encodedCredentials);
                    string credentials = Encoding.ASCII.GetString(credentialBytes);
                    string[] credentialParts = credentials.Split(AuthorizationHeaderSeparator);

                    if (credentialParts.Length == 2)
                    {
                        string username = credentialParts[0].Trim();
                        string password = credentialParts[1].Trim();

                        if (this.ValidateUser(username, password))
                        {
                            this.SetPrincipal(username);
                            return;
                        }
                    }
                }
            }

            actionContext.Response = this.CreateUnauthorizedResponse(actionContext);
        }

        /// <summary>
        /// The create unauthorized response.
        /// </summary>
        /// <param name="actionContext">
        /// The action context.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        private HttpResponseMessage CreateUnauthorizedResponse(HttpActionContext actionContext)
        {
            var response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            response.Headers.Add(ChallengeAuthenticationHeaderName, BasicScheme);

            return response;
        }

        /// <summary>
        /// The validate user.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool ValidateUser(string username, string password)
        {
            string apiUsername = "";
            string apiPassword = "";

            return apiUsername == username && apiPassword == password;
        }

        /// <summary>
        /// The set principal.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        private void SetPrincipal(string username)
        {
            var identity = new GenericIdentity(username, BasicScheme);

            string[] roles = { };
            var principal = new UserPrincipal(identity, roles);
            Thread.CurrentPrincipal = principal;

            var httpContext = new HttpContextWrapper(HttpContext.Current);
            if (httpContext != null)
            {
                httpContext.User = principal;
            }
        }
    }
}