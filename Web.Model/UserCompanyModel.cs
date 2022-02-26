namespace Web.Model
{
    using System;
    public class UserCompanyModel
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int ID { get; set; }

        public int CompanyId { get; set; }
        public decimal Balance { get; set; }
    }
}
