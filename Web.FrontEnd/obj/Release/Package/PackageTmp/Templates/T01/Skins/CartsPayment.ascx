<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CartsPayment.ascx.cs" Inherits="Web.FrontEnd.Modules.CartsPayment" %>
 

<%if(this.UserContext != null){ %>






<asp:Literal ID="lblMsg" runat="server"></asp:Literal>
<h3>2. Địa chỉ giao hàng</h3>
<div class="row-style-2">
                                                        <h3 class="step-title">Thông tin người nhận</h3>
                                                        <div class="container">
                                                            <p class="other">
                                                                <a href="javascript:void(0)" onclick="FillInfo()">Nhập địa chỉ mới
                                                                </a>
                                                            </p>
                                                        </div>
                                                    <div class="panel panel-default address-form is-edit" style="display: block; width: 100%">
                                                        <div class="panel-body">
                                                            <div class="form-horizontal bv-form" role="form" id="address-info" action="javascript:void(0)" novalidate="novalidate"><button type="submit" class="bv-hidden-submit" style="display: none; width: 0px; height: 0px;"></button>
                                                                <div class="form-group row">
                                                                    <label for="full_name" class="col-lg-4 control-label visible-lg-block">Họ tên </label>
                                                                    <div class="col-lg-8 input-wrap has-feedback">
                                                                        <asp:TextBox ID="txtHoTen" required="required" ClientIDMode="Static" runat="server" CssClass="form-control address" MaxLength="300" placeholder="Họ tên người nhận"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">
                                                                    <label for="telephone" class="col-lg-4 control-label visible-lg-block">Điện thoại di động</label>
                                                                    <div class="col-lg-8 input-wrap has-feedback">
                                                                        <asp:TextBox ID="txtDienThoai" required="required" ClientIDMode="Static" runat="server" MaxLength="300" placeholder="Số điện thoại người nhận" CssClass="form-control address"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">
                                                                    <label for="telephone" class="col-lg-4 control-label visible-lg-block">Email</label>
                                                                    <div class="col-lg-8 input-wrap has-feedback">
                                                                        <asp:TextBox ID="txtMail" ClientIDMode="Static" CssClass="form-control address" runat="server" placeholder="Email người đặt hàng"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">
                                                                    <label for="telephone" class="col-lg-4 control-label visible-lg-block">Địa chỉ</label>
                                                                    <div class="col-lg-8 input-wrap has-feedback">
                                                                        <asp:TextBox ID="txtDiaChi" required="required" ClientIDMode="Static" CssClass="form-control address" runat="server" placeholder="Địa chỉ giao hàng"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label for="telephone" class="col-lg-4 control-label visible-lg-block">Ghi chú</label>
                                                                    <div class="col-lg-8 input-wrap has-feedback">
                                                                        <asp:TextBox ID="txtNote" TextMode="MultiLine" Height="100px" CssClass="form-control address" runat="server" placeholder="Ghi chú"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

    <div id="choose_shipper_suplier" class="">
                        <h3 class="step-title">Chọn nhà vận chuyển</h3>
                        <div class="panel panel-default shipper_suplier ">
                            <div class="panel-body" style="margin-bottom:15px">
                                <div class="col-lg-12 col-md-13 col-sm-13 col-xs-13">
                                    <p class="">Giao hàng đến <span>
                                            <select name="den" id="giaoden" class="form-control">
                         <option value="0">--- Giao đến ---</option>
                         <%foreach (var province in Web.Asp.ObjectData.DataSource.ProvinceSourceCollection) { %>
                         <option value="<%=province.Id %>"><%=province.Name %></option>
                         <%} %>
                     </select>

                                        </span> bằng
                                        <span>
                                            <select name="ShippingId" id="ShippingId" class="form-control"><option value="2">Giao Hàng Tiết Kiệm</option><option value="1">Giao Hàng Nhanh</option></select>

                                        </span>
                                        với phí
                                        <span class="price_red" id="shipfee">25.000<span class="symbol">đ</span> </span>
                                    </p>
                                    <h6 class="mb-10">Thời gian giao hàng <span>2-3 ngày</span></h6>
                                </div>

                            </div>
                        </div>
                        <div class="shipping-notice mb-15">
                            <i class="fas fa-truck" style="color: #357DFD"></i>
                            <div class="pl-40">
                                Miễn phí vận chuyển cho đơn hàng có giá trị từ
                                <span class="price_red">25.000<span class="symbol">đ</span> </span> (giảm tối đa <span class="price_red">25.000<span class="symbol">đ</span> </span>).
                                <br /> Bạn được miễn phí vận chuyển 3 lần trong 30 ngày.
                            </div>

                        </div>
                    </div>
                                                </div>

<h3>3. Hình thức thanh toán</h3>
<div class="row-style-2">
                    <div id="choose_payment_method" class="">
                            <div class="panel panel-default">
                                <div class="panel-body row" style="padding-bottom:15px">
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                        <label for="tt" class="radio">
                                            <input type="radio" name="rdo" id="tt" class="hidden">
                                            <span class="label"></span>
                                            <span class="payment-type">
                                                <a class="btn btn-social btn-cod user-name-loginfb" title="THANH TOÁN KHI NHẬN HÀNG" href="javascript: void(0)" data-url="#">
                                                    <i class="fas fa-box-open"></i>
                                                    <span>THANH TOÁN KHI NHẬN HÀNG</span>
                                                </a>
                                            </span>
                                        </label>

                                    </div>
                                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                        <label for="online" class="radio">
                                            <input type="radio" name="rdo" id="online" class="hidden">
                                            <span class="label"></span>
                                            <span class="payment-type">
                                                <a class="btn btn-social btn-online-payment btn-facebook user-name-loginfb" title="THANH TOÁN TRỰC TUYẾN" href="javascript: void(0)" data-url="#">
                                                    <i class="fas fa-money-check"></i>
                                                    <span>THANH TOÁN TRỰC TUYẾN</span>
                                                </a>
                                            </span>
                                        </label>

                                    </div>
                                </div>
                            </div>
                        </div>

                    <!-- Sent As Gift -->
                   <%-- <div class="form-group row row-style-3 js-payment-sub">
                        <div class="col-lg-11 col-lg-offset-1 col-md-12 col-sm-12 col-xs-12">
                            <div class="panel panel-default payment-sub">
                                <div class="panel-body">
                                    <div class="form-group row row-style-4" id="tax_name">
                                        <label for="company" class="col-lg-3 control-label visible-lg-block">Tên công ty</label>
                                        <div class="col-lg-6">
                                            <input type="text" name="tax_company_name" placeholder="Ít nhất 2 từ" id="company" class="form-control infoOrderCompany hasNote" value="" data-error="Vui lòng nhập tên công ty">
                                            <span class="help-block"></span>
                                        </div>
                                        <span style="display: none; font-size: 11px; color: red;">Vui
                                            lòng chỉ nhập Tên công ty.<br>Không nhập lại
                                            Loại hình doanh nghiệp</span>
                                    </div>

                                    <div class="form-group row row-style-4">
                                        <label for="tax" class="col-lg-3 control-label visible-lg-block">Mã
                                            số thuế</label>
                                        <div class="col-lg-6">
                                            <input type="tel" name="tax_company_code" placeholder="Mã số thuế" id="tax" class="form-control infoOrderCompany" value="" data-error="Chỉ chấp nhận ký tự - và  ký tự số">
                                            <span class="help-block"></span>
                                        </div>
                                    </div>

                                    <div class="form-group row row-style-4 ">
                                        <label for="street" class="col-lg-3 control-label visible-lg-block">Địa
                                            chỉ</label>
                                        <div class="col-lg-6">
                                            <textarea class="form-control infoOrderCompany hasNote" name="tax_company_address" id="street" cols="30" rows="4" data-error="Vui lòng nhập địa chỉ" placeholder="Nhập địa chỉ công ty (bao gồm Phường/Xã, Quận/Huyện, Tỉnh/Thành phố nếu có)"></textarea>
                                            <span class="help-block"></span>
                                        </div>
                                        <span style="display: none; font-size: 11px; color: red;">Vui
                                            lòng chỉ nhập Số nhà, Tên đường,<br>Tên phường
                                            (nếu có).<br>Không nhập lại Quận/Huyện
                                            và<br>Tỉnh/Thành phố</span>
                                    </div>
                                </div>
                            </div>

                            <div class="tax-notice">
                                <div class="title">Lưu ý:</div>
                                <ul>
                                    <li>Hóa đơn cho các sản phẩm của nhà cung cấp khác
                                        Banmuagi
                                        sẽ được xuất sau 14 ngày kể từ thời điểm khách hàng
                                        nhận hàng</li>
                                    <li>
                                        Các mặt hàng sau đây trong đơn hàng của bạn không
                                        được hỗ trợ xuất hoá đơn
                                        <ul>
                                            <li>
                                                <b><span>1 x Đậu Phộng Rang Tỏi Ớt Lạc Lạc
                                                        (250g)</span></b> - Cung cấp bởi
                                                TADA FOODS </li>
                                        </ul>
                                    </li>
                                    <li>Trường hợp khách hàng không nhập thông tin hóa đơn,
                                        Tiki sẽ xuất hóa đơn theo thông tin mua hàng</li>
                                    <li>Tiki từ chối xử lý các yêu cầu phát sinh trong việc
                                        kê khai thuế đối với những hóa đơn từ 20 triệu đồng
                                        trở lên thanh toán bằng tiền mặt</li>
                                </ul>

                            </div>
                        </div>
                    </div>--%>
                </div>

<div class="panel-body">
                                                                        <div>
                                                                            <asp:Button ID="imbHoanTat" ClientIDMode="Static" runat="server" onclick="imbHoanTat_Click" CssClass="btn btn-block btn-default" Text="ĐẶT
                                                                                HÀNG" />
                                                                            <p class="note">(Xin vui lòng kiểm tra lại đơn hàng trước khi
                                                                                Đặt Mua)</p>
                                                                        </div>                                                            
</div>
<script type="text/javascript">
    function FillInfo() {
        $("#txtHoTen").val('');
              $("#txtDienThoai").val('');
              $("#txtMail").val('');
        $("#txtDiaChi").val('');
    }
    $(document).ready(function () {
        GetCustomer();
    });

    function GetCustomer() {
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>JsonPost.aspx/GetCustomer",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ companyId: <%=Config.ID%>, phone: '<%=this.UserContext.Phone%>' }),
            success: function (data) {
                if (data.d != null) {
                    $('#hasPoint').html(data.d.Point);
                    var amount = data.d.Point * data.d.TranferPrice;
                    $('#pricePoint').html(amount);
                    $('#PointTranfer').val(data.d.TranferPrice);
                    $('#Point').val(data.d.Point);
                }
            }
        });
    }
</script>  
<%
        txtHoTen.Text = this.UserContext.FullName;
        txtMail.Text = this.UserContext.Email;
        txtDienThoai.Text = this.UserContext.Phone;
        txtDiaChi.Text = this.UserContext.Address;
    } %>
<style>
    #imbHoanTat{    background: #ef3959;
    height: 44px;
    color: white;}
</style>

<input type="hidden" name="Point" id="Point"/>
<input type="hidden" name="PointTranfer" id="PointTranfer" value="0"/>
<input type="hidden" name="PointFee" id="PointFee" value="0"/>
<input type="hidden" name="VoucherFee" id="VoucherFee" value="0"/>
<input type="hidden" name="DeliveryFee" id="DeliveryFee" value="0"/>

<script type="text/javascript">
    $('#online').change(function () {
        if ($(this).is(":checked") == true) {
            $("#thanhtoanonline").show();
        }
    });
    $('#tt').change(function () {
        if ($(this).is(":checked") == true) {
            $("#thanhtoanonline").hide();
        }
    });
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

                        var km = parseInt($('#VoucherFee').val(), 10);
                        var pt = parseInt($('#PointFee').val(), 10);
                        var total = data.d.fee + <%=Carts.Sum(e => e.TotalCost)  %> - km - pt;
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
            data: JSON.stringify({ companyId: <%=Config.ID%>, code: voucherCode }),
        success: function (data) {
            if (data.d == null) alert('Không tồn tại voucher [' + voucherCode + ']');
            else if (!data.d.Code) alert('Voucher [' + voucherCode + '] đã hết hạn sử dụng.');
            else if (data.d.Quantity == 0) alert('Voucher [' + voucherCode + '] đã hết.');
            else {
                var amount = <%=Carts.Sum(e => e.TotalCost)  %> + parseInt($('#DeliveryFee').val(), 10) - parseInt($('#PointFee').val(), 10);
                var km = 0;
                if (data.d.IsPercent) km = <%=Carts.Sum(e => e.TotalCost)  %> * data.d.Value / 100;
                else km = data.d.Value;
                amount -= km;

                $('#VoucherFee').val(km);
                var strKM = (km + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
                strKM = strKM.substring(0, strKM.length - 1);
                $('#UseVoucher').html('<b>Voucher [' + data.d.Code + '] :</b><span class="price_red">-' + strKM + '</span>');
                $('#InputVoucher').remove();

                var strTotal = (amount + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
                strTotal = strTotal.substring(0, strTotal.length - 1);
                $('#totalfee').html(strTotal);
            }
        }
        });
    }

    function AddPoint() {
        var point = parseInt($('#PointUse').val(), 10);
        var pointUser = parseInt($('#Point').val(), 10);
        var pointTranfer = parseFloat($('#PointTranfer').val());
        if (point > 0) {
            if (point > pointUser) {
                alert('Bạn không đủ điểm');
            }
            else {
                var pointprice = point * pointTranfer;
                $('#PointFee').val(strTotal);

                var strPoint = (pointprice + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
                strPoint = strPoint.substring(0, strPoint.length - 1);
                $('#UsePoint').html('<b>Sử dụng [' + point + '] điểm :</b><span class="price_red">-' + strPoint + '</span>');
                $('#InputPoint').remove();

                var amount = <%=Carts.Sum(e => e.TotalCost)  %> + parseInt($('#DeliveryFee').val(), 10) - parseInt($('#VoucherFee').val(), 10) - pointprice;
                var strTotal = (amount + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
                strTotal = strTotal.substring(0, strTotal.length - 1);
                $('#totalfee').html(strTotal);
            }
        }
    }
</script>
