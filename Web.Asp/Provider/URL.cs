namespace Web.Asp.Provider
{
    using Library;
    using Library.Web;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Web;
    using Web.Business;
    using Web.Model;

    public class URL
    {
        public string AppPath
        {
            get
            {
                string appPath = HttpContext.Current.Request.ApplicationPath;
                if (appPath != "/") appPath = appPath + "/";
                return appPath;
            }
        }

        // duong dan web, vd: BaseUrl = http: //hoangvuit.name.vn

        public string BaseUrl
        {
            get
            {
                return string.Format("{0}://{1}{2}{3}",
                                        this.GetScheme(),
                                        HttpContext.Current.Request.Url.Host,
                                        HttpContext.Current.Request.Url.Port == 80
                                            ? string.Empty
                                            : ":" + HttpContext.Current.Request.Url.Port,
                                        HttpContext.Current.Request.ApplicationPath);
            }
        }

        // duong dan vat ly den thu muc goc chua website
        public string BaseDir
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/");
            }
        }

        public static List<SEOLinkModel> LinkSEOs
        {
            get
            {
                var list = HttpContext.Current.Application[SettingsManager.Constants.AppSEOLinkCache + HttpContext.Current.Session[SettingsManager.Constants.SessionCompanyConfig + HttpContext.Current.Request.Url.Authority]] as List<SEOLinkModel>;
                if (list == null) list = new List<SEOLinkModel>();
                return list;
            }
        }

        private static readonly ILog logger = LogManager.GetLogger(typeof(URL));

        private static string _DomainPublic = string.Empty;

        /// <summary>
        /// Domain trỏ ra ngoài web public
        /// </summary>
        public string DomainPublic
        {
            get
            {
                if (!string.IsNullOrEmpty(_DomainPublic)) return _DomainPublic;
                _DomainPublic = SettingsManager.AppSettings.DomainPublic;
                return _DomainPublic;
            }
        }
        private static string _DomainStore = string.Empty;
        /// <summary>
        /// Domain trỏ đến hosting chứa các file upload lên server
        /// </summary>
        public string DomainStore
        {
            get
            {
                if (_DomainStore.Length > 0) return _DomainStore;
                _DomainStore = SettingsManager.AppSettings.DomainStore;
                return _DomainStore;
            }
        }
        public string Domain
        {
            get
            {
                return HttpContext.Current.Request.Url.Authority;
            }
        }
        public string DomainLink
        {
            get
            {
                return string.Format("{0}://{1}{2}",
                                    this.GetScheme(),
                                    HttpContext.Current.Request.Url.Authority,
                                    HttpContext.Current.Request.ApplicationPath);
            }
        }

        public string TemplatePath {
            get
            {
                return Convert.ToString(HttpContext.Current.Session["TemplatePath"]);
            }
            set
            {
                HttpContext.Current.Session["TemplatePath"] = value;
            }
        }

        public string CurrentPath
        {
            get
            {
                var directoryName = Path.GetDirectoryName(HttpContext.Current.Request.FilePath);
                return directoryName != null ? directoryName.Replace("\\", "/") : null;
            }
        }

        // trang web va parametter hien tai, vd: BaseUrl = http ://hoangvuit.name.vn?vu=good 
        public string PathAndQuery
        {
            get
            {
                return HttpContext.Current.Request.Url.PathAndQuery;
            }
        }

        public string CurrentComponent
        {
            get
            {
                string[] url = HttpContext.Current.Request.Url.LocalPath.Split('/');
                string view = url[url.Length - 1];
                return view.Split('.')[0];
            }
        }
        /// <summary>
        /// lấy Ip may client request lên server
        /// </summary>
        public string ClientID
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
        }

        static URL()
        {
        }

        public string PhysicalPath(string folderPath, int companyId)
        {
            var companyPath = string.Format(SettingsManager.AppSettings.FolderUpload, companyId);
            return string.Format("{0}{1}{2}", this.BaseDir, companyPath, folderPath);
        }

        public string VirtualPath(string folderPath, int companyId)
        {
            var companyPath = string.Format(SettingsManager.AppSettings.FolderUpload, companyId);
            return string.IsNullOrEmpty(folderPath) ? string.Empty : string.Format("{0}{1}{2}", SettingsManager.AppSettings.DomainStore, companyPath, folderPath);
        }

        public List<string> LinkTag(string ComponentName, string tags, string doamin = "")
        {
            if (string.IsNullOrEmpty(tags)) return new List<string>();
            var links = new List<string>();
            var tagList = tags.Split(',').ToArray();
            foreach (var tag in tagList)
            {
                if (string.IsNullOrEmpty(tag)) continue;
                var str = new StringBuilder();
                if (string.IsNullOrEmpty(doamin)) str.AppendFormat("<a href='{0}{1}/vit/{2}/{3}' title='{4}'>{4}</a>", AppPath, ComponentName, SettingsManager.Constants.SendTag, tag.ConvertToUnSign(), tag);
                else str.AppendFormat("<a href='http://{0}{1}{2}/vit/{3}/{4}' title='{5}'>{5}</a>", doamin, AppPath, ComponentName, SettingsManager.Constants.SendTag, tag.ConvertToUnSign(), tag);
                links.Add(str.ToString().ToLower());
            }

            return links;
        }

        public string LinkComponent(string ComponentName, string Param = "", bool seoFormat = true)
        {
            if (string.IsNullOrEmpty(ComponentName)) return string.Empty;
            var str = new StringBuilder();
            str.Append(AppPath);

            if (!seoFormat)
            {
                str.Append(ComponentName);
                if (!string.IsNullOrEmpty(Param))
                {
                    str.Append("/vit/");
                    str.Append(Param);
                }
                else str.Append("/vit");

                return str.ToString();
            }
            else
            {
                if (!string.IsNullOrEmpty(Param))
                {
                    string[] param = Param.Split('/');
                    int n = param.Length;
                    if (n % 2 == 1) str.AppendFormat("{0}-vit", param[--n]);
                    else str.Append("vit");

                    for (int i = 0; i < n; i++)
                    {
                        str.AppendFormat("-{0}-{1}", param[i++], param[i]);
                    }
                }
                else str.Append("vit");

                str.AppendFormat("-{0}", ComponentName);
                var url = str.ToString().Replace("--", "-").Replace("--", "-").ToLower();

                var list = GetSEOLink();
                if (list != null)
                {
                    var seo = list.FirstOrDefault(l => l.Url == url);
                    if (seo != null) url = string.Format("{0}://{1}", this.GetScheme(), HttpContext.Current.Request.Url.Authority) + seo.SeoUrl;
                }

                return url;
            }
        }
        public void TransferComponent(string ComponentName, string View = "", string Param = "")
        {
            if (string.IsNullOrEmpty(View)) View = ComponentName;

            var url = string.Format("/Components/{0}/{1}.aspx", ComponentName, View);
            if (!string.IsNullOrEmpty(Param)) url += "?" + Param;

            HttpContext.Current.Server.Transfer(url);

        }
        public void RedirectComponent(string ComponentName, string Param = "", bool seo = true, bool endResponse = true)
        {
            HttpContext.Current.Response.Redirect(LinkComponent(ComponentName, Param, seo), endResponse);
        }

        public void ClearCache(string key)
        {
            var th = new System.Threading.Thread(delegate()
                                                     {
                                                         this.Post(SettingsManager.AppSettings.DomainPublic + "/Clear.aspx?" + SettingsManager.Constants.SendClearData + "=" + key, "");
                                                         this.Post(SettingsManager.AppSettings.DomainPublic, "");
                                                     });
            th.Start();
        }

        /// <summary>
        /// The post.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        public string Post(string url, string data)
        {
            string vystup = null;
            try
            {
                //Our postvars
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                //Initialisation, we use localhost, change if appliable
                var WebReq = (HttpWebRequest)WebRequest.Create(url);
                //Our method is post, otherwise the buffer (postvars) would be useless
                WebReq.Method = "POST";
                //We use form contentType, for the postvars.
                WebReq.ContentType = "application/x-www-form-urlencoded";
                //The length of the buffer (postvars) is used as contentlength.
                WebReq.ContentLength = buffer.Length;
                //We open a stream for writing the postvars
                var PostData = WebReq.GetRequestStream();
                //Now we write, and afterwards, we close. Closing is always important!
                PostData.Write(buffer, 0, buffer.Length);
                PostData.Close();
                //Get the response handle, we have no true response yet!
                var WebResp = (HttpWebResponse)WebReq.GetResponse();
                //Let's show some information about the response
                Console.WriteLine(WebResp.StatusCode);
                Console.WriteLine(WebResp.Server);

                //Now, we read the response (the string), and output it.
                var Answer = WebResp.GetResponseStream();
                if (Answer != null)
                {
                    var _Answer = new StreamReader(Answer);
                    vystup = _Answer.ReadToEnd();
                }

                //Congratulations, you just requested your first POST page, you
                //can now start logging into most login forms, with your application
                //Or other examples.
            }
            catch (Exception)
            {
            }
            return (vystup ?? string.Empty).Trim() + "\n";
        }

        /// <summary>
        /// The post json.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="data">
        /// The Json string.
        /// </param>
        public string PostJson(string url, string data)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var rev = streamReader.ReadToEnd();
                    return rev;
                }
            }
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="contextPath">
        /// The context Path.
        /// </param>
        /// <returns>
        /// The <see cref="IApiCaller"/>.
        /// </returns>
        public IEnumerable<TEntity> CallApi<TEntity>(string url, IDictionary<string, object> parameters = null) where TEntity : class
        {
            string urlBase = null;
            try
            {
                var httpClientHandler = new HttpClientHandler();

                httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                httpClientHandler.Credentials = new NetworkCredential("", "");

                var httpClient = new HttpClient(httpClientHandler, false);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson", 1));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json", 0.8));

                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

                var apiCaller = new ApiCaller(httpClient, urlBase);

                var data = apiCaller.GetAll<TEntity>(parameters);

                apiCaller.Dispose();
                httpClientHandler.Dispose();

                return data;
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Cannot create ApiCaller. UrlBase: {0}, ContextPath: {1}", urlBase, url));
                if (ex is ApplicationException)
                {
                    throw;
                }

                throw new ApplicationException(string.Format("Cannot create ApiCaller. UrlBase: {0}, ContextPath: {1}", urlBase, url), ex);
            }
        }

        private List<SEOLinkModel> GetSEOLink()
        {
            var companyId = 1;
            var domain = HttpContext.Current.Request.Url.Authority;
            var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
            if (!domainCompanyMap.ContainsKey(domain))
            {
                var company = (new CompanyBLL()).GetCompanyByDomain(domain);
                domainCompanyMap[domain] = company;
                HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
            }
            if (domainCompanyMap.ContainsKey(domain) && domainCompanyMap[domain] != null)
            {
                companyId = domainCompanyMap[domain].ID;

                var list = HttpContext.Current.Application[SettingsManager.Constants.AppSEOLinkCache + companyId] as List<SEOLinkModel>;
                if (list == null || list.Count == 0)
                {
                    var bll = new SEOBLL();
                    list = bll.GetAll(companyId).ToList();
                    HttpContext.Current.Application[SettingsManager.Constants.AppSEOLinkCache + companyId] = list;
                }

                return list;
            }
            return new List<SEOLinkModel>();
        }

        public SEOLinkModel CreateSEO(int id, string title, string keyWord, string description, int companyId, string component, int parentId, string languageId)
        {
            var bll = new SEOBLL();
            var seo = bll.GetById(id, companyId);
            if (seo == null) //thêm
            {
                seo = new SEOLinkModel();
                seo.RefItem = id;
            }

            var param = string.Empty;
            switch(component)
            {
                case "Article": param = SettingsManager.Constants.SendArticle + "/" + id; break;
                case "Product": param = SettingsManager.Constants.SendProduct + "/" + id; break;
                case "Articles":
                case "Products": param = SettingsManager.Constants.SendCategory + "/" + id; break;
                case "Attributes":
                    param = SettingsManager.Constants.SendAttributeId + "/" + parentId + SettingsManager.Constants.SendAttributeValue + "/" + id;
                    component = "Products";
                    break;
            }

            seo.Url = this.LinkComponent(component, param);
            seo.MetaKeyWork = keyWord.Trim();
            if (!string.IsNullOrEmpty(description))
            {
                seo.MetaDescription = description.DeleteHTMLTag().Trim();
                seo.MetaDescription = description.Length > 200 ? description.Substring(0, 200) : seo.MetaDescription;
            }
            seo.SeoUrl = CreateLink(title, parentId, companyId, languageId);

            bll.Save(seo, companyId, languageId);

            return seo;
        }

        public string CreateLink(string title, int parentId, int companyId, string languageId)
        {
            var catBLL = new ArticleBLL();
            var seoBLL = new SEOBLL();
            var link = string.Empty;
            var seoTitle = title.Trim().ConvertToUnSign();
            if (parentId > 0)
            {
                var category = catBLL.GetCategoryById(companyId, languageId, parentId);
                if (category != null)
                {
                    var seoCat = seoBLL.GetById(parentId, companyId);
                    if (seoCat == null)
                    {
                        if (category.TYPEID == "ART")
                            seoCat = CreateSEO(category.ID, category.NAME, category.NAME, category.DESCRIPTION, companyId, "Articles", category.PARENTID, languageId);
                        else if (category.TYPEID == "PRO")
                            seoCat = CreateSEO(category.ID, category.NAME, category.NAME, category.DESCRIPTION, companyId, "Products", category.PARENTID, languageId);
                        //else if (category.TYPEID == "ATR")
                        //    seoCat = CreateSEO(category.ID, category.NAME, category.NAME, category.DESCRIPTION, companyId, "Products", category.PARENTID ?? 0, languageId);
                    }

                    link = seoCat.SeoUrl + "/" + seoTitle;
                }
            }

            if (string.IsNullOrEmpty(link))
            {
                link = "/" + seoTitle;
            }

            return link;
        }

        public string GetScheme()
        {
            if (HttpContext.Current.Request.Headers.AllKeys.Any(e => e.ToLower() == "x-forwarded-proto"))
                return HttpContext.Current.Request.Headers["X-Forwarded-Proto"];
            else if (HttpContext.Current.Request.IsSecureConnection)
                return "https";
            else
                return HttpContext.Current.Request.Url.Scheme;
        }
    }
}
