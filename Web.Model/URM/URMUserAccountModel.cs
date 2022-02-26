namespace Web.Model
{
    using System;
    public class URMUserAccountModel
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string Roles { get; set; }
    }
}
