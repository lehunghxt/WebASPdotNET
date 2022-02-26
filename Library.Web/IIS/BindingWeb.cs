namespace Library.Web.IIS
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Win32;

    public class BindingWeb
    {
        public string WebsiteId { get; set; }
        public string SiteName { get; set; }

        public BindingWeb(string websiteId, string siteName)
        { WebsiteId = websiteId; SiteName = siteName; }

        public string Add(List<string> domains)
        {
            int IISVersion = GetIISVersion().Major;
            switch (IISVersion)
            {
                case 6:
                    if (!string.IsNullOrEmpty(WebsiteId))
                    {
                        var binding = new IIS6Binding(WebsiteId);
                        binding.AddBinding(domains);
                    }
                    return "IIS " + IISVersion;

                case 7:
                    if (!string.IsNullOrEmpty(SiteName))
                    {
                        var binding = new IIS7Binding();
                        binding.AddBinding(domains, SiteName);
                    }
                    return "IIS " + IISVersion;

                default:
                    return "IIS Version " + IISVersion + " is not supported by system.";
            }
        }

        public string Remove(List<string> domains)
        {
            int IISVersion = GetIISVersion().Major;
            switch (IISVersion)
            {
                case 6:
                    if (!string.IsNullOrEmpty(WebsiteId))
                    {
                        var binding = new IIS6Binding(WebsiteId);
                        binding.RemoveBinding(domains);
                    }
                    return "IIS Version " + IISVersion;

                case 7:
                    if (!string.IsNullOrEmpty(SiteName))
                    {
                        var binding = new IIS7Binding();
                        binding.RemoveBinding(domains, SiteName);
                    }
                    return "IIS Version " + IISVersion;

                default:
                    return "IIS Version " + IISVersion + " is not supported by system.";
            }
        }

        public List<string> GetList()
        {
            List<string> list = new List<string>();

            int IISVersion = GetIISVersion().Major;
            switch (IISVersion)
            {
                case 6:
                    if (!string.IsNullOrEmpty(WebsiteId))
                    {
                        var binding = new IIS6Binding(WebsiteId);
                        list = binding.BindingList();
                    }
                    list.Add("IIS Version " + IISVersion);
                    return list;

                case 7:
                    if (!string.IsNullOrEmpty(SiteName))
                    {
                        var binding = new IIS7Binding();
                        list = binding.GetList(SiteName);
                    }
                    list.Add("IIS Version " + IISVersion);
                    return list;

                default:
                    list.Add("IIS Version " + IISVersion + " is not supported by system.");
                    return list;
            }
        }

        /// <summary>
        /// The get iis version.
        /// </summary>
        /// <returns>
        /// The <see cref="Version"/>.
        /// </returns>
        public Version GetIISVersion()
        {
            using (var key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\InetStp", false))
            {
                if (key == null) return new Version(0, 0);
                var majorVersion = (int)key.GetValue("MajorVersion", -1);
                var minorVersion = (int)key.GetValue("MinorVersion", -1);
                if (majorVersion == -1 || minorVersion == -1) return new Version(0, 0);
                return new Version(majorVersion, minorVersion);
            }
        }
    }
}
