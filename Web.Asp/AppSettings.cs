namespace Web.Asp.Provider
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;

    /// <summary>
    /// Class that managed application settings from every sources.
    /// </summary>
    public class AppSettings
    {
        public string LinkLogin { get; private set; }

        public int AlertDay { get; private set; }
        public int FreeDay { get; private set; }
        public int MaxItem { get; private set; }
        public bool IsTestEnviroment { get; private set; }
        public string FolderUpload { get; private set; }
        public string FolderCache { get; private set; } 
        
        public string FTPServerIP { get; private set; }
        public string FTPRootPath { get; private set; }
        public string FTPUserID { get; private set; }
        public string FTPPassword { get; private set; }

        public string MailServer { get; private set; }
        public string MailAccount { get; private set; }
        public string MailPassWord { get; private set; }
        public int MailPort { get; private set; }
        public bool MailEnableSSL { get; private set; }
        
        public string GoogleApiKey { get; private set; }

        public string WebsiteId { get; private set; }
        public string Sitename { get; private set; }

        public int ApplicationId { get; private set; }
        public int MemberGroupId { get; private set; }
        
        public string URMService { get; private set; }
        public string URMUserName { get; private set; }
        public string URMPassword { get; private set; }
        public int GroupUserTry { get; private set; }

        public string DomainStore { get; private set; }
        public string DomainPublic { get; private set; }
        public string DomainService { get; private set; }
        public string IpPublic { get; private set; }
        public string Copyright { get; private set; }

        public bool EnablePreventDDOSHits { get; private set; }
        public bool EnablePreventDDOSSearchEgine { get; private set; }

        public string GHNToken { get; private set; }

        /// <summary>
        /// The collection of application settings from Web.config file.
        /// </summary>
        private readonly NameValueCollection appSettings;

        public AppSettings()
        {
            this.appSettings = ConfigurationManager.AppSettings;

            this.LinkLogin = this.Query<string>("LinkLogin");

            this.FreeDay = this.Query<int>("FreeDay");
            this.AlertDay = this.Query<int>("AlertDay");
            this.MaxItem = this.Query<int>("MaxItem");

            this.IsTestEnviroment = this.Query<bool>("IsTestEnviroment");

            this.FolderUpload = this.Query<string>("FolderUpload");
            this.FolderCache = this.Query<string>("FolderCache");

            this.FTPServerIP = this.Query<string>("FTPServerIP");
            this.FTPRootPath = this.Query<string>("FTPRootPath");
            this.FTPUserID = this.Query<string>("FTPUserID");
            this.FTPPassword = this.Query<string>("FTPPassword");

            this.MailServer = this.Query<string>("MailServer");
            this.MailAccount = this.Query<string>("MailAccount");
            this.MailPassWord = this.Query<string>("MailPassWord");
            this.MailPort = this.Query<int>("MailPort");
            this.MailEnableSSL = this.Query<bool>("MailEnableSSL");

            this.GoogleApiKey = this.Query<string>("GoogleApiKey");

            this.WebsiteId = this.Query<string>("WebsiteId");
            this.Sitename = this.Query<string>("Sitename");

            this.ApplicationId = this.Query<int>("ApplicationId");
            this.MemberGroupId = this.Query<int>("MemberGroupId");

            this.URMService = this.Query<string>("URMService");
            this.URMUserName = this.Query<string>("URMUserName");
            this.URMPassword = this.Query<string>("URMPassword");
            this.GroupUserTry = this.Query<int>("GroupUserTry");

            this.DomainStore = this.Query<string>("DomainStore");
            this.DomainPublic = this.Query<string>("DomainPublic");
            this.DomainService = this.Query<string>("DomainService");
            this.IpPublic = this.Query<string>("IpPre");
            this.Copyright = this.Query<string>("Copyright");

            this.EnablePreventDDOSHits = this.Query<bool>("EnablePreventDDOSHits");
            this.EnablePreventDDOSSearchEgine = this.Query<bool>("EnablePreventDDOSSearchEgine");

            this.GHNToken = this.Query<string>("GHNToken");
        }

        /// <summary>
        /// Gets the configuration value by key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <typeparam name="T">
        /// Type of returned value.
        /// </typeparam>
        /// <returns>
        /// Return the configuration value in given type.
        /// </returns>
        private T Query<T>(string key)
        {
            if (this.appSettings[key] != null)
            {
                var value = Convert.ChangeType(this.appSettings[key], typeof(T));
                return value is T ? (T)value : default(T);
            }

            return default(T);
        }
    }
}