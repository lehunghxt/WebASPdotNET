namespace URM.Model
{
    public class URMUserLoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool CreatePersistentCookie { get; set; }
        public int ApplicationId { get; set; }
    }
}
