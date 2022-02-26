<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/DataSimple.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.DataSimple" %>
<%@ Import Namespace="VIT.Library" %>
<%@ Register TagPrefix="VIT" Namespace="VIT.Web.Controls" Assembly="VIT.Web" %>

<asp:UpdatePanel runat="server" ID="udpDataView">
    <ContentTemplate>
        <asp:ListView ID="ListView" runat="server">
            <ItemTemplate>






                <div class="item_l1"><a href="<%#(Eval("URL") != null && Eval("URL").ToString().Length > 0) ? Eval("URL") : HREF.LinkComponent(RederectComponent, RederectView, RederectSendKey + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>" title="<%#Eval("Title") %>"><%#Eval("Title") %></a></div>
            </ItemTemplate>
        </asp:ListView>
        <VIT:Pager ID="pager" runat="server" OnPagerCommand="pager_PagerCommand"/>
    </ContentTemplate>
</asp:UpdatePanel>