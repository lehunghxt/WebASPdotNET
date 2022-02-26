namespace Web.Backend.Models
{
    using System.Collections.Generic;
    using Web.Model;

    public class CompanyViewModel
    {
        public string PathLogo { get; set; }
        public string PathFavicon { get; set; }
        public string PathBackground { get; set; }
        public string TypeBackground { get; set; }
        public COMPANYLANGUAGEModel Company { get; set; } // dùng để tạo thêm
        public WEBCONFIGModel Config { get; set; }
        public bool AutoUpdateSiteMap { get; set; }

        public IList<LANGUAGEModel> Languages { get; set; }
        public IList<string> Domains { get; set; }

        public CompanyViewModel()
        {
            Languages = new List<LANGUAGEModel>();
            Domains = new List<string>();
        }
    }
}
