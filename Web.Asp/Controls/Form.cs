using System.Web.UI;
using System.Web.UI.HtmlControls;
using Web.Asp.UrlRewrite;

[assembly: TagPrefix("Web.Asp.Controls", "VIT")]
namespace Web.Asp.Controls
{
    /// <summary>
	/// Replacement for &lt;asp:form&gt; to handle rewritten form postback.
	/// </summary>
	/// <remarks>
	/// <p>This form should be used for pages that use the url rewriter and have
	/// forms that are posted back.  If you use the normal ASP.NET <see cref="System.Web.UI.HtmlControls.HtmlForm">HtmlForm</see>,
	/// then the postback will not be able to correctly resolve the postback data to the form data.
	/// </p>
	/// <p>This form is a direct replacement for the &lt;asp:form&gt; tag.
	/// </p>
	/// <p>The following code demonstrates the usage of this control.</p>
	/// <code>
	/// &lt;%@ Page language="c#" Codebehind="MyPage.aspx.cs" AutoEventWireup="false" Inherits="MyPage" %&gt;
	/// &lt;%@ Register TagPrefix="url" Namespace="Intelligencia.UrlRewriter" Assembly="Intelligencia.UrlRewriter" %&gt;
	/// &lt;html&gt;
	/// ...
	/// &lt;body&gt;
	/// &lt;url:form id="MyForm" runat="server"&gt;
	/// ...
	/// &lt;/url:form&gt;
	/// &lt;/body&gt;
	/// &lt;/html&gt;
	/// </code>
	/// </remarks>
    [ToolboxData("<{0}:Form runat=server></{0}:Form>")]
	public class Form : HtmlForm
	{
		/// <summary>
		/// Renders children of the form control.
		/// </summary>
		/// <param name="writer">The output writer.</param>
		/// <exclude />
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			base.RenderChildren(writer);
			writer.RenderEndTag();
		}

		/// <summary>
		/// Renders attributes.
		/// </summary>
		/// <param name="writer">The output writer.</param>
		/// <exclude />
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			writer.WriteAttribute(Constants.AttrName, GetName());
			Attributes.Remove(Constants.AttrName);

			writer.WriteAttribute(Constants.AttrMethod, GetMethod());
			Attributes.Remove(Constants.AttrMethod);

			writer.WriteAttribute(Constants.AttrAction, GetAction(), true);
			Attributes.Remove(Constants.AttrAction);

			Attributes.Render(writer);

			if (ID != null)
			{
				writer.WriteAttribute(Constants.AttrID, GetID());
			}
		}

		private string GetID()
		{
			return ClientID;
		}

		private string GetName()
		{
			return Name;
		}

		private string GetMethod()
		{
			return Method;
		}

		private string GetAction()
		{
            return RewriteUrl.RawUrl;
		}
	}

    internal sealed class Constants
    {
        private Constants()
        {
        }

        public const string Messages = "Intelligencia.UrlRewriter.Messages";
        public const string RewriterNode = "rewriter";
        public const string RemoteAddressHeader = "REMOTE_ADDR";
        public const string AttributeAction = "{0}-{1}";
        public const string HeaderXPoweredBy = "X-Powered-By";

        public const string AttrID = "id";
        public const string AttrAction = "action";
        public const string AttrExists = "exists";
        public const string AttrFile = "file";
        public const string AttrAddress = "address";
        public const string AttrHeader = "header";
        public const string AttrMethod = "method";
        public const string AttrMatch = "match";
        public const string AttrValue = "value";
        public const string AttrProperty = "property";
        public const string AttrStatus = "status";
        public const string AttrCookie = "cookie";
        public const string AttrRewrite = "rewrite";
        public const string AttrRedirect = "redirect";
        public const string AttrProcessing = "processing";
        public const string AttrPermanent = "permanent";
        public const string AttrValueContinue = "continue";
        public const string AttrValueRestart = "restart";
        public const string AttrValueStop = "stop";
        public const string AttrFrom = "from";
        public const string AttrName = "name";
        public const string AttrTo = "to";
        public const string AttrType = "type";
        public const string AttrCode = "code";
        public const string AttrUrl = "url";
        public const string AttrParser = "parser";
        public const string AttrTransform = "transform";
        public const string AttrLogger = "logger";

        public const string ElementIf = "if";
        public const string ElementUnless = "unless";
        public const string ElementAnd = "and";
        public const string ElementAdd = "add";
        public const string ElementSet = "set";
        public const string ElementErrorHandler = "error-handler";
        public const string ElementForbidden = "forbidden";
        public const string ElementNotImplemented = "not-implemented";
        public const string ElementNotAllowed = "not-allowed";
        public const string ElementGone = "gone";
        public const string ElementNotFound = "not-found";
        public const string ElementRewrite = "rewrite";
        public const string ElementRedirect = "redirect";
        public const string ElementMap = "map";
        public const string ElementMapping = "mapping";
        public const string ElementRegister = "register";
        public const string ElementDefaultDocuments = "default-documents";
        public const string ElementDocument = "document";

        public const string TransformDecode = "decode";
        public const string TransformEncode = "encode";
        public const string TransformBase64 = "base64";
        public const string TransformBase64Decode = "base64decode";
        public const string TransformLower = "lower";
        public const string TransformUpper = "upper";
    }
}
