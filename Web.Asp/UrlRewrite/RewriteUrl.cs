namespace Web.Asp.UrlRewrite
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    using log4net;
    using Web.Business;
    using Web.Model;
    using Provider;

    public class RewriteUrl : IHttpModule
    {
        #region IHttpModule Members

        public void Dispose()
        {
        }

        protected static readonly ILog log = LogManager.GetLogger(typeof(RewriteUrl));

        public static string RawUrl
        {
            get { return HttpContext.Current.Request.RawUrl.Replace('\'', '/'); }
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += Context_BeginRequest;
        }

        private static void Context_BeginRequest(object sender, EventArgs e)
        {
            var httpApplication = (HttpApplication)sender;
            var rawUrl = RawUrl.ToLower();

            if (!rawUrl.Contains("__browserlink") && !rawUrl.Contains("/uploads/") && !rawUrl.Contains("/includes/") && !rawUrl.Contains("/templates/") && !rawUrl.Contains("/modules/"))
            {
                var domain = HttpContext.Current.Request.Url.Authority;

                var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
                if (!domainCompanyMap.ContainsKey(domain) || domainCompanyMap[domain] == null || domainCompanyMap[domain].ID == 0 || domainCompanyMap[domain].Language == "")
                {
                    var company = (new CompanyBLL()).GetCompanyByDomain(domain);
                    domainCompanyMap[domain] = company;
                    HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
                }

                var originalString = HttpContext.Current.Request.Url.OriginalString;

                if (rawUrl != "/")
                {
                    var list = HttpContext.Current.Application[SettingsManager.Constants.AppSEOLinkCache + domainCompanyMap[domain].ID] as List<SEOLinkModel>;
                    if (list == null || list.Count == 0)
                    {
                        list = (new SEOBLL()).GetAll(domainCompanyMap[domain].ID)
                            .Select(dto => new SEOLinkModel
                            {
                                Url = dto.Url,
                                SeoUrl = dto.SeoUrl,
                                Title = dto.Title,
                                MetaDescription = dto.MetaDescription,
                                MetaKeyWork = dto.MetaKeyWork,
                                LanguageId = dto.LanguageId
                            }).ToList();
                        HttpContext.Current.Application[SettingsManager.Constants.AppSEOLinkCache + domainCompanyMap[domain].ID] = list;
                    }

                    var query = string.Empty;
                    if (rawUrl.Contains("?"))
                    {
                        var urlarr = rawUrl.Split('?');
                        query = urlarr[urlarr.Length - 1];
                        rawUrl = urlarr[0];
                    }

                    var seo = list.FirstOrDefault(l => l.SeoUrl.ToLower() == rawUrl);
                    if (seo != null)
                    {
                        rawUrl = domain + seo.Url;
                        if (domainCompanyMap[domain].Language != seo.LanguageId)
                        {
                            var querylang = SettingsManager.Constants.SendLanguage + "=" + seo.LanguageId;
                            if (string.IsNullOrEmpty(query)) query = querylang;
                            else query = "&" + querylang;
                        }
                    }

                    var vit_ = rawUrl.LastIndexOf("vit-"); //title-vit-sendKey-value-Component
                    var _vit = rawUrl.LastIndexOf("/vit"); //Component/vit/sendKey/value/title
                    
                    try
                    {
                        var newURL = new StringBuilder();
                        if (vit_ > 0)
                        {
                            rawUrl = rawUrl.Substring(vit_ + 4);
                            var p = rawUrl.Split('-');

                            newURL.AppendFormat("/Templates/{0}/Components/{1}.aspx", domainCompanyMap[domain].Template, p[p.Length - 1]);

                            var lengParam = p.Length - 2;
                            if (lengParam > 0)
                            {
                                newURL.Append("?");

                                var param = new StringBuilder();
                                for (var i = 0; i < lengParam; i++)
                                {
                                    param.AppendFormat("{0}={1}&", p[i++], p[i]);
                                }
                                newURL.Append(param.ToString().Substring(0, param.Length - 1));
                            }
                        }
                        else if (_vit > 0)
                        {
                            var param = Regex.Split(rawUrl, "/vit");

                            if (param[0].Split('/').Length > 1) newURL.AppendFormat("/Templates/{0}/Components", domainCompanyMap[domain].Template);
                            newURL.Append(param[0]).Append(".aspx");
                            if (param[1].Length > 0)
                            {
                                newURL.Append("?");
                                string[] p = param[1].Split('/');
                                if (p.Length > 1) // "vit/" đã có 1 ký tự '/'
                                {
                                    for (int i = 1; i < p.Length; i++)
                                    {
                                        newURL.Append(p[i++]);
                                        if (i < p.Length) newURL.Append("=" + p[i] + "&");
                                    }
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(newURL.ToString()))
                        {
                            if (!string.IsNullOrEmpty(query))
                            {
                                if (newURL.ToString().Contains("?")) newURL.Append("&");
                                else newURL.Append("?");
                                newURL.Append(query);
                            }

                            httpApplication.Context.RewritePath(newURL.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        // xu ly loi
                        log.Error(ex);
                    }
                }
                else
                {

                    httpApplication.Context.RewritePath(string.Format("/Templates/{0}/Components/Home.aspx", domainCompanyMap[domain].Template));
                }
            }
        }

        #endregion
    }
}
