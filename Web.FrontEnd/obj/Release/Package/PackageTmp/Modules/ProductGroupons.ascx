<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductGroupons.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductGroupons" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>
<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>

<%=this.ColumnCount%>
<%=this.WidthImage%>
<%=this.HeightImage%>
<%=this.WidthProduct%>
<%=this.HeightProduct%>
<%=this.HasOrder%>

<asp:ListView ID="rpt" runat="server">
    <ItemTemplate>
        <%#Eval("ID")%>
        <%#Eval("Title").ToString().ConvertToUnSign()%>
        <%#Eval("ImageThumbName")%>
        <%#Eval("ImageThumdPath")%>
        <%#Eval("Description")%>
        <%#Eval("Price")%>
        <%#Eval("Sale")%>
        <%#Eval("VoteNumber")%>
        <%#Eval("VoteRate")%>
        <%#Eval("Vote")%>
        <%#Eval("PayNumber")%> 
        <script type="text/javascript" language="javascript">
            generate_stars(10, 'rate<%#Eval("Id") %>');
            current('rate<%#Eval("Id") %>', <%#Eval("Vote") %>, <%=VoteNumber%>);
        </script> 
    </ItemTemplate>
</asp:ListView>
 <VIT:Pager ID="pager" runat="server"/> 
 <script type="text/javascript">

            // Send the rating information somewhere using Ajax or something like that.
            function sendRate(sel) {
                var rid = sel.id.split('_');
                var id = rid[0].replace("rate", '');
                $.ajax({
                    type: "POST",
                    url: "<%=HREF.BaseUrl %>/JsonPost.aspx/UpdateVote",
             data: JSON.stringify({ Id: id, Rate: rid[1] }),
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (data) {
                 if (data != "") {
                     current(rid[0], data.d.VoteRate, <%=VoteNumber/2%>);
                 }
             }

         })
            }
</script>

<script type="text/javascript">
    function SelectProduct(id)
    {
        $("#ProdicyId").val(id);
    }

    // Send the rating information somewhere using Ajax or something like that.
    function AddToCart(go) {
        var productQuantity = $("#txtQuantity").val();
        var proId = $("#ProdicyId").val();
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>Components/Page/JsonPost.aspx/AddProductsToCarts",
            data: JSON.stringify({ productId: proId, quantity: productQuantity}),
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (data) {
                 if (data != "" && go == true) {
                     location.href = '<%=HREF.BaseUrl %>/vit-product-carts';
                 }
             }
         });
    }
</script>

<script type="text/javascript" language="javascript">
    function fly(iddivGio) {
        var proId = $("#ProdicyId").val();
        iddivSP = "#product" + proId;
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
		                $("#" + iddivGio).html("");
		                $("#" + iddivGio).html(msg);
		                $("#" + iddivGio).show("slow");
		                <%--$(location).attr('href', '<%=HREF.LinkComponent("Product", "Carts")%>');--%>
		            }
		        });
		    }); 
    }
</script>