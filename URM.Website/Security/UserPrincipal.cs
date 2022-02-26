namespace URM.Website.Security
{
    using System;
    using System.Linq;
    using System.Security.Principal;

    /// <summary>
    /// The rapid identity.
    /// </summary>
    public class UserPrincipal : GenericPrincipal
    {
        /// <summary>
        /// Gets or sets the firtname.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public string[] Roles { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the company id.
        /// </summary>
        public int AppId { get; set; }
        public string Token { get; set; }

        public UserPrincipal(GenericIdentity identity, string[] roles)
            : base(identity, roles)
        {
        }

        public UserPrincipal(IIdentity identity, string[] roles)
            : base(identity, roles)
        {
        }

        //public override bool IsInRole(string roleName)
        //{
        //    roleName = roleName.Trim().ToLower();
        //    return Roles.Any(e => e.ToLower() == roleName);
        //}
    }
}