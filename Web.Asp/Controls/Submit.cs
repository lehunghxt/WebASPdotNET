// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Submit.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Submit type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("Web.Asp.Controls", "VIT")]
namespace Web.Asp.Controls
{
    using System.Web.Security;
    using Library.Web.Security;
    using System.Web;

    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Submit runat=server Function= ViewStateMode=Disabled/>")]
    public class Submit : Button
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]

        public Submit()
        {
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

                if (HttpContext.Current != null)
                {
                    var principal = HttpContext.Current.User as UserPrincipal;
                    if (this.Hide) this.Visible = principal.IsInRole(value);
                    else this.Enabled = principal.IsInRole(value);
                }
            }
        }

        public bool Hide
        {
            get
            {
                if (ViewState["Hide"] == null) return true;
                return (Boolean)ViewState["Hide"];
            }

            set
            {
                ViewState["Hide"] = value;
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
