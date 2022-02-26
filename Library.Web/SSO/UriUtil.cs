using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace Library.Web.SSO
{
    /// <summary>
    /// Summary description for UriUtil
    /// </summary>
    public class UriUtil
    {
        public static string RemoveParameter(string url, string key)
        {
            url = url.ToLower();
            key = key.ToLower();
            if (HttpContext.Current.Request[key] == null) return url;

            string fragmentToRemove = string.Format("{0}={1}",key , HttpContext.Current.Request[key].ToLower());

            String result = url.ToLower().Replace("&" + fragmentToRemove, string.Empty).Replace("?" + fragmentToRemove, string.Empty);
            return result;
        }

        public static string GetAbsolutePathForRelativePath(string relativePath)
        {
            HttpRequest Request = HttpContext.Current.Request;
            var root = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, string.Empty);
            var path = VirtualPathUtility.ToAbsolute(relativePath.Replace(root, string.Empty));
            string returnUrl = string.Format("{0}{1}", root, path);
            return returnUrl;
        }

        /// <summary>
        /// Author: Nguyễn Đức Duy
        /// Desc: Cập nhật chuỗi URL
        /// </summary>
        /// <param name="QueryStringKey"></param>
        /// <param name="QueryStringValue"></param>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string UpdateQueryString(string QueryStringKey, string QueryStringValue, string Url)
        {
            string NewUrl = Url;
            if (Url == "")
            {
                NewUrl = HttpContext.Current.Request.Url.PathAndQuery;
            }
            string PageUrl = NewUrl;
            if (NewUrl.IndexOf("?", 0) >= 0)
                PageUrl = NewUrl.Substring(0, NewUrl.IndexOf("?", 0));

            string NewKey = QueryStringKey + "=" + QueryStringValue;
            string NewQueryString = "";

            string OldQueryString = "";
            if (NewUrl.IndexOf("?", 0) >= 0)
                OldQueryString = NewUrl.Substring(NewUrl.IndexOf("?", 0) + 1);

            if (OldQueryString != "" && QueryStringKey != "")
            {
                string[] ArrayQuery = OldQueryString.Split('&');
                for (int i = 0; i < ArrayQuery.Length; i++)
                {
                    if (string.Compare(QueryStringKey, ArrayQuery[i].Substring(0, ArrayQuery[i].LastIndexOf("=")), true) == 0)
                    {
                        //NewQueryString += NewKey;
                    }
                    else
                    {
                        if (NewQueryString != "")
                            NewQueryString += "&";

                        NewQueryString += ArrayQuery[i];
                    }
                }


                if (QueryStringValue != "")
                {
                    if (NewQueryString != "")
                        NewQueryString += "&";

                    NewQueryString += NewKey;
                }


                if (NewQueryString != "")
                    NewUrl = PageUrl + "?" + NewQueryString;
                else
                    NewUrl = PageUrl;

            }
            else
            {
                if (QueryStringKey != "" && QueryStringValue != "")
                {
                    NewUrl = PageUrl + "?" + NewKey;
                }
            }
            return NewUrl;
        }

        /// <summary>
        /// Author: Nguyễn Đức Duy
        /// Desc: Xoá key trong chuỗi Quyery String
        /// </summary>
        /// <param name="QueryStringKey">Key cần xoá</param>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string RemoveQueryString(string queryStringKey, string Url)
        {
            string key = queryStringKey.Trim();
            var url = Url.Trim();

            var positionkey = url.IndexOf(key);
            if (positionkey >= 0)
            {
                var keys = url.Substring(positionkey).Split('&');
                foreach (var item in keys)
                {
                    if (item.Contains(key))
                    {
                        url = url.Replace(item, "").Replace("&&", "&");
                    }
                }
            }
            if (url.EndsWith("?"))
            {
                url = url.Replace("?", "");
            }
            return url;
        }

        public static string GetStringQueryString(string key)
        {
            return HttpContext.Current.Request.QueryString[key];
        }
    }
}