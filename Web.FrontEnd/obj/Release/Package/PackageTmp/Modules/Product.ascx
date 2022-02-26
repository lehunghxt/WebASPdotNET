<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>

<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>

<%=WidthImage%>
<%=HeightImage%>
<%=VoteNumber%>

<%=DisplayCode%>
<%=DisplayCorlor%>
<%=DisplayTitle%>

<%=dto.Title%>
<%=dto.CategoryId%>
<%=dto.Brief%>
<%=dto.Price%>
<%=dto.Sale%>
        

<script src="<%=HREF.BaseUrl%>templates/t01/js/flexslider.min.js" type="text/javascript"></script>
<link rel="stylesheet" href="<%=HREF.BaseUrl%>templates/t01/css/flexslider.css" type="text/css" media="screen" />

<link rel="stylesheet" href="<%=HREF.BaseUrl%>templates/t01/css/owl.carousel.css">
<link rel="stylesheet" href="<%=HREF.BaseUrl%>templates/t01/css/owl.theme.css">

<!-- Owl Carousel -->
		<script src="<%=HREF.BaseUrl%>templates/t01/js/owl.carousel.min.js"></script>
		

		<!-- Cloud Zoom -->
<link rel="stylesheet" href="<%=HREF.BaseUrl%>templates/t01/css/cloud-zoom.css">
		<script src="<%=HREF.BaseUrl%>templates/t01/js/zoomsl-3.0.min.js"></script>

 <script type="text/javascript">

     // Send the rating information somewhere using Ajax or something like that.
     function sendRate(sel) {
         var rid = sel.id.split('_');
         var id = rid[0].replace("rate", '');

         $.ajax({
             type: "POST",
             url: "<%=HREF.BaseUrl %>JsonPost.aspx/UpdateVote",
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
    function AddToCart(go) {
        var productQuantity = $("#txtQuantity").val();
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/AddProductsToCarts",
            data: JSON.stringify({ productId: <%=dto.ID%>, quantity: productQuantity, properties:''}),
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
		                for (var i = 0; i < data.d.length; i++)
		                {
		                    total += data.d[i].Quantity;
		                }
		                $("#hanggio").html("(" + total + ") sp");
		            }
		        });
		    }); 
    }
</script>