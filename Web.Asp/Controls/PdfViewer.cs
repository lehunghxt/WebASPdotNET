using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: TagPrefix("Web.Asp.Controls", "VIT")]
namespace Web.Asp.Controls
{
    [DefaultProperty("FilePath")]
    [ToolboxData("<{0}:PdfViewer></{0}:PdfViewer>")]
    public class PdfViewer : WebControl
    {
        private string _filepath;
        public string FilePath
        {
            get
            {
                return _filepath;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _filepath = string.Empty;
                }
                else
                {
                    int tild = -1;
                    //check ~ symbol including in pdf path then remove
                    tild = value.IndexOf('~');
                    if (tild != -1)
                    {
                        _filepath = value.Substring((tild + 2)).Trim();
                    }
                    else
                    {
                        _filepath = value;
                    }
                }
            }
        }

        public string Title { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<object type='application/pdf' data='" + Convert.ToString(FilePath) + "' ");
                sb.AppendFormat("width='{0}' height='{1}px'>", Width == 0 ? "100%" : Width + "px", Height);
                sb.AppendFormat("Download : <a href='" + Convert.ToString(FilePath) + "'>" + Title + "</a>");
                sb.AppendFormat("</object>");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(Convert.ToString(sb));
                writer.RenderEndTag();
            }
            catch
            {
                //If any problem in the PDF at that time display below information
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write("PDF Control...");
                writer.RenderEndTag();
            }
        }
    }
}
