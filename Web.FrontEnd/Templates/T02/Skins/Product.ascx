<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>
<%@ Import Namespace="Library"%>
<%@ Import Namespace="Library.Web"%>
<%@ Import Namespace="Web.Asp.Provider"%>







<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>
<link href="<%=HREF.BaseUrl%>Includes/Rating/Rating.css" rel="stylesheet" type="text/css"/>


<!-- Portfolio Item Row -->
<div class="row prd-detail">
            <div class="col-md-6">
                <div id="carousel-example-generic" class="carousel slide" data-ride="carousel">
                    <!-- Indicators -->
                    <ol class="carousel-indicators">
                        <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                        <%for (int i = 1; i <= CountImageDetails; i++)
                            {%>
                        <li data-target="#carousel-example-generic" data-slide-to="<%=i %>"></li>
                        <%} %>
                    </ol>

                    <!-- Wrapper for slides -->
                    <div class="carousel-inner">
                        <div class="item active" id="product<%=dto.ID %>">
                            <img class="img-responsive" src="<%=dto.PathImage%>" alt="<%=dto.Title%>">
                        </div>
                        <%foreach (var image in this.Images)
                                   {%>
					     <div class="item">
                            <img class="img-responsive" src="<%=image.PathImage%>" alt="<%=dto.Title%>">
                        </div>				
                               <%} %>
                    </div>

                    <!-- Controls -->
                    <a class="left carousel-control" href="#carousel-example-generic" data-slide="prev">
                        <span class="glyphicon glyphicon-chevron-left"></span>
                    </a>
                    <a class="right carousel-control" href="#carousel-example-generic" data-slide="next">
                        <span class="glyphicon glyphicon-chevron-right"></span>
                    </a>
                </div>
            </div>

            <div class="col-md-6" style="padding:0px 20px">
                <h1 class="product-name prd-title" style="display:<%=DisplayTitle ? "" : "none"%>" title="<%=dto.Title %>"><%=dto.Title%></h1>
                <hr />
                <div class="product-to-buy">
                <h4>Mã: <%=dto.Code%></h4>
                    <%if (VoteNumber > 0)
                            { %>
                    <div class="price">Đánh giá (<%=dto.VoteNumber%>) <div id="rate<%=dto.ID%>" class="ratethis"></div>
                         <script type="text/javascript" language="javascript">
                                                     generate_stars(<%=VoteNumber%>, 'rate<%=dto.ID%>');
                                                     current('rate<%=dto.ID%>', <%=dto.Vote%>, <%=VoteNumber / 2%>);
</script> </div><%} %>
                <p><%=dto.Brief%></p>
                
                </div>
                <hr />
                <table class="table table-striped table-bordered table-hover">
                     <tr>
                        <td>Số lượng mua tối thiểu</td>
                        <td><%=dto.SaleMin %></td>
                    </tr>
                    <%foreach (var attribute in ProductAttributes)
                        {%>
                    <tr>
                        <td><%=attribute.Name %></td>
                        <td><%=string.IsNullOrEmpty(attribute.ValueName) ? attribute.Value : attribute.ValueName%></td>
                    </tr>
                    <%} %>
                </table>
                <%if (DisplayOrder)
                    {
                                %>
                <hr />
                <div class="nutmuahang prd-buy uk-clearfix">
                    <div class="wrap-price" style="text-align:left">
            <%=Convert.ToDecimal(dto.Price) > 0 ? Convert.ToDecimal(dto.Sale) > 0 ? "(<span class='oldprice'>" + String.Format("{0:0,0} đ", dto.Price) + "</span>)" : "<span class='prd-price'>" + String.Format("{0:0,0} đ", dto.Price) + "</span>" : ""%>
            <%=Convert.ToDecimal(dto.Sale) > 0 ? "<span class='prd-price'>" + String.Format("{0:0,0} đ", dto.Sale) + "</span>" : ""%>
            <%=Convert.ToDecimal(dto.Price) == 0 && Convert.ToDecimal(dto.Sale) == 0 ? "<span class='prd-price'>Liên hệ</span>" : ""%>
		</div>
                 <label>Số lượng:</label> <input type="number" value="<%=dto.SaleMin %>" id="txtProductQuantity" class="form-control" min="<%=dto.SaleMin %>" style="width: 100px;display: inline;"/>
                    <div class=" prd-property uk-flex uk-flex-bottom" style="margin-top:20px">
                        
            <a class="btn-add-to-cart ajax-addtocart" onclick="AddProductToCart(true);fly('carts')" href="javascript:void(0);">MUA NGAY</a></div></div>
                <%} %>
            </div>
    <%if (this.Relatied.Count > 0)
        {%>
    <div class="col-md-12 panel-body">
                <div class="prd-infor">
                    <div class="main-title">Gợi ý khác cho bạn</div>
                <div class="row">
            <div class="main-featured-content">
                <%foreach (var r in Relatied)
                             { %>
                    <div class="col-md-2 col-sm-4 col-xs-6">
				<div class="product">
                    <div class="thumb img-shine">
						<a class="image img-scaledown" href="<%=HREF.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/" + r.Id + "/" + r.Name.ConvertToUnSign())%>" title="<%=r.Name%>">
                            <img src="<%=r.ImagePath%>" alt="<%=r.Name%>" style="max-height:100%; max-width:100%;display: inline;"/>
                        </a>
                    </div>   
                    
                    <div class="info">
                        <h2 class="title">
                            <a class="" href="<%=HREF.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/"+r.Id+"/"+r.Name.ConvertToUnSign())%>" title="<%=r.Name%>">
                                <%=r.Name%>
                            </a>
                        </h2>
                        <div class="price uk-flex uk-flex-middle uk-flex-space-between">
                            <%=Convert.ToDecimal(r.Price) > 0 
                                    ? Convert.ToDecimal(r.Sale) > 0 ? 
                                        "<span class='sale-price'>"+ String.Format("{0:0,0}", r.Sale) + "<sup>đ</sup></span><br><del>" + String.Format("{0:0,0}đ", r.Price) + "<sup>đ</sup></del>" 
                                        : "<span class='sale-price'>"+ String.Format("{0:0,0}", r.Price) +"<sup>đ</sup></span>" 
                                    : "Liên hệ"%>
                        </div>
                        <%=Convert.ToDecimal(r.Price) > 0 && Convert.ToDecimal(r.Sale) > 0 ? "<div class='product-row-percent'>"+Math.Round(Convert.ToDouble(((Convert.ToDecimal(r.Price)) - Convert.ToDecimal(r.Sale))*100/Convert.ToDecimal(r.Price)))+"%</div>" : ""%>

                         <%if (DisplayOrder)
                            {
                                %>
                        <input data-toggle="modal" data-target="#buyModal" style='width:100%' class="addCarts btn btn-primary" onclick="SelectProduct('<%=r.Id%>','<%=r.Name%>','<%=r.ImagePath%>','<%=r.Quantity%>','<%=r.Name%>','<%=r.Sale > 0 ? String.Format("{0:0,0}", r.Sale) : r.Price > 0 ? String.Format("{0:0,0}", r.Price) : "Liên hệ"%>')" value="Mua ngay"></input>
                    <%} %>
                    </div>
				</div>
			</div> 
                <%} %>
                </div>
                    </div>
                    </div>
            </div>
    
    <%}%>
            <div class="col-md-12 panel-body">
                <div class="prd-infor">
                    <div class="main-title">Thông tin sản phẩm</div>
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