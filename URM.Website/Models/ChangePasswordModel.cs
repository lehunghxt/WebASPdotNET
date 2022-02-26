namespace URM.Website.Models
{
    using Model;
    using System;
    public class ChangePasswordModel
    {
        public URMUserAccountModel Account { get; set; }
        public string ConfirmNewPassword { get; set; }

        public ChangePasswordModel()
        {
            Account = new URMUserAccountModel();
        }
    }
}
