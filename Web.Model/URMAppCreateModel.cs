namespace URM.Model
{
    using System;
    public class URMAppCreateModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string AppName { get; set; }
    }
}
