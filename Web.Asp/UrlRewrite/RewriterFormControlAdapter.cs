using System.Web.UI.Adapters;

namespace Web.Asp.UrlRewrite
{
    /// <summary>
    /// ControlAdapter for rewriting form actions
    /// </summary>
    public class RewriterFormControlAdapter : ControlAdapter
    {
        /// <summary>
        /// Renders the control.
        /// </summary>
        /// <param name="writer">The writer to write to</param>
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(new Web.Asp.UrlRewrite.RewriteFormHtmlTextWriter(writer));
        }
    }
}
