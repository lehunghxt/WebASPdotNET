namespace URM.Model
{
    using System;
    public class URMChangePasswordModel
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int ID { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public int ApplicationId { get; set; }
    }
}
