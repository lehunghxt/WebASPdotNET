namespace URM.Website.Models
{
    using Model;

    public class ProfileModel
    {
        public string Action { get; set; }
        public ChangePasswordModel UserAccount { get; set; }
        public int AppId { get; set; }
        public string AppName { get; set; }
        public URMUserInfoModel Info { get; set; }
        public bool IsAdministrator { get; set; }

        public ProfileModel()
        {
            UserAccount = new ChangePasswordModel();
            Info = new URMUserInfoModel();
        }
    }
}
