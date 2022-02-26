<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CartsPayment.ascx.cs" Inherits="Web.FrontEnd.Modules.CartsPayment" %>
 

<div class="row">                    							
    <div class="col-lg-12 col-md-12 col-sm-12">                        	
		






        <asp:Literal ID="lblMsg" runat="server"></asp:Literal>
        					
        <table class="shopping-table" style="width:100%">                            	
            <tr>
                <th colspan="2">Sản phẩm</th>
                <th>Mã</th>
                <th>Số lượng</th>
                <th>Đơn giá</th>
                
                <th>Thành tiền</th>
                <th></th>
            </tr> 
            
            <%foreach (var product in Carts)
                {%>                 
                    <tr id="row<%=product.ProductId %>">
                        <td style="background:#fff;padding:5px;width:80px;"><img src="<%=product.ProductImage %>" style="width:80px" alt="<%=product.ProductImage %>"/></td>
                        <td><p><%=product.ProductName %></p></td>
                        <td><p><%=product.ProductCode %></p></td>
                        <td class="quantity">
                            <input type="number" id="sl<%=product.ProductId%>" value="<%=product.Quantity%>" 
                                onblur="TinhTongTien('<%=product.ProductId%>')" style="width:80px;" <%=product.IsAddOn ? "readonly" : ""%>/>
                        </td>
                        <td><p>
                            <label id="lblUnit<%=product.ProductId%>"><%=string.Format("{0:0,0} đ", Convert.ToDecimal(product.Price))%>
                            </label>
                            </p>
                            <input type="hidden" id='DGia<%=product.ProductId%>' value="<%=product.Price%>" />
                            <input type="hidden" id='tongtiencot<%=product.ProductId%>' value="<%=product.TotalCost%>" />
                        </td>
                        
                        <td>
                            <label id="lbl<%=product.ProductId%>"><%=string.Format("{0:0,0}", Convert.ToDecimal(product.TotalCost))%> </label>                         
                        </td>
                        <td><a href="#" class="xoa" onclick="RemoveProductFromCarts(<%=product.ProductId %>)">Xóa</a></td>
                    </tr> 
                <%} %>
                   
			<tr>
				<td class="align-right" colspan="7">
                    <strong>Tổng tiền:
                    <input type="hidden" id="tongtienHiden" name="tongtienHiden" value="<%=tongtienHiden.Replace(".","")%>"/>
                    <label id="tongthanhtien"><%=tongtienHiden%></label> đồng.</strong>
				</td>
             </tr><tr>
                <td class="align-right" colspan="7">
                    <strong>Tổng tiền:
                            <label class="value" id="shipfee"> </label>
                        </strong>
                    </td>
                 </tr><tr>
                <td class="align-right" colspan="7">
                    <strong>Tổng thanh toán: 
                            <label class="value" id="totalfee"><%=tongtienHiden%> đ</label>
                        </strong>
                    </td>
			</tr>
                                
        </table>
                            
    </div>                        
</div>

<div class="row">     	
    <div class="col-lg-12 col-md-12 col-sm-12">
        <div class="carousel-heading no-margin">
            <h4>Thông tin đặt hàng</h4>
        </div>
                            
        <div class="page-content">
                            	
            <div class="row">
                                	
                <div class="col-lg-6 col-md-6 col-sm-6">
                    <asp:TextBox ID="txtHoTen" required="required" runat="server" MaxLength="300" placeholder="Họ tên người nhận"></asp:TextBox>
                    <br /><br />
                </div>
                                    
                <div class="col-lg-6 col-md-6 col-sm-6">
                    <asp:TextBox ID="txtDienThoai" runat="server" MaxLength="300" required="required" placeholder="Số điện thoại người nhận"></asp:TextBox>
                    <br /><br />
                </div>

                 <div class="col-lg-6 col-md-6 col-sm-6">
                    <asp:TextBox ID="txtMail" runat="server" placeholder="Email người đặt hàng"></asp:TextBox>
                     <br /><br />
                </div>

                <div class="col-lg-6 col-md-6 col-sm-6">
                    <asp:TextBox ID="txtDiaChi" runat="server" required="required" placeholder="Địa chỉ giao hàng"></asp:TextBox>
                    <br /><br />
                </div>

                <div class="col-lg-12 col-md-12 col-sm-12">
                    <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Height="100px" placeholder="Ghi chú"></asp:TextBox>
                    <br />
                </div>
               
            </div>
                                
        </div>
                            
    </div>
                          
</div>

<div class="row">
    
                <div class="col-lg-12 col-md-12 col-sm-12  form-inline">
                    <div class="carousel-heading no-margin">
            <h4>Thông tin vận chuyển và thanh toán</h4>
        </div>
                    <div class="page-content">
                    <section class="cart-panel delivery giftcode">
                <h4>Chọn nhà vận chuyển: </h4>
                        <div class="form-group">
    <label for="giaoden">Giao hàng đến: </label>
    <select name="den" id="giaoden" class="destination form-control">
                         <option value="0">--- Giao đến ---</option>
                         <%foreach (var province in Web.Asp.ObjectData.DataSource.ProvinceSourceCollection) { %> 
                         <option value="<%=province.Id %>"><%=province.Name %></option>
                         <%} %>
                     </select>
  </div>
                <div class="form-group">
                    <label for="ShippingId">Bằng: </label>
                     <select name="ShippingId" id="ShippingId" class="form-control"><option value="2">Giao Hàng Tiết Kiệm</option><option value="1">Giao Hàng Nhanh</option></select>

                </div>
            </section> 
            <section class="cart-panel delivery giftcode">
                <h4>Hình thức thanh toán: </h4>
                <div> 
                  <div style="margin-bottom:15px"><input id="tt" class="tt" type="radio" name="thanhtoan" value="Local" checked="checked">Thanh toán khi nhận hàng</div>
 
                  <div>
                      <input id="online" class="online" type="radio" value="Online" name="thanhtoan">Thanh toán trực tuyến
                  <div class="phukien" id="thanhtoanonline" style="display:none; margin-top:13px">

        <input type="radio" name="PaymentMethod" value="ATM" /> Internet Banking - thẻ thanh toán nội địa<br />
        <input type="radio" name="PaymentMethod" value="VISA" /> Thẻ Visa - Master - thẻ thanh toán quốc tế<br />
</div>
                      </div> 
            </div> 
                </section>
                </div>
                    </div>
</div>

<div class="row" style="margin-bottom:15px">
<div class="col-lg-12 col-md-12 col-sm-12">
                    <asp:Button ID="imbHoanTat" runat="server" onclick="imbHoanTat_Click" CssClass="btn btn-primary pull-right" Text="Gửi đơn hàng" />
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
                $(".destination").chosen();
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
            data: JSON.stringify({ productId: id, properties: ''}),
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
            data: JSON.stringify({ productId: id, quanlity: sl, properties: '' }),
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
