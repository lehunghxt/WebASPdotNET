namespace Web.Asp.Security
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
        /// Gets or sets the balance.
        /// </summary>
        public decimal Balance { get; set; }

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
        public int CompanyId { get; set; }

        public int AppId { get; set; }
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Phone.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the Phone.
        /// </summary>
        public string Address { get; set; }

        public decimal Point { get; set; }
        public DateTime? Birthday { get; set; }

        public UserPrincipal(GenericIdentity identity, string[] roles)
            : base(identity, roles)
        {
        }

        public UserPrincipal(IIdentity identity, string[] roles)
            : base(identity, roles)
        {
        }
    }
}