namespace Web.Backend.Models
{
    using Model;
    using System;
    public class ChangePasswordModel
    {
        public UserAccountModel Account { get; set; }
        public string ConfirmNewPassword { get; set; }

        public ChangePasswordModel()
        {
            Account = new UserAccountModel();
        }
    }
}
