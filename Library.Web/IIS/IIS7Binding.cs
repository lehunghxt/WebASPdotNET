using System.Collections.Generic;
using Microsoft.Web.Administration;
using log4net;

namespace Library.Web.IIS
{
    public class IIS7Binding
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(IIS7Binding));

        public void AddBinding(List<string> Hostnames, string Sitename)
        {
            foreach (string Hostname in Hostnames)
            {
                this.AddBinding(Hostname, Sitename);
            }
        }

        public void AddBinding(string Hostname, string Sitename)
        {
            ServerManager server = new ServerManager();
            Site mySite = server.Sites[Sitename];

            log.InfoFormat("Bind domain {0} to sitename {1}", Hostname, Sitename);
            if (mySite == null) log.InfoFormat("Error: sitename {0} is null", Sitename);
            else
            { 
                mySite.Bindings.Add("*:80:" + Hostname, "http");
                //mySite.ServerAutoStart = true;
                server.CommitChanges();
            }
        }

        public void AddBinding(string Hostname, string Sitename, string Port, string Protocol)
        {
            ServerManager server = new ServerManager();
            Site mySite = server.Sites[Sitename];
            log.InfoFormat("Bind domain {0} to sitename {1}", Hostname, Sitename);
            if (mySite == null) log.InfoFormat("Error: sitename {0} is null", Sitename);
            else
            {
                mySite.Bindings.Add("*:" + Port + ":" + Hostname, Protocol);
                //mySite.ServerAutoStart = true;
                server.CommitChanges();
            }
        }

        public void RemoveBinding(List<string> Hostnames, string Sitename)
        {
            foreach (string Hostname in Hostnames)
            {
                this.RemoveBinding(Hostname, Sitename);
            }
        }

        public void RemoveBinding(string Hostname, string Sitename)
        {
            ServerManager server = new ServerManager();
            Site mySite = server.Sites[Sitename];

            log.InfoFormat("Remove binding {0} from sitename {1}", Hostname, Sitename);
            if (mySite == null) log.InfoFormat("Error: sitename {0} is null", Sitename);
            else
            {
                for (int i = 0; i < mySite.Bindings.Count; i++)
                {
                    if (mySite.Bindings[i].Host == Hostname)
                    {
                        mySite.Bindings.RemoveAt(i);
                        break;
                    }
                }
                mySite.ServerAutoStart = true;
                server.CommitChanges();
            }
        }

        public List<string> GetList(string Sitename)
        {
            List<string> HostList = new List<string>();
            ServerManager server = new ServerManager();
            Site mySite = server.Sites[Sitename];

            log.InfoFormat("Get list binding of sitename {1}", Sitename);
            if (mySite == null) log.InfoFormat("Error: sitename {0} is null", Sitename);
            else
            {
                foreach (Binding binding in mySite.Bindings)
                {
                    HostList.Add(binding.Host + ":" + binding.EndPoint.Port);
                }
            }
            return HostList;
        }
    }



}
