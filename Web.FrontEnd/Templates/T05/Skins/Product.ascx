<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>







<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>
<link href="<%=HREF.BaseUrl%>Includes/Rating/Rating.css" rel="stylesheet" type="text/css"/>


<!-- Portfolio Item Row -->
<div class="row prd-detail">
            <div class="col-md-6">
                    <!-- Wrapper for slides -->
					<div class="nbs-flexisel-container">
					<div class="nbs-flexisel-inner">
                    <ul id="product-carousel" class="nbs-flexisel-ul">
                        <li class="nbs-flexisel-item" id="product<%=dto.ID %>">
                            <img src="<%=dto.PathImage%>" alt="<%=dto.Title%>" width="100%">
                        </li>
                        <%foreach (var image in this.Images)
                                   {%>
					     <li class="nbs-flexisel-item">
                            <img width="100%" src="<%=image.PathImage%>" alt="<%=dto.Title%>">
                        </li>				
                               <%} %>
                    </ul>
					</div>
					</div>
            </div>

            <div class="col-md-6" style="padding:0px 20px">
                <h1 class="product-name prd-title" <%=DisplayTitle ? "" : "style='display:none'"%>" title="<%=dto.Title %>"><%=dto.Title%></h1>
                <hr />
                <div class="product-to-buy">
                    <%if (DisplayCode)
                        { %>
                <h4>Mã: <%=dto.Code%></h4>
                    <%} %>
                    <%if (VoteNumber > 0)
                            { %>
                    <div class="price">Đánh giá (<%=dto.VoteNumber%>) <div id="rate<%=dto.ID%>" class="ratethis"></div>
                         <script type="text/javascript" language="javascript">
                             generate_stars(<%=VoteNumber%>, 'rate<%=dto.ID%>');
                             current('rate<%=dto.ID%>', <%=dto.Vote%>, <%=VoteNumber / 2%>);
</script> </div><%} %>
                <strong><%=dto.Brief%></strong>
                
                </div>
                <%if(ProductAttributes.Count > 0){ %>
                <hr />
                <table class="table table-striped table-bordered table-hover">
                    <%foreach (var attribute in ProductAttributes)
                        {%>
                    <tr>
                        <td><%=attribute.Name %></td>
                        <td><%=string.IsNullOrEmpty(attribute.ValueName) ? attribute.Value : attribute.ValueName%></td>
                    </tr>
                    <%} %>
                </table>
                <%} %>
                <%if (DisplayOrder)
                    { %>
                <hr />
                <div class="nutmuahang prd-buy uk-clearfix">
                    <div class="wrap-price" style="text-align:left">
            <%=Convert.ToDecimal(dto.Price) > 0 ? Convert.ToDecimal(dto.Sale) > 0 ? "(<span class='oldprice'>" + String.Format("{0:0,0} đ", dto.Price) + "</span>)" : "<span class='prd-price'>" + String.Format("{0:0,0} đ", dto.Price) + "</span>" : ""%>
            <%=Convert.ToDecimal(dto.Sale) > 0 ? "<span class='prd-price'>" + String.Format("{0:0,0} đ", dto.Sale) + "</span>" : ""%>
            <%=Convert.ToDecimal(dto.Price) == 0 && Convert.ToDecimal(dto.Sale) == 0 ? "<span class='prd-price'>Liên hệ</span>" : ""%>
		</div>
                 <label>Số lượng:</label> <input type="number" value="1" id="txtProductQuantity" class="form-control" style="width: 100px;display: inline;"/>
                    <div class=" prd-property uk-flex uk-flex-bottom" style="margin-top:20px">
                        
            <a class="btn-add-to-cart ajax-addtocart" onclick="AddProductToCart(true);fly('carts')" href="javascript:void(0);">MUA NGAY</a></div></div>
                <%} %>

                <%if (this.Relatied.Count > 0)
        {%>
                <div class="relatied">
                    <h2 title="Sản phẩm tương tự <%=dto.Title %>">Gợi ý khác cho bạn</h2>
                <div class="row">
                <%foreach (var r in Relatied)
                             { %>
                    
                    <div class="col-md-3 col-sm-4 col-xs-6">
                        <a class="image img-scaledown" href="<%=HREF.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/" + r.Id + "/" + r.Name.ConvertToUnSign())%>" title="<%=r.Name%>">
                            <img src="<%=r.ImagePath%>" alt="<%=r.Name%>" style="max-height:100%; max-width:100%;display: inline;"/>
                        </a>
                            <a class="relatied_title" href="<%=HREF.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/"+r.Id+"/"+r.Name.ConvertToUnSign())%>" title="<%=r.Name%>">
                                <%=r.Name.Length > 30 ? r.Name.Substring(0, 30) + "..." : r.Name%>
                            </a>
                        <%if (DisplayOrder)
                            { %>
                        <div class="price uk-flex uk-flex-middle uk-flex-space-between">
                            <%=Convert.ToDecimal(r.Price) > 0
                                    ? Convert.ToDecimal(r.Sale) > 0 ?
                                        "<span class='sale-price'>" + String.Format("{0:0,0}", r.Sale) + "<sup>đ</sup></span><br><del>" + String.Format("{0:0,0}đ", r.Price) + "<sup>đ</sup></del>"
                                        : "<span class='sale-price'>" + String.Format("{0:0,0}", r.Price) + "<sup>đ</sup></span>"
                                    : "Liên hệ"%>
                        </div>
                        <%=Convert.ToDecimal(r.Price) > 0 && Convert.ToDecimal(r.Sale) > 0 ? "<div class='product-row-percent'>" + Math.Round(Convert.ToDouble(((Convert.ToDecimal(r.Price)) - Convert.ToDecimal(r.Sale)) * 100 / Convert.ToDecimal(r.Price))) + "%</div>" : ""%>
                    <%} %>
                    </div>
                        
                <%} %>
                </div>
                    </div>
    
    <%}%>
            </div>
            <div class="col-md-12 panel-body">
                <div class="prd-infor">
                    <h2 class="main-title">Thông tin sản phẩm</h2>
                <div class="content"><%=dto.Contents%></div>
                    </div>
            </div>
            <div class="col-md-12">
                Tag: 
                <%for (int i = 0; i < Tags.Count; i++ )
                    {%>
                    <span><%=Tags[i]%></span>, 
                <%} %>
            </div>
</div>
        <!-- /.row -->   

<script>
                                                     /* Single Product Page */
                                                     function singleProduct() {
                                                         /* Product Images Carousel */
                                                         $('#product-carousel').flexisel({
                                                             visibleItems: 1,
                                                             itemsToScroll: 1,
                                                             autoPlay: {
                                                                 enable: true,
                                                                 interval: 5000,
                                                                 pauseOnHover: true
                                                             }
                                                         });

                                                     };
</script>
	

<script>
        $(window).load(function () {
            singleProduct();
        });
</script>

<!-- /Product Tabs -->
<script type="text/javascript">
    // Send the rating information somewhere using Ajax or something like that.
    function AddProductToCart(go) {
        var productQuantity = $("#txtProductQuantity").val();
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/AddProductsToCarts",
            data: JSON.stringify({ productId: <%=dto.ID%>, quantity: productQuantity }),
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
                        $("#hanggio").html("(" + total + ") sản phẩm");
                    }
                });
            });
    }
</script>