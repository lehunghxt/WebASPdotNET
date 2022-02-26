<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>






























</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content-wapper">   
    <div class="contain detail-none">
        <div class="row">
            <div class="col-md-3">
                <VIT:Position runat="server" ID="psLeft"></VIT:Position>
            </div>
            <div class="col-md-9">
        <VIT:Position runat="server" ID="psRight"></VIT:Position>
                </div>
            </div>
     </div></div>
</asp:Content>