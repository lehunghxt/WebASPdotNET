<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CartsView.ascx.cs" Inherits="Web.FrontEnd.Modules.CartsView" %>
 
        <div class="col-full">
                <div class="row">
                    <div id="primary" class="content-area">
                        <main id="main" class="site-main">
                            <section id="suggest-section" class="section-hot-new-arrivals section-products-carousel-tabs product-carousel">
                                <div class="section-products-carousel-tabs-wrap">
                                    <%--<div class="free-ship mt-10 ">
                                        <div class="container-fluid">
                                            <div class="shipping-info">
                                                <span><i class="fa fa-info-circle"></i></span>
                                                <span>Miễn phí giao hàng cho đơn hàng có giá trị từ </span>






                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="cart-info mt-10">
                                        <div class="container-fluid">
                                            <ul class="d-flex align-items-center">
                                                <li class="active"><a href="cart.html">GIỎ HÀNG</a> <span>(<%=string.Format("{0:0,0}", this.Carts.Sum(e=>e.Quantity)) %> sản phẩm)</span></li>
                                            </ul>
                                        </div>
                                        <!-- Container End -->
                                    </div>
                                    <div class="cart-main-area ptb-10 ptb-sm-10">
                                        <div class="container-fluid">
                                            <div class="row">
                                                <div class="col-md-12 col-sm-12">
                                                    <!-- Form Start -->
                                                    <form action="checkout.html">
                                                        <!-- Table Content Start -->
                                                        <div class="row">
                                                            <!--                                                           table-responsive-->
                                                            <span class="col-xs-3 col-md-9 col-sm-12 table-content  mb-45" style=" float: left">
                                                                <table>
                                                                    <thead>
                                                                        <tr>
                                                                            <th class="product-thumbnail">Sản phẩm</th>
                                                                            <th class="product-name"></th>  
                                                                            <th class="product-price">Đơn giá</th>
                                                                            <th class="product-quantity">Số Lượng</th>
                                                                            <th class="product-subtotal">Số tiền</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <%foreach (var sanpham in this.Carts)
                                                                            { %>
                                                                        <tr id="row<%=sanpham.ProductId %>">
                                                                            <td class="product-thumbnail">
                                                                                <img src="<%=sanpham.ProductImage %>" alt="c" />
                                                                            </td>
                                                                            <td class="product-name">
                                                                                <p class="product-title"><%=sanpham.ProductName %>
                                                                                    <span class="s-remove" style="float: right;">
                                                                                        <i class="fa fa-times"></i>
                                                                                    </span>
                                                                                </p>
                                                                                <%--<p class="product-quantity">Chỉ cỏn lại 5 sản phẩm</p>--%>
                                                                                <a class="delete-product" onclick="RemoveProductFromCarts(<%=sanpham.ProductId %>)" href="#">Xóa</a>
                                                                                <%--<a class="wishlist" href="#">Để dành mua sau</a>
                                                                                <div class="sub-product-quantity">
                                                                                    <div class="flex items-center _1FzU2Y">
                                                                                        <div class="flex items-center">

                                                                                            <div class="cart-plus-minus">
                                                                                                <input class="cart-plus-minus-box" type="text" name="qtybutton" value="2">
                                                                                            </div>

                                                                                        </div>
                                                                                    </div>
                                                                                </div>--%>
                                                                            </td>
                                                                            <td class="product-price price highline-red">
                                                                                <label id="lblUnit<%=sanpham.ProductId%>"><%=string.Format("{0:0,0}", sanpham.Price)%> <span class="symbol">đ</span></label>
                                                                                <input type="hidden" id='DGia<%=sanpham.ProductId%>' value="<%=sanpham.Price%>" />
                                                                            </td>
                                                                            <td class="product-quantity">
                                                                                <div class="flex items-center _1FzU2Y">
                                                                                    <div class="flex items-center">

                                                                                        <div class="cart-plus-minus">
                                                                                            <input class="cart-plus-minus-box" name="qtybutton" type="number" id="sl<%=sanpham.ProductId%>" value="<%=sanpham.Quantity%>" 
                                onchange="TinhTongTien('<%=sanpham.ProductId%>')">
                                                                                        </div>

                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                            <td class="product-subtotal highline-red">
                                                                                <label id="lbl<%=sanpham.ProductId%>"><%=string.Format("{0:0,0}", sanpham.TotalCost)%> </label> <span class="symbol">đ</span> 
                                                                                <input type="hidden" id='tongtiencot<%=sanpham.ProductId%>'  value="<%=sanpham.TotalCost%>" />
                                                                            </td>
                                                                        </tr>
                                                                        <%} %>
                                                                    </tbody>
                                                                </table>
                                                            </span>
                                                            <span class="col-xs-3 col-md-3 col-sm-12 table-content  mb-45 total-amount">
                                                                <!--                                                               table-responsive-->
                                                                <table>
                                                                    <thead>
                                                                        <tr>
                                                                            <th>
                                                                                <span>Tạm tính:</span>
                                                                                <span class="subamount" id="tamtinhtien"><%=string.Format("{0:0,0}", this.Carts.Sum(e => e.TotalCost))  %> d<span><span class="symbol">đ</span></span></span>
                                                                            </th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td><span>Thành tiền:</span>
                                                                                <span class="t-amount">
                                                                                    <p class="highline-red" id="tongthanhtien"><%=string.Format("{0:0,0}", this.Carts.Sum(e => e.TotalCost))  %> <span class="symbol">đ</span></p>
                                                                                    <p class="vat">(Đã bao gồm VAT)</p>
                                                                                </span>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <div class="pt-10 cart-concern">
                                                                    <a class="buttons-cart checkout" href="<%=HREF.LinkComponent("Order") %>">
                                                                        <span class="button-text">
                                                                            <span class="">Tiến hành đặt hàng</span>
                                                                        </span>
                                                                    </a>
                                                                    <%--<span class="coupon">Bạn có mã giảm giá? <a href="#">Click để nhập mã</a></span>
                                                                    <div class="item-content coupon-code">
                                                                        <label for="vouncher-input">
                                                                            Nhập mã
                                                                        </label>
                                                                        <input type="text" id="vouncher-input">
                                                                        <div class="apply">
                                                                            <button class="btn btn-info">Áp dụng</button>
                                                                        </div>

                                                                    </div>--%>
                                                                </div>
                                                            </span>
                                                        </div>
                                                        <!-- Table Content Start -->

                                                        <!-- Row End -->
                                                    </form>
                                                    <!-- Form End -->
                                                </div>
                                            </div>
                                            <!-- Row End -->
                                        </div>
                                    </div>
                                </div>
                            </section>


                        </main>
                    </div>
                </div>
            </div>

        <input type="hidden" id="tongtienHiden" name="tongtienHiden" value="<%=this.Carts.Sum(e => e.TotalCost)  %>"/>
<script src="/Templates/T01/js/cart.js"></script>        
<script type="text/javascript">
    // Send the rating information somewhere using Ajax or something like that.
    function RemoveProductFromCarts(id) {
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/RemoveProductFromCarts",
            data: JSON.stringify({ productId: id}),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    $("#row" + id).remove();

                    var totalPro = 0;
                    var totalPri = 0;
                    for (var i = 0; i < data.d.length; i++) {
                        totalPro += data.d[i].Quantity;
                        totalPri += data.d[i].TotalCost;
                    }
                    $("#tongtienHiden").val(totalPri);

                    var tongtien = totalPri;
                    tongtien = (tongtien + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
                    tongtien = tongtien.substring(0, tongtien.length - 1);
                    document.getElementById("tongthanhtien").innerHTML = tongtien;
                    document.getElementById("tamtinhtien").innerHTML = tongtien;
                    
                    $("#cartcount").html(totalPro);
                    $("#cartprice").html(tongtien);
                 }
             }
        });
     }
</script>	

<script language="javascript" type="text/javascript">
    function TinhTongTien(id) {
        var arrQuatity = new Array();
        var arrPrice = new Array();

        <% var productIds = this.ProductPrices.Select(e => e.ProductId);%>
        <%foreach(var id in productIds){%>
            <% var prices = this.ProductPrices.Where(e => e.ProductId == id);%>
        arrQuatity[<%=id %>] = [<%= string.Join(",",prices.Select(e => e.Quantity))%>]
        arrPrice[<%=id %>] = [<%= string.Join(",",prices.Select(e => e.Price))%>] 
        <%}%>

        var tongtienold = $("#tongtienHiden").val();
        var tongtiencot = $("#tongtiencot" + id).val();
        var sl = $("#sl" + id).val();
        var dg = $("#DGia" + id).val();

        if(arrQuatity[id].length && arrPrice[id].length && arrQuatity[id].length == arrPrice[id].length)
        {
            for(i = arrQuatity[id].length - 1; i >= 0 ; i--)
            {
                if(arrQuatity[id][i] <= sl)
                {
                    dg = arrPrice[id][i];
                    break;
                }
            }
        }

        var t = sl * dg;

        var newd = (dg + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
        newd = newd.substring(0, newd.length - 1);
        document.getElementById("lblUnit" + id).innerHTML = "<label id='lblUnit" + id + "'>" + newd + " <input type='hidden' id='DGia" + id + "' value=" + newd.replace('.', '') + " /> </label>";

        var newt = (t + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
        newt = newt.substring(0, newt.length - 1);
        document.getElementById("lbl" + id).innerHTML = "<label id='lbl" + id + "'>" + newt + " <input type='hidden' id='tongtiencot" + id + "' value=" + newt.replace('.', '') + " /> </label>";
        
        var tongtien = (tongtienold - tongtiencot) + t;
        tongtien = (tongtien + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
        tongtien = tongtien.substring(0, tongtien.length - 1);
        document.getElementById("tongthanhtien").innerHTML = tongtien;
        document.getElementById("tamtinhtien").innerHTML = tongtien;
        $("#cartprice").html(tongtien);
        $("#tongtienHiden").val(tongtien.replace('.', ''));
        

        // Send the rating information somewhere using Ajax or something like that.
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/EditCarts",
            data: JSON.stringify({ productId: id, quanlity: sl }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    
                }
            }
        });
    }
</script>