using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("Web.Asp.Controls", "VIT")]
namespace Web.Asp.Controls
{
    using Library.Web.Security;
    using System.Web;

    [DefaultProperty("Text")]
    [ToolboxData("<{0}:LinkSubmit runat=server ViewStateMode=Disabled Param=/>")]
    public class LinkSubmit : LinkButton
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]

        public string Param
        {
            get
            {
                var s = (String)ViewState["Param"];
                return (s ?? String.Empty);
            }

            set
            {
                ViewState["Param"] = value;
            }
        }

        public string Function
        {
            get
            {
                var s = (String)ViewState["Function"];
                return (s ?? String.Empty);
            }

            set
            {
                ViewState["Function"] = value;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if (this.Function.Length > 0 && HttpContext.Current != null)
            {
                var principal = HttpContext.Current.User as UserPrincipal;
                if (principal.IsInRole(this.Function))
                    base.OnClick(e);
                else throw new UnauthorizedAccessException("Bạn không có quyền thực hiện thao tác này");
            }
            else base.OnClick(e);
        }
    }
}
