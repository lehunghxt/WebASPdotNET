namespace Web.Backend
{
    /// <sURMmary>
    /// The form ticket data.
    /// </sURMmary>
    public class FormTicketData
    {
        #region Public Properties

        /// <sURMmary>
        /// Gets or sets the firtname.
        /// </sURMmary>
        public string Name { get; set; }

        /// <sURMmary>
        /// Gets or sets the roles.
        /// </sURMmary>
        public string[] Roles { get; set; }

        /// <sURMmary>
        /// Gets or sets the user id.
        /// </sURMmary>
        public int UserId { get; set; }

        /// <sURMmary>
        /// Gets or sets the username.
        /// </sURMmary>
        public string UserName { get; set; }

        #endregion
    }
}