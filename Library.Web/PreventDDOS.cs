using System;
using System.Web;

using log4net;

namespace Library.Web
{
    public class ActionValidator
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ActionValidator));

        private const int DURATION = 10; // 10 min period

        public enum ActionTypeEnum
        {
            None = 0,
            FirstVisit = 100, // The most expensive one, choose the value wisely.
            ReVisit = 1000, // Welcome to revisit as many times as user likes
            PostBack = 5000, // Not must of a problem for us
            AddNewWidget = 100,
            AddNewPage = 100,
        }

        public bool EnablePreventDDOSSearchEgine { get; set; }
        public bool EnablePreventDDOSHits { get; set; }

        public ActionValidator(bool enablePreventDDOSSearchEgine, bool enablePreventDDOSHits)
        {
            EnablePreventDDOSSearchEgine = enablePreventDDOSSearchEgine;
            EnablePreventDDOSHits = enablePreventDDOSHits;
        }

        private class HitInfo
        {
            public int Hits;
            private DateTime _ExpiresAt = DateTime.Now.AddMinutes(DURATION);
            public DateTime ExpiresAt
            {
                get { return _ExpiresAt; }
                set
                {
                    _ExpiresAt = value;
                }
            }
        }

        public bool IsValid(ActionTypeEnum actionType)
        {
            HttpContext context = HttpContext.Current;
            if (EnablePreventDDOSSearchEgine && context.Request.Browser.Crawler)
            {
                log.WarnFormat("{0} Prevent DDOS: The browser is a search engine", DateTime.Now);
                return false;
            }

            if (EnablePreventDDOSHits)
            {
                string key = actionType.ToString() + context.Request.UserHostAddress;

                HitInfo hit = (HitInfo)(context.Cache[key] ?? new HitInfo());

                if (hit.Hits > (int)actionType)
                {
                    log.WarnFormat("{0} Prevent DDOS actionType: {1}", DateTime.Now, actionType.ToString());
                    log.WarnFormat("{0} Prevent DDOS UserHostAddress: {1}", DateTime.Now, context.Request.UserHostAddress);
                    log.WarnFormat("{0} Prevent DDOS Hits: {1}", DateTime.Now, hit.Hits);
                    return false;
                }
                else hit.Hits++;

                if (hit.Hits == 1)
                    context.Cache.Add(
                        key, hit, null,
                        DateTime.Now.AddMinutes(DURATION),
                        System.Web.Caching.Cache.NoSlidingExpiration,
                        System.Web.Caching.CacheItemPriority.Normal, null);
            }

            return true;
        }
    }
}
