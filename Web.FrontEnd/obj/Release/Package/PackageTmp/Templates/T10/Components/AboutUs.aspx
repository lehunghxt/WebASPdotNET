<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <%var template = Master as Web.Asp.UI.VITTemplate; %>

    <h1 title="<%=template.Company.DISPLAYNAME %>"><%=template.Company.FULLNAME %></h1>
    <h2><%=template.Company.SLOGAN %></h2>
    <strong><%=template.Company.DESCRIPTION %></strong>
    <div><%=template.Company.ABOUTUS %></div>
        <VIT:Position runat="server" ID="psContent"></VIT:Position>

</asp:Content>