<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
        <VIT:Position runat="server" ID="psContent"></VIT:Position>
</asp:Content>