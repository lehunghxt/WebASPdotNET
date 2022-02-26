<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Product.ascx.cs" Inherits="Web.FrontEnd.Modules.Product" %>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<%@ Import Namespace="Web.Asp.Provider" %>
<%@ Import Namespace="Web.Model" %>







<script src="<%=HREF.BaseUrl%>Includes/Rating/Rating.js" type="text/javascript"></script>
<link href="<%=HREF.BaseUrl%>Includes/Rating/Rating.css" rel="stylesheet" type="text/css" />
<style>
    .lnr{line-height:30px}
</style>

<section class="section-landscape-products-carousel">
    <div class="product-details-area pt-20">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-3 col-md-6">
                    <div class="product-details-img">
                        <div class="zoompro-border zoompro-span" id="product<%=dto.ID%>">
                            <img class="zoompro" src="<%=dto.PathImage%>" data-zoom-image="<%=dto.PathImage%>" alt="<%=dto.Title %>" />
                            <span>-<%=Math.Round((dto.Price - dto.Sale)*100/(dto.Price == 0 ? 1 : dto.Price))%>%</span>
                            <div class="product-video">
                                <%var video = ProductAttributes.FirstOrDefault(e => e.Name == "Video");%>
                                <%if (video != null && !string.IsNullOrEmpty(video.Value))
                                    { %>
                                <a class="video-popup" href="<%=video.Value %>">
                                    <i class="sli sli-control-play"></i>
                                    Xem video
                                </a>
                                <%} %>
                            </div>
                        </div>
                        <div id="gallery" class="mt-20 product-dec-slider">
                            <%foreach (var image in this.Images)
                                {%>
                            <a data-image="<%=image.PathImage %>" data-zoom-image="<%=image.PathImage %>">
                                <img src="<%=image.PathImage %>" alt="<%=dto.Title %>">
                            </a>
                            <%} %>
                        </div>
                    </div>
                </div>
                <div class="col-lg-5 col-md-6">
                    <div class="product-details-content ml-30">
                        <h2><%=dto.Title %></h2>
                        <div class="product-details-price">
                            <span class="price_red"><%=String.Format("{0:0,0}", dto.Sale)%>
                                <div class='symbol'>đ</div>
                            </span>
                            <span class="old"><%=String.Format("{0:0,0}", dto.Price)%><div class='symbol'>đ</div>
                            </span>
                            <div class="pro-details-rating ratethis" id="rate<%=dto.ID %>">
                                <%--<i class="lnr lnr-star yellow"></i>
                                                            <i class="lnr lnr-star yellow"></i>
                                                            <i class="lnr lnr-star yellow"></i>
                                                            <i class="lnr lnr-star yellow"></i>
                                                            <i class="lnr lnr-star"></i>--%>
                            </div>
                            <%--<div class="fb-share-button" data-href="<%=HttpContext.Current.Request.Url.AbsoluteUri %>" data-layout="button_count"></div>--%>
                            <span class="pro-details-share">

                                <a title="Chia sẽ" href="#"><i class="lnr lnr-link"s></i></a>
                            </span>
                            <span class="pro-details-wishlist">
                                <a title="Yêu thích" href="#"><i class="lnr lnr-heart"></i></a>
                            </span>

                        </div>
                        <!--
                                                    <div class="pro-details-rating-wrap">



                                                    </div>
-->

                        <div class="product-brand">
                            <%var thuonghieu = ProductAttributes.FirstOrDefault(e => e.Name == "Thương hiệu");%>
                                <%if (thuonghieu != null && !string.IsNullOrEmpty(thuonghieu.Value))
                                    { %>
                                                        <span>
                                                            <span class="product-brand-name">Thương hiệu:
                                                            </span>
                                                            <a class="product-brand-link" target="_self" href=""><%=thuonghieu.ValueName %></a>
                                                        </span>
                            <%} %>
                             <%var diem = ProductAttributes.FirstOrDefault(e => e.Name == "Điểm");%>
                                <%if (diem != null && !string.IsNullOrEmpty(diem.Value))
                                    { %>
                                                         <span class="sku">
                                                            <span class="product-sku">Điểm tích lũy:
                                                            </span>
                                                            <a class="product-sku-link" target="_self" href=""><%=diem.Value %></a>

                                                        </span>
                            <%} %>
                                                       



                                                    </div>

                        <%if (this.Colors.Count > 0)
                            { %>
                        <div class="pro-details-size-color">
                            <div class="pro-details-color-wrap">
                                <span>Màu sắc</span>
                                <div class="pro-details-color-content">
                                    <ul>
                                        <%foreach (var color in this.Colors)
                                            { %>
                                        <li style="background-color: <%=color.Value%>; border: <%=color.Value%>"></li>
                                        <%} %>
                                    </ul>
                                </div>
                            </div>

                        </div>
                        <%} %>
                        <%--<div class="pro-details-size-color">

                                                        <div class="pro-details-size">
                                                            <span>Phân loại</span>
                                                            <div class="pro-details-size-content">
                                                                <ul>
                                                                    <li><a href="#">IP 6/6s</a></li>
                                                                    <li><a href="#">IP 6 Plus/6s Plus</a></li>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                        <div class="pro-details-quality">
                            <div class="pro-details-quality-wrap">
                                <span>Số lượng</span>
                                <div class="cart-plus-minus">
                                    <input id="txtQuantity" class="cart-plus-minus-box" type="text" name="qtybutton" value="<%=dto.SaleMin %>">
                                </div>
                            </div>
                        </div>
                        <div class="cart-concern">
                            <button class="chat-btn button button-xl button_theme_bluedaraz">
                                <span class="button-text">
                                    <span class="">
                                        <i class="lnr lnr-bubble"></i>
                                    </span>
                                </span>
                            </button>
                            <a class="add-to-cart-btn  button button-xl button_theme_bluedaraz" href="#" onclick="AddProductToCart(false)">
                                <span class="button-text"><span class="">
                                    <i class="lnr lnr-cart"></i>Thêm vào giỏ hàng</span>
                                </span>
                            </a>
                            <a class="buy-now-btn  pdp-button button button-xl button_theme_orange" href="#" onclick="AddProductToCart(true)">
                                <span class="button-text">
                                    <span class="">MUA NGAY</span>
                                </span>
                            </a>
                            <form method="post" action="">
                                <input type="hidden" name="buyParams" value="{&quot;items&quot;:[{&quot;itemId&quot;:&quot;248162747&quot;,&quot;skuId&quot;:&quot;324087919&quot;,&quot;quantity&quot;:1,&quot;attributes&quot;:null}]}">
                            </form>
                        </div>

                        <%--<div class="pro-details-meta">
                                                        <span>Danh mục :</span>
                                                        <ul>
                                                            <li><a href="#">Minimal,</a></li>
                                                            <li><a href="#">Furniture,</a></li>
                                                            <li><a href="#">Fashion</a></li>
                                                        </ul>
                                                    </div>--%>
                        <div class="pro-details-meta">
                            <span>Tag :</span>
                            <ul>
                                <%foreach (var tag in this.Tags)
                                    { %>
                                <li><%= tag%></li>
                                <%} %>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-12">
                    <div class="delivery">
                        <div class="delivery-header">
                            <div class="delivery-header__title">Tuỳ chọn giao hàng</div>
                        </div>
                        <div class="delivery__header">
                            <div class="location delivery__location">
                                <div class="location__current">
                                    <div class="location__body">
                                        <i class="location__icon"><span class=""><span class="lnr lnr-map-marker"></span></span></i>
                                        <div class="location__address">
                                            <select name="den" id="giaoden" class="form-control">
                                                <option value="0">--- Giao đến ---</option>
                                                <%foreach (var province in Web.Asp.ObjectData.DataSource.ProvinceSourceCollection)
                                                    { %>
                                                <option value="<%=province.Id %>"><%=province.Name %></option>
                                                <%} %>
                                            </select>
                                        </div>
                                        <%--<div class="location__link-change"><a class="location-link location-link_size_xs automation-location-link-change">THAY
                                                                            ĐỔI</a></div>--%>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="delivery__header">
                            <div class="location delivery__location">
                                <div class="location__current">
                                    <div class="location__body">
                                        <i class="location__icon"><span class=""><span class="lnr lnr-map-marker"></span></span></i>
                                        <div class="location__address">
                                            <select name="ShippingId" id="ShippingId" class="form-control">
                                                <option value="2">Giao Hàng Tiết Kiệm</option>
                                                <option value="1">Giao Hàng Nhanh</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="delivery__content">
                            <div class="delivery__option">
                                <div class="delivery__option">
                                    <div class="delivery-option-item delivery-option-item_type_standard">
                                        <div class="delivery-option-item__body">
                                            <i class="delivery-option-item__icon">
                                                <span class="lnr lnr-map-marker"></span>
                                            </i>
                                            <div class="delivery-option-item__info">
                                                <div class="delivery-option-item__title"><span id="shipfee">0</span> ₫</div>
                                                <div class="delivery-option-item__time"></div>
                                            </div>
                                            <%--                                                                        <div class="delivery-option-item__shipping-fee" id="shipfee">31.000 ₫</div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="delivery__header">
                            <div class="location delivery__location">
                                <div class="location__current">
                                    <div class="location__body">
                                        <i class="location__icon"><span class=""><span class="lnr lnr-bookmark"></span></span></i>
                                        <div class="location__address">Thanh toán khi nhận hàng(Được kiểm tra hàng trước khi nhận)</div>

                                    </div>
                                </div>
                            </div>

                        </div>

                        <%var company = Session[SettingsManager.Constants.SessionCompanyInfo + Config.Language] as CompanyInfoModel;
                            if (company == null) company = new CompanyInfoModel(); %>
                        <div class="delivery__header">
                            <div class="location delivery__location">
                                <div class="location__current">
                                    <div class="location__body">
                                        <i class="location__icon"><span class=""><span class="lnr  lnr-phone-handset"></span></span></i>
                                        <div class="location__address">
                                            Liên hệ
                                            <br>
                                            Hotline đặt hàng: <%=company.PHONE %>
                                            <div class="delivery-option-item__time">(09-21 cả T7-CN)</div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="delivery__header">
                            <div class="location delivery__location">
                                <div class="location__current">
                                    <div class="location__body">
                                        <i class="location__icon"><span class=""><span class="lnr lnr-inbox"></span></span></i>
                                        <div class="location__address">Email: <%=company.EMAIL %> </div>

                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>


        </div>
    </div>
    <div class="description-review-area pb-95">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-8 col-md-12">
                    <div class="description-review-wrapper" style="padding-bottom:10px">
                        <div class="description-review-topbar nav">
                            <a class="active" data-toggle="tab" href="#des-details1">Mô tả</a>
                            <a data-toggle="tab" href="#des-details3">Thông tin thêm</a>
                            <a data-toggle="tab" href="#des-details2">Nhận xét</a>
                        </div>
                        <div class="tab-content description-review-bottom">
                            <div id="des-details1" class="tab-pane active">
                                <div class="product-description-wrapper content js-content " itemprop="description">
                                    <%=dto.Contents %>
                                </div>
                                <p class="show-more"><a class="js-show-more" href="#" title="Xem Thêm Nội Dung">Xem Thêm Nội Dung</a></p>
                            </div>
                            <div id="des-details3" class="tab-pane">
                                <div class="product-anotherinfo-wrapper">
                                    <ul>
                                        <%foreach (var attribute in ProductAttributes)
                                            {%>
                                        <%if (attribute.Name == "Video" || attribute.Name == "Điểm") continue;%>
                                        <%if (string.IsNullOrEmpty(attribute.Value)) continue;%>
                                        <li><span><%=attribute.Name %></span> <%=string.IsNullOrEmpty(attribute.ValueName) ? attribute.Value : attribute.ValueName%></li>
                                        <%} %>
                                    </ul>
                                </div>
                            </div>
                            <div id="des-details2" class="tab-pane">
                                <div id="review-wrapper" class="review-wrapper">
                                </div>
                                <%--<div class="review-wrapper">
                                                                <div class="single-review">
                                                                    <div class="review-img">
                                                                        <img src="./img/product-details/client-1.jpg" alt="">
                                                                    </div>
                                                                    <div class="review-content">
                                                                        <p>“In convallis nulla et magna congue convallis. Donec eu nunc vel justo maximus posuere. Sed viverra nunc erat, a efficitur nibh”</p>
                                                                        <div class="review-top-wrap">
                                                                            <div class="review-name">
                                                                                <h4>Stella McGee</h4>
                                                                            </div>
                                                                            <div class="review-rating">
                                                                                <i class="lnr lnr-star"></i>
                                                                                <i class="lnr lnr-star"></i>
                                                                                <i class="lnr lnr-star"></i>
                                                                                <i class="lnr lnr-star"></i>
                                                                                <i class="lnr lnr-star"></i>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="ratting-form-wrapper">
                                                                <span>Thêm nhận xét</span>
                                                                <p>Các trường đánh dấu là bắt buộc <span>*</span></p>
                                                                <div class="star-box-wrap">
                                                                    <div class="single-ratting-star">
                                                                        <i class="lnr lnr-star"></i>
                                                                    </div>
                                                                    <div class="single-ratting-star">
                                                                        <i class="lnr lnr-star"></i>
                                                                        <i class="lnr lnr-star"></i>
                                                                    </div>
                                                                    <div class="single-ratting-star">
                                                                        <i class="lnr lnr-star"></i>
                                                                        <i class="lnr lnr-star"></i>
                                                                        <i class="lnr lnr-star"></i>
                                                                    </div>
                                                                    <div class="single-ratting-star">
                                                                        <i class="lnr lnr-star"></i>
                                                                        <i class="lnr lnr-star"></i>
                                                                        <i class="lnr lnr-star"></i>
                                                                        <i class="lnr lnr-star"></i>
                                                                    </div>
                                                                    <div class="single-ratting-star">
                                                                        <i class="lnr lnr-star"></i>
                                                                        <i class="lnr lnr-star"></i>
                                                                        <i class="lnr lnr-star"></i>
                                                                        <i class="lnr lnr-star"></i>
                                                                        <i class="lnr lnr-star"></i>
                                                                    </div>
                                                                </div>
                                                                <div class="ratting-form">
                                                                    <form action="#">
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="rating-form-style mb-20">
                                                                                    <label>Nhận xét của bạn <span>*</span></label>
                                                                                    <textarea name="Your Review"></textarea>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-12">
                                                                                <div class="rating-form-style mb-20">
                                                                                    <label>Họ tên <span>*</span></label>
                                                                                    <input type="text">
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-12">
                                                                                <div class="rating-form-style mb-20">
                                                                                    <label>Email <span>*</span></label>
                                                                                    <input type="email">
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-12">
                                                                                <div class="form-submit">
                                                                                    <input type="submit" value="GỬI">
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </form>
                                                                </div>
                                                            </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4">
                    <div class="pro-dec-banner">
                        <a href="#">
                            <img src="./img/banner/banner-15.png" alt=""></a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>

<script>
    $(document).ready(function () {
        $("#divBinhluan").detach().appendTo("#review-wrapper");
    });
</script>
<script type="text/javascript" language="javascript">
    generate_stars(<%=VoteNumber%>, 'rate<%=dto.ID%>');
    current('rate<%=dto.ID %>', <%=dto.Vote %>, <%=VoteNumber%>);
</script>
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

<!-- /Product Tabs -->
<script type="text/javascript">
    // Send the rating information somewhere using Ajax or something like that.
    function AddProductToCart(go) {
        var productQuantity = $("#txtQuantity").val();
        if (parseInt(productQuantity, 10) < parseInt(<%=dto.SaleMin%>, 10)) {
            alert('Số lượng mua ít nhất là <%=dto.SaleMin%>, bạn không thể mua ' + productQuantity + ' sản phẩm');
        }
        else {
            $.ajax({
                type: "POST",
                url: "<%=HREF.BaseUrl %>JsonPost.aspx/AddProductsToCarts",
                data: JSON.stringify({ productId: <%=dto.ID%>, quantity: productQuantity }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data != "") {
                        if (go == true) location.href = '<%=HREF.BaseUrl %>vit-order';
                        else location.href = '<%=HREF.BaseUrl %>vit-carts';
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
                         $("#hanggio").html("(" + total + ") sp");
                     }
                 });
             });
    }
</script>

<script>
    $.ajax({
        type: "POST",
        url: "<%=HREF.BaseUrl %>JsonPost.aspx/GetGHNDistrict",
         contentType: "application/json; charset=utf-8",
         dataType: "json",
         success: function (data) {
             $('#giaoden').find('option').remove();  // remove all previous options
             $('#giaoden').append($('<option>', {
                 text: '--- Giao đến ---'
             }));
             for (var i = 0; i < data.d.length; i++) {
                 $('#giaoden').append($('<option>', {
                     value: data.d[i].ProvinceID + "-" + data.d[i].DistrictID,
                     text: data.d[i].ProvinceName + " - " + data.d[i].DistrictName
                 }));
             }
         }
    });
</script>

<script>
     $('#giaoden').on('change', function () {
         var shipping = $('#ShippingId').val();
         if (shipping == 2) {
             var text = $('#giaoden :selected').text();
             $.ajax({
                 type: "POST",
                 url: "<%=HREF.BaseUrl %>JsonPost.aspx/GetGHTKFee",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ companyId: <%=Config.ID %>, to: text }),
                success: function (data) {
                    if (data.d.delivery == true) {
                        $('#DeliveryFee').val(data.d.fee);
                        var fee = (data.d.fee + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
                        fee = fee.substring(0, fee.length - 1);
                        $('#shipfee').html(fee);
                    }
                    else {

                    }
                }
            });
        }
     });
</script>
