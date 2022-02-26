<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
    #psTop,.breadcrumb{display:none}
</style>

    <link rel="stylesheet" type="text/css" href="/Templates/T07/style/sliders.css" />
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>






























</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
        <VIT:Position runat="server" ID="psContent"></VIT:Position>

    <div class="newshome">
        <div class="container">
            <VIT:Position runat="server" ID="psBottom" SkinName="Empty"></VIT:Position>
        </div>
    </div>
</asp:Content>