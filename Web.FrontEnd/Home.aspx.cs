using System;
using System.Web;
using Web.Asp.UI;

namespace Web.FrontEnd
{
    public partial class Home : VITPage
    {
        protected void Page_PreInit(Object sender, System.EventArgs e)
        {
            var url = string.Format("/Templates/{0}/Components/Home.aspx", Config.Template);
            HttpContext.Current.Server.Transfer(url);
        }
    }
}