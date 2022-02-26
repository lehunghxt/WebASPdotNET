using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

using System.Collections.Generic;
using Web.Asp.Provider;
using Web.Business;
using Web.Asp.ObjectData;
using Web.Model;

namespace Web.Asp.Controls
{
    public class PagerCommandEventArgs : CommandEventArgs
    {
        public int StartRowIndex { get; set; }
        public int MaximumRows { get; set; }

        public PagerCommandEventArgs(int startRowIndex, int maximumRows, CommandEventArgs originalArgs)
            : base(originalArgs)
        {
            this.StartRowIndex = startRowIndex;
            this.MaximumRows = maximumRows;
        }
    }

    public class Pager : WebControl, INamingContainer
    {
        public string Title
        {
            get { return (this.ViewState["Title"] ?? string.Empty).ToString(); }
            set { this.ViewState["Title"] = value; }
        }

        public new int Page
        {
            get
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[this.QueryStringField]))
                    return Convert.ToInt32(HttpContext.Current.Request.QueryString[this.QueryStringField]);
                else
                    return (int)(this.ViewState["Page"] ?? 1);
            }
            set { this.ViewState["Page"] = value; }
        }

        public int PageSize
        {
            get { return (int)(this.ViewState["PageSize"] ?? 15); }
            set { this.ViewState["PageSize"] = value; }
        }

        public int TotalRowCount
        {
            get { return (int)(this.ViewState["TotalRowCount"] ?? 0); }
            set { this.ViewState["TotalRowCount"] = value; }
        }

        public int StartRowIndex
        {
            get { return (this.Page - 1) * this.PageSize; }
        }

        public int TotalPageCount
        {
            get { return (int)Math.Ceiling((double)this.TotalRowCount / (double)this.PageSize); }
        }

        public string QueryStringField
        {
            get { return (string)(this.ViewState["QueryStringField"] ?? string.Empty); }
            set { this.ViewState["QueryStringField"] = value; }
        }

        public bool ShowPreviousNextLinks
        {
            get { return (bool)(this.ViewState["ShowPreviousNextLinks"] ?? true); }
            set { this.ViewState["ShowPreviousNextLinks"] = value; }
        }
        private static readonly object EventPagerCommand = new object();
        public event EventHandler PagerCommand
        {
            add
            {
                this.Events.AddHandler(EventPagerCommand, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventPagerCommand, value);
            }
        }

        protected void OnPagerCommand(PagerCommandEventArgs e)
        {
            EventHandler handler = (EventHandler)base.Events[EventPagerCommand];

            if (handler != null)
                handler(this, e);
        }

        protected override void CreateChildControls()
        {
            if (!string.IsNullOrEmpty(this.QueryStringField))
                this.CreatePageForQueryString();
            else
                this.CreatePagerForCommand();
        }

        private void CreatePageForQueryString()
        {
            if (this.TotalRowCount > this.PageSize && !string.IsNullOrEmpty(this.Title))
                this.Controls.Add(new LiteralControl(this.Title + ":"));

            // Add the previous links
            if (this.Page > 1 && this.ShowPreviousNextLinks)
            {
                this.Controls.Add(new LiteralControl(" "));
                this.Controls.Add(this.CreateLink("«", 1, "thefirst"));
                this.Controls.Add(new LiteralControl(" "));
                this.Controls.Add(this.CreateLink("‹", this.Page - 1, "back"));
            }

            // Loop through the pages
            if (this.TotalRowCount > this.PageSize)
            {
                for (int i = (this.Page <= 2 ? 1 : this.Page - 2); i <= (this.Page >= this.TotalPageCount - 2 ? this.TotalPageCount : this.Page + 2); i++)
                {
                    if (this.Page == i)
                    {
                        this.Controls.Add(new LiteralControl(" "));
                        this.Controls.Add(this.CreateLink(i.ToString(), i, "current"));
                    }
                    else
                    {
                        this.Controls.Add(new LiteralControl(" "));
                        this.Controls.Add(this.CreateLink(i.ToString(), i, "page"));
                    }
                }

                // Add a link to the last page
                if (this.Page < this.TotalPageCount - 2)
                {
                    this.Controls.Add(new LiteralControl(" ... "));
                    this.Controls.Add(this.CreateLink(this.TotalPageCount.ToString(), this.TotalPageCount, "page"));
                }
            }

            // Add the next links
            if (this.Page < this.TotalPageCount && this.ShowPreviousNextLinks)
            {
                this.Controls.Add(new LiteralControl(" "));
                this.Controls.Add(this.CreateLink("›", this.Page + 1, "next"));
                this.Controls.Add(new LiteralControl(" "));
                this.Controls.Add(this.CreateLink("»", this.TotalPageCount, "thelast"));
            }
        }

        private void CreatePagerForCommand()
        {
            if (this.TotalRowCount > this.PageSize && !string.IsNullOrEmpty(this.Title))
                this.Controls.Add(new LiteralControl(this.Title + ":"));
            // Add the previous links
            if (this.Page > 1 && this.ShowPreviousNextLinks)
            {
                this.Controls.Add(new LiteralControl(" "));
                this.Controls.Add(this.CreateCommand("First", "«", 1, "thefirst"));
                this.Controls.Add(new LiteralControl(" "));
                this.Controls.Add(this.CreateCommand("Previous", "‹", this.Page - 1, "back"));
            }

            // Loop through the pages
            if (this.TotalRowCount > this.PageSize)
            {
                for (int i = (this.Page <= 2 ? 1 : this.Page - 2); i <= (this.Page >= this.TotalPageCount - 2 ? this.TotalPageCount : this.Page + 2); i++)
                {
                    if (this.Page == i)
                    {
                        this.Controls.Add(new LiteralControl(" "));
                        this.Controls.Add(this.CreateCommand("PageChange", i.ToString(), i, "current"));
                    }
                    else
                    {
                        this.Controls.Add(new LiteralControl(" "));
                        this.Controls.Add(this.CreateCommand("PageChange", i.ToString(), i, "page"));
                    }
                }

                // Add a link to the last page
                if (this.Page < this.TotalPageCount - 2)
                {
                    this.Controls.Add(new LiteralControl(" ... "));
                    this.Controls.Add(this.CreateCommand("Last", this.TotalPageCount.ToString(), this.TotalPageCount, "page"));
                }
            }

            // Add the next links
            if (this.Page < this.TotalPageCount && this.ShowPreviousNextLinks)
            {
                this.Controls.Add(new LiteralControl(" "));
                this.Controls.Add(this.CreateCommand("Next", "›", this.Page + 1, "next"));
                this.Controls.Add(new LiteralControl(" "));
                this.Controls.Add(this.CreateCommand("Last", "»", this.TotalPageCount, "thelast"));
            }
        }

        private string Seo, Component, Params;
        private HyperLink CreateLink(string text, int page, string css = "")
        {
            var SeoPath = false;

            HyperLink control = new HyperLink();
            if (Component == null || Params == null || Seo == null)
            {
                string url = HttpContext.Current.Request.RawUrl.ToLower();
                var domain = HttpContext.Current.Request.Url.Authority;

                var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
                if (!domainCompanyMap.ContainsKey(domain))
                {
                    var company = (new CompanyBLL()).GetCompanyByDomain(domain);
                    domainCompanyMap[domain] = company;
                    HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
                }

                var list = HttpContext.Current.Application[SettingsManager.Constants.AppSEOLinkCache + domainCompanyMap[domain].ID] as System.Collections.Generic.List<SeoLinkData>;
                if (list == null)
                {
                    list = (new SEOBLL()).GetAll(domainCompanyMap[domain].ID)
                        .Select(dto => new SeoLinkData
                        {
                            Url = dto.Url,
                            SeoUrl = dto.SeoUrl,
                            Title = dto.Title,
                            MetaDescription = dto.MetaDescription,
                            MetaKeyWork = dto.MetaKeyWork
                        }).ToList();
                    HttpContext.Current.Application[SettingsManager.Constants.AppSEOLinkCache + domainCompanyMap[domain].ID] = list;
                }

                var seo = list.FirstOrDefault(l => l.SeoUrl.ToLower() == url.ToLower());
                if (seo != null) url = domain + seo.Url;

                var vit_ = url.LastIndexOf("vit-");
                if (vit_ > 0)
                {
                    Seo = url.Substring(0, vit_).Split('/')[1];
                    url = url.Substring(vit_ + 4);
                    var p = url.Split('-');
                    Component = p[p.Length - 1];
                    Params = url.Substring(0, url.LastIndexOf(Component));
                    if (Params.Contains(QueryStringField + "-"))
                    {
                        var s = Params.Replace(QueryStringField + "-", "|").Split('|');
                        if (s.Length > 1)
                        {
                            var index = s[1].IndexOf('-');
                            Params = s[0] + (index > 0 ? s[1].Substring(index + 1): string.Empty);
                        }
                        else
                        {
                            var index = s[0].IndexOf('-');
                            Params = index > 0 ? s[0].Substring(index + 1) : string.Empty;
                        }
                    }
                }
                else if (url.Contains("/vit/"))
                {
                    SeoPath = true;
                    vit_ = url.LastIndexOf("/vit/");
                    var p = url.Split('/');
                    Params = url.Substring(vit_ + 5);
                    Component = p[1];
                    if (Params.Contains(QueryStringField + "/"))
                    {
                        Params = Params.Substring(QueryStringField.Length + 1);
                        var index = Params.IndexOf("/");
                        Params = Params.Substring(index + 1);
                    }
                }
                else
                {
                    var param = url.Split('?');
                    if (param.Length > 1)
                    {
                        Params = param[1].Replace("=", "-").Replace("&", "-");
                        if (Params.Contains(QueryStringField + "="))
                        {
                            var s = Params.Replace(QueryStringField + "=", "|").Split('|');
                            if (s.Length > 1)
                            {
                                var index = s[1].IndexOf('&');
                                Params = s[0] + (index > 0 ? s[1].Substring(index + 1) : string.Empty);
                            }
                            else
                            {
                                var index = s[0].IndexOf('&');
                                Params = index > 0 ? s[0].Substring(index + 1) : string.Empty;
                            }
                        }
                    }
                    param = param[0].Split('/');
                    Component = param[param.Length - 1].Replace(".aspx", "");
                }               
            }

            if (SeoPath) control.NavigateUrl = string.Format("{0}{1}/vit/{2}/{3}/{4}", new URL().BaseUrl, string.IsNullOrEmpty(Component) ? "home" : Component, QueryStringField, page, Params).ToLower();
            else
            {
                control.NavigateUrl = string.Format("{0}{1}vit-{2}-{3}-{4}-{5}", new URL().BaseUrl, Seo, Params, QueryStringField, page, string.IsNullOrEmpty(Component) ? "home" : Component).ToLower();
                control.NavigateUrl = control.NavigateUrl.Replace("--", "-");
            }

            control.Text = text;
            control.CssClass = css;
            return control;
        }

        private LinkButton CreateCommand(string commandName, string text, int page, string css = "")
        {
            LinkButton control = new LinkButton();
            control.Text = text;
            control.CommandName = commandName;
            control.CommandArgument = page.ToString();
            control.Command += new CommandEventHandler(HandleEvent);
            control.CssClass = css;
            return control;
        }

        private void HandleEvent(Object sender, CommandEventArgs e)
        {
            this.Page = Convert.ToInt32(e.CommandArgument);
            OnPagerCommand(new PagerCommandEventArgs(this.StartRowIndex, this.PageSize, e));
            this.ChildControlsCreated = false;
        }
    }
}






//Without a DataSource control

//With the new control this is now pretty easy because the TotalRowCount property is not read only. All you have to do is set this property once you have bound your data, ie (GetData is a method which passes in the StartRowIndex and the MaximumRows and returns only the rows you want to display for that page and GetDataCount returns the total number of rows):
//01.protected void Page_Load(object sender, EventArgs e)
//02.{
//03.if (!this.IsPostBack)
//04.{
//05.this.BindData(0);
//06.pager.TotalRowCount = this.GetDataCount();
//07.}
//08.}
//09. 
//10.protected void BindData(int startRowIndex)
//11.{
//12.lvwItems.DataSource = this.GetData(startRowIndex, pager.PageSize);
//13.lvwItems.DataBind();
//14.}
//15. 
//16.protected void pager_PagerCommand(Object sender, Flixon.Web.UI.WebControls.PagerCommandEventArgs e)
//17.{
//18.this.BindData(e.StartRowIndex);
//19.}

//Then in your aspx file you reference the control in the standard way and put:
//1.<flixon:Pager ID="pager" runat="server" PageSize="15" OnPagerCommand="pager_PagerCommand" />

//Without a DataSoruce control (but using the query string to make it seo friendly)

//You could modify the above to:
//1.<flixon:Pager ID="pager" runat="server" PageSize="15" QueryStringField="Page" Url="/Default.aspx?Page={0}" />

//The PagerCommand event is not necessary if we are using the query string therefore change the code behind file accordingly:
//01.protected void Page_Load(object sender, EventArgs e)
//02.{
//03.if (!this.IsPostBack)
//04.{
//05.lvwItems.DataSource = this.GetData(pager.StartRowIndex, pager.PageSize);
//06.lvwItems.DataBind();
//07.pager.TotalRowCount = this.GetDataCount();
//08.}
//09.}