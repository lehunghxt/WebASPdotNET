<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<link rel="stylesheet" id="cache-custom-css" href="/templates/t0160/Styles/jquery.bxslider.css" type="text/css" media="all">






<script type="text/javascript" src="/templates/t0160/scripts/jquery.bxslider.js"></script>

<div class="group blog-big-image row">
    <div class="span9">
        <!-- post content -->
        <div class="blog-big-image-content row">
            <div class="post-footer meta span3" style="margin-top:50px;">
                <%=Convert.ToDecimal(dto.Price) > 0 && Convert.ToDecimal(dto.Sale) > 0 ? "<p>" + Language["Price"] +": <font style='text-decoration: line-through;'>" + String.Format("{0:0,0 VND}", dto.Price) + "</font></p>" : ""%>
                <%=Convert.ToDecimal(dto.Sale) > 0 ? "<p>" + (Convert.ToDecimal(dto.Price) > 0 ? "Còn: " : Language["Price"] + ": ") + "<strong>" + String.Format("{0:0,0 VND}", dto.Sale) + "</strong></p>" : ""%>
                <%=Convert.ToDecimal(dto.Price) == 0 && Convert.ToDecimal(dto.Sale) == 0 ? "<p>" + Language["Price"] +": " + Language["Contact"] +"</p>" : ""%> 
                <p>Lượt xem: <%=dto.ViewNumber%></p>
                <div style="display:none"><%=Language["Rate"] %>: <div id="rate<%=dto.ID %>" class="ratethis"></div></div>
            
                <div class="color"  style="display:none">
                    Màu sắc:<br />
                    <asp:Repeater runat="server" ID="rptColor">
                        <ItemTemplate>
                            <div class="color-item" style='background:#<%# Convert.ToInt64(Eval("Value")).ToString("X6")%>'></div>                        
                        </ItemTemplate>
                    </asp:Repeater>                
                </div>
                <!--<input type="button" class="btn_eve" onclick="AddToCart(<%=dto.ID %>);fly('bay<%=dto.ID %>','Carts');" value="<%=Language["Buy"] %>" />-->
                                                      
                <%=dto.Brief%>
            </div>

            <div class="the-content-post">
                <!-- post title -->
                <h2 class="post-title upper"><%=dto.Title%></h2>
                <div class="img-slider" id="bay<%=dto.ID %>">
                    <ul id="bxslider">                        
                        <li><img src="<%=dto.PathImage%>" title="<%=dto.Title%>" /></li>
                        <asp:Repeater runat="server" ID="rptProductImage">
                            <ItemTemplate>
                                <li><img src="<%#Eval("ImageDetailPath")%>" title="<%=dto.Title%>" /></li>                        
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <script>
                        jQuery(document).ready(function ($) {
                            $('#bxslider').bxSlider({
                                mode: 'fade',
                                captions: true
                            });
                        });
                    </script>
                </div>

                <div class="content-post">
                    <%=dto.Contents%>                      
                </div>
            </div>

        </div>
        <div class="clear"></div>
    </div>
</div>

<script type="text/javascript" language="javascript">
    //generate_stars(<%=VoteNumber%>, 'rate<%=dto.ID %>');
    //current('rate<%=dto.ID %>', <%=dto.Vote %>, <%=VoteNumber%>);
</script> 

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
             success: function (data) {
                 if (data != "") {
                     current(rid[0], data.d, <%=VoteNumber%>);
                 }
             }
         })
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
            success: function (data) {
                if (data != "") {

                }
            }

        })
    }
</script>