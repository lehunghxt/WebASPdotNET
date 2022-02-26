<%@ Page Language="C#" Inherits="Web.Asp.UI.VITComponent" MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="Web.Asp" Namespace="Web.Asp.Controls" TagPrefix="VIT" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="/Templates/T01/css/icons.min.css">
    <link rel="stylesheet" href="/Templates/T01/css/ionicons.min.css">
    <link rel="stylesheet" href="/Templates/T01/css/profile.css">
    <link rel="stylesheet" href="/Templates/T01/css/tab.css">
    <VIT:Position runat="server" ID="HeaderBottom"></VIT:Position>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">  <%if (UserContext == null) HREF.RedirectComponent("Login"); %>
        <div class="breadcrumb-area mt-30">
            <ul>
                <li class="active"><a href="/"><span><i class="fa fa-fw fa-home"></i> Trang chủ</span></a></li>
                <li><a href="javascript:void(0)"><span><i class="fa fa-user"></i> Thông tin tài khoản</span></a></li>
            </ul>

            <!-- Container End -->
        </div>
        <section class="section-landscape-products-carousel">
                                <div class="mt-10">

                                    <div class="row profile-container">
                                        <div class="col-md-3">
                                            <div class="avt-area mt-10 mb-10">

                                                <a class="user-img" href="javascript:void(0)">
                                                    <img src="/Templates/T01/img/icon/user_circle.png" height="34px">
                                                    <span class="mini-avt" style="color: black;">
                                                        <span class="mininame"><%=UserContext.FullName %></span>
                                                        <span class="subinfo">Chỉnh sửa tài khoản</span>
                                                    </span>
                                                </a>
                                            </div>
                                            <div class="collapsible-menu">

                                                <label for="menu" class="menu-check">
                                                    <input type="checkbox" id="menu">
                                                </label>
                                                <div id="cssmenu" style="max-height: 100%;">
                                                    <ul>
                                                        <li class="parent active">
                                                            <a href="javascript:void(0)">Quản lý tài khoản</a></li>
                                                        <li class="subactive" data-rel="user-profile"><a href="javascript:void(0)"><span><i class="fa fa-user"></i> Thông tin tài khoản</span></a>
                                                        </li>
                                                        <li data-rel="user-ticket"><a href="javascript:void(0)"><span><i class="fas fa-ticket-alt"></i> Phiếu quà tặng</span></a></li>
                                                        <li data-rel="user-coupon"><a href="javascript:void(0)"><span><i class="fas fa-wallet"></i> Ví điểm BMG</span></a></li>
                                                        <li data-rel="user-notification"><a href="javascript:void(0)"><span><i class="fa fa-bell"></i> Thông báo của tôi</span></a></li>
                                                        <li class="parent active"><a href="javascript:void(0)"><span>Quản lý giao dịch</span></a></li>
                                                        <li data-rel="checkout-manager"><a href="javascript:void(0)"><span><i class="fa fa-fw fa-cog"></i> Quản lý đơn hàng</span></a></li>
                                                        <li data-rel="user-favorite"><a href="javascript:void(0)"><span><i class="fa fa-heart"></i> Sản phẩm yêu thích</span></a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-9 main-content">
                                            
                                            <section id="user-favorite" style="display:none">
                                                <h4>Sản phẩm yêu thích</h4>
                                                <div class="mb-20"></div>
                                                <div class="item mb-10">
                                                    <div class="row">
                                                        <div class="col-md-2 img-cont">
                                                            <a href="javascript:void(0)">
                                                                <img src="img/icon/user_circle.png" height="128px">
                                                            </a>
                                                        </div>
                                                        <div class="col-md-8">

                                                            <p>Ốp lưng in hình cô gái</p>

                                                            <div class="container-star starCtn mb-10" style="height:16.625px">
                                                                <img class="star" src="img/icon/star-64-64.png" style="width:16.625px;height:16.625px">
                                                                <img class="star" src="img/icon/star-64-64.png" style="width:16.625px;height:16.625px">
                                                                <img class="star" src="img/icon/star-64-64.png" style="width:16.625px;height:16.625px">
                                                                <img class="star" src="img/icon/star-64-64.png" style="width:16.625px;height:16.625px">
                                                                <img class="star" src="img/icon/star-64-64.png" style="width:16.625px;height:16.625px">
                                                            </div>

                                                            <p class="small-grey">Ốp in hình sắc nét, giao hàng nhanh lắm ạ</p>

                                                        </div>
                                                        <div class="col-md-2 right-align">
                                                            <p class="highline-red">45.000đ</p>
                                                            <p class="small-grey">90.000đ | -50%</p>
                                                            <div class="close">
                                                                <i class="fa fa-times"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="item mb-10">
                                                    <div class="row">
                                                        <div class="col-md-2 img-cont">
                                                            <a href="javascript:void(0)">
                                                                <img src="img/icon/user_circle.png" height="128px">
                                                            </a>
                                                        </div>
                                                        <div class="col-md-8">

                                                            <p>Ốp lưng in hình cô gái</p>

                                                            <div class="container-star starCtn mb-10" style="height:16.625px">
                                                                <img class="star" src="img/icon/star-64-64.png" style="width:16.625px;height:16.625px">
                                                                <img class="star" src="img/icon/star-64-64.png" style="width:16.625px;height:16.625px">
                                                                <img class="star" src="img/icon/star-64-64.png" style="width:16.625px;height:16.625px">
                                                                <img class="star" src="img/icon/star-64-64.png" style="width:16.625px;height:16.625px">
                                                                <img class="star" src="img/icon/star-64-64.png" style="width:16.625px;height:16.625px">
                                                            </div>

                                                            <p class="small-grey">Ốp in hình sắc nét, giao hàng nhanh lắm ạ</p>

                                                        </div>
                                                        <div class="col-md-2 right-align">
                                                            <p class="highline-red">45.000đ</p>
                                                            <p class="small-grey">90.000đ | -50%</p>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="item mb-10">
                                                    <div class="row">
                                                        <div class="col-md-2 img-cont">
                                                            <a href="javascript:void(0)">
                                                                <img src="img/icon/user_circle.png" height="128px">
                                                            </a>
                                                        </div>
                                                        <div class="col-md-8">

                                                            <p>Ốp lưng in hình cô gái</p>

                                                            <div class="container-star starCtn mb-10" style="height:16.625px">
                                                                <img class="star" src="img/icon/star-64-64.png" style="width:16.625px;height:16.625px">
                                                                <img class="star" src="img/icon/star-64-64.png" style="width:16.625px;height:16.625px">
                                                                <img class="star" src="img/icon/star-64-64.png" style="width:16.625px;height:16.625px">
                                                                <img class="star" src="img/icon/star-64-64.png" style="width:16.625px;height:16.625px">
                                                                <img class="star" src="img/icon/star-64-64.png" style="width:16.625px;height:16.625px">
                                                            </div>

                                                            <p class="small-grey">Ốp in hình sắc nét, giao hàng nhanh lắm ạ</p>

                                                        </div>
                                                        <div class="col-md-2 right-align">
                                                            <p class="highline-red">45.000đ</p>
                                                            <p class="small-grey">90.000đ | -50%</p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                            
                                            <section id="user-address" style="display:none">
                                                <h4>Địa chỉ nhận hàng</h4>
                                                <div class="row">
                                                    <div class="col-md-9">
                                                        <h5>Đoàn Đạt<span class="green-verify"><i class="fa fa-check"></i>Địa chỉ mặc định</span></h5>
                                                        <p>Địa chỉ: 311/19 Vườn Lài, P.Phú Thọ Hòa, Quận Tân Phú, TP.HCM</p>
                                                        <p>Điện thoại: 0902996872</p>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <span class="edit">Chỉnh sửa</span>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-9">
                                                        <h5>Đoàn Đạt<span class="green-verify"><i class="fa fa-check"></i>Địa chỉ mặc định</span></h5>
                                                        <p>Địa chỉ: 311/19 Vườn Lài, P.Phú Thọ Hòa, Quận Tân Phú, TP.HCM</p>
                                                        <p>Điện thoại: 0902996872</p>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <span class="edit">Chỉnh sửa</span>
                                                        <span class="remove">Xóa</span>
                                                    </div>
                                                </div>
                                            </section>
                                            
                                            <VIT:Position runat="server" ID="psContent"></VIT:Position>
                                        </div>
                                        <div class="col-md-8" style="display:none">

                                        </div>
                                    </div>
                                </div>
                            </section>
     <script src="/Templates/T01/js/profile.js"></script>
</asp:Content>