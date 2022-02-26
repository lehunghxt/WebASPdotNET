<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <%var template = Master as Web.Asp.UI.VITTemplate; %>
    <div class="container">
    <article style="background:#fff; padding:20px; opacity:0.9">
    <h1 class="text-center" title="<%=template.Company.DISPLAYNAME %>"><%=template.Company.FULLNAME %></h1>
    <h2 class="text-center" ><%=template.Company.SLOGAN %></h2>
    <strong><%=template.Company.DESCRIPTION %></strong>
    <div><%=template.Company.ABOUTUS %></div>
        <VIT:Position runat="server" ID="psContent"></VIT:Position>
        </article>
    </div>
</asp:Content>