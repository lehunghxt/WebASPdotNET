<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ProductSimilars.ascx.cs" Inherits="Web.FrontEnd.Modules.ProductSimilars" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Register TagPrefix="VIT" Namespace="Web.Asp.Controls" Assembly="Web.Asp" %>


<link rel="stylesheet" type="text/css" href="<%=HREF.BaseUrl%>Includes/Rating/Rating.css" />






<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>

<div class="woocommerce">
    <ul class="products row">
        <asp:ListView ID="rpt" runat="server">
            <ItemTemplate>
                <li class="grid with-hover add-hover span3 product" style="opacity: 1;">
                    <div class="product-wrapper" id="bay_<%=this.Id %>_<%#Eval("Id") %>">
                        <a href="<%#HREF.LinkComponent(ComponentDetail, SettingsManager.Constants.SendProduct + "/"+Eval("Id")+"/"+Eval("Title").ToString().ConvertToUnSign())%>" class="thumb">
                            <img width="253" height="253" src="<%#Eval("ImagePath")%>" alt="<%#Eval("Title")%>"/></a>
                        <h3><%#Eval("Title")%></h3>
                        <span class="price"><%=Language["Price"] %>: <b><%#Convert.ToDecimal(Eval("Sale")) > 0 ? String.Format("{0:0,0} đ", Eval("Sale")) : (Convert.ToDecimal(Eval("Price")) > 0 ? String.Format("{0:0,0} đ", Eval("Price")) : Language["Contact"])%></b></span>
                        <div class="clear"></div>

                        <div class="product-meta">
                            <div class="product-meta-wrapper">
                                <div class="classic-rating vote" style='display:<%=VoteNumber == 0 ? "none" : "" %>'>                            
                                    <div class="ratethis"></div>
                                    <script type="text/javascript" language="javascript">
                                        generate_stars(<%=VoteNumber%>, 'rate<%#Eval("Id") %>');
                                        current('rate<%#Eval("Id") %>', <%#Eval("Vote") %>, <%=VoteNumber%>);
                                    </script>                            
                                </div>
                                <div class="description">
                                    <%# Eval("Brief")%>
                                </div>
                                <div class="buttons-list-wrapper" style='display:<%=HasOrder == false ? "none" : "block" %>'>
                                    <a href="#" rel="nofollow" class="add_to_cart_button button product_type_simple"
                                        onclick="AddToCart(<%#Eval("Id") %>);fly('bay_<%=this.Id %>_<%#Eval("Id") %>','imgGio');"><%=Language["Buy"] %></a>
                                </div>
                            </div>
                        </div>
                    </div>
                </li>
            </ItemTemplate>
        </asp:ListView>
    </ul>
</div>
 <p class="paging">
    <VIT:Pager ID="pager" runat="server" QueryStringField="p" Title="Trang"/> 
</p>

<div class="clear"></div>


<script type="text/javascript">
    // Send the rating information somewhere using Ajax or something like that.
    function sendRate(sel) {
        var rid = sel.id.split('_');
        var id = rid[0].replace("rate", '');
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>Components/Page/JsonPost.aspx/UpdateVote",
             data: JSON.stringify({ Id: id, Rate: rid[1] }),
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function(data) {
                 if (data != "") {
                     current(rid[0], data.d, <%=VoteNumber%>);
         }
             }
         });
}
</script>

 <script type="text/javascript">
     // Send the rating information somewhere using Ajax or something like that.
     function AddToCart(id) {
         $.ajax({
             type: "POST",
             url: "<%=HREF.BaseUrl %>Components/Page/JsonPost.aspx/AddToCarts",
             data: JSON.stringify({ productId: id }),
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function(data) {
                 if (data != "") {
                     

                 }
             }
         });
     }
</script>