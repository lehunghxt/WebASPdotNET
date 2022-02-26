using System;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library;
using Library.Web.Security;

[assembly: TagPrefix("Web.Asp.Controls", "VIT")]
namespace Web.Asp.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:Statistics runat=server />")]
    public class Statistics : Literal
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]

        public string Online
        {
            get
            {
                String s = (String)ViewState["Online"];
                return ((s == null) ? "Online" : s);
            }

            set
            {
                ViewState["Online"] = value;
            }
        }
        public string All
        {
            get
            {
                String s = (String)ViewState["All"];
                return ((s == null) ? "All" : s);
            }

            set
            {
                ViewState["All"] = value;
            }
        }

        public string ToDay
        {
            get
            {
                String s = (String)ViewState["ToDay"];
                return ((s == null) ? "ToDay" : s);
            }

            set
            {
                ViewState["ToDay"] = value;
            }
        }
        public string Yesterday
        {
            get
            {
                String s = (String)ViewState["Yesterday"];
                return ((s == null) ? "Yesterday" : s);
            }

            set
            {
                ViewState["Yesterday"] = value;
            }
        }
      
        public string ThisWeek
        {
            get
            {
                String s = (String)ViewState["ThisWeek"];
                return ((s == null) ? "ThisWeek" : s);
            }

            set
            {
                ViewState["ThisWeek"] = value;
            }
        }
        public string LastWeek
        {
            get
            {
                String s = (String)ViewState["LastWeek"];
                return ((s == null) ? "LastWeek" : s);
            }

            set
            {
                ViewState["LastWeek"] = value;
            }
        }

        public string ThisMonth
        {
            get
            {
                String s = (String)ViewState["ThisMonth"];
                return ((s == null) ? "ThisMonth" : s);
            }

            set
            {
                ViewState["ThisMonth"] = value;
            }
        }
        public string LastMonth
        {
            get
            {
                String s = (String)ViewState["LastMonth"];
                return ((s == null) ? "LastMonth" : s);
            }

            set
            {
                ViewState["LastMonth"] = value;
            }
        }

        public string FontSize
        {
            get
            {
                String s = (String)ViewState["FontSize"];
                return ((s == null) ? "12px" : s);
            }

            set
            {
                ViewState["FontSize"] = value;
            }
        }

        public string IconOnline
        {
            get
            {
                String s = (String)ViewState["IconOnline"];
                return ((s == null) ? string.Empty : s);
            }

            set
            {
                ViewState["IconOnline"] = value;
            }
        }
        public string IconAll
        {
            get
            {
                String s = (String)ViewState["IconAll"];
                return ((s == null) ? string.Empty : s);
            }

            set
            {
                ViewState["IconAll"] = value;
            }
        }

        public string IconToDay
        {
            get
            {
                String s = (String)ViewState["IconToDay"];
                return ((s == null) ? string.Empty : s);
            }

            set
            {
                ViewState["IconToDay"] = value;
            }
        }
        public string IconYesterday
        {
            get
            {
                String s = (String)ViewState["IconYesterday"];
                return ((s == null) ? string.Empty : s);
            }

            set
            {
                ViewState["IconYesterday"] = value;
            }
        }

        public string IconThisWeek
        {
            get
            {
                String s = (String)ViewState["IconThisWeek"];
                return ((s == null) ? string.Empty : s);
            }

            set
            {
                ViewState["IconThisWeek"] = value;
            }
        }
        public string IconLastWeek
        {
            get
            {
                String s = (String)ViewState["IconLastWeek"];
                return ((s == null) ? string.Empty : s);
            }

            set
            {
                ViewState["IconLastWeek"] = value;
            }
        }

        public string IconThisMonth
        {
            get
            {
                String s = (String)ViewState["IconThisMonth"];
                return ((s == null) ? string.Empty : s);
            }

            set
            {
                ViewState["IconThisMonth"] = value;
            }
        }
        public string IconLastMonth
        {
            get
            {
                String s = (String)ViewState["IconLastMonth"];
                return ((s == null) ? string.Empty : s);
            }

            set
            {
                ViewState["IconLastMonth"] = value;
            }
        }

        private int CompanyId
        {
            get
            {
                if (HttpContext.Current.Session[SettingsManager.Constants.SessionCompanyId + HttpContext.Current.Request.Url.Authority] == null)
                {
                    var principal = HttpContext.Current.User as UserPrincipal;
                    // trang quan tri
                    if (principal != null)
                        HttpContext.Current.Session[SettingsManager.Constants.SessionCompanyId + HttpContext.Current.Request.Url.Authority] = principal.CompanyId;
                    else // trang public
                    {
                        HttpContext.Current.Session[SettingsManager.Constants.SessionCompanyId + HttpContext.Current.Request.Url.Authority] = 1;
                    }
                }

                int companyId;
                int.TryParse(HttpContext.Current.Session[SettingsManager.Constants.SessionCompanyId + HttpContext.Current.Request.Url.Authority].ToString(), out companyId);
                return companyId;
            }
        }

        /// <summary>
        /// The http request wrapper.
        /// </summary>
        private HttpContextBase httpContext;

        public Statistics()
        {
            this.httpContext = new HttpContextWrapper(HttpContext.Current);
            BinData();
        }

        public void BinData()
        {
            StringBuilder strbd = new StringBuilder();
            strbd.Append("<table cellpadding=\"0\" cellspacing=\"0\" Width=\"100%\" style=\"font-size:"); strbd.Append(FontSize); strbd.Append("\">");
            strbd.Append("<tr>");
            strbd.Append("<td style=\"width:75%; padding-left:10px\">");
            if (!string.IsNullOrEmpty(IconOnline))
            {
                strbd.Append("<img src=\"");
                strbd.Append(IconOnline);
                strbd.Append("\"/> ");
            }
            strbd.Append(Online);
            strbd.Append(":</td>");
            strbd.Append("<td style=\"width:25%\">");
            strbd.Append(HttpContext.Current.Application["visitors_online" + this.CompanyId].ToString());
            strbd.Append("</td>");
            strbd.Append("</tr>");
            strbd.Append("<tr>");
            strbd.Append("<td style=\"width:75%; padding-left:10px\">");
            if (!string.IsNullOrEmpty(IconToDay))
            {
                strbd.Append("<img src=\"");
                strbd.Append(IconToDay);
                strbd.Append("\"/> ");
            }
            strbd.Append(ToDay);
            strbd.Append(":</td>");
            strbd.Append("<td style=\"width:25%\">");
            strbd.Append(HttpContext.Current.Application["HomNay" + this.CompanyId].ToString());
            strbd.Append("</td>");
            strbd.Append("</tr>");
            strbd.Append("<tr>");
            strbd.Append("<td style=\"width:75%; padding-left:10px\">");
            if (!string.IsNullOrEmpty(IconYesterday))
            {
                strbd.Append("<img src=\"");
                strbd.Append(IconYesterday);
                strbd.Append("\"/> ");
            }
            strbd.Append(Yesterday);
            strbd.Append(":</td>");
            strbd.Append("<td style=\"width:25%\">");
            strbd.Append(HttpContext.Current.Application["HomQua" + this.CompanyId].ToString());
            strbd.Append("</td>");
            strbd.Append("</tr>");
            strbd.Append("<tr>");
            strbd.Append("<td style=\"width:75%; padding-left:10px\">");
            if (!string.IsNullOrEmpty(IconThisWeek))
            {
                strbd.Append("<img src=\"");
                strbd.Append(IconThisWeek);
                strbd.Append("\"/> ");
            }
            strbd.Append(ThisWeek);
            strbd.Append(":</td>");
            strbd.Append("<td style=\"width:25%\">");
            strbd.Append(HttpContext.Current.Application["TuanNay" + this.CompanyId].ToString());
            strbd.Append("</td>");
            strbd.Append("</tr>");
            strbd.Append("<tr>");
            strbd.Append("<td style=\"width:75%; padding-left:10px\">");
            if (!string.IsNullOrEmpty(IconLastWeek))
            {
                strbd.Append("<img src=\"");
                strbd.Append(IconLastWeek);
                strbd.Append("\"/> ");
            }
            strbd.Append(LastWeek);
            strbd.Append(":</td>");
            strbd.Append("<td style=\"width:25%\">");
            strbd.Append(HttpContext.Current.Application["TuanTruoc" + this.CompanyId].ToString());
            strbd.Append("</td>");
            strbd.Append("</tr>");
            strbd.Append("<tr>");
            strbd.Append("<td style=\"width:75%; padding-left:10px\">");
            if (!string.IsNullOrEmpty(IconThisMonth))
            {
                strbd.Append("<img src=\"");
                strbd.Append(IconThisMonth);
                strbd.Append("\"/> ");
            }
            strbd.Append(ThisMonth);
            strbd.Append(":</td>");
            strbd.Append("<td style=\"width:25%\">");
            strbd.Append(HttpContext.Current.Application["ThangNay" + this.CompanyId].ToString());
            strbd.Append("</td>");
            strbd.Append("</tr>");
            strbd.Append("<tr>");
            strbd.Append("<td style=\"width:75%; padding-left:10px\">");
            if (!string.IsNullOrEmpty(IconLastMonth))
            {
                strbd.Append("<img src=\"");
                strbd.Append(IconLastMonth);
                strbd.Append("\"/> ");
            }
            strbd.Append(LastMonth);
            strbd.Append(":</td>");
            strbd.Append("<td style=\"width:25%\">");
            strbd.Append(HttpContext.Current.Application["ThangTruoc" + this.CompanyId].ToString());
            strbd.Append("</td>");
            strbd.Append("</tr>");
            strbd.Append("<tr>");
            strbd.Append("<td style=\"width:75%; padding-left:10px\">");
            if (!string.IsNullOrEmpty(IconAll))
            {
                strbd.Append("<img src=\"");
                strbd.Append(IconAll);
                strbd.Append("\"/> ");
            }
            strbd.Append(All);
            strbd.Append(":</td>");
            strbd.Append("<td style=\"width:25%\">");
            strbd.Append(HttpContext.Current.Application["TatCa" + this.CompanyId].ToString());
            strbd.Append("</td>");
            strbd.Append("</tr>");
            strbd.Append("</table>");
            this.Text = strbd.ToString();
        }
    }
}
