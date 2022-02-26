namespace Library.Web.IIS
{
    using System;
    using System.Collections.Generic;
    using System.DirectoryServices;

    public class IIS6Binding
    {
        private string Id { get; set; }
        private string Servername { get; set; }

        /// <summary>
        /// Initialize IIS6Binding Object
        /// </summary>
        /// <param name="WebsiteId">Id of website in IIS</param>
        public IIS6Binding(string WebsiteId)
        {
            this.Id = WebsiteId;
            this.Servername = System.Environment.MachineName;
        }

        /// <summary>
        /// Add a list of domain to website bindings.
        /// </summary>
        /// <param name="domains">List of domain name</param>
        public void AddBinding(List<string> domains)
        {           
            foreach (string domain in domains)
            {
                this.AddBinding(domain);
            }
        }

        /// <summary>
        /// Add a domain to website bindings.
        /// </summary>
        /// <param name="domain">A domain name</param>
        /// <returns>True or False</returns>
        public bool AddBinding(string domain)
        {
            List<string> list = this.BindingList();
            foreach (string binding in list)
            {
                string[] tmp = binding.Split(':');
                if (domain.ToLower() == tmp[2].ToLower()) return false;
            }
            using (DirectoryEntry dirent = new DirectoryEntry(string.Format("IIS://{0}/w3svc/{1}", this.Servername, this.Id)))
            {
                dirent.Properties["ServerBindings"].Add(":80:" + domain);            
                dirent.CommitChanges();
            }
            return true;
        }

        /// <summary>
        /// Get a list of bindings
        /// </summary>
        /// <returns>A list of bindings in string type.</returns>
        public List<string> BindingList()
        {
            List<string> list = new List<string>();
            using (DirectoryEntry dirent = new DirectoryEntry(string.Format("IIS://{0}/w3svc/{1}", this.Servername, this.Id)))
            {
                PropertyValueCollection tmp = dirent.Properties["ServerBindings"];
                foreach (var t in tmp)
                    list.Add(t.ToString());
            }
            return list;
        }

        /// <summary>
        /// Remove a list of domain from website bindings.
        /// </summary>
        /// <param name="domains">List of domain name</param>
        public void RemoveBinding(List<string> domains)
        {
            foreach (string domain in domains)
            {
                this.RemoveBinding(domain);
            }
        }

        /// <summary>
        /// Remove a domain from website bindings.
        /// </summary>
        /// <param name="domain">A domain name</param>
        /// <returns>True or False</returns>
        public bool RemoveBinding(string domain)
        {
            using (DirectoryEntry dirent = new DirectoryEntry(string.Format("IIS://{0}/w3svc/{1}", this.Servername, this.Id)))
            {
                int total = dirent.Properties["ServerBindings"].Count;
                for (int i = 2; i < total; i++)
                {
                    string bind = dirent.Properties["ServerBindings"][i].ToString();
                    string tmp = bind.Split(':')[2];
                    if (domain.ToLower() == tmp.ToLower())
                    {
                        dirent.Properties["ServerBindings"].RemoveAt(i);
                        dirent.CommitChanges();
                        return true;
                    }
                }
                return false;
            }
        }

        // Chưa hoàn thành
        private string GetWebSiteId(string WebsiteName)
        {
            if (string.IsNullOrEmpty(WebsiteName)) { throw new Exception("Parameter [websiteName] can't be null or empty"); }

            using (DirectoryEntry w3svc = new DirectoryEntry(string.Format("IIS://{0}/w3svc", this.Servername)))
            {
                w3svc.RefreshCache();

                foreach (DirectoryEntry site in w3svc.Children)
                {
                    using (site)
                    {
                        site.RefreshCache();
                        if (site.Properties["ServerComment"] != null)                        
                            if (site.Properties["ServerComment"].Value != null)
                                if (site.Properties["ServerComment"].Value.ToString().ToLower() == WebsiteName.ToLower())                                
                                    return site.Name;                                                                                    
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get Website Name from Website ID
        /// </summary>
        /// <param name="Id">Id of website in IIS</param>
        /// <returns>Website Name</returns>
        public string GetWebSiteName(string Id)
        {
            string result = string.Empty;

            using (DirectoryEntry w3svc = new DirectoryEntry(string.Format("IIS://{0}/w3svc/{1}", this.Servername)))
            {
                w3svc.RefreshCache();
                if (w3svc.Properties["ServerComment"] != null)
                    if (w3svc.Properties["ServerComment"].Value != null)
                        return w3svc.Properties["ServerComment"].Value.ToString();                
            }

            return result;
        }
    }
}
