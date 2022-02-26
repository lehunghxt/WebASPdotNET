<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/SearchTitleInput.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.SearchTitleInput" %>

<div class="container">
				<div class="row">
					<div class="col-sm-12">
						<div class="search_box pull-right">






                            <asp:TextBox runat="server" ID="txtKey" ClientIDMode="Static" CssClass="txtsearch" placeholder="Nhập tên sản phẩm cần tìm"></asp:TextBox>
                            <asp:LinkButton ID="btnSearch" runat="server" Onclick="btnSearch_Click" CssClass="btnsearch">
                            <%--<img src="/Templates/T0152/images/search.png"/>--%>
                            <i class="glyphicon glyphicon-search"></i>
                        </asp:LinkButton>
						</div>
					</div>
				</div>
			</div>

<input type="hidden" id ="search" name="search" value ="false"/>
<script type="text/javascript">
    $("#txtKey").keypress(function (event) {
var keycode = (event.keyCode ? event.keyCode : event.which);
if (keycode == '13' && $("#txtKey").val()) {
    $("#search").val('true');
    document.forms['frmMain'].submit();
}
});
</script>