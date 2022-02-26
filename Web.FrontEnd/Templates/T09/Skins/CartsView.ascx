<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CartsView.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.CartsView" %>
 
 <div style="display:none">
    <asp:Repeater ID="rptGioHang" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <%#Eval("ProductName")%>
                </td>
                <td>
                    <img src="<%#Eval("Image") %>" alt="<%#Eval("ProductName")%>" width="20px" height="20px"/>
                </td>
                <td>
                    x<%#Eval("Quantity")%>
                </td>
            </tr>
        </ItemTemplate>






    </asp:Repeater>
</div>
<div id="cart">
		<a href="<%=HREF.LinkComponent("Product","Carts")%>"><strong id="imgGio">Giỏ hàng:</strong></a> <%= TotalProducts%>  sản phẩm
</div>

<script type="text/javascript" language="javascript">
    function fly(iddivSP, iddivGio) {
        iddivSP = "#" + iddivSP; 
        var productX = $(iddivSP).offset().left;
        var productY = $(iddivSP).offset().top;
        var basketX = 0;
        var basketY = 0;

        basketX = $("#" + iddivGio).offset().left;
        basketY = $("#" + iddivGio).offset().top;

        var gotoX = basketX - productX;
        var gotoY = basketY - productY;

        var newImageWidth = $(iddivSP).width() / 6;
        var newImageHeight = $(iddivSP).height() / 6;

        $(iddivSP + " img")
		    .clone()
		    .prependTo(iddivSP)
		    .css({ 'position': 'absolute' })
		    .animate({ opacity: 0.4 }, 100)
		    .animate({ opacity: 0.1, marginLeft: gotoX, marginTop: gotoY, width: newImageWidth, height: newImageHeight }, 1200, function () {
		        $(this).remove();
		        
		        //reload cart
		        $.ajax({
		            type: "GET",
		            url: "<%=HREF.BaseUrl %>Components/Page/AjaxCartCount.aspx",
		            success: function (msg) {
		                $("#cart").html("");
		                $("#cart").html(msg);
		                $("#cart").show("slow");
		            }
		        });

		    });
    }
</script>