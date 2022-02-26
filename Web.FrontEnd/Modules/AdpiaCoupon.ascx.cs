using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web.Asp.Provider;
using Web.Asp.Provider.Cache;
using Web.Asp.UI;
using Web.Business;
using Web.Model;
using Library.Web.RSS;
using Library;
using Library.Web;
using System.Net;
using System.Collections.Specialized;
using System.Text;

namespace Web.FrontEnd.Modules
{
    public partial class AdpiaCoupon : VITModule
    {
        protected string Api { get; set; }
        protected string Token { get; set; }

        protected string Link { get; set; }
        protected string Data { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Api = this.GetValueParam<string>("Api");
            this.Token = this.GetValueParam<string>("Token");
            
            this.Link = this.Api;
            if (!string.IsNullOrEmpty(this.Token)) this.Link += "?token=" + this.Token;

            var api = new ApiHelper();
            this.Data = api.Get(this.Link).ToString();

            //var loginAddress = "https://newpub.adpia.vn/login";
            //var loginData = new NameValueCollection
            //{
            //  { "username", "m0nkey61" },
            //  { "password", "290893326" },
            //    {"remember", "true" }
            //};

            //var client = new CookieAwareWebClient();
            //client.Login(loginAddress, loginData);
            //var data = client.DownloadString("https://newpub.adpia.vn/newpub/discount");
        }
    }
}