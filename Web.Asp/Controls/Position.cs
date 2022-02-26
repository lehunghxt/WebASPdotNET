using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("Web.Asp.Controls", "VIT")]
namespace Web.Asp.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Position runat=server SkinName= />")]
    public class Position : PlaceHolder
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]

        public string SkinName
        {
            get
            {
                var s = (String)ViewState["SkinName"];
                return (s ?? String.Empty);
            }

            set
            {
                ViewState["SkinName"] = value;
            }
        }
    }
}
