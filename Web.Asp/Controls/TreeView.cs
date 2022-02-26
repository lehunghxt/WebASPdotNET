using System;
using System.Data;
using System.ComponentModel;
using System.Web.UI;

[assembly: TagPrefix("Web.Asp.Controls", "VIT")]
namespace Web.Asp.Controls
{
    [ToolboxData("<{0}:TreeView runat=server ColumnText ColumnValue />")]
    public class TreeView : System.Web.UI.WebControls.TreeView
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]

        public string ColumnText
        {
            get
            {
                var s = (String)ViewState["ColumnText"];
                return (s ?? String.Empty);
            }

            set
            {
                ViewState["ColumnText"] = value;
            }
        }

        public string ColumnValue
        {
            get
            {
                var s = (String)ViewState["ColumnValue"];
                return (s ?? String.Empty);
            }

            set
            {
                ViewState["ColumnValue"] = value;
            }
        }

        public string ColumnParent
        {
            get
            {
                var s = (String)ViewState["ColumnParent"];
                return (s ?? "ParentId");
            }

            set
            {
                ViewState["ColumnParent"] = value;
            }
        }

        public int RootId
        {
            get
            {
                if(ViewState["RootId"]==null) return 0;
                return (int)ViewState["RootId"];
            }

            set
            {
                ViewState["RootId"] = value;
            }
        }

        public DataTable Table
        {
            get
            {
                return (DataTable)ViewState["Table"];
            }

            set
            {
                ViewState["Table"] = value;
            }
        }
    }
}
