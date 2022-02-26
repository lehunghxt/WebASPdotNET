using System;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

[assembly: TagPrefix("Web.Asp.Controls", "VIT")]
namespace Web.Asp.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:PagingNews runat=server />")]
    public class PagingNews : Literal
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]

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

        public string LinkAction
        {
            get
            {
                String s = (String)ViewState["LinkAction"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["LinkAction"] = value;
            }
        }

        public string SelectedID
        {
            get
            {
                String s = (String)ViewState["SelectedID"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["SelectedID"] = value;
            }
        }

        public string TitleBefore
        {
            get
            {
                String s = (String)ViewState["TitleBefore"];
                return ((s == null) ? "Các tin trước" : s);
            }

            set
            {
                ViewState["TitleBefore"] = value;
            }
        }

        public string TitleAfter
        {
            get
            {
                String s = (String)ViewState["TitleAfter"];
                return ((s == null) ? "Các tin sau" : s);
            }

            set
            {
                ViewState["TitleAfter"] = value;
            }
        }

        public string CssNameForTitle
        {
            get
            {
                String s = (String)ViewState["CssNameForTitle"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["CssNameForTitle"] = value;
            }
        }

        public string CssNameForNews
        {
            get
            {
                String s = (String)ViewState["CssNameForNews"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["CssNameForNews"] = value;
            }
        }

        public string CssNameForDate
        {
            get
            {
                String s = (String)ViewState["CssNameForDate"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["CssNameForDate"] = value;
            }
        }

        public string ColumnText
        {
            get
            {
                String s = (String)ViewState["ColumnText"];
                return ((s == null) ? String.Empty : s);
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
                String s = (String)ViewState["ColumnValue"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["ColumnValue"] = value;
            }
        }

        public int Before
        {
            get
            {
                if (ViewState["Before"] == null) return 5;
                return (int)ViewState["Before"];
            }

            set
            {
                ViewState["Before"] = value;
            }
        }

        public int After
        {
            get
            {
                if (ViewState["After"] == null) return 5;
                return (int)ViewState["After"];
            }

            set
            {
                ViewState["After"] = value;
            }
        }

        public void BinData()
        {
            var currentItem = Table.Select(ColumnValue + "='" + SelectedID + "'", "DisplayDate");

            StringBuilder output = new StringBuilder();

            var beforeData = Table.Select("DisplayDate < " + currentItem[0]["DisplayDate"], "DisplayDate DESC");
            if (beforeData.Length > 0)
            {
                output.Append("<div class='");
                output.Append(CssNameForTitle + "'>");
                output.Append(TitleBefore);
                output.Append("</div>");

                output.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>");

                for (int i = 0; i < beforeData.Length && i < Before; i++)
                {
                    output.Append("<tr><td><a href='");
                    output.Append(LinkAction);
                    output.Append(beforeData[i][ColumnValue]);
                    output.Append("' class='");
                    output.Append(CssNameForNews);
                    output.Append("'>");
                    output.Append(beforeData[i][ColumnValue]);
                    output.Append("</a></td>");

                    output.Append("<td class='");
                    output.Append(CssNameForDate);
                    output.Append("'>(");
                    output.Append(beforeData[i]["DisplayDate"]);
                    output.Append(")</td></tr>");
                }
            }

            var afterData = Table.Select("DisplayDate > " + currentItem[0]["DisplayDate"], "DisplayDate");
            if (afterData.Length > 0)
            {
                output.Append("<div class='");
                output.Append(CssNameForTitle);
                output.Append("'>");
                output.Append(TitleAfter);
                output.Append("</div>");

                output.Append("<table border='0' cellpadding='0' cellspacing='0' width='100%'>");

                for (int i = 0; i < afterData.Length && i < After; i++)
                {
                    output.Append("<tr><td><a href='");
                    output.Append(LinkAction);
                    output.Append(afterData[i][ColumnValue]);
                    output.Append("' class='");
                    output.Append(CssNameForNews);
                    output.Append("'>");
                    output.Append(beforeData[i][ColumnValue]);
                    output.Append("</a></td>");

                    output.Append("<td class='");
                    output.Append(CssNameForDate);
                    output.Append("'>(");
                    output.Append(afterData[i]["DisplayDate"]);
                    output.Append(")</td></tr>");
                }
            }

            Text = output.ToString();
        }
    }
}
