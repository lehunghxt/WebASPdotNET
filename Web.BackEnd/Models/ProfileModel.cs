namespace Web.Backend.Models
{
    using Model;

    public class ProfileModel
    {
        public string Action { get; set; }
        public ChangePasswordModel UserAccount { get; set; }
        public int AppId { get; set; }
        public string AppName { get; set; }
        public UserInfoModel Info { get; set; }
        public bool IsAdministrator { get; set; }

        public ProfileModel()
        {
            UserAccount = new ChangePasswordModel();
            Info = new UserInfoModel();
        }
    }
}
