namespace Library.Web
{
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI.WebControls;
    using Library;

    public static class Ultility
    {
        #region static method
        private static TreeNode AddNode(DataTable table, string columnId, string columnText, string parentId, string columnParentName = "ParentId")
        {

            TreeNode node = new TreeNode();
            DataRow[] root = table.Select(columnId + " = " + parentId, columnText);
            node.Text = root[0][columnText].ToString();
            node.Value = root[0][0].ToString();

            DataRow[] subcats = table.Select("ParentId = " + parentId);
            if (subcats.Length == 0) return node;

            foreach (DataRow subcat in subcats)
            {
                TreeNode nodetemp = new TreeNode();
                nodetemp = AddNode(table, columnId, columnText, subcat[columnId].ToString(), columnParentName);
                    node.ChildNodes.Add(nodetemp);
            }
            return node;
        }
        #endregion

        #region Table
        public static string GenTagOption(this DataTable table,string columnIdName, string columnChange, string parentId, string columParentName="ParentId", string space="", bool group=false, bool selectParent=false, string selected="")
        {
            string list_menu;
            DataRow[] subcats = table.Select(columParentName + " = '" + parentId + "'", columnChange);
            if (subcats.Length == 0) return null;
            list_menu = "";
            foreach (DataRow subcat in subcats)
            {
                string space_next = space + "&nbsp;&nbsp;&nbsp;";
                string res = string.Empty;
                res = GenTagOption(table, columnIdName, columnChange, subcat[columnIdName].ToString(), columParentName, space_next, group, selectParent, selected);
                string select = "";
                try
                {
                    if (selectParent) select = (table.Select(columnIdName + "='" + selected + "' and " + columParentName + "='" + subcat[columnIdName] + "'").Length > 0) ? "selected=selected" : "";
                    else select = (selected == subcat[columnIdName].ToString()) ? "selected=selected" : "";
                }
                catch { } 

                if (group)
                {
                    if (res != null) 
                    {
                        list_menu += "<optgroup label='" + space + subcat[columnChange] + "'>";
                        list_menu += res;
                        list_menu += "</optgroup>";
                    }
                    else
                    {
                        list_menu += "<option value=" + subcat[columnIdName] + " " + select + " >" + space + subcat[columnChange] + "</option>";
                    }
                }
                else
                {
                    list_menu += "<option value=" + subcat[columnIdName] + " " + select + ">" + space + subcat[columnChange] + "</option>";
                    if (res != null) list_menu += res;
                }
            }
            return list_menu;
        }
        public static void FillTreeView(this DataTable table, TreeView treeView, string columnIdName, string columnText, string parentId, string columnParentName = "ParentId")
        {
            var subcats = table.Select("ParentId = " + parentId, columnText);
            foreach (var subcat in subcats)
                treeView.Nodes.Add(AddNode(table, columnIdName, columnText, subcat[columnIdName].ToString(), columnParentName));
        }
        #endregion

        #region String

        /// <summary>
        /// Hàm chuyển Mã BBCode sang HTML
        /// </summary>
        /// <param name="BBCode">Đoạn mã BBCode</param>
        /// <returns>Mã HTML</returns>
        public static string ConvertBBCodeToHtml(this string BBCode)
        {
            Regex exp;
            // Định dạng lại thẻ [b]
            exp = new Regex(@"\[b\](.+?)\[/b\]");
            BBCode = exp.Replace(BBCode, "<b>$1</b>");

            // Định dạng lại thẻ [i][/i]
            exp = new Regex(@"\[i\](.+?)\[/i\]");
            BBCode = exp.Replace(BBCode, "<em>$1</em>");
            
            // Định dạng lại thẻ: [u][/u]
            exp = new Regex(@"\[u\](.+?)\[/u\]");
            BBCode = exp.Replace(BBCode, "<u>$1</u>");
            
            // Định dạng lại thẻ : [s][/s]
            exp = new Regex(@"\[s\](.+?)\[/s\]");
            BBCode = exp.Replace(BBCode, "<strike>$1</strike>");
            
            // Định dạng lại thẻ: [url=www.website.com]my site[/url]
            // Đầu ra: <a href="www.website.com">my site</a> // Ở đây nếu không muốn cho thuộc tính title bạn có thể bỏ đi
            exp = new Regex(@"\[url\=([^\]]+)\]([^\]]+)\[/url\]");
            BBCode = exp.Replace(BBCode, "<a href=\"$1\"  target=\"_blank\"  title=\"$2\">$2</a>");
            exp = new Regex(@"\[url href\=([^\]]+)\]([^\]]+)\[/url\]");
            BBCode = exp.Replace(BBCode, "<a href=\"$1\"  target=\"_blank\"  title=\"$2\">$2</a>"); 
            exp = new Regex(@"\[url]([^\]]+)\[/url\]");
            BBCode = exp.Replace(BBCode, "<a href=\"$1\"  target=\"_blank\" title=\"$1\">$1</a>");
            
            // Định dạng lại thẻ: [img]www.website.com/img/image.jpeg[/img]
            // Đầu ra: <img src="www.website.com/img/image.jpeg" />
            exp = new Regex(@"\[img\]([^\]]+)\[/img\]");
            BBCode = exp.Replace(BBCode, "<img src=\"$1\" />");// Thêm thuộc tính alt cho img
            exp = new Regex(@"\[img\=([^\]]+)\]([^\]]+)\[/img\]");
            BBCode = exp.Replace(BBCode, "<img src=\"$1\" alt=\"$2\" />");
            
            //Định dạng lại thẻ: [color=red][/color]
            exp = new Regex(@"\[color\=([^\]]+)\]([^\]]+)\[/color\]");
            BBCode = exp.Replace(BBCode, "<font color=\"$1\">$2</font>");
            exp = new Regex(@"\[colour\=([^\]]+)\]([^\]]+)\[/colour\]");
            BBCode = exp.Replace(BBCode, "<font color=\"$1\">$2</font>");
            
            // Định dạng lại thẻ: [size=3][/size]
            exp = new Regex(@"\[size\=([^\]]+)\]([^\]]+)\[/size\]");
            BBCode = exp.Replace(BBCode, "<font size=\"+$1\">$2</font>");
            
            // Định dạng lại thẻ<br />
            BBCode = BBCode.Replace("\r\n", "<br />\r\n"); 
            
            return BBCode;
        }

        /// <summary>
        /// Hàm chuyển mã HTML sang BBCode
        /// </summary>
        /// <param name="HTML">Đoạn mã HTML</param>
        /// <returns>Mã BBCode</returns>
        public static string ConvertHtmlIntoBBCode(this string HTML)
        {
            HTML = Regex.Replace(HTML, @"<br(.*?) />", "[br]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<UL[^>]*>", "[ulist]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<\/UL>", "[/ulist]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<OL[^>]*>", "[olist]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<\/OL>", "[/olist]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<LI>", "[*]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<\/li>", "", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<B>", "[b]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<\/B>", "[/b]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<STRONG>", "[strong]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<\/STRONG>", "[/strong]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<u>", "[u]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<\/u>", "[/u]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<i>", "[i]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<\/i>", "[/i]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<em>", "[em]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<\/em>", "[/em]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<sup>", "[sup]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<\/sup>", "[/sup]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<sub>", "[sub]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<\/sub>", "[/sub]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<HR[^>]*>", "[hr]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<STRIKE>", "[strike]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<\/STRIKE>", "[/strike]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<h1>", "[h1]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<\/h1>", "[/h1]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<h2>", "[h2]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<\/h2>", "[/h2]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<h3>", "[h3]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<\/h3>", "[/h3]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<A HREF=", "[url=", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"<\/A>", "[/url]", RegexOptions.IgnoreCase);
            HTML = Regex.Replace(HTML, @"'>", "']", RegexOptions.IgnoreCase);

            //match on image tags
            var match = Regex.Matches(HTML, @"<IMG[\s\S]*?SRC=\'([\s\S]*?)\'[\s\S]*?>", RegexOptions.IgnoreCase);
            if (match.Count > 0)
                HTML = Regex.Replace(HTML, match[0].ToString(), "[img]" + match[0].Groups[1].Value + "[/img]", RegexOptions.IgnoreCase);

            return HTML;
        }

        // DeleteHTMLTag
        public static string DeleteHTMLTag(this string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            return Regex.Replace(text, "<[^>]*>", string.Empty);

            //string htmlContents = new System.IO.StreamReader(text, Encoding.UTF8, true).ReadToEnd();

            //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //doc.LoadHtml(htmlContents);
            //if (doc == null) return null;

            //string output = "";
            //foreach (var node in doc.DocumentNode.ChildNodes)
            //{
            //    output += node.InnerText;
            //}

        }

        /// <summary>
        /// Converts to HTML to plain-text.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns>The plain text representation of the HTML</returns>
        public static string ConvertHTMLToPlainText(this string html)
        {
            string[] NewWords = { "&amp;", "&quot;", "&lt;", "&gt;", "&reg;", "&copy;", "&bull;", "&trade;" };
            string[] OldWords = { "&", "\"", "<", ">", "Â®", "Â©", "â€¢", "â„¢" };
            for (int i = 0; i < OldWords.Length; i++)
            {
                html = html.Replace(OldWords[i], NewWords[i]);
            }

            return html;
        }

        /// <summary>
        /// Hàm bỏ đi tag thừa (dùng 2 lần cho ra kết quả chính xác)
        /// </summary>
        /// <param name="ListTag">Danh sách tag cần kiểm tra</param>
        /// <returns>Chuỗi HTML đúng</returns>
        public static string FixHTMLTag(this string HTML, List<string> ListTag)
        {
            List<List<string>> arr = new List<List<string>>();
            List<List<string>> arr1 = new List<List<string>>();
            List<List<string>> save = new List<List<string>>();
            for (int i = 0; i < ListTag.Count; i++)
            {
                int count = 0;
                //loai bo khoang trang trong tag truyen vao voi tag dong
                HTML = Regex.Replace(HTML, "</[ ]*" + ListTag[i] + "[ ]*>", "</" + ListTag[i] + ">");
                //cat string thanh mang theo tag truyen vao(doi voi tag mo)
                arr.Add(Regex.Split(HTML, "<[ ]*" + ListTag[i] + "[^>]*>").ToList());
                //cat string thanh mang theo tag truyen vao(doi voi tag dong)
                arr1.Add(Regex.Split(HTML, "</[ ]*" + ListTag[i] + "[ ]*>").ToList());

                if (arr[i].Count > arr1[i].Count)
                {
                    count = arr[i].Count - arr1[i].Count;

                    List<string> s = new List<string>();
                    for (int j = 0; j < count; j++)
                    {
                        if (!string.IsNullOrWhiteSpace(arr[i][arr1[i].Count + j]))
                        {
                            s.Add(arr[i][arr1[i].Count + j]);
                            arr[i][arr1[i].Count + j] = "";
                        }
                    }
                    save.Add(s);
                    arr[i].RemoveAll(o => string.IsNullOrWhiteSpace(o));

                    if (save[i] != null)
                    {
                        HTML = string.Join("<" + ListTag[i] + ">", arr[i]);
                        HTML += string.Join("", save[i]);
                    }
                    else
                    {
                        HTML = string.Join("<" + ListTag[i] + ">", arr[i]);
                    }
                }

                else if (arr[i].Count < arr1[i].Count)
                {
                    count = arr1[i].Count - arr[i].Count;

                    List<string> s = new List<string>();
                    for (int j = 0; j < count; j++)
                    {
                        if (!string.IsNullOrWhiteSpace(arr1[i][arr[i].Count + j]))
                        {
                            s.Add(arr1[i][arr[i].Count + j]);
                            arr1[i][arr[i].Count + j] = "";
                        }
                    }
                    save.Add(s);
                    arr1[i].RemoveAll(o => string.IsNullOrWhiteSpace(o));

                    if (save[i] != null)
                    {
                        HTML = string.Join("</" + ListTag[i] + ">", arr1[i]);
                        HTML += string.Join("", save[i]);
                    }
                    else
                    {
                        HTML = string.Join("</" + ListTag[i] + ">", arr1[i]);
                    }
                }

                if(save.Count==i) save.Add(new List<string>());
            }
            return HTML;
        }

        /// <summary>
        /// Hàm phân trang nội dung bài viết
        /// </summary>
        /// <param name="content">
        /// The content of article.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// Text: các trang
        /// </returns>
        public static string PageArticleLink(this string content, string text = "Go to Page ")
        {
            string output;

            var pageNo = HttpContext.Current.Request.QueryString["p"];

            //Article = Regex.Replace(Article, "<p[^>]*>[^<]*<a name=\"pagebreak\"></a></p>", "<a name=\"pagebreak\"></a>");
            if (content.IndexOf("<a name=\"pagebreak\"></a>") != -1)
            {
                try
                {
                    var pages = Regex.Split(content, "<a name=\"pagebreak\"></a>");
                    var totalPages = pages.GetUpperBound(0) + 1;
                    var counter = 1;
                    var pageNumber = 1;
                    if (pageNo != null)
                    {
                        pageNumber = System.Convert.ToInt32(pageNo);
                    }

                    string contain = pages[pageNumber - 1].Trim();

                    //// bỏ tag rác sau khi cắt nội dung
                    //// phần đầu
                    while (contain.StartsWith("</"))
                        contain = contain.Substring(contain.IndexOf(">") + 1).Trim();

                    // phần đuôi
                    while ((contain.LastIndexOf('<') + 1 > contain.Length) && contain[contain.LastIndexOf('<') + 1] != '/')
                        contain = contain.Substring(0, contain.LastIndexOf("<")).Trim();

                    // xử lý kết nối
                    var thisPage = HttpContext.Current.Request.RawUrl.ToLower();

                    output = contain.FixHTMLTag(new List<string>() { "div", "p" }).FixHTMLTag(new List<string>() { "div", "p" });

                    var outputs = Regex.Split(thisPage, "vit-");
                    string seo = text.Trim().ConvertToUnSign().ToLower();

                    if (pageNumber > 1) // bỏ param cũ
                    {
                        int pvit1 = outputs[0].LastIndexOf(seo);
                        if (pvit1 > 0) outputs[0] = outputs[0].Substring(0, pvit1);

                        outputs[1] = outputs[1].Substring(outputs[1].IndexOf("-") + 1);
                        outputs[1] = outputs[1].Substring(outputs[1].IndexOf("-") + 1);
                    }

                    output += "<p>" + text;
                    while (counter <= totalPages)
                    {
                        if (counter == pageNumber)
                        {
                            output += counter.ToString() + " ";
                        }
                        else
                        {
                            if (counter > 1)    
                                output += string.Format("<a href=\"{0}{1}-{2}-vit-p-{2}-{3}\">{2} </a>", outputs[0], seo, counter, outputs[1]);
                            else
                                output += string.Format("<a href=\"{0}vit-{1}\">{2} </a>", outputs[0], outputs[1], counter);
                        }

                        counter++;
                    }

                    output += "</p>";
                }
                catch
                {
                    output = content;
                }
            }
            else
            {
                output = content;
            }

            return output;
        }

        /// <summary>
        /// Hàm phân trang nội dung bài viết
        /// </summary>
        /// <param name="Text">Nút xem thêm</param>
        /// <returns>Trang đầu hiện và các trang sau ẩn</returns>
        public static string PageArticleCommand(this string Article, string Text = "Read More")
        {
            StringBuilder Output = new StringBuilder();
            string closeTag = string.Empty;

            string ThisPage = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];

            if (Article.IndexOf("<a name=\"pagebreak\"></a>") != -1)
            {
                string[] Pages = Regex.Split(Article, "<a name=\"pagebreak\"></a>");
                int TotalPages = Pages.GetUpperBound(0) + 1;

                for (int i = 0; i < TotalPages; i++)
                {
                    string contain = Pages[i].Trim();

                    // bỏ tag rác sau khi cắt nội dung
                    // phần đầu
                    while (contain.StartsWith("</"))
                        contain = contain.Substring(contain.IndexOf(">") + 1).Trim();
                    // phần đuôi
                    while (contain[contain.LastIndexOf('<') + 1] != '/')
                        contain = contain.Substring(0, contain.LastIndexOf("<")).Trim();

                    Output.AppendFormat("<div id='pagebreak{0}' class='pagebreak'>", i);
                    Output.Append(contain.FixHTMLTag(new List<string>() { "div", "p" }).FixHTMLTag(new List<string>() { "div", "p" }));
                    if (i + 1 < TotalPages) Output.AppendFormat("<div class='pagebreak_readmore' onclick=\"slide_onclick('pagebreak{0}'); VIT_ScrollTo('pagebreak{0}');\">{1}</div>", i + 1, Text);
                    closeTag += "</div>";
                }

                Output.Append(closeTag);

                //Output.Append("<script type='text/javascript' language='javascript'>");
                //Output.Append("function pagebreak_readmore_onclick(id) {");
                //Output.Append("if ($('#' + id).css('display') == 'none')");
                //Output.Append("$('#' + id).slideDown('slow');");
                //Output.Append("else $('#' + id).slideUp('slow');");
                //Output.Append("}");
                //Output.Append("</script>");
            }
            else Output.Append(Article);
            return Output.ToString();
        }

        public static string genFlash(this string Src, int Width, int Height)
        {
            StringBuilder flash = new StringBuilder();
            flash.Append("<object title=\"flash\" codeBase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0\" height=\"" + Height + "\" width=\"" + Width + "\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" VIEWASTEXT>");

            flash.Append("<param name=\"_cx\" value=\"14552\"/>");
            flash.Append("<param name=\"_cy\" value=\"4577\"/>");

            flash.Append("<param name=\"FlashVars\" value=\"\"/>");
            flash.Append("<param name=\"Movie\" value=\"" + Src + "\"/>");
            flash.Append("<param name=\"Src\" value=\"" + Src + "\"/>");
            flash.Append("<param name=\"WMode\" value=\"transparent\"/>");
            flash.Append("<param name=\"Play\" value=\"-1\"/>");
            flash.Append("<param name=\"Loop\" value=\"true\"/>");
            flash.Append("<param name=\"Quality\" value=\"High\"/>");
            flash.Append("<param name=\"SAlign\" value=\"\"/>");

            flash.Append("<param name=\"Menu\" value=\"-1\"/>");
            flash.Append("<param name=\"Base\" value=\"\"/>");
            flash.Append("<param name=\"AllowScriptAccess\" value=\"\"/>");
            flash.Append("<param name=\"Scale\" value=\"ShowAll\"/>");
            flash.Append("<param name=\"DeviceFont\" value=\"0\"/>");
            flash.Append("<param name=\"EmbedMovie\" value=\"0\"/>");
            flash.Append("<param name=\"BGColor\" value=\"FFFFFF\"/>");
            flash.Append("<param name=\"SWRemote\" value=\"\"/>");
            flash.Append("<param name=\"MovieData\" value=\"\"/>");

            flash.Append("<param name=\"SeamlessTabbing\" value=\"1\"/>");
            flash.Append("<param name=\"Profile\" value=\"0\"/>");
            flash.Append("<param name=\"ProfileAddress\" value=\"\"/>");
            flash.Append("<param name=\"ProfilePort\" value=\"0\"/>");
            flash.Append("<param name=\"AllowNetworking\" value=\"all\"/>");
            flash.Append("<param name=\"AllowFullScreen\" value=\"false\"/>");
            flash.Append("<embed src=\"" + Src + "\" quality=\"high\" pluginspage=\"http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash\" type=\"application/x-shockwave-flash\" wmode=\"transparent\" width=\"" + Width + "\" height=\"" + Height + "\"></embed> ");
            flash.Append("</object>");

            return flash.ToString();
        }
        public static string genImage(this string Src)
        {
            return "<img src='" + Src + "' style='border:0'></img>";
        }
        #endregion

        #region Page
        /// <summary>
        /// kiểm tra control nào đã gây ra sự kiện PostPack
        /// </summary>
        /// <param name="page"></param>
        /// <returns>Control gây ra sự kiện PostPack</returns>
        public static System.Web.UI.Control GetPostBackControl(this System.Web.UI.Page page)
        {
            System.Web.UI.Control postbackControlInstance = null;
            string postbackControlName = page.Request.Params.Get("__EVENTTARGET");
            if (postbackControlName != null && postbackControlName != string.Empty)
            {
                postbackControlInstance = page.FindControl(postbackControlName);
            }
            else
            {
                // handle the Button control postbacks
                for (int i = 0; i < page.Request.Form.Keys.Count; i++)
                {
                    postbackControlInstance = page.FindControl(page.Request.Form.Keys[i]);
                    if (postbackControlInstance is System.Web.UI.WebControls.Button)
                    {
                        return postbackControlInstance;
                    }
                }
            }
            return postbackControlInstance;
        }
        #endregion
    }
}
