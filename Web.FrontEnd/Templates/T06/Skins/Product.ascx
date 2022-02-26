<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>







<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>
<link href="<%=HREF.BaseUrl%>Includes/Rating/Rating.css" rel="stylesheet" type="text/css"/>
<section class=" container py-lg-4 py-md-3 py-sm-3 py-3">

<!-- Portfolio Item Row -->
<div id="product-single">
	<!-- Product -->
	<div class="product-single">
        <div class="row">
		<!-- Product Images Carousel -->
		<div class="col-lg-5 col-md-5 col-sm-5 product-single-image">
									
			<div id="product-slider">
				<ul class="slides">
                    <li id="product<%=dto.ID %>">
						<img class="cloud-zoom" src="<%=dto.PathImage%>" data-large="<%=dto.PathImage%>" alt="<%=dto.Title %>"/>
						<a class="fullscreen-button" href="<%=dto.PathImage%>">
							<div class="product-fullscreen">
								<i class="icons icon-resize-full-1"></i>
							</div>
						</a>
					</li>						                        
				</ul>
			</div>
			<div id="product-carousel">
				<ul class="slides">
                    <li>
						<a class="fancybox" rel="product-images" href="<%=dto.PathImage%>"></a>
						<img src="<%=dto.PathImage%>" data-large="<%=dto.PathImage%>" alt="<%=dto.Title %>"/>
					</li>
                    <%foreach (var image in this.Images)
                                   {%>
                            <li>
						        <a class="fancybox" rel="product-images" href="<%=image.PathImage%>"></a>
						        <img src="<%=image.PathImage%>" data-large="<%=image.PathImage%>" alt="<%=dto.Title %>"/>
					        </li>
                        <%} %>
				</ul>
				<div class="product-arrows">
					<div class="left-arrow">
						<i class="icons icon-left-dir"></i>
					</div>
					<div class="right-arrow">
						<i class="icons icon-right-dir"></i>
					</div>
				</div>
			</div>
		</div>
		<!-- /Product Images Carousel -->			
								
		<div class="col-lg-7 col-md-7 col-sm-7 product-single-info">
									
			<h1 title="<%=dto.Title%>"><%=dto.Title%></h1>
            
            <table>
                <tr>
                    <td><%=this.Language["Views"]%> </td>
                    <td>
                        <span class="views"> <%=dto.ViewNumber%> <i class="icons icon-eye-1"></i></span>
                    </td>
                </tr>
                <tr>
                    <td><%=this.Language["Code"]%></td>
                    <td><%=dto.Code%></td>
                </tr>
            </table>
            <%if (VoteNumber > 0)
                            { %>
                    <div class="price">Đánh giá (<%=dto.VoteNumber%>) <div id="rate<%=dto.ID%>" class="ratethis"></div>
                         <script type="text/javascript" language="javascript">
                             generate_stars(<%=VoteNumber%>, 'rate<%=dto.ID%>');
                             current('rate<%=dto.ID%>', <%=dto.Vote%>, <%=VoteNumber / 2%>);
</script> </div><hr /><%} %>
						 
                <table class="table table-striped table-bordered table-hover">
                    <%foreach (var attribute in ProductAttributes)
                        {%>
                    <tr>
                        <td><%=attribute.Name %></td>
                        <td><%=string.IsNullOrEmpty(attribute.ValueName) ? attribute.Value : attribute.ValueName%></td>
                    </tr>
                    <%} %>
                </table>
                <hr />			
			<span class="price">
                <%=Convert.ToDecimal(dto.Price) > 0 ? Convert.ToDecimal(dto.Sale) > 0 ? "<del>" + String.Format("{0:0,0}đ", dto.Price) + "</del>" : String.Format("{0:0,0}đ", dto.Price) : ""%>
                <%=Convert.ToDecimal(dto.Sale) > 0 ? String.Format("{0:0,0}đ", dto.Sale) : ""%>
                <%=Convert.ToDecimal(dto.Price) == 0 && Convert.ToDecimal(dto.Sale) == 0 ? "Liên hệ" : ""%>
            </span>
						
           
            			
			<table class="product-actions-single">		
                <%if (DisplayCorlor) { %>
				<tr>
					<td><%=this.Language["Color"]%>:</td>
                    <td>
                        <div class="color_stock">
                            <div class="options">
                                <div>
                                    <%foreach (var color in Colors)
                        {%>
                                            <div class="color-normal" title="<%=color.Name%>" style='float:left; margin: 0px 5px; border:solid 1px #000000; width:18px; height:18px; background:#<%=color.Value%>'></div>
                                         <%} %>
                                </div>
                            </div>
                            <div class="clear"></div>
                        </div>
                    </td>    
				</tr>
                <%} %>
                <tr>
                    <td colspan="2">  
                        <p><%=dto.Brief%></p>
                    </td>
                </tr>
                <%if (DisplayOrder)
                    { %>
                <tr>
                    <td><label>Số lượng:</label> <input type="number" value="1" id="txtProductQuantity" placeholder="Nhập số lượng" class="form-control"/></td>
                    <td>                  
                        <span class="add-to-cart"  style='cursor:pointer; margin-top:5px;' onclick="AddProductToCart(true);fly('carts');">
                            <span class="action-wrapper">
                                <i class="icons icon-basket-2"></i>
                                <span class="action-name"><%=this.Language["Order"]%></span>
                            </span>
                        </span>
                    </td>

                </tr>
                <%} %>
			</table> 
            
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
	
        </div>
    </div>
	<!-- /Product -->
	
			
	<!-- Product Tabs -->
	<div class="row">						
	    <div class="col-lg-12 col-md-12 col-sm-12">
								
		    <%--<div class="tabs">--%>
									
			    <div class="page-content tab-content">
										
				    <%=dto.Contents%> 

                    <div class="tag">
                        Tag: 
                        <%for (int i = 0; i < Tags.Count; i++ )
                        {%>
                            <%=Tags[i]%>,  
                        <%} %>    
                    </div>
										
			    </div>
									
		    <%--</div>--%>					
	    </div>
	</div>						
</div>
<!-- /Product Tabs -->

    </section>

<script src="<%=HREF.BaseUrl%>templates/t01/js/flexslider.min.js" type="text/javascript"></script>
<link rel="stylesheet" href="<%=HREF.BaseUrl%>templates/t01/css/flexslider.css" type="text/css" media="screen" />

<link rel="stylesheet" href="<%=HREF.BaseUrl%>templates/t01/css/owl.carousel.css">
<link rel="stylesheet" href="<%=HREF.BaseUrl%>templates/t01/css/owl.theme.css">

<!-- Owl Carousel -->
		<script src="<%=HREF.BaseUrl%>templates/t01/js/owl.carousel.min.js"></script>
		

		<!-- Cloud Zoom -->
<link rel="stylesheet" href="<%=HREF.BaseUrl%>templates/t01/css/cloud-zoom.css">
		<script src="<%=HREF.BaseUrl%>templates/t01/js/zoomsl-3.0.min.js"></script>

<script>
                                                     /* Single Product Page */
                                                     function singleProduct() {
                                                         /* Product Images Carousel */
                                                         $('#product-carousel').flexslider({
                                                             animation: "slide",
                                                             controlNav: false,
                                                             animationLoop: false,
                                                             directionNav: false,
                                                             slideshow: false,
                                                             itemWidth: 80,
                                                             itemMargin: 0,
                                                             start: function (slider) {

                                                                 setActive($('#product-carousel li:first-child img'));
                                                                 slider.find('.right-arrow').click(function () {
                                                                     slider.flexAnimate(slider.getTarget("next"));
                                                                 });

                                                                 slider.find('.left-arrow').click(function () {
                                                                     slider.flexAnimate(slider.getTarget("prev"));
                                                                 });

                                                                 slider.find('img').click(function () {
                                                                     var large = $(this).attr('data-large');
                                                                     setActive($(this));
                                                                     $('#product-slider img').fadeOut(300, changeImg(large, $('#product-slider img')));
                                                                     $('#product-slider a.fullscreen-button').attr('href', large);
                                                                 });

                                                                 function changeImg(large, element) {
                                                                     var element = element;
                                                                     var large = large;
                                                                     setTimeout(function () { startF() }, 300);
                                                                     function startF() {
                                                                         element.attr('src', large)
                                                                         element.attr('data-large', large)
                                                                         element.fadeIn(300);
                                                                     }

                                                                 }

                                                                 function setActive(el) {
                                                                     var element = el;
                                                                     $('#product-carousel img').removeClass('active-item');
                                                                     element.addClass('active-item');
                                                                 }

                                                             }

                                                         });



                                                         /* FullScreen Button */
                                                         $('a.fullscreen-button').click(function (e) {
                                                             e.preventDefault();
                                                             var target = $(this).attr('href');
                                                             $('#product-carousel a.fancybox[href="' + target + '"]').trigger('click');
                                                         });


                                                         /* Cloud Zoom */
                                                         $(".cloud-zoom").imagezoomsl({
                                                             zoomrange: [3, 3]
                                                         });


                                                         ///* FancyBox */
                                                         //$(".fancybox").fancybox();


                                                     }


                                                     /* Set Carousels */
                                                     function installCarousels() {

                                                         $('.owl-carousel').each(function () {

                                                             /* Max items counting */
                                                             var max_items = $(this).attr('data-max-items');
                                                             var tablet_items = max_items;
                                                             if (max_items > 1) {
                                                                 tablet_items = max_items - 1;
                                                             }
                                                             var mobile_items = 1;


                                                             /* Install Owl Carousel */
                                                             $(this).owlCarousel({

                                                                 items: max_items,
                                                                 pagination: false,
                                                                 itemsDesktop: [1199, max_items],
                                                                 itemsDesktopSmall: [1000, max_items],
                                                                 itemsTablet: [920, tablet_items],
                                                                 itemsMobile: [560, mobile_items],
                                                             });


                                                             var owl = $(this).data('owlCarousel');

                                                             /* Arrow next */
                                                             $(this).parent().parent().find('.icon-left-dir').click(function (e) {
                                                                 owl.prev();
                                                             });

                                                             /* Arrow previous */
                                                             $(this).parent().parent().find('.icon-right-dir').click(function (e) {
                                                                 owl.next();
                                                             });

                                                         });

                                                     }
</script>

<script>
    $(document).ready(function () {
        singleProduct(); // Cloud Zoom
        installCarousels();
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
            data: JSON.stringify({ productId: <%=dto.ID%>, quantity: productQuantity, properties: '' }),
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