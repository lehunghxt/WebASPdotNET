<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CartsPayment.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.CartsPayment" %>
<%@ Import Namespace="VIT.DataTransferObjects" %>
<%@ Import Namespace="VIT.Library" %>






<asp:Literal ID="lblMsg" runat="server"></asp:Literal>

<section class="steps-nav delivery-nav" style="margin-top: 10px;">
    <ul class="uk-list uk-clearfix">
        <li class="item"><a href="" title=""><span class="number">1</span> Nhập thông tin giao hàng</a></li>
        <li class="item"><a href="" title=""><span class="number">2</span> Chọn hình thức thanh toán</a></li>
        <li class="item"><a href="" title=""><span class="number">3</span> Hoàn tất</a></li>
    </ul>
</section>

<div class="uk-clearfix wrapper">
    <div class="main-content col-md-6">
        <div action="" method="post" class="uk-form form">
            <section class="cart-panel delivery delivery-address">
                <header class="panel-head">
                    <h2 class="heading"><span>Địa chỉ nhận hàng</span></h2>
                </header>
                <section class="panel-body">
                    <div class="form-row uk-clearfix">
                        <div class="first-child">
                            <div class="label">Họ và tên</div>
                        </div>
                        <div class="last-child">
                            <asp:TextBox ID="txtHoTen" CssClass="text uk-width-1-1" name="name" required="required" runat="server" MaxLength="300" placeholder="Ví dụ: Nguyễn Văn A"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-row uk-clearfix">
                        <div class="first-child">
                            <div class="label">Điện thoại</div>
                        </div>
                        <div class="last-child">
                            <div class="uk-grid lib-grid-15 uk-grid-width-1-2">
                                <div class="column">
                                    <asp:TextBox ID="txtDienThoai" runat="server" MaxLength="300" required="required" name="email" CssClass="text uk-width-1-1" placeholder="Ví dụ: 0987654321"></asp:TextBox>
                                </div>
                                <div class="column">
                                    <div class="text">Nhân viên giao nhận  sẽ liên hệ với SĐT này.</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-row uk-clearfix">
                        <div class="first-child">
                            <div class="label">Email</div>
                            <span class="no-required">(Bắt buộc)</span>
                        </div>
                        <div class="last-child">
                            <asp:TextBox ID="txtMail" runat="server" name="message" required="required" CssClass="text uk-width-1-1" placeholder="supportxyz@gmail.com"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-row uk-clearfix">
                        <div class="first-child">
                            <div class="label">Địa chỉ giao hàng</div>
                        </div>
                        <div class="last-child">
                            <asp:TextBox ID="txtDiaChi" runat="server" name="message" required="required" CssClass="text uk-width-1-1" placeholder="Ví dụ: Số 10, Ngõ 50, Đường ABC"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-row uk-clearfix">
                        <div class="first-child">
                            <div class="label">Lời nhắn</div>
                            <span class="no-required">(Không bắt buộc)</span>
                        </div>
                        <div class="last-child">
                            <asp:TextBox ID="txtNote" runat="server" name="message" TextMode="MultiLine" CssClass="uk-width-1-1 form-textarea" Rows="6" placeholder="Ví dụ: Chuyển hàng ngoài giờ hành chính"></asp:TextBox>
                        </div>
                    </div>
                </section>
                <!-- .panel-body -->
            </section>
            <!-- .delivery -->
            <section class="cart-panel delivery giftcode">
                <header class="panel-head uk-flex uk-flex-middle uk-flex-space-between">
                    <h2 class="heading"><span>Hình thức thanh toán</span></h2>
                </header>
                <section class="panel-body">
                    <div class="form-group col-md-6">
                        <asp:RadioButtonList ID="rdbPhuongThuc" runat="server" Width="350%">
                            <asp:ListItem Selected="True">Nhận h&#224;ng trực tiếp từ trung t&#226;m</asp:ListItem>
                            <asp:ListItem>Giao h&#224;ng tại nh&#224;</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="form-group col-md-6">
                        <asp:RadioButtonList ID="rdbHinhThuc" runat="server" Width="350%">
                            <asp:ListItem Selected="True">Thanh toán bằng tiền mặt</asp:ListItem>
                            <asp:ListItem>Chuyển khoản qua ngân hàng</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </section>
            </section> 
            <div class="continue-box uk-text-right">
                <asp:Button ID="imbHoanTat" runat="server" OnClick="imbHoanTat_Click" CssClass="btn-continue" Text="Hoàn tất" />
            </div>
        </div>
        <!-- form -->
    </div>
    <!-- .main-content -->

    <%
        var giohang = Session[SettingsManager.Constants.SessionGioHang + this.CompanyId] as List<GioHangDto>;
        if (giohang == null) giohang = new List<GioHangDto>();
    %>
    <aside class="aside-content col-md-6">
            <section class="cart-panel panel-order">
                <header class="panel-head uk-flex uk-flex-middle uk-flex-space-between">
                    <h2 class="heading">Đơn hàng (<span class="count"><%=giohang.Select(o => o.Quantity).Sum() %> Sản phẩm</span>)</h2>
                    <a class="link" href="/">Tiếp tục mua</a>
                </header>
                <section class="panel-body">
                    <ul class="uk-list list-product">
                        <asp:Repeater ID="rptGioHang" runat="server">
                            <ItemTemplate>
                                <li>
                                    <div class="box uk-clearfix">
                                        <div class="prd-infor uk-clearfix">
                                            <div class="thumb">
                                                <a title="<%#Eval("ProductName")%>">
                                                    <img src="<%#Eval("Image")%>" alt="<%#Eval("ProductName")%>"></a>
                                            </div>
                                            <div class="desc">
                                                <h3 class="title"><%#Eval("ProductName")%></h3>
                                                <span>Mã: <%#Eval("ProductCode")%></span>
                                            </div>
                                        </div>
                                        <div class="prd-price">
                                            <input type="hidden" id='DGia<%#Eval("ProductID")%>' value="<%#Eval("UnitPrice")%>" />
                                            <input type="hidden" id='tongtiencot<%#Eval("ProductID")%>' value="<%#Eval("TotalCost")%>" />
                                            <div class="price"><label id="lblUnit<%#Eval("ProductID")%>"><%#string.Format("{0:0,0} đ", Convert.ToDecimal(Eval("UnitPrice")))%></label></div>
                                            <div class="quantity" style="color: red;">x
                                                <input class="input-quantity" id="sl<%#Eval("ProductID")%>" type="text" value="<%#Eval("Quantity")%>" onblur="TinhTongTien('<%#Eval("ProductID")%>')" name="1[qty]" style="width: 30px; text-align: right;"></div>
                                            <div class="price">
                                                <label class="cart_total_price" id="lbl<%#Eval("ProductID")%>"><%#string.Format("{0:0,0}", Convert.ToDecimal(Eval("TotalCost")))%> đ</label></div>
                                        </div>
                                    </div>
                                    <!-- .box -->
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <button type="submit" name="updated" value="action" class="uk-button button uk-hidden" id="fc-cart-updated">Cập nhật</button>
                    </ul>
                </section>
                <!-- .panel-body -->

                <input type="hidden" id="tongtienHiden" name="tongtienHiden" value="<%=tongtienHiden.Replace(".","")%>" />
                <div class="panel-foot">
                    <div class="total-amount">
                        <div class="uk-flex uk-flex-middle uk-flex-space-between item">
                            <div class="label">Tổng tiền</div>
                            <div class="value"><%=tongtienHiden%> đ</div>
                        </div>
                    </div>
                    <div class="label">Giá trên chưa bao gồm phí vận chuyển <a target="_blank" href="<%=HREF.LinkComponent("Page", "Info3") %>" title="">Click để xem chi tiết!</a></div>
                </div>
            </section>
            <!-- .panel-order -->
    </aside>
    <!-- .aside -->
</div>
<script src="<%=HREF.BaseUrl%>Templates/T0151/js/KiemTra.js" type="text/javascript"></script>


<script language="javascript" type="text/javascript">
    var arrQuatity = new Array();
    var arrPrice = new Array();
    function TinhTongTien(id) {
        <% var productIds = this.ProductPrices.Select(e => e.ProductId);%>
        <%foreach (var id in productIds)
    {%>
            <% var prices = this.ProductPrices.Where(e => e.ProductId == id);%>
        arrQuatity[<%=id %>] = [<%= string.Join(",",prices.Select(e => e.Quatity))%>]
        arrPrice[<%=id %>] = [<%= string.Join(",",prices.Select(e => e.Price))%>] 
        <%}%>

        var tongtienold = $("#tongtienHiden").val();
        var tongtiencot = $("#tongtiencot" + id).val();
        var sl = $("#sl" + id).val();
        var dg = $("#DGia" + id).val();

        if(arrQuatity[id] && arrQuatity[id].length && arrPrice[id] && arrPrice[id].length && arrQuatity[id].length == arrPrice[id].length)
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
        $("#tongtienHiden").val(tongtien.replace('.', ''));
         
        // Send the rating information somewhere using Ajax or something like that.
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>Components/Page/JsonPost.aspx/EditCarts",
            data: JSON.stringify({ productId: id, quantity: sl }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {

                }
            }
        });
    }
</script>
<script language="javascript" type="text/javascript">
    removejscssfile('/Includes/disablecopy/disable-copy.css', 'js');
</script>

<%if (!string.IsNullOrEmpty(this.Skin.BackgroundColor))
    {%>
<style>
    #cart_items .cart_info .cart_menu {
        color: <%=this.Skin.BackgroundColor%>;
    }
</style>
<%} %>

<%if (!string.IsNullOrEmpty(this.Skin.FontColor))
    {%>
<style>
    #cart_items .cart_info .cart_menu {
        background-color: <%=this.Skin.FontColor%>;
    }
</style>
<%} %>
    
    
    
    
    

    

