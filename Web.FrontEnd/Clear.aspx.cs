namespace Web.FrontEnd.Components.Errors
{
    using System;
    using System.Web;
    using Web.Asp.Provider;
    using Web.Asp.Provider.Cache;
    using Web.Asp.UI;

    public partial class Clear : VITPage
    {
        private string key
        {
            get
            {
                return this.Request[SettingsManager.Constants.SendClearData] ?? "";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.key.Length > 0)
            {
                if (this.key.ToLower().StartsWith(SettingsManager.Constants.AllDataCache.ToLower()))
                {
                    var companyId = this.key.ToLower().Replace(SettingsManager.Constants.AllDataCache.ToLower(), string.Empty);
                    CacheProvider.ClearCache(Convert.ToInt32(companyId));
                }
                else HttpContext.Current.Application[this.key] = null;
            }
        }
    }
}