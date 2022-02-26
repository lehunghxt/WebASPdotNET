namespace Web.FrontEnd.Modules
{
    using System;
    using Asp.UI;
    using Business;
    using Model;
    using Asp.Provider.Cache;

    public partial class GoogleMap : VITModule
    {
        private CompanyBLL _bll;

        protected string GoogleAPIKey { get; set; }

        protected int Zoom { get; set; }
        protected string Address { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this._bll = new CompanyBLL();

            this.Address = this.GetValueParam<string>("Address");
            this.GoogleAPIKey = this.GetValueParam<string>("GoogleAPIkey");
            this.Zoom = this.GetValueParam<int>("Zoom");

            if (string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(GoogleAPIKey))
            {
                var webConfig = CacheProvider.GetCache<WEBCONFIGModel>(CacheProvider.Keys.Web, this.Config.ID);
                if (webConfig == null)
                {
                    webConfig = this._bll.GetConfig(this.Config.ID);
                    CacheProvider.SetCache(webConfig, CacheProvider.Keys.Web, this.Config.ID);
                }

                if (string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(webConfig.GoogleMapAddress))
                    this.Address = webConfig.GoogleMapAddress;
                if (string.IsNullOrEmpty(GoogleAPIKey) && !string.IsNullOrEmpty(webConfig.GoogleApiKey))
                    this.GoogleAPIKey = webConfig.GoogleApiKey;
            }
        }
    }
}