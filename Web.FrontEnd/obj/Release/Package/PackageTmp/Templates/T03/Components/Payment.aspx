<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
     <link rel="stylesheet" href="/Includes/chosen-1.8.7/chosen.css">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>






























</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-wapper">   
    <div class="contain">
        <VIT:Position runat="server" ID="psContent"></VIT:Position>
        <script src="/Includes/chosen-1.8.7/chosen.jquery.min.js"></script>
        </div>
        </div>
</asp:Content>