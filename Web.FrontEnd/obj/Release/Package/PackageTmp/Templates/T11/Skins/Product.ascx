<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>

<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>
<link href="<%=HREF.BaseUrl%>Includes/Rating/Rating.css" rel="stylesheet" type="text/css"/>

          <div class="clearfix m_bottom_30 t_xs_align_c">
            <div
              class="photoframe type_2 shadow r_corners f_left f_sm_none d_xs_inline_b product_single_preview relative m_right_30 m_bottom_5 m_sm_bottom_20 m_xs_right_0 w_mxs_full"
            >
              <div class="row">
                <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12" id="product<%=dto.ID %>">
                  <img src="<%=dto.PathImage%>" class="pic" width="400px" height="400px" alt="<%=dto.Title %>"/>
                </div>
                <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12" id="info_product">
                  <h1 style="text-align: center" title="<%=dto.Title%>"><%=dto.Title%></h1>
                  <hr />
                  <div><%=dto.Brief%></div>
                  <hr />
                  <span class="product-prize"><%=Convert.ToDecimal(dto.Price) > 0 ? Convert.ToDecimal(dto.Sale) > 0 ? "<del>" + String.Format("{0:0,0}đ", dto.Price) + "</del>" : String.Format("{0:0,0}đ", dto.Price) : ""%>
                <%=Convert.ToDecimal(dto.Sale) > 0 ? String.Format("{0:0,0}đ", dto.Sale) : ""%>
                <%=Convert.ToDecimal(dto.Price) == 0 && Convert.ToDecimal(dto.Sale) == 0 ? "Liên hệ" : ""%></span>
                  <div class="attr">
                     
                
                    <div class="row">
                      <p>Màu sắc:</p>
                    </div>
                    <div class="row">
                         <%if (DisplayCorlor) { %>
                                    <%foreach (var color in Colors)
                        {%>
                      <button class="btn"><span  style='background:#<%=color.Value%>'><%=color.Name%></span></button>
                        <%} %>
                    </div>
                      <%} %>
                    <div class="row" style="align-items: flex-start; padding: 10px 0px;">
                      <span>Số lượng: <input type="number" id="txtProductQuantity" style="width: 70px"/></span>
                    </div>
                    <div class="row m_bottom_20">
                      <div class="col-sm-12 col-xs-12">
                      <button class="col-sm-6 col-xs-10" onclick="AddProductToCart(true);fly('carts')">Thêm vào giỏ hàng</button>
                    </div>
                    </div>
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
                </div>
              </div>
            </div>
          </div>

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

<script type="text/javascript">
    // Send the rating information somewhere using Ajax or something like that.
    function AddProductToCart(go) {
        var productQuantity = $("#txtProductQuantity").val();
        if (parseInt(productQuantity, 10) < parseInt(<%=dto.SaleMin%>, 10))
        {
            alert('Số lượng mua ít nhất là <%=dto.SaleMin%>, bạn không thể mua ' + productQuantity + ' sản phẩm');
        }
        else {
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

 <script>
    $(document).ready(function () {
      $(".pic").imagezoomsl({
        zoomrange: [3, 3],
      });

     
    });
    $(window).resize(function () {
      $(".pic").imagezoomsl({
        zoomrange: [3, 3],
      });
    });
  </script>	