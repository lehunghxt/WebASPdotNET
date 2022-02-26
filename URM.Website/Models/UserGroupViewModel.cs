namespace URM.Website.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using URM.Model;

    public class UserGroupViewModel
    {
        public string Action { get; set; }
        public int GroupId { get; set; }

        public IList<SelectListItem> Groups { get; set; }
        public List<URMUserModel> UserGroups { get; set; } // dùng để hiển thị
        public List<URMUserModel> Users { get; set; } // chưa có nhóm

        public UserGroupViewModel()
        {
            Groups = new List<SelectListItem>();
            UserGroups = new List<URMUserModel>();
            Users = new List<URMUserModel>();
        }
    }
}
