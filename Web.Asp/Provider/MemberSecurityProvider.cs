// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityProvider.cs" company="ACOM Solutions, Inc.">
//   Copyright (c) 2014 ACOM Solutions, Inc. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Web.Asp.Provider
{
    using Library;
    using Web.Business;
    using System;
    using System.Security.Principal;
    using System.Threading;
    using System.Web;
    using System.Web.Security;
    using Web.Model;
    using Security;

    /// <summary>
    ///     The security provider.
    /// </summary>
    public class MemberSecurityProvider
    {
        #region Fields

        /// <summary>
        /// The authenticate type.
        /// </summary>
        public const string AuthenticateType = "Forms";

        /// <summary>
        ///     The basic scheme.
        /// </summary>
        public const string BasicScheme = "Basic";

        /// <summary>
        /// The http request wrapper.
        /// </summary>
        private readonly HttpContextBase httpContext;

        private CustomerBLL customerBLL;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityProvider"/> class.
        /// </summary>
        public MemberSecurityProvider()
        {
            this.httpContext = new HttpContextWrapper(HttpContext.Current);
            customerBLL = new CustomerBLL();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get principal.
        /// </summary>
        /// <param name="userName">
        /// The username.
        /// </param>
        /// <param name="authenticationType">
        /// The authentication Type.
        /// </param>
        /// <param name="encodeUserdata">
        /// The encode user data.
        /// </param>
        /// <returns>
        /// The <see cref="IPrincipal"/>.
        /// </returns>
        public IPrincipal GetPrincipal(string userName, string authenticationType, string encodeUserdata = null)
        {
            FormTicketData userData = this.GetFormTicketDataFromUserData(encodeUserdata);

            if (userData == null)
            {
                var id = userName.Split('|');
                userData = new FormTicketData();
                            var companyBLL = new CustomerBLL();
                            var user = companyBLL.GetCustomer(Convert.ToInt32(id));
                            userData.Balance = user.TranferPrice ?? 0;
                            userData.CompanyId = user.CompanyId;

                            userData.FullName = user.Name;
                            userData.UserId = user.Id;
                            userData.ID = user.Id + "|" + user.Phone;
                userData.Phone = user.Phone;
                userData.Point = user.Point ?? 0;
                userData.Birthday = user.BirthDay;
                userData.Email = user.Email;
                userData.Address = user.Address;
            }

            if (userData != null)
            {
                IIdentity identity = new GenericIdentity(userName, authenticationType);
                var userPrincipal = new UserPrincipal(identity, new string[] { });
                userPrincipal.FullName = userData.FullName;
                userPrincipal.Phone = userData.Phone;
                userPrincipal.Balance = userData.Balance;
                userPrincipal.UserId = userData.UserId;
                userPrincipal.CompanyId = userData.CompanyId;
                userPrincipal.Email = userData.Email;
                userPrincipal.Birthday = userData.Birthday;
                userPrincipal.Point = userData.Point;
                userPrincipal.Address = userData.Address;
                return userPrincipal;
            }

            return null;
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

            string[] roles = { SettingsManager.Constants.PermissonLoginAdmin };
            var principal = new UserPrincipal(identity, roles);
            Thread.CurrentPrincipal = principal;

            if (this.httpContext != null)
            {
                this.httpContext.User = principal;
            }
        }

        /// <summary>
        /// The initialize principal.
        /// </summary>
        public void InitializePrincipal()
        {
            var principal = this.httpContext.User;
            if (principal == null || principal.Identity == null || !this.httpContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            var identity = principal.Identity;
            string userData = null;

            var formsIdentity = identity as FormsIdentity;
            if (formsIdentity != null && formsIdentity.AuthenticationType.Equals("Forms", StringComparison.InvariantCultureIgnoreCase))
            {
                // For forms authentication we get user data from cookie ticket to avoid hit database
                userData = formsIdentity.Ticket.UserData;
            }

            var replacePricipal = this.GetPrincipal(identity.Name, identity.AuthenticationType, userData);
            if (replacePricipal != null)
            {
                this.httpContext.User = replacePricipal;
            }
        }

        /// <summary>
        /// The authenticate.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="createPersistentCookie">
        /// The create Persistent Cookie.
        /// </param>
        public void LogIn(CustomerModel user, bool createPersistentCookie)
        {
            // Set to Principal
            this.SetPrincipal(user.Id + "|" + user.Phone);

            // set to cookie
            this.httpContext.Response.SetAuthCookie(user.Id + "|" + user.Phone, createPersistentCookie, this.GetFormTicketData(user));
        }

        /// <summary>
        ///     The log out.
        /// </summary>
        public void LogOut()
        {
            FormsAuthentication.SignOut();
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
        /// The roles
        /// </returns>
        public bool ValidateUser(int companyId, string userName, string password, bool isSavePass)
        {
            var check = customerBLL.ValidateUser(companyId, userName, password);
            if (check == 1)
            {
                var user = customerBLL.GetCustomer(companyId, userName);
                this.LogIn(user, isSavePass);
                return true;
            }

            return false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get form ticket data from user data.
        /// </summary>
        /// <param name="userData">
        /// The user data.
        /// </param>
        /// <returns>
        /// The <see cref="FormTicketData"/>.
        /// </returns>
        private FormTicketData GetFormTicketDataFromUserData(string userData)
        {
            if (string.IsNullOrEmpty(userData))
            {
                return null;
            }

            return JsonHelper.DeserializeObject<FormTicketData>(userData);
        }

        /// <summary>
        /// The get form ticket data.
        /// </summary>
        /// <param name="userName">
        /// The username.
        /// </param>
        /// <returns>
        /// The <see cref="FormTicketData"/>.
        /// </returns>
        private FormTicketData GetFormTicketData(CustomerModel user)
        {
            var data = new FormTicketData
            {
                CompanyId = user.CompanyId,
                ID = user.Id + "|" + user.Phone,
                Balance = user.Point ?? 0,
                FullName = user.Name,
                UserId = user.Id,
                Phone = user.Phone,
                Email = user.Email,
                Birthday = user.BirthDay,
                Address = user.Address
            };

            return data;
        }
        #endregion
    }
}