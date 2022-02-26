namespace Web.Backend
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;

    /// <sURMmary>
    /// Class that managed application settings from every sources.
    /// </sURMmary>
    public class AppSettings
    {
        public string URMService { get; private set; }
        public string MQService { get; private set; }
        public int ApplicationId { get; set; }
        public int FreeDays { get; set; }

        public bool IsTestEnviroment { get; private set; }
        public string FolderUpload { get; private set; }

        public bool EnablePreventDDOSHits { get; private set; }
        public bool EnablePreventDDOSSearchEgine { get; private set; }

        public string MailServer { get; private set; }
        public string MailAccount { get; private set; }
        public string MailPassWord { get; private set; }
        public int MailPort { get; private set; }
        public bool MailEnableSSL { get; private set; }

        public string FTPServerIP { get; private set; }
        public string FTPRootPath { get; private set; }
        public string FTPUserID { get; private set; }
        public string FTPPassword { get; private set; }

        /// <summary>
        /// The collection of application settings from Web.config file.
        /// </summary>
        private readonly NameValueCollection appSettings;

        public AppSettings()
        {
            this.appSettings = ConfigurationManager.AppSettings;

            this.URMService = this.Query<string>("URMService");
            this.MQService = this.Query<string>("MQService");
            this.ApplicationId = this.Query<int>("ApplicationId");
            this.FreeDays = this.Query<int>("FreeDays");

            this.IsTestEnviroment = this.Query<bool>("IsTestEnviroment");

            this.FolderUpload = this.Query<string>("FolderUpload");

            this.EnablePreventDDOSHits = this.Query<bool>("EnablePreventDDOSHits");
            this.EnablePreventDDOSSearchEgine = this.Query<bool>("EnablePreventDDOSSearchEgine");

            this.MailServer = this.Query<string>("MailServer");
            this.MailAccount = this.Query<string>("MailAccount");
            this.MailPassWord = this.Query<string>("MailPassWord");
            this.MailPort = this.Query<int>("MailPort");
            this.MailEnableSSL = this.Query<bool>("MailEnableSSL");

            this.FTPServerIP = this.Query<string>("FTPServerIP");
            this.FTPRootPath = this.Query<string>("FTPRootPath");
            this.FTPUserID = this.Query<string>("FTPUserID");
            this.FTPPassword = this.Query<string>("FTPPassword");
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