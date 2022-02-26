namespace Web.FrontEnd
{
    using System;

    public partial class Error : Web.Asp.UI.VITPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.error_content.InnerText = this.Server.UrlDecode(this.Request["msg"]);           
        }
    }
}