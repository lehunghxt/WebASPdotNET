<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CartsPayment.ascx.cs" Inherits="Web.FrontEnd.Modules.CartsPayment" %>
<%@ Import Namespace="Web.Asp.Provider"%>

<div class="container">
<div class="row" style="margin-bottom:20px">                    							
    <div class="col-lg-12 col-md-12 col-sm-12">        






        <asp:Literal ID="lblMsg" runat="server"></asp:Literal>
         </div>   
    <div class="detail">	
    <div class="col-lg-9 col-md-9 col-sm-12">						
              		    
				<div class="d_right d_donhang">	
                 <h5>Thông tin người nhận</h5>                   
                 <div class="phukien">
                  <div class="row">
                                	
                <div class="col-lg-6 col-md-6 col-sm-6">
                    <asp:TextBox ID="txtHoTen" CssClass="form-control" required="required" runat="server" MaxLength="300" placeholder="Họ tên người nhận"></asp:TextBox><br />
                </div>
                                    
                <div class="col-lg-6 col-md-6 col-sm-6">
                    <asp:TextBox ID="txtDienThoai" ClientIDMode="Static" CssClass="form-control" runat="server" MaxLength="300" required="required" placeholder="Số điện thoại người nhận"></asp:TextBox><br />
                </div>

                 <div class="col-lg-6 col-md-6 col-sm-6">
                    <asp:TextBox ID="txtMail" CssClass="form-control" runat="server" placeholder="Email người đặt hàng"></asp:TextBox><br />
                </div>

                <div class="col-lg-6 col-md-6 col-sm-6">
                    <asp:TextBox ID="txtDiaChi" CssClass="form-control" runat="server" required="required" placeholder="Địa chỉ giao hàng"></asp:TextBox><br />
                </div>                                   
            </div>
                 </div>
                  <h5>Chọn nhà vận chuyển</h5>                   
                 <div class="phukien">
                  <b>Vận chuyển toàn quốc:</b><br>
                  <label>Giao hàng đến:</label>
                     <select name="den" id="giaoden">
                         <option value="0">--- Giao đến ---</option>
                         <%foreach (var province in Web.FrontEnd.DataSource.ProvinceSourceCollection) { %>
                         <option value="<%=province.Id %>"><%=province.Name %></option>
                         <%} %>
                     </select>
                     <label>Bằng</label>
                  <select name="ShippingId" id="ShippingId"><option value="2">Giao Hàng Tiết Kiệm</option><option value="1">Giao Hàng Nhanh</option></select> <label><a>Với phí <sup>đ</sup><b id="shipfee">30.000</b></a></label>
                  <h6>Thời gian giao hàng <a>2-3 ngày</a></h6>
                 </div>
                 <div class="mienphi">
                  <img src="/Templates/T01/images/truck.png"><div class="mienphi-note">Miên phí vận chuyển cho đơn hàng có giá trị từ<span  class="toida"> <sup>đ</sup>400.000</span> giảm giá tối đa <span class="thapnhat">(<sup>đ</sup> 50.000)</span><div>Bạn được miễn phí vận chuyển</div></div>
                 </div>
                 <h5>Hình thức thanh toán</h5>                   
                 <div class="phukien">                
                  <ul>
                  <li><input id="tt" class="tt" type="radio" name="thanhtoan" value="Local" checked="checked">Thanh toán khi nhận hàng</li>
                  <li class="pt-center">&nbsp;</li>
                  <li>
                      <input id="online" class="online" type="radio" value="Online" name="thanhtoan">Thanh toán trực tuyến
                  <div class="phukien" id="thanhtoanonline" style="display:none; margin-top:13px">

        <input type="radio" name="PaymentMethod" value="ATM" /> Internet Banking - thẻ thanh toán nội địa<br />
        <input type="radio" name="PaymentMethod" value="VISA" /> Thẻ Visa - Master - thẻ thanh toán quốc tế<br />
</div>
                      </li>
                  </ul>
                 </div>

				 </div>
                            </div>
        <div class="col-lg-3 col-md-3 col-sm-12">
            <div class="d_right_right">
                    <div class="ctdonhang">
                     <h5>Đơn Hàng(<%=Carts.Sum(e => e.Quantity) %> Sản Phẩm)</h5> <a href="<%=HREF.BaseUrl %>vit-carts" class="suadh">Sửa</a><br>
                     
                        <%foreach (var product in Carts) {%>   
                        <hr class="clearfix">
                     <h5><a><%=product.Quantity%>x &nbsp;&nbsp; <%=product.ProductName%></a></h5> <a href="#" class="suadh suadhnone"><sup>đ</sup> <%=string.Format("{0:0,0}", product.Price)%></a>
                      <% } %>
                     <hr class="clearfix">                   
                     <h5><b>Tổng đơn hàng</b></h5> <a href="#" class="suadh suadhnone"><sup>đ</sup> <%=string.Format("{0:0,0}", Carts.Sum(e => e.TotalCost))  %></a>
                     <hr class="clearfix">                   
                     <h5 class="pk" id="UsePoint" data-toggle="modal" data-target="#pointModal">Sử dụng ví PK</h5>
                     <hr class="clearfix">     
                     <h5 class="pk" id="UseVoucher" data-toggle="modal" data-target="#promotionModal">Sử dụng Voucher</h5>
                     <hr class="clearfix">                   
                     <h5><b>Phí vận chuyển (PVC):</b></h5> <a href="#" class="suadh suadhnone"><sup>đ</sup> <b id="shipfee2">30.000</b></a>
                      <hr class="clearfix">                   
                     <h5><b class="ctthanhtien">Thành Tiền:</b></h5> <a href="#" class="suadh suadhnone suatt"><sup>đ</sup> <b id="totalfee"><%=string.Format("{0:0,0}", Carts.Sum(e => e.TotalCost))  %></b></a>
                        <span class="vat">(Đã bao gồm VAT)</span>
                        <asp:Button ID="imbHoanTat" runat="server" onclick="imbHoanTat_Click" CssClass="btn btn-danger pull-right" Text="Đặt hàng" Width="100%"/>

                     <h5>Ghi chú</h5>  
                        <asp:TextBox ID="txtNote" CssClass="ghichu" runat="server" TextMode="MultiLine" Height="100px" placeholder="Ghi chú"></asp:TextBox>
                    </div>
                   </div> 
            </div> 
     </div>  
</div>
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
                        $('#shipfee2').html(fee);

                        var km = $('#VoucherFee').val();
                        var total = data.d.fee + <%=Carts.Sum(e => e.TotalCost)  %> - km;
                        var strTotal = (total + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
                        strTotal = strTotal.substring(0, strTotal.length - 1);
                        $('#totalfee').html(strTotal);
                    }
                    else {

                    }
                }
            });
        }
        });
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

