namespace Web.Asp.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.SessionState;
    using System.Web.UI.WebControls;
    
    using Web.Asp.Controls;
    using System.Web.UI;
    using System.Threading;
    using Web.Business;
    using Web.Asp.ObjectData;
    using Web.Asp.Provider;
    using Web.Model;
    using Provider.Cache;
    using Library;

    abstract public class VITComponent : AbsVITPage
    {
        private readonly CompanyBLL _webConfigBLL;
        private readonly TemplateBLL _templateBLL;
        private readonly ModuleBLL _moduleBLL;

        private readonly string _moduleDataKey;
        
        private readonly string _licenseKey;

        private readonly string ComponentName;
        protected VITTemplate Template;

        protected VITComponent()
        {
            this._webConfigBLL = new CompanyBLL();
            this._moduleBLL = new ModuleBLL();
            this._templateBLL = new TemplateBLL();

            this._moduleDataKey = SettingsManager.Constants.AppModuleCache;
            this._licenseKey = SettingsManager.Constants.SessionLicense;
            
            this.ComponentName = HREF.CurrentComponent;
        }

        protected override void InitializeCulture()
        {
            try
            {
                if (Page.Request.Params.AllKeys.Any(e => e != null && e.ToLower() == SettingsManager.Constants.SendLanguage.ToLower()))
                { 
                    var request = Page.Request.Params.Get(SettingsManager.Constants.SendLanguage);
                    if (request.Contains("="))
                    {
                        var arr = request.Split('=');
                        request = arr[arr.Length - 1];
                    }
                    Session[SettingsManager.Constants.SessionLanguage] = request;
                }

                var language = Session[SettingsManager.Constants.SessionLanguage] as string;
                if (string.IsNullOrEmpty(language))
                {
                    var config = Session[SettingsManager.Constants.SessionCompanyConfig] as CompanyConfigModel;
                    if (config == null)
                    {
                        config = _webConfigBLL.GetCompanyByDomain(HREF.Domain);
                        if (config != null)
                        {
                            language = config.Language;
                            Session[SettingsManager.Constants.SessionCompanyConfig] = config;
                        }
                    }
                }

                if (string.IsNullOrEmpty(language)) language = SettingsManager.Constants.DefauleLanguage;

                UICulture = language;
                Culture = language;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Redirect(HREF.BaseUrl + "Error.aspx?msg=" + Server.UrlEncode("Lỗi định nghĩ ngôn ngữ, thử reload website"));
                log.Error(ex.TraceInformation());
            }
        }
        
        //private bool CheckLicense
        //{
        //    get
        //    {
        //        if (HREF.Domain.Contains("localhost:")) Session[this._licenseKey] = true;
        //        if (Session[this._licenseKey] == null || Convert.ToBoolean(Session[this._licenseKey]) == false)
        //        {
        //            try
        //            {
        //                // kiểm tra bản quyền
        //                var dto = this._webConfigBLL.GetByCompany(this.CompanyId);
        //                if (dto == null) throw new ProviderException("Company does not exist");
        //                if (dto.IsDemo) Session[this._licenseKey] = true;
        //                else Session[this._licenseKey] = MainCore.CheckKey(dto.ExperDate, dto.CreateDate, dto.Keys.Split('|').ToList(), HREF.Domain);
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new ProviderException(ex.Message, ex);
        //            }
        //        }

        //        return Convert.ToBoolean(Session[this._licenseKey]);
        //    }
        //}

        protected void Page_PreInit(Object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //    try
                //    {
                //        this.CheckConfig();
                //        if (!CheckLicense)
                //            HttpContext.Current.Response.Redirect(
                //                HREF.DomainStore + "Components/Errors/Restrict.aspx?msg=" + Server.UrlEncode("License không đúng, vui lòng nhập chính xác License và Số Năm Đăng Ký"));
                //    }
                //    catch (BusinessException ex)
                //    {
                //        HttpContext.Current.Response.Redirect(HREF.DomainStore + "Components/Errors/Restrict.aspx?msg=" + Server.UrlEncode(ex.Message));
                //    }
            }

            LoadTemplate(this); // lấy ra tên template

            LoadModule(this);
            LoadModule(this.Master);
            LoadThirdParty(this.Master);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            // add header: favico, googleanalytics
            var literal = new Literal();
            literal.Text = this.GetHeader();
            this.Header.Controls.Add(literal);
        }
        
        #region load Template & Module
        private void LoadTemplate(VITComponent page)
        {
            var str = new StringBuilder();
            str.Append(HREF.AppPath);
            str.Append("templates/");
            str.Append(Config.Template);
            str.Append("/");

            var templatePath = str.ToString();
            HREF.TemplatePath = templatePath;

            page.MasterPageFile = templatePath + Config.Template + ".master";
            Template = page.Master as VITTemplate;
        }

        private void LoadModule(VITComponent page)
        {
            if (page.Master != null)
            {
                var workerThreads = new List<Thread>();
                var md = GetModuleData(page.Application, page.Session, 1);
                var templatePath = this.HREF.TemplatePath;
                HttpContext ctx = HttpContext.Current;

                foreach (var position in md.Positions)
                {
                    try
                    {
                        //var thread = new Thread(() =>
                        //{
                            HttpContext.Current = ctx;

                            var contentControl = page.Master.FindControl("MainContent");
                            if (contentControl != null)
                            {
                                var pt = contentControl.FindControl(position) as Position;
                                if (pt != null)
                                {
                                    var count = md.PathSkins[position].Length;
                                    var pathSkins = md.PathSkins[position];
                                    var listParams = md.ListParams[position];
                                    var listTitles = md.Titles[position];
                                    var listSkins = md.Skins[position];
                                    var listIds = md.Ids[position];
                                    for (var i = 0; i < count; i++)
                                    {
                                        VITModule control;
                                        try
                                        {
                                            control = (VITModule)page.LoadControl(pathSkins[i]);
                                        }
                                        catch (Exception ex)
                                        {
                                            var mesage = new Label();
                                            mesage.Text = string.Format("Không load được skin {0}: {1}", listSkins[i], ex.Message);
                                            pt.Controls.Add(mesage);
                                            continue;
                                        }

                                        control.Id = listIds[i];
                                        control.Title = listTitles[i];

                                        // gán param vào module
                                        control.Params = new Dictionary<string, string>();
                                        if (listParams[i].Count > 0)
                                        {

                                            foreach (var param in listParams[i])
                                            {
                                                control.Params[param.ParamName] = param.ParamValue;
                                            }
                                        }

                                        pt.Controls.Add(control);
                                    }
                                }
                            }
                        //});
                        //workerThreads.Add(thread);
                        //thread.Start();
                    }
                    catch (Exception ex)
                    {
                        log.Warn("Lỗi không load được module", ex);
                    }
                }

                //// Wait for all the threads to finish so that the results list is populated.
                //// If a thread is already finished when Join is called, Join will return immediately.
                //foreach (Thread thread in workerThreads)
                //{
                //    thread.Join();
                //}
            }
        }
        private void LoadModule(System.Web.UI.MasterPage page)
        {
            List<Thread> workerThreads = new List<Thread>();
            var md = GetModuleData(page.Application, page.Session, 0);
            var templatePath = this.HREF.TemplatePath;
            HttpContext ctx = HttpContext.Current; 

            foreach (var position in md.Positions)
            {
                try
                {
                    //var thread = new Thread(() =>
                    //{
                        HttpContext.Current = ctx;

                        var pt = (Position)page.FindControl(position);
                        if (pt != null)
                        {
                            var count = md.PathSkins[position].Length;
                            var pathSkins = md.PathSkins[position];
                            var listParams = md.ListParams[position];
                            var listTitles = md.Titles[position];
                            var listSkins = md.Skins[position];
                            var listIds = md.Ids[position];

                            for (var i = 0; i < count; i++)
                            {
                                VITModule control;
                                try
                                {
                                    control = (VITModule)page.LoadControl(pathSkins[i]);
                                }
                                catch (Exception ex)
                                {
                                    var mesage = new Label();
                                    mesage.Text = string.Format("Không load được skin {0} [{1}]", listSkins[i], ex.Message);
                                    pt.Controls.Add(mesage);
                                    continue;
                                }

                                control.Id = listIds[i];
                                control.Title = listTitles[i];

                                control.Params = new Dictionary<string, string>();
                                if (listParams[i].Count > 0)
                                {
                                    foreach (var param in listParams[i])
                                    {
                                        control.Params[param.ParamName] = param.ParamValue;
                                    }
                                }

                                pt.Controls.Add(control);
                            }
                        }
                    //});
                    //workerThreads.Add(thread);
                    //thread.Start();
                }
                catch (Exception ex)
                {
                    log.Warn("Lỗi không load được module", ex);
                }
            }

            //// Wait for all the threads to finish so that the results list is populated.
            //// If a thread is already finished when Join is called, Join will return immediately.
            //foreach (Thread thread in workerThreads)
            //{
            //    thread.Join();
            //}
        }

        private ModuleData GetModuleData(HttpApplicationState app, HttpSessionState session, int type)
        {
            if (app == null || session == null) return null;
            var result = app[_moduleDataKey + this.Config.ID] as CacheDictionary<string, ModuleData>;
            if (SettingsManager.AppSettings.IsTestEnviroment) result = null;
            if (result == null)
            {
                result = new CacheDictionary<string, ModuleData>();
                app[_moduleDataKey + this.Config.ID] = result;
            }

            var key = string.Empty;

            try
            {
                if (type == 0) // For Template
                {
                    key = string.Format("T:{0}-C:{1}-L:{2}-A:{3}", Config.Template, this.ComponentName.ToLower(), this.Config.Language, this.Config.ID);

                    if (!result.ContainsKey(key))
                    {
                        var md = new ModuleData();
                        result.Add(key, md);

                        md.Positions = this._templateBLL.GetAllPositionTemplates(Config.Template).Select(e => e.ID).ToList();
                        foreach (var position in md.Positions)
                        {
                            var query = this._moduleBLL.GetAllModuleConfigs(
                                this.Config.ID,
                                this.Config.Language,
                                Config.Template,
                                this.ComponentName,
                                position,
                                true);

                            var lstPathSkins = new List<string>();
                            var lstListParams = new List<IList<ModuleParamData>>();
                            var lstTitles = new List<string>();
                            var lstSkins = new List<string>();
                            var lstIds = new List<int>();

                            var modules = query.Where(e => e.Publish).ToList();
                            foreach (var row in modules)
                            {
                                var pathModule = new StringBuilder();
                                //pathModule.Append("~");
                                pathModule.Append(HREF.TemplatePath);
                                pathModule.Append("Skins/");
                                pathModule.Append(row.SkinName);
                                pathModule.Append(".ascx");
                                lstPathSkins.Add(pathModule.ToString());
                                lstTitles.Add(row.Title);
                                lstSkins.Add(row.SkinName);
                                lstIds.Add(Convert.ToInt32(row.Id));

                                var moduleParams = this._moduleBLL.GetParamConfig(row.ModuleName, row.Id)
                                    .Select(e => new ModuleParamData
                                    {
                                        ParamValue = e.Value,
                                        ParamName = e.ID
                                    }).ToList();
                                lstListParams.Add(moduleParams);
                            }

                            md.PathSkins[position] = lstPathSkins.ToArray();
                            md.ListParams[position] = lstListParams;
                            md.Titles[position] = lstTitles.ToArray();
                            md.Skins[position] = lstSkins.ToArray();
                            md.Ids[position] = lstIds.ToArray();
                        }
                    }
                }
                else if (type == 1) // For Component
                {
                    key = string.Format("C:{0}-T:{1}-L:{2}-A:{3}", this.ComponentName, this.Config.Template.ToLower(), this.Config.Language, this.Config.ID);

                    if (!result.ContainsKey(key))
                    {
                        var md = new ModuleData();
                        result.Add(key, md);

                        var listpositions = this._templateBLL.GetAllPositionComponents(this.Config.Template, this.ComponentName);
                        md.Positions = listpositions.Select(dto => dto.ID).ToArray();

                        foreach (var position in md.Positions)
                        {
                            var query = this._moduleBLL.GetAllModuleConfigs(
                                this.Config.ID,
                                this.Config.Language,
                                this.Config.Template,
                                this.ComponentName,
                                position,
                                false);

                            var lstPathSkins = new List<string>();
                            var lstListParams = new List<IList<ModuleParamData>>();
                            var lstTitles = new List<string>();
                            var lstSkins = new List<string>();
                            var lstIds = new List<int>();

                            var modules = query.Where(e => e.Publish).ToList();
                            foreach (var row in modules)
                            {
                                var pathModule = new StringBuilder();
                                pathModule.Append(HREF.TemplatePath);
                                pathModule.Append("Skins/");
                                pathModule.Append(row.SkinName);
                                pathModule.Append(".ascx");
                                lstPathSkins.Add(pathModule.ToString());
                                lstTitles.Add(row.Title);
                                lstSkins.Add(row.SkinName);
                                lstIds.Add(Convert.ToInt32(row.Id));
                                var moduleParams = this._moduleBLL.GetParamConfig(row.ModuleName, row.Id)
                                    .Select(e => new ModuleParamData
                                    {
                                        ParamValue = e.Value,
                                        ParamName = e.ID
                                    }).ToList();
                                lstListParams.Add(moduleParams);
                            }

                            md.PathSkins[position] = lstPathSkins.ToArray();
                            md.ListParams[position] = lstListParams;
                            md.Titles[position] = lstTitles.ToArray();
                            md.Skins[position] = lstSkins.ToArray();
                            md.Ids[position] = lstIds.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                HttpContext.Current.Response.Redirect(HREF.BaseUrl + "Error.aspx?msg=" + Server.UrlEncode("Xảy ra lỗi trong lần chạy đầu tiên, thử reload website"));
            }

            return result[key];
        }
        #endregion

        #region Third Party for template
        private void LoadThirdParty(System.Web.UI.MasterPage page)
        {
                var thirdpartys = this._webConfigBLL.GetThirdPartyByWebConfigId(Config.ID).Where(e => e.IsPublished);
                var positions = thirdpartys.Select(e => e.PositionName).Distinct().ToList();
                foreach (var position in positions)
                {
                    try
                    {
                        var pt = (Position)page.FindControl(position);
                        if (pt == null) continue;

                        var thirdpartyInPositions = thirdpartys.Where(e => e.PositionName == position).ToList();
                        foreach (var thirdparty in thirdpartyInPositions)
                        {
                            pt.Controls.Add(new LiteralControl(thirdparty.ContentHTML));
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Warn("Lỗi không load được third party", ex);
                    }
                }
        }
        #endregion

        #region Header
        private string GetFacebookOpenGraph(string facebookAppId, string facebookPersonalId)
        {
            var itemId = 0;

            var oGType = "website";
            string facebookOpenGraph = string.Empty;
            if (itemId == 0)
            {
                itemId = this.GetValueRequest<int>(SettingsManager.Constants.SendProduct);
                if (itemId > 0) oGType = "product";
            }
            if (itemId == 0)
            {
                itemId = this.GetValueRequest<int>(SettingsManager.Constants.SendArticle);
                if (itemId > 0) oGType = "article";
            }
            if (itemId == 0)
            {
                itemId = this.GetValueRequest<int>(SettingsManager.Constants.SendCategory);
                if(itemId > 0) oGType = "category";
            }
            if (itemId == 0)
            {
                itemId = this.GetValueRequest<int>(SettingsManager.Constants.SendDocument);
                if (itemId > 0) oGType = "document";
            }

            var data = CacheProvider.GetCache<DataSimpleModel>(CacheProvider.Keys.Obj, Config.ID, oGType, Config.Language, itemId);
            if (data == null)
            {
                data = new DataSimpleModel();
                var articleBLL = new ArticleBLL();
                switch (oGType)
                {
                    case "category": data = articleBLL.GetDataSingle(Config.ID, Config.Language, "CAT", itemId);
                        if (data != null && !string.IsNullOrEmpty(data.ImagePath))
                        {
                            // update path image
                            data.ImagePath = HREF.VirtualPath(SettingsManager.Constants.PathCompanyImage + data.ImagePath, Config.ID);
                        }
                        break;
                    case "article": data = articleBLL.GetDataSingle(Config.ID, Config.Language, "ART", itemId);
                        if (data != null && !string.IsNullOrEmpty(data.ImagePath))
                        {
                            // update path image
                            data.ImagePath = HREF.VirtualPath(SettingsManager.Constants.PathArticleImage + data.ImagePath, Config.ID);
                        }
                        break;
                    case "product":
                        data = articleBLL.GetDataSingle(Config.ID, Config.Language, "PRO", itemId);
                        if (data != null && !string.IsNullOrEmpty(data.ImagePath))
                        {
                            // update path image
                            data.ImagePath = HREF.VirtualPath(SettingsManager.Constants.PathProductImage + data.ImagePath, Config.ID);
                        }
                        break;
                    case "document":
                        data = articleBLL.GetDataSingle(Config.ID, Config.Language, "DOC", itemId);
                        if (data != null && !string.IsNullOrEmpty(data.ImagePath))
                        {
                            // update path image
                            data.ImagePath = HREF.VirtualPath(SettingsManager.Constants.PathDocumentFile + data.ImagePath, Config.ID);
                        }
                        break;
                    default:
                        data = _webConfigBLL.GetDataSingle(Config.ID, Config.Language);
                        if (data != null && !string.IsNullOrEmpty(data.ImagePath))
                        {
                            // update path image
                            data.ImagePath = HREF.VirtualPath(SettingsManager.Constants.PathCompanyImage + data.ImagePath, Config.ID);
                        }
                        
                        // search|tag
                        var search = this.GetValueRequest<string>("key");
                        if (string.IsNullOrEmpty(search)) search = this.GetValueRequest<string>(SettingsManager.Constants.SendTag);
                        if (!string.IsNullOrEmpty(search))
                        {
                            search = search.Replace('-', ' ');
                            data.Title = search + " - " + data.Title;
                            data.TargetTag = search + ", " + data.TargetTag;
                            data.Description = search + ", " + data.Description;
                        }

                        //attribute
                        var title = string.Empty;
                        var description = string.Empty;
                        var tag = string.Empty;
                        itemId = this.GetValueRequest<int>(SettingsManager.Constants.SendAttributeId);
                        var productBLL = new ProductBLL();
                        var attributeCategory = productBLL.GetAttributeCategory(this.GetValueRequest<int>(SettingsManager.Constants.SendAttributeId), this.Config.ID);
                        if (attributeCategory != null)
                        {
                            title = attributeCategory.Name;
                            description = attributeCategory.Name;
                            tag = attributeCategory.Name;
                        }

                        var attribute = productBLL.GetAttributeValue(this.Config.ID, this.GetValueRequest<int>(SettingsManager.Constants.SendAttributeValue));
                        if (attribute != null)
                        {
                            title = title + " - " + attribute.Value;
                            description = description + " " + attributeCategory.Name;
                            tag = tag + "," + attributeCategory.Name;
                        }

                        if (!string.IsNullOrEmpty(title))  data.Title = title + " | " + data.Title;
                        if (!string.IsNullOrEmpty(description)) data.Description = description + ", " + data.Description;
                        if (!string.IsNullOrEmpty(tag)) data.TargetTag = tag + ", " + data.TargetTag;
                        break;
                }

                    CacheProvider.SetCache(data, CacheProvider.Keys.Obj, Config.ID, oGType, Config.Language, itemId);
            }

            if (data != null)
            {
                if (string.IsNullOrEmpty(Title)) Title = data.Title;
                if (string.IsNullOrEmpty(MetaDescription))  MetaDescription = data.Description;
                if (string.IsNullOrEmpty(MetaKeywords)) MetaKeywords = data.TargetTag;

                var company = _webConfigBLL.GetDataSingle(Config.ID, Config.Language);
                if (company != null && !string.IsNullOrEmpty(company.Title) && !Title.Contains(company.Title)) Title += " - " + company.Title;

                if (!string.IsNullOrEmpty(Title) && Title.Length > SettingsManager.Constants.MaxLengthTitle)
                    Title = Title.Substring(0, SettingsManager.Constants.MaxLengthTitle);
                if (!string.IsNullOrEmpty(MetaDescription) && MetaDescription.Length > SettingsManager.Constants.MaxLengthDescription)
                    MetaDescription = MetaDescription.Substring(0, SettingsManager.Constants.MaxLengthDescription);

                var tbTag = new StringBuilder();
                tbTag.AppendFormat("<link rel='image_src' href='{0}' />", data.ImagePath);
                if (!string.IsNullOrEmpty(facebookAppId)) tbTag.AppendFormat("<meta property='fb:app_id' content='{0}'/>", facebookAppId);
                if (!string.IsNullOrEmpty(facebookPersonalId)) tbTag.AppendFormat("<meta property='fb:admins' content='{0}'/>", facebookPersonalId);
                tbTag.AppendFormat("<meta property='og:title' content='{0}' />", data.Title);
                tbTag.AppendFormat("<meta property='og:type' content='{0}' />", oGType);
                tbTag.AppendFormat("<meta property='og:image' content='{0}' />", data.ImagePath);
                tbTag.AppendFormat("<meta property='og:site_name' content='{0}' />", data.Title);
                facebookOpenGraph += tbTag.ToString();

                if (!string.IsNullOrEmpty(facebookAppId))
                {
                    var script = new StringBuilder();
                    script.Append("<script>");
                    script.Append("window.fbAsyncInit = function ");
                    script.Append("() {");
                    script.Append("FB.init({");
                    script.AppendFormat("appId: '{0}',", facebookAppId);
                    script.Append("xfbml: true,");
                    script.Append("version: 'v2.2'");
                    script.Append("});");
                    script.Append("};");

                    script.Append("(function (d, s, id) {");
                    script.Append("var js, fjs = d.getElementsByTagName(s)[0];");
                    script.Append("if (d.getElementById(id)) { return; }");
                    script.Append("js = d.createElement(s); js.id = id;");
                    script.Append("js.src = '//connect.facebook.net/en_US/sdk.js';");
                    script.Append("fjs.parentNode.insertBefore(js, fjs);");
                    script.Append("} (document, 'script', 'facebook-jssdk'));");
                    script.Append("</script>");
                    facebookOpenGraph += script.ToString();
                }
            }
            return facebookOpenGraph;
        }

        private string GetHeader()
        {
            var header = string.Empty;

            var favicon = this.HREF.VirtualPath(SettingsManager.Constants.PathCompanyImage + Company.WebIcon, Config.ID);

            header = string.Format("<link href='{0}' rel='shortcut icon' type='image/x-icon' />", favicon);
            header += string.Format("<link href='{0}' rel='icon' type='image/ico' />", favicon);
            header += string.Format("<meta http-equiv='content-language' content='{0}' />", this.Config.Language);
            header += string.Format("<link rel='alternate' href='/' hreflang='{0}' />", this.Config.Language);
            
            if (!Company.IsRightClick)
            {
                header += "<script type='text/javascript'>$(document).bind('contextmenu', function (e) { return false; });</script>";
            }
            if (!Company.IsSelectCoppy)
            {
                header += "<script src='/Includes/disablecopy/disable-copy.js' type='text/javascript'></script>";
                header += "<link rel='stylesheet' type='text/css' href='/Includes/disablecopy/disable-copy.css' />";
            }

            header += Company.GoogleAnalytics;
            header += Company.VerifyOutside;
            header += this.GetFacebookOpenGraph(Company.FacebookAppId, Company.FacebookPersonalId);

            return header;
        }
        #endregion

        private void CheckConfig()
        {
            if (Application[SettingsManager.Constants.CheckConfigCache] == null || Convert.ToBoolean(Application[SettingsManager.Constants.CheckConfigCache]) == false)
            {
                // check ConnectionString
                var connec = System.Configuration.ConfigurationManager.ConnectionStrings["TVYConnection"].ConnectionString.ToLower().Replace(" ", string.Empty);
                if (string.IsNullOrEmpty(connec)
                    || connec.Contains("trusted_connection=true;")
                    || connec.Contains("integratedsecurity=true;")
                    || connec.Contains("datasource=localhost;")
                    || connec.Contains("source=(local)")
                    || connec.Contains("source=."))
                {
                    log.Error("Không thể kết nối đến Data base, hãy chắc chắc rằng database của bạn đang ở trang thái start và public");
                    HttpContext.Current.Response.Redirect(
                       (new URL()).BaseUrl + "Error.aspx?msg="
                       + Server.UrlEncode("Không thể kết nối đến Data base, hãy chắc chắc rằng database của bạn đang ở trang thái start và public"));
                }
                // check MaxItem
                if (SettingsManager.AppSettings.MaxItem < 1 || 300 < SettingsManager.AppSettings.MaxItem)
                {
                    log.Error("Cấu hình không đúng: 0 < MaxItem < 301");
                    HttpContext.Current.Response.Redirect(
                        (new URL()).BaseUrl + "Error.aspx?msg="
                        + Server.UrlEncode("Cấu hình không đúng: 0 < MaxItem < 301 "));
                }

                // check exist file JsonPost
                if (!System.IO.File.Exists(string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/"), "JsonPost.aspx")))
                {
                    log.Error("Không tìm thấy file JsonPost");
                    HttpContext.Current.Response.Redirect(
                        (new URL()).BaseUrl + "Error.aspx?msg="
                        + Server.UrlEncode("Không thể JsonPost, vui lòng liên hệ hỗ trợ kỹ thuật"));
                }

                Application[SettingsManager.Constants.CheckConfigCache] = true;
            }
        }

        private string VirtualPath(string folderPath, string fileName)
        {
            return string.IsNullOrEmpty(fileName) ? string.Empty : string.Format("{0}{1}{2}", SettingsManager.AppSettings.DomainStore, folderPath, fileName);
        }
    }
}
