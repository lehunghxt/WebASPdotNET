using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

[assembly: TagPrefix("Web.Asp.Controls", "VIT")]
namespace Web.Asp.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Paging runat=server ItemOnPage=15 CurrentPage=1 NumPage=5 ColumnId=Id />")]
    public class Paging : Literal
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

        public int ItemOnPage
        {
            get
            {
                if (ViewState["ItemOnPage"] == null) return 5;
                return (int)ViewState["ItemOnPage"];
            }

            set
            {
                ViewState["ItemOnPage"] = value;
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

        public string ColumnId
        {
            get
            {
                String s = (String)ViewState["ColumnId"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["ColumnId"] = value;
            }
        }

        public string TitleHeader
        {
            get
            {
                String s = (String)ViewState["TitleHeader"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["TitleHeader"] = value;
            }
        }

        public string TitleFooter
        {
            get
            {
                String s = (String)ViewState["TitleFooter"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["TitleFooter"] = value;
            }
        }

        public string SeparatedHeader
        {
            get
            {
                String s = (String)ViewState["SeparatedHeader"];
                return ((s == null) ? TitleHeader : s);
            }

            set
            {
                ViewState["SeparatedHeader"] = value;
            }
        }

        public string SeparatedFooter
        {
            get
            {
                String s = (String)ViewState["SeparatedFooter"];
                return ((s == null) ? TitleHeader : s);
            }

            set
            {
                ViewState["SeparatedFooter"] = value;
            }
        }

        public string CssTitle
        {
            get
            {
                String s = (String)ViewState["CssTitle"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["CssTitle"] = value;
            }
        }

        public string CssSeparated
        {
            get
            {
                String s = (String)ViewState["CssSeparated"];
                return ((s == null) ? CssTitle : s);
            }

            set
            {
                ViewState["CssSeparated"] = value;
            }
        }

        public int NumPage
        {
            get
            {
                if (ViewState["NumPage"] == null) return 5;
                return (int)ViewState["NumPage"];
            }

            set
            {
                ViewState["NumPage"] = value;
            }
        }

        public string CssPage
        {
            get
            {
                String s = (String)ViewState["CssPage"];
                return ((s == null) ? CssTitle : s);
            }

            set
            {
                ViewState["CssPage"] = value;
            }
        }

        public int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null) return 1;
                return (int)ViewState["CurrentPage"];
            }

            set
            {
                ViewState["CurrentPage"] = value;
            }
        }

        public string CssCurrentPage
        {
            get
            {
                String s = (String)ViewState["CssCurrentPage"];
                return ((s == null) ? CssTitle : s);
            }

            set
            {
                ViewState["CssCurrentPage"] = value;
            }
        }

        public void BinData(dynamic rpt = null)
        {
            if (Table != null)
            {
                if (rpt != null)
                {
                    DataTable dt = Table.Copy();
                    int to = (CurrentPage - 1) * ItemOnPage;
                    int from = CurrentPage * ItemOnPage;
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        if (i > from - 1) dt.Rows.RemoveAt(i);
                        else if (i < to) dt.Rows.RemoveAt(i);
                        else continue;
                    }
                    rpt.DataSource = dt;
                    rpt.DataBind();
                }


                StringBuilder output = new StringBuilder();
                output.Append("<span style='text-align:center;'>");

                int total = Table.Rows.Count / ItemOnPage;
                if (Table.Rows.Count % ItemOnPage != 0) total++;
                if (total > 1)
                {
                    int batdau = (CurrentPage - NumPage / 2 > 0) ? (CurrentPage - NumPage / 2) : 1;
                    int n = batdau + NumPage;
                    if (batdau > 1)
                    {
                        output.Append("<a href='");
                        output.Append(LinkAction + 1);
                        output.Append("' class='");
                        output.Append(CssTitle);
                        output.Append("'>");
                        output.Append(TitleHeader);
                        output.Append("</a> &nbsp;");

                        output.Append("<a  class='");
                        output.Append(CssSeparated);
                        output.Append("'>");
                        output.Append(SeparatedHeader);
                        output.Append("</a> &nbsp;");
                    }
                    int j; string css = string.Empty;
                    for (j = batdau; j <= total && j < n; j++)
                    {
                        if (j != CurrentPage) css = CssPage;
                        else css = CssCurrentPage;
                        output.Append("<a href='");
                        output.Append(LinkAction + j);
                        output.Append("' class='");
                        output.Append(css);
                        output.Append("' >");
                        output.Append(j);
                        output.Append("</a> &nbsp;");
                    }
                    if (j <= total)
                    {
                        output.Append("<a class='");
                        output.Append(CssSeparated);
                        output.Append("'>");
                        output.Append(SeparatedFooter);
                        output.Append("</a> &nbsp;");

                        output.Append("<a href='");
                        output.Append(LinkAction + total);
                        output.Append("' class='");
                        output.Append(CssTitle);
                        output.Append("' >");
                        output.Append(TitleFooter);
                        output.Append("</a>");
                    }
                }
                output.Append("</span>");

                Text = output.ToString();
            }
        }
    }
}
