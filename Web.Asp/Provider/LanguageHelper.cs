namespace Web.Asp.Provider
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Xml.Linq;
    using Library;
    using Web.Model;
    using Cache;

    public class LanguageHelper
    {
        public string this[string key]
        {
            get
            {
                int companyId;
                var url = HttpContext.Current.Request.RawUrl.Replace('\'', '/').ToLower();
                if (url.StartsWith(SettingsManager.AppSettings.DomainStore)) companyId = 1;
                else
                {
                    var config = HttpContext.Current.Session[SettingsManager.Constants.SessionCompanyConfig] as CompanyConfigModel;
                    if (config == null) return key;
                    companyId = config.ID;
                }
                

                var langs = HttpContext.Current.Application[SettingsManager.Constants.LanguageCache + companyId] as IDictionary<string, IDictionary<string, string>>;

                // neu mat cache thi get lai
                if (langs == null) langs = new Dictionary<string, IDictionary<string, string>>();
                if (!langs.ContainsKey(this.LanguageId) || langs[LanguageId] == null)
                {
                    langs[LanguageId] = new Dictionary<string, string>();

                    var languagePath = (new URL()).PhysicalPath(SettingsManager.Constants.PathLanguageFile, companyId);
                    var path = languagePath + string.Format("{0}.xml", this.LanguageId);
                    if (!System.IO.File.Exists(path))
                    {
                        languagePath = (new URL()).PhysicalPath(SettingsManager.Constants.PathLanguageFile, 0);
                        path = languagePath + string.Format("{0}.xml", this.LanguageId);
                    }

                    if (System.IO.File.Exists(path))
                    {
                        var maps = XElement.Load(path).Elements("Language").ToList();
                        foreach (var e in maps)
                        {
                            try
                            {
                                var akey = e.Attribute("Key");
                                if (akey != null && !string.IsNullOrEmpty(e.Value))
                                {
                                    langs[LanguageId][akey.Value] = e.Value;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    HttpContext.Current.Application[SettingsManager.Constants.LanguageCache] = langs;
                }

                return langs[LanguageId].GetValueOrDefault(key) ?? key;
            }
        }

        public string LanguageId
        {
            get
            {
                var language = HttpContext.Current.Session[SettingsManager.Constants.SessionLanguage] as string;
                if (!string.IsNullOrEmpty(language))
                {
                    return language;
                }
                else
                {
                    var config = HttpContext.Current.Session[SettingsManager.Constants.SessionCompanyConfig] as CompanyConfigModel;
                    return config.Language;
                }
            }
        }

        public List<LANGUAGEModel> Languages
        {
            get
            {
                int companyId = 0;
                var url = HttpContext.Current.Request.RawUrl.Replace('\'', '/').ToLower();
                if (url.StartsWith(SettingsManager.AppSettings.DomainStore)) companyId = 1;
                else
                {
                    var config = HttpContext.Current.Session[SettingsManager.Constants.SessionCompanyConfig] as CompanyConfigModel;
                    if (config != null) companyId = config.ID;
                }

                var data = CacheProvider.GetCache<List<LANGUAGEModel>>(CacheProvider.Keys.Lan, companyId);
                if (data == null || data.Count == 0)
                {
                    data = new Business.CompanyBLL().GetLanguage(companyId).ToList();
                    CacheProvider.SetCache(data, CacheProvider.Keys.Lan, companyId);
                }

                return data;
            }
        }
    }
}
