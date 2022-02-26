<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/SearchTitleOutput.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.SearchTitleOutput" %>
<%@ Register TagPrefix="VIT" Namespace="VIT.Web.Controls" Assembly="VIT.Web" %>
<%@ Import Namespace="VIT.Library"%>
<%@ Import Namespace="VIT.Library.Web"%>

<div class="row">
	<ul>
    <asp:ListView ID="ListView" runat="server">
        <ItemTemplate>
                <li>
			        <a class="product" title="Product <%#Container.DataItemIndex + 1%>" href="<%#HREF.LinkComponent("Product","Product", SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>">
				        <img src="<%#Eval("ImagePath")%>" alt="Product Image <%#Container.DataItemIndex + 1%>"/>
				        <span class="order model"><%#Eval("Title")%></span>
				        <p><%#Eval("Description").ToString().DeleteHTMLTag().Length > 100 ? Eval("Description").ToString().DeleteHTMLTag().Substring(0, 100) + " ..." : Eval("Description").ToString().DeleteHTMLTag()%></p>
			        </a>
		        </li>
        </ItemTemplate>






    </asp:ListView>
</ul>
	<div class="cl">&nbsp;</div>
    <div class="paging pagination">
        <VIT:Pager ID="pager" runat="server" OnPagerCommand="pager_PagerCommand"/>
    </div>
</div>
	