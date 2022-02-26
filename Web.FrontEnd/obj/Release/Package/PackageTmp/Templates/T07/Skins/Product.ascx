<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>







<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>
<link href="<%=HREF.BaseUrl%>Includes/Rating/Rating.css" rel="stylesheet" type="text/css"/>


            <div class="t_cont">

                <h1 class="h_t_cont"><%=dto.Title%></h1>

            </div><!-- End .t_cont -->

            <div class="m_cont">

            	<%if (VoteNumber > 0)
                            { %>
                    <div class="price">Đánh giá (<%=dto.VoteNumber%>) <div id="rate<%=dto.ID%>" class="ratethis"></div>
                         <script type="text/javascript" language="javascript">
                                                     generate_stars(<%=VoteNumber%>, 'rate<%=dto.ID%>');
                                                     current('rate<%=dto.ID%>', <%=dto.Vote%>, <%=VoteNumber / 2%>);
</script> </div><%} %>
                <div class="f-detail clearfix">
                    <%=dto.Contents%>
                </div><!-- End .f-detail -->

                <div class="tag">
                    Tag: 
                <%for (int i = 0; i < Tags.Count; i++ )
                    {%>
                    <span><%=Tags[i]%></span>, 
                <%} %>
                </div><!-- End .tag -->

            </div><!-- End .m_cont -->


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
    function singleProduct(){
        /* Product Images Carousel */
        $('#product-carousel').flexslider({
            animation: "slide",
            controlNav: false,
            animationLoop: false,
            directionNav: false,
            slideshow: false,
            itemWidth: 80,
            itemMargin: 0,
            start: function(slider){
			
                setActive($('#product-carousel li:first-child img'));
                slider.find('.right-arrow').click(function(){
                    slider.flexAnimate(slider.getTarget("next"));
                });
				
                slider.find('.left-arrow').click(function(){
                    slider.flexAnimate(slider.getTarget("prev"));
                });
				
                slider.find('img').click(function(){
                    var large = $(this).attr('data-large');
                    setActive($(this));
                    $('#product-slider img').fadeOut(300, changeImg(large, $('#product-slider img')));
                    $('#product-slider a.fullscreen-button').attr('href', large);
                });
				
                function changeImg(large, element){
                    var element = element;
                    var large = large;
                    setTimeout(function(){ startF()},300);
                    function startF(){
                        element.attr('src', large)
                        element.attr('data-large', large)
                        element.fadeIn(300);
                    }
					
                }
				
                function setActive(el){
                    var element = el;
                    $('#product-carousel img').removeClass('active-item');
                    element.addClass('active-item');
                }
				
            }
			
        });
			
			
		
        /* FullScreen Button */
        $('a.fullscreen-button').click(function(e){
            e.preventDefault();
            var target = $(this).attr('href');
            $('#product-carousel a.fancybox[href="'+target+'"]').trigger('click');
        });
		
		
        /* Cloud Zoom */
        $(".cloud-zoom").imagezoomsl({
            zoomrange: [3, 3]
        });
		
		
        ///* FancyBox */
        //$(".fancybox").fancybox();
		
		
    }
	
	
    /* Set Carousels */
    function installCarousels(){
		
        $('.owl-carousel').each(function(){
		
            /* Max items counting */
            var max_items = $(this).attr('data-max-items');
            var tablet_items = max_items;
            if(max_items > 1){
                tablet_items = max_items - 1;
            }
            var mobile_items = 1;
			
			
            /* Install Owl Carousel */
            $(this).owlCarousel({
				
                items:max_items,
                pagination : false,
                itemsDesktop : [1199,max_items],
                itemsDesktopSmall : [1000,max_items],
                itemsTablet: [920,tablet_items],
                itemsMobile: [560,mobile_items],
            });
		
			
            var owl = $(this).data('owlCarousel');
			
            /* Arrow next */
            $(this).parent().parent().find('.icon-left-dir').click(function(e){
                owl.prev();
            });
			
            /* Arrow previous */
            $(this).parent().parent().find('.icon-right-dir').click(function(e){
                owl.next(); 
            });
			
        });
		
    }
</script>

<script>
    $(document).ready(function(){
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
            data: JSON.stringify({ productId: <%=dto.ID%>, quantity: productQuantity}),
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