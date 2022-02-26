namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web.Model;

    public class ThirdPartyViewModel
    {
        public string Action { get; set; }
        public ThirdPartyModel ThirdParty { get; set; } // dùng để tạo thêm
        public IList<ThirdPartyModel> ThirdParties { get; set; }

        public IList<SelectListItem> Poitions { get; set; }

        public ThirdPartyViewModel()
        {
            ThirdParty = new ThirdPartyModel();
            ThirdParties = new List<ThirdPartyModel>();
            Poitions = new List<SelectListItem>();
        }
    }
}
