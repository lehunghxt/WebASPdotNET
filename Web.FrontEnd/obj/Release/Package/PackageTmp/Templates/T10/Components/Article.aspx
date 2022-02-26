<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
	<script>window.addEventListener('load', function (event) { if(document.getElementsByTagName('iframe')[0].offsetWidth < document.getElementsByTagName('iframe')[0].width) { document.getElementsByTagName('iframe')[0].style.height = (document.getElementsByTagName('iframe')[0].height * document.getElementsByTagName('iframe')[0].offsetWidth / document.getElementsByTagName('iframe')[0].width) + 'px' } })</script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="row">
<div class="col-md-12"><VIT:Position runat="server" ID="psContent"></VIT:Position></div>
        <div class="col-md-9">
		<VIT:Position runat="server" ID="psLeft"></VIT:Position>
		</div>
        <div class="col-md-3 slideright">
		
		<iframe style='border:none; max-width: 100%' src='https://smartlink.adpia.vn/adpia-ads.php?width=300&height=300&cate=A&a_id=A100047119&u_id=' height='300' width='300'></iframe>
		
		<VIT:Position runat="server" ID="psRight"></VIT:Position>
		
		<iframe style='border:none; max-width: 100%;' src='https://smartlink.adpia.vn/adpia-ads.php?width=300&height=300&cate=U&a_id=A100047119&u_id=' height='300' width='300'></iframe>
		
		</div>
		
	</div>
        

</asp:Content>