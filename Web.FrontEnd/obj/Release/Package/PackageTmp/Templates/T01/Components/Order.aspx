<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<%@ Import Namespace="Web.Asp.Provider"%>
<%@ Import Namespace="Web.Model"%>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="/Templates/T01/css/checkout.css">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>






























</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="contain">
        <div class="col-full">
                <div class="row">
                    <div id="primary" class="content-area">
                        <main id="main" class="site-main">
                            <section class="section-landscape-products-carousel">
                                <div class="checkout-container">
                                    <div class="checkout-panel">
                                        <div class="shipping-header">
                                            <div class="container">
                                                <div class="row row-style-1">

                                                    <div class="col-md-12">
                                                        <div id="checkout-step">
                                                            <ul>
                                                                <li class="active" ><a href="#" target="_blank">Đăng nhập</a></li>
                                                                <li class="<%=UserContext != null ? "active": "" %>"><a href="#">Địa chỉ</a></li>
                                                                <li><a href="#">Thanh toán</a></li>
                                                            </ul>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>

                                        <div class="wrap">
                                            
<div id="step-1">
    <div class="row">
        <%--<div class="container">
            <div class="payment-top">
                <p class="text">Thanh toán đơn hàng trong chỉ một bước với:</p>
                <div class="form-group last">
                    <a class="btn btn-block btn-social btn-facebook user-name-loginfb" title="Đăng nhập bằng Facebook" href="javascript: void(0)" data-url="#">
                        <i class="fab fa-facebook-f"></i>
                        <span>Đăng nhập bằng Facebook</span>
                    </a>
                    <p class="or">Hoặc</p>
                    <a class="btn btn-block btn-social btn-google user-name-login-google" title="Đăng nhập bằng Google" href="javascript: void(0)" data-url="#">
                        <i class="fab fa-google"></i>
                        <span>Đăng nhập bằng Google</span>
                    </a>
                    <p class="or">Hoặc</p>
                    <a class="btn btn-block btn-social btn-zalo user-name-login-zalo" title="Đăng nhập bằng Zalo" href="javascript: void(0)" data-url="#">
                        <img src="./img/icon/zalo-icon.svg" class="icon">
                        <span class="text">Đăng nhập bằng Zalo</span>
                    </a>
                </div>
            </div>
        </div>--%>
        <div class="col-xs-12 col-sm-8 pad-right-0">
            <VIT:Position runat="server" ID="psContent"></VIT:Position>
        </div>

        <div class="col-xs-12 col-sm-4 no-address">
            <%var Carts = Session[SettingsManager.Constants.SessionGioHang + this.Config.ID] as List<OrderProductModel>;
            if (Carts == null) Carts = new List<OrderProductModel>(); %>

            <div id="panel-cart">
                <div class="panel panel-default cart">
                    <div class="panel-body">
                        <div class="order">
                            <span class="title">Đơn Hàng</span>
                            <span class="title"> (<%=Carts.Sum(e => e.Quantity) %> sản phẩm)</span>

                            <a href="<%=HREF.LinkComponent("Carts") %>" class="btn btn-default btn-custom1">Sửa</a>
                        </div>
                        <div class="product">
                            <%foreach (var sanpham in Carts)
                                { %>
                            <div class="item">
                                <p class="title">
                                    <strong><%=sanpham.Quantity %> x</strong><a href="#" target="_blank"><%=sanpham.ProductName %></a>
                                </p>
                                <p class="price text-right">
                                    <span><%=string.Format("{0:0,0}", sanpham.Price)%> <span class="symbol">đ</span></span>
                                </p>
                            </div>
                            <%} %>
                            <div class="item">
                                <p class="">
                                    <b>Tổng đơn hàng</b>
                                    <span class="price_red"><%=string.Format("{0:0,0}", Carts.Sum(e => e.TotalCost))%><span class="symbol">đ</span> </span>
                                </p>
                            </div>

                            <div class="item">
                                <input type="checkbox" id="point-1" class="toggle" />
                                <label class="item-title" for="point-1">
                                    <i class="fab fa-confluence"></i>Sử dụng ví điểm
                                </label>
                                <div class="item-content input-group" id="InputPoint">
                                    <div>Bạn có <strong id="hasPoint">0</strong> điểm. Tương đương <strong id="pricePoint">0 đ</strong></div>
                                    <div>Bạn có muốn sử dụng điểm tích lũy không?</div>
                                    <label for="vouncher-input">
                                        Nhập mã
                                    </label>
                                    <input type="text" name="PointUse" id="PointTemp">
                                    <span class="input-group-btn">
                                        <button class="btn btn-info" type="button" onclick="AddPoint()">Áp dụng</button>
                                    </span>
                                </div>
                                <p id="UsePoint"></p>
                            </div>
                            <div class="item">
                                <input type="checkbox" id="vouncher-1" class="toggle" />
                                <label class="item-title" for="vouncher-1">
                                    <i class="far fa-credit-card"></i>Sử dụng Voucher
                                </label>
                                
                                <div id="InputVoucher" class="item-content input-group">
                                    <label for="vouncher-input">
                                        Nhập mã
                                    </label>
                                    <input type="text" id="Voucher">
                                    <span class="input-group-btn">
                                        <button class="btn btn-info" type="button" onclick="AddVoucher()">Áp dụng</button>
                                    </span>
                                </div>
                                <p id="UseVoucher"></p>
                            </div>

                            <div class="item">
                                <p class="">
                                    <b>Phí vận chuyển</b>
                                    <span class="price_red" id="shipfee2">0 <span class="symbol">đ</span></span>
                                </p>
                            </div>
                        </div>

                        <p class="total2">
                            Thành tiền:
                            <span class="price_red" id="totalfee"><%=string.Format("{0:0,0}", Carts.Sum(e => e.TotalCost))%>  <span class="symbol">đ</span> </span>
                        </p>
<!--
                        <p class="vat text-right">
                            <i>(Đã bao gồm VAT)</i>
                        </p>
-->
                    </div>
                </div>

            </div>
        </div>
    </div>
</div>
                                            
                                        </div>


                                    </div>

                                </div>
                            </section>

                        </main>
                    </div>
                </div>
            </div>
        
        </div>
    <script src="/Templates/T01/js/checkout.js"></script>
</asp:Content>