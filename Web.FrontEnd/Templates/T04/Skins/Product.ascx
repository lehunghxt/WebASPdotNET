<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>







<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>
<link href="<%=HREF.BaseUrl%>Includes/Rating/Rating.css" rel="stylesheet" type="text/css"/>

<div id="content-wapper">   
<section>
    <div class="w3-row productTab">
        <div class="w3-col l4 s12">
            <div class="w3-content" style="max-width:1200px" id="product<%=dto.ID %>">
                 <img class="mySl" src="<%=dto.PathImage%>" style="width:100%">
                <%if(this.Images.Count > 0) { %>
                <%foreach (var image in this.Images)
                                   {%>
						        <img src="<%=image.PathImage %>" class="mySl" alt="<%=dto.Title %>" style="width:100%"/>		
                               <%} %>	
               
                <div class="w3-row-padding w3-section">
                    <%var col = 12 / (this.Images.Count + 1);
                        var i = 1; %>
                    <div class="w3-col s<%=col %>">
                    <img class="mySl1 w3-opacity w3-hover-opacity-off" src="<%=dto.PathImage%>" style="width:100%" onclick="currentdiv(<%= i++ %>)">
                    </div>
                     <%foreach (var image in this.Images)
                         {%>
                     <div class="w3-col s<%=col %>">
                    <img class="mySl1 w3-opacity w3-hover-opacity-off" src="<%=image.PathImage %>" style="width:100%" onclick="currentdiv(<%= i++ %>)">
                    </div>
						        <img src="<%=image.PathImage %>" class="mySl" alt="<%=dto.Title %>" style="width:100%"/>		
                               <%} %>	
                </div>

                <%} %>	
                </div>
            <script>
                var slideindex = 1;
                showdivs(slideindex);

                function plusdivs(n) {
                    showdivs(slideindex += n);
                }

                function currentdiv(n) {
                    showdivs(slideindex = n);
                }


                function showdivs(n) {
  var i;
  var x = document.getElementsByClassName("mySl");
  var dots = document.getElementsByClassName("mySl1");
  if (n > x.length) {slideindex = 1}
  if (n < 1) {slideindex = x.length}
  for (i = 0; i < x.length; i++) {
     x[i].style.display = "none";
  }
  for (i = 0; i < dots.length; i++) {
     dots[i].className = dots[i].className.replace(" w3-opacity-off", "");
  }
  x[slideindex-1].style.display = "block";
  dots[slideIndex-1].className += " w3-opacity-off";
}

</script>
        </div>
        <div class="w3-col l8 s12 plr10">
            <Div>
                <div class="w3-hide-small" id="backlink"><!-- đường dẫn đến sản phẩm -->
                    <%--<ul class="breadcrumb">
                    <li class="item_b">
                        <a href="#">Sức khỏe & Làm đẹp</a>
                    </li>
                    <li class="item_b">
                        <a href="#"> Chăm sóc răng miệng</a>
                    </li>
                    <li class="item_b">
                        <a href="#"> Chăm sóc răng miệng</a>
                    </li>
                    </ul>--%>
                </div>
                <h1 style="font-size: 22px;"><%=dto.Title %></h1>
            </Div>
            <div style="border-top: 1px solid #ddd;" class="w3-row">
                <div class="w3-col rps100">
                    <div style="border-bottom: 1px solid #ddd;padding:10px 0">
                        <p>
                            <span class="price">
                <%=Convert.ToDecimal(dto.Price) > 0 ? Convert.ToDecimal(dto.Sale) > 0 ? "<del>" + String.Format("{0:0,0}đ", dto.Price) + "</del>" : String.Format("{0:0,0}đ", dto.Price) : ""%>
                <%=Convert.ToDecimal(dto.Sale) > 0 ? String.Format("{0:0,0}đ", dto.Sale) : ""%>
                <%=Convert.ToDecimal(dto.Price) == 0 && Convert.ToDecimal(dto.Sale) == 0 ? "Liên hệ" : ""%>
            </span>
                        </p>
                        <%if (this.VoteNumber > 0)
                                { %>
                        <div>
                            <p>
                                <div id="rate<%=dto.ID%>" class="ratethis"></div>
                                <script type="text/javascript" language="javascript">
                                                     generate_stars(<%=VoteNumber%>, 'rate<%=dto.ID%>');
                                                     current('rate<%=dto.ID%>', <%=dto.Vote%>, <%=VoteNumber / 2%>);
</script>
                                <span style="border-left:1px solid #000">&nbsp&nbsp<%=dto.PayNumber%> Giao dịch</span>
                                <span style="border-left:1px solid #000">&nbsp&nbsp<span class="span_p3"><%=VoteNumber%></span> Đánh giá</span>
                            </p>
                                    
                            </div>
                        <%} %>
                    </div>

                    <table>
                     <%foreach (var attribute in ProductAttributes)
                        {%>
                    <tr>
                        <td style="width:30%; background: #efefef; padding:10px"><%=attribute.Name %></td>
                        <td style="width:70%; padding:10px"><%=string.IsNullOrEmpty(attribute.ValueName) ? attribute.Value : attribute.ValueName%></td>
                    </tr>
                    <%} %>
                  </table>

                    <%if (this.DisplayOrder)
                                { %>
                    <div><!-- nút mua hàng-->
                        <p style="padding:10px 0"><%=Language["Quantity"] %>&nbsp&nbsp&nbsp<input class="input-number" type="number" value="1" id="txtQuantity" placeholder="Nhập số lượng" min="0" max="1000"></p>
                            <button class="w3-btn w3-blue w3-border" onclick="AddProductToCart(false);fly('carts');"><i class="fa fa-shopping-cart"></i><%=Language["AddToCart"] %></button>
                            <button class="w3-btn w3-red w3-border" onclick="AddProductToCart(true);"><%=Language["BuyNow"] %></button>
                    </div>
                    <%} %>

                    <div style="border-bottom: 1px solid #ddd;padding:10px 0">
                        <%=dto.Brief%>
                    </div>
                </div>
            </div>
            </div>
        </div>
</section>

<section>
            <div class="productTab">
                <div class="panel">
                  <h3><%=Language["Description"] %></h3>
                </div>
                <div class="w3-row" style="margin:10px">
                  <div class="w3">
                    <div style="padding:5px"><%=dto.Contents%></div>
                  </div>
                </div>
            </div>
        </section>

    <section>
            <div class="productTab">
                Tag: 
                <%for (int i = 0; i < Tags.Count; i++ )
                    {%>
                    <span><%=Tags[i]%></span>, 
                <%} %>
            </div>
        </section>
</div>

<!-- /Product Tabs -->
<script type="text/javascript">
    // Send the rating information somewhere using Ajax or something like that.
    function AddProductToCart(go) {
        var productQuantity = $("#txtQuantity").val();
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/AddProductsToCarts",
            data: JSON.stringify({ productId: <%=dto.ID%>, quantity: productQuantity, properties: ''}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "" && go == true) {
                    location.href = '<%=HREF.BaseUrl %>vit-carts';
                 }
             }
        });
     }
</script>	


<script type="text/javascript" language="javascript">
    function fly(iddivGio) {
        iddivSP = "#product" + "<%=dto.ID%>";
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
                    type: "POST",
                    url: "<%=HREF.BaseUrl %>JsonPost.aspx/GetCarts",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var total = 0;
                        for (var i = 0; i < data.d.length; i++) {
                            total += data.d[i].Quantity;
                        }
                        $("#hanggio").html("(" + total + ") sp");
                    }
                });
            });
    }
</script>