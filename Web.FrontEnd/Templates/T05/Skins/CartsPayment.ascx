<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CartsPayment.ascx.cs" Inherits="Web.FrontEnd.Modules.CartsPayment" %>
 

<section class="steps-nav delivery-nav">
    <ul class="uk-list uk-clearfix">
        <li class="item"><a href="" title=""><span class="number">1</span> Nhập thông tin giao hàng</a></li>
        <li class="item"><a href="" title=""><span class="number">2</span> Chọn hình thức thanh toán</a></li>
        <li class="item"><a href="" title=""><span class="number">3</span> Hoàn tất</a></li>
    </ul>
</section>

<div class="steps-nav delivery-nav">        






        <asp:Literal ID="lblMsg" runat="server"></asp:Literal>
</div> 

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
                            <div class="label">Điện thoại</div><span class="no-required">(Bắt buộc)</span>
                        </div>
                        <div class="last-child">
                                    <asp:TextBox ID="txtDienThoai" runat="server" MaxLength="300" required="required" name="email" CssClass="text uk-width-1-1" placeholder="Ví dụ: 0987654321"></asp:TextBox>

                        </div>
                    </div>
                    <div class="form-row uk-clearfix">
                        <div class="first-child">
                            <div class="label">Email</div> 
                        </div>
                        <div class="last-child">
                            <asp:TextBox ID="txtMail" runat="server" name="message" CssClass="text uk-width-1-1" placeholder="supportxyz@gmail.com"></asp:TextBox>
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
                    <h2 class="heading"><span>Chọn nhà vận chuyển</span></h2>
                </header>
                <section class="panel-body">
                    <table>
                        <tr>
                        <td>
                            <div class="label">Giao hàng đến</div>
                        </td>
                        <td>
                            <select name="den" id="giaoden" style="width:100%">
                         <option value="0">--- Giao đến ---</option>
                         <%foreach (var province in Web.Asp.ObjectData.DataSource.ProvinceSourceCollection) { %> 
                         <option value="<%=province.Id %>"><%=province.Name %></option>
                         <%} %>
                     </select>
                        </td>
                            </tr>
                        <tr><td colspan="2" style="height:15px"></td></tr>
                    
                    <tr>
                        <td>
                            <div class="label">Bằng</div>
                        </td>
                        <td>
                            <select name="ShippingId" id="ShippingId" style="width:100%"><option value="2">Giao Hàng Tiết Kiệm</option><option value="1">Giao Hàng Nhanh</option></select>
                        </td>
                    </tr>
                        </table>
                </section>
            </section> 
            <section class="cart-panel delivery giftcode">
                <header class="panel-head uk-flex uk-flex-middle uk-flex-space-between">
                    <h2 class="heading"><span>Hình thức thanh toán</span></h2>
                </header>
                <section class="panel-body"> 
                  <div style="margin-bottom:15px"><input id="tt" class="tt" type="radio" name="thanhtoan" value="Local" checked="checked">Thanh toán khi nhận hàng</div>
 
                  <div>
                      <input id="online" class="online" type="radio" value="Online" name="thanhtoan">Thanh toán trực tuyến
                  <div class="phukien" id="thanhtoanonline" style="display:none; margin-top:13px">

        <input type="radio" name="PaymentMethod" value="ATM" /> Internet Banking - thẻ thanh toán nội địa<br />
        <input type="radio" name="PaymentMethod" value="VISA" /> Thẻ Visa - Master - thẻ thanh toán quốc tế<br />
</div>
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

    <aside class="aside-content col-md-6">
            <section class="cart-panel panel-order">
                <header class="panel-head uk-flex uk-flex-middle uk-flex-space-between">
                    <h2 class="heading" id="totalproduct">Đơn hàng (<span class="count"><%=Carts.Select(o => o.Quantity).Sum() %> Sản phẩm</span>)</h2>
                    <a class="link" href="/">Tiếp tục mua</a>
                </header>
                <section class="panel-body">
                    <ul class="uk-list list-product">
                        <%foreach (var product in Carts)
                            {%> 
                                <li id="row<%=product.ProductId %>">
                                    <div class="box uk-clearfix">
                                        <div class="prd-infor uk-clearfix">
                                            <div class="thumb">
                                                <a title="<%=product.ProductName%>">
                                                    <img src="<%=product.ProductImage%>" alt="<%=product.ProductName%>"></a>
                                            </div>
                                            <div class="desc">
                                                <h3 class="title"><%=product.ProductName%></h3>
                                                <span>Mã: <%=product.ProductCode%></span><br />
                                                <a href="#" class="xoa" onclick="RemoveProductFromCarts(<%=product.ProductId %>)">Xóa</a>
                                            </div>
                                        </div>
                                        <div class="prd-price">
                                            <input type="hidden" id='DGia<%=product.ProductId%>' value="<%=product.Price%>" />
                                            <input type="hidden" id='tongtiencot<%=product.ProductId%>' value="<%=product.TotalCost%>" />
                                            <div class="price" style="height:20px"><label id="lblUnit<%=product.ProductId%>"><%=string.Format("{0:0,0} đ", Convert.ToDecimal(product.Price))%></label></div>
                                            <div class="quantity" style="color: red;">x
                                                <input class="input-quantity" id="sl<%=product.ProductId%>" type="text" value="<%=product.Quantity%>" onblur="TinhTongTien('<%=product.ProductId%>')" name="1[qty]" style="width: 50px; text-align: right;" <%=product.IsAddOn ? "readonly" : "" %></div>
                                            <div class="price">
                                                <label class="cart_total_price" id="lbl<%=product.ProductId%>"><%=string.Format("{0:0,0}", Convert.ToDecimal(product.TotalCost))%> đ</label>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- .box -->
                                </li>
                            <%} %>
                        <button type="submit" name="updated" value="action" class="uk-button button uk-hidden" id="fc-cart-updated">Cập nhật</button>
                    </ul>
                </section>
                <!-- .panel-body -->
                
                <input type="hidden" id="tongtienHiden" name="tongtienHiden" value="<%=tongtienHiden.Replace(".","")%>" />
                <div class="panel-foot">
                    <div class="total-amount">
                        <div class="uk-flex uk-flex-middle uk-flex-space-between item">
                            <div class="label">Tổng tiền: </div>
                            <div class="value" id="tongthanhtien"><%=tongtienHiden%> đ</div>
                        </div>
                         <div class="uk-flex uk-flex-middle uk-flex-space-between item">
                            <div class="label">Phí vận chuyển: </div>
                            <div class="value" id="shipfee"> </div>
                        </div>
                         <div class="uk-flex uk-flex-middle uk-flex-space-between item">
                            <div class="label">Tổng thanh toán: </div>
                            <div class="value" id="totalfee"><%=tongtienHiden%> đ</div>
                        </div>
                    </div>
                </div>
            </section>
            <!-- .panel-order -->
    </aside>
    <!-- .aside -->
</div>



<div id="promotionModal" class="modal fade" role="dialog">
  <div class="modal-dialog">
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Nhập mã voucher</h4>
      </div>
      <div class="modal-body">
        <label>Mã voucher:</label> <input type="text" id="Voucher" name="Voucher" placeholder="Nhập mã voucher" class="form-control" style="display:unset"/>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-warning" onclick="AddVoucher();" data-dismiss="modal">Đồng ý</button>
      </div>
    </div>

  </div>
</div>

<div id="pointModal" class="modal fade" role="dialog">
  <div class="modal-dialog">
    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Điểm của bạn</h4>
      </div>
      <div class="modal-body">
          <input type="hidden" name="PointTemp" id="PointTemp"/>
        <div>Bạn có <strong id="hasPoint">0</strong> điểm. Tương đương <strong id="pricePoint">0 đ</strong></div>
          <div>Bạn có muốn sử dụng điểm tích lũy không?</div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-warning" onclick="AddPoint();" data-dismiss="modal">Đồng ý</button>
          <button type="button" class="btn btn-warning" data-dismiss="modal">Không</button>
      </div>
    </div>

  </div>
</div>

<input type="hidden" name="Point" id="Point"/>
<input type="hidden" name="PointFee" id="PointFee" value="0"/>
<input type="hidden" name="VoucherFee" id="VoucherFee" value="0"/>
<input type="hidden" name="DeliveryFee" id="DeliveryFee" value="0"/>

<script type="text/javascript">
    $('#online').change(function () {
        if ($(this).is(":checked") == true)
        {
            $("#thanhtoanonline").show();
        }
    });
    $('#tt').change(function () {
        if ($(this).is(":checked") == true) {
            $("#thanhtoanonline").hide();
        }
    });

    function Action(id)
    {
        $("#action").val(id);   
    }
</script>

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
                 }
             }
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
        CalculatorShipFee();
    });
    $('#ShippingId').on('change', function () {
        CalculatorShipFee();
    });

    function CalculatorShipFee()
    {
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
                        $('#shipfee').html(fee + " đ");

                        var km = $('#VoucherFee').val(); 
                        var tonghang = $('#tongtienHiden').val();
                        var total = data.d.fee + parseInt(tonghang, 10) - km;
                        var strTotal = (total + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
                        strTotal = strTotal.substring(0, strTotal.length - 1);
                        $('#totalfee').html(strTotal + " đ");
                    }
                    else {
                        alert('GHTK không hỗ trợ giao hàng khu vực này, vui lòng chọn dich vụ khác!');
                    }
                }
            });
        } else if (shipping == 1) {
            var value = $('#giaoden').val();
            $.ajax({
                type: "POST",
                url: "<%=HREF.BaseUrl %>JsonPost.aspx/GetGHNFee",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({ companyId: <%=Config.ID %>, to: value, weight: 1 }),
                success: function (data) { 
                        $('#DeliveryFee').val(data.d.ServiceFee);
                        var fee = (data.d.ServiceFee + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
                        fee = fee.substring(0, fee.length - 1);
                        $('#shipfee').html(fee + " đ");

                        var km = $('#VoucherFee').val(); 
                        var tonghang = $('#tongtienHiden').val();
                        var total = data.d.ServiceFee + parseInt(tonghang, 10) - km;
                        var strTotal = (total + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
                        strTotal = strTotal.substring(0, strTotal.length - 1);
                        $('#totalfee').html(strTotal + " đ"); 
                }
            });
        }
    }
</script>

<script>
    function AddVoucher() {
        var voucherCode = $('#Voucher').val();
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/GetVoucher",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ companyId: <%=Config.ID%>, code: voucherCode  }),
        success: function (data) {
            if (data.d == null) alert('Không tồn tại voucher [' + voucherCode + ']');
            else if (!data.d.Code) alert('Voucher [' + voucherCode + '] đã hết hạn sử dụng.');
            else if (data.d.Quantity == 0) alert('Voucher [' + voucherCode + '] đã hết.');
            else {
                var amount = <%=Carts.Sum(e => e.TotalCost)  %> + $('#DeliveryFee').val();
                var km = 0;
                if (data.d.IsPercent) km= <%=Carts.Sum(e => e.TotalCost)  %> * data.d.Value / 100;
                else km = data.d.Value;
                amount -= km;

                $('#VoucherFee').val(km);
                var strKM = (km + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
                strKM = strKM.substring(0, strKM.length - 1);
                $('#UseVoucher').html('Voucher [' + data.d.Code + '] : -' + strKM);

                var strTotal = (amount + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
                strTotal = strTotal.substring(0, strTotal.length - 1);
                $('#totalfee').html(strTotal);
            }
        }
        });
    }

    function GetCustomer() {
        var cusphone = $('#txtDienThoai').val();
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/GetCustomer",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ companyId: <%=Config.ID%>, phone: cusphone }),
            success: function (data) {
                if (data.d != null) {
                    $('#hasPoint').html(data.d.Point); 
                    var amount = data.d.Point * data.d.TranferPrice;
                    $('#pricePoint').html(amount);
                    $('#PointFee').val(amount); 
                    $('#PointTemp').val(data.d.Point);
                }
        }
        });
    }

    function AddPoint() {
        var point = $('#PointTemp').val();
        if (point > 0) {
            $('#Point').val(point);
            var pointprice = $('#PointFee').val();
            var strPoint = (pointprice + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
            strPoint = strPoint.substring(0, strPoint.length - 1);
            $('#UsePoint').html('Sử dụng [' + point + '] điểm : -' + strPoint);

            var amount = <%=Carts.Sum(e => e.TotalCost)  %> + $('#DeliveryFee').val() - $('#VoucherFee').val() - $('#PointFee').val();
            var strTotal = (amount + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
            strTotal = strTotal.substring(0, strTotal.length - 1);
            $('#totalfee').html(strTotal);
        }
    }
</script>


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

                    var total = 0; 
                    var totalPrice = 0;
                    for (var i = 0; i < data.d.length; i++) {
                        total += data.d[i].Quantity;
                        totalPrice+= data.d[i].TotalCost;
                    }
                    $("#hanggio").html("(" + total + ") sản phẩm");
                    $("#totalproduct").html("Đơn hàng (" + total + " Sản phẩm)");

                    tongtienmoi = (totalPrice + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
                    tongtienmoi = tongtienmoi.substring(0, tongtienmoi.length - 1);
                    document.getElementById("tongthanhtien").innerHTML = tongtienmoi;
                    $("#tongtienHiden").val(totalPrice);
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

        //var newt = (t + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
        //newt = newt.substring(0, newt.length - 1);
        //document.getElementById("lbl" + id).innerHTML = "<label id='lbl" + id + "'>" + newt + " <input type='hidden' id='tongtiencot" + id + "' value=" + newt.replace('.', '') + " /> </label>";
        
        //var tongtien = (tongtienold - tongtiencot) + t;
        //tongtien = (tongtien + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
        //tongtien = tongtien.substring(0, tongtien.length - 1);
        //document.getElementById("tongthanhtien").innerHTML = tongtien;
        //$("#tongtienHiden").val(tongtien.replace('.', ''));

        // Send the rating information somewhere using Ajax or something like that.
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/EditCarts",
            data: JSON.stringify({ productId: id, quanlity: sl }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {
                    $.ajax({
                    type: "POST",
                    url: "<%=HREF.BaseUrl %>JsonPost.aspx/GetCarts",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (carts) {
                        if (carts.d != "") {
                            var totalQuantity = 0;
                            var totalPrice = 0;
                            for (var i = 0; i < carts.d.length; i++) {
                                totalQuantity += carts.d[i].Quantity;
                                totalPrice+= carts.d[i].TotalCost;
                            }
                            $("#hanggio").html("(" + totalQuantity + ") sản phẩm");
                            $("#totalproduct").html("Đơn hàng (" + totalQuantity + " Sản phẩm)");

                            tongtienmoi = (totalPrice + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
                            tongtienmoi = tongtienmoi.substring(0, tongtienmoi.length - 1);
                            document.getElementById("tongthanhtien").innerHTML = tongtienmoi;
                            $("#tongtienHiden").val(totalPrice);
                        }
            }
        });
                }
            }
        });
    }
</script>
