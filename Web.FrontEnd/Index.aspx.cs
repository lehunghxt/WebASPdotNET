using System;
using System.Web;
using Web.Asp.UI;

namespace Web.FrontEnd
{
    public partial class Index : VITPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var url = string.Format("/Templates/{0}/Components/Home.aspx", Config.Template);
            HttpContext.Current.Server.Transfer(url);
        }
    }
}