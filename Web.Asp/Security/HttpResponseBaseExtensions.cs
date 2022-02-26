namespace Web.Asp.Security
{
    using Library;
    using System;
    using System.Diagnostics;
    using System.Web;
    using System.Web.Security;

    /// <summary>
    ///     The http response base extensions.
    /// </summary>
    public static class HttpResponseBaseExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The set authenticate cookie.
        /// </summary>
        /// <param name="response">
        /// The response base.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="rememberMe">
        /// The remember me.
        /// </param>
        /// <param name="userData">
        /// The user data.
        /// </param>
        /// <typeparam name="TUserData">
        /// Type of user data.
        /// </typeparam>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int SetAuthCookie<TUserData>(this HttpResponseBase response, string name, bool rememberMe, TUserData userData)
        {
            //simple
            FormsAuthentication.SetAuthCookie(name, rememberMe);

            // In order to pickup the settings from config, 
            // we create a default cookie and use its values to create a new one.
            HttpCookie cookie = FormsAuthentication.GetAuthCookie(name, rememberMe);
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

            Debug.Assert(ticket != null, "ticket != null");
            var newTicket = new FormsAuthenticationTicket(
                ticket.Version, 
                ticket.Name, 
                ticket.IssueDate, 
                ticket.Expiration, 
                ticket.IsPersistent,
                JsonHelper.SerializeObject(userData),
                FormsAuthentication.FormsCookiePath);
            string encTicket = FormsAuthentication.Encrypt(newTicket);

            // Use existing cookie. Could create new one but would have to copy settings over...
            cookie.Value = encTicket;

            if (rememberMe) cookie.Expires = ticket.Expiration;

            response.Cookies.Add(cookie);
            //response.Cookies.Add(new HttpCookie("NET_SessionId", HttpContext.Current.Session.SessionID) { Expires = cookie.Expires });

            Debug.Assert(encTicket != null, "encTicket != null");
            return encTicket.Length;
        }

        /// <summary>
        /// The set authentication ticket.
        /// </summary>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <param name="ticket">
        /// The ticket.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int SetAuthTicket(this HttpResponseBase response, FormsAuthenticationTicket ticket)
        {
            var encryptTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = FormsAuthentication.GetAuthCookie(ticket.Name, ticket.IsPersistent);
            cookie.Value = encryptTicket;

            response.Cookies.Add(cookie);

            Debug.Assert(encryptTicket != null, "encryptTicket != null");
            return encryptTicket.Length;
        }

        #endregion
    }
}