namespace Web.Asp.Security
{
    using System;

    /// <summary>
    /// The form ticket data.
    /// </summary>
    public class FormTicketData
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets Token.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the Point.
        /// </summary>
        public decimal Point { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Phone.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string FullName { get; set; }
        
        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the company id.
        /// </summary>
        public int CompanyId { get; set; }

        public DateTime? Birthday { get; set; }
        public string Token { get; set; }

        public int Version { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsPersistent { get; set; }

        #endregion
    }
}