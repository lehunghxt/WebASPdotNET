<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/OrderInfo.ascx.cs" Inherits="Web.FrontEnd.Modules.OrderInfo" %>
 <%@ Import Namespace="Web.Asp.Provider"%>

<%if(dto!= null && !string.IsNullOrEmpty(txtOrderId.Text)) { %>
<div class="col-full">
    <div class="row">
        <div id="primary" class="content-area">
            <main id="main" class="site-main">
                <section class="section-landscape-products-carousel">
                    <div class="container mt-20">
                        <div class="row">
                            <div class="col-md-8 offset-md-2 col-12 bt-1 bl-1 br-1 pt-10 pb-10 border-gray">
                                <div class="confirm-header">
                                    <div class="confirm-title">
                                        ĐẶT HÀNG THÀNH CÔNG






                                    </div>
                                    <span class="lnr lnr-checkmark-circle"></span>
                                    <p class="quote">Cảm ơn bạn đã lựa chọn mua hàng tại <a href="http://<%=HREF.Domain %>"><%=HREF.Domain %></a></p>
                                </div>

                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-8 offset-md-2 col-12">
                                <div class="row">
                                    <div class="col-md-6 bt-1 bl-1 br-1 pt-10 pb-10 border-gray">
                                        <div class="box recipt-info">
                                            <p>Mã số đơn hàng: <span class="reciept-code ml-20"><%=dto.Id %></span></p>
                                            <p>Giá trị đơn hàng: <span class="price_red">360.000<span class="symbol">đ</span></span></p>
                                            <p>Quản lý: <%if (dto.CustomerId > 0)
                                                            { %> <a class="redirect-link" href="<%=HREF.LinkComponent("Profile") %>">Thông tin tài khoản</a><%} %></p>
                                        </div>

                                    </div>
                                    <div class="col-md-6 bt-1 br-1 pt-10 pb-10 border-gray shipping-addr">
                                        <div class="box">
                                            <p class="heading-title">Địa chỉ giao hàng</p>
                                            <p class="pl-10">Tên người mua:<span id="name"><%=dto.CustomerName %></span></p>
                                            <p class="pl-10">Số điện thoại: <span id="phone"><%=dto.CustomerPhone%></span></p>
                                            <p class="pl-10">Địa chỉ: <span id="address"><%=dto.CustomerAddress %></span></p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-8 offset-md-2 col-12 bt-1 br-1 br-1 bl-1 pt-10 pb-10 border-gray">
                                <p>
                                    <span class="note-title">Phương thức thanh toán:</span>
                                    <span class="note-content"><%=dto.IsPaid == true ? "Đã thanh toán" : "<a href='"+HREF.LinkComponent( "Payment", SettingsManager.Constants.SendOrder + "/" + dto.Id)+"' style='color:red'>Thanh toán khi nhận hàng</a>"%></span>
                                </p>
                                <p>
                                    <span class="note-title">Ghi chú:</span>
                                    <span class="note-content"><%=dto.Note %></span>
                                </p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8 offset-md-2 col-12 bt-1 bb-1 br-1 bl-1 pt-10 pb-10 border-gray">
                                <div class="shipping-info">
                                    <p>
                                        <i class="fas fa-shipping-fast"></i>
                                        Thời gian giao hàng dự kiến:
                                        <span class="shipping-time"><%=Data.DeliveryTime.Year == 1 ? "3 - 5 ngày" : string.Format("{0: dd/MM/yyyy}",Data.DeliveryTime) %></span>
                                    </p>
                                    <p>Thông tin về đơn hàng đã được gửi đến mail <%=dto.CustomerEmail %></p>
                                </div>
                                <div class="deal-navi row ">
                                    <div class="col-md-6">
                                        <a class="btn btn-info" href="<%=HREF.LinkComponent("Products") %>">Tiếp tục mua sắm</a>
                                    </div>
                                    <div class="col-md-6">
                                        <a class="btn btn-info  ml-40" href="<%=HREF.LinkComponent("Groupons") %>">Xem deal <span class="red-deal">Hot</span></a>
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
<%} %> 
<div class="row" style="padding:10px 0px 10px 0px;display:none">
    <div class="col-md-8 col-md-offset-2">
        <div class="input-group custom-search-form">
            <asp:TextBox runat="server" ID="txtOrderId" TextMode="Number" ClientIDMode="Static" CssClass="form-control" placeholder="Nhập mã đơn hàng" Height="38" Width="100%"></asp:TextBox>
            <asp:LinkButton ID="btnSearch" runat="server" Onclick="btnSearch_Click" CssClass="input-group-btn">
                        <button class="btn btn-default" type="button" style="height:38px; ">
                            <i class="fa fa-search"></i> Tìm
                        </button> 
</asp:LinkButton>
              </div>     
        </div>
    </div>

 <%if(dto!= null && !string.IsNullOrEmpty(txtOrderId.Text)) { %>
<div class="row" style="padding:10px 0px 10px 0px;display:none">
    <div class="col-md-4">
        <div class="panel panel-info"> 
            <div class="panel-heading"> 
                <h3 class="panel-title">Thông tin đơn hàng</h3> 
            </div> 
            <div class="panel-body" style="font-size:12px">
                <div><label>Mã đơn hàng: </label> <%=dto.Id %></div>
                <div class="divider"></div>
                <div><label>Khách hàng: </label> <%=dto.CustomerName %></div>
                <div><label>Điện thoại: </label> <%=dto.CustomerPhone.Length > 5 ? "*****" + dto.CustomerPhone.Substring(5) : dto.CustomerPhone%></div>
                <div><label>Địa chỉ: </label> <%=dto.CustomerAddress %></div>
                <div><label>Ghi chú: </label> <%=dto.Note %></div>
            </div> 
        </div>
    </div>
    <div class="col-md-4">
        <div class="panel panel-info"> 
            <div class="panel-heading"> 
                <h3 class="panel-title">Trạng thái đơn hàng</h3> 
            </div> 
            <div class="panel-body" style="font-size:12px">
                <div><label>Trạng thái: </label> <%=this.dto.Status == 4 ? "Khách không nhận" : this.dto.Status == 3 ? "Khách đã nhận" : this.dto.Status == 2 ? "Đã gửi hàng" : this.dto.Status == 1 ? "Đã xác nhận" : "Đơn hàng mới"%></div> 
                <div><label>Thanh toán: </label> <%=dto.IsPaid == true ? "Đã thanh toán" : "<a href='"+HREF.LinkComponent( "Payment", SettingsManager.Constants.SendOrder + "/" + dto.Id)+"' style='color:red'>Thanh toán khi nhận hàng</a>"%></div> 
                <div><label>Cập nhật: </label> <%=string.Format("{0: dd/MM/yyyy hh:mm:ss}", dto.LastUpdate) %></div>
                <div><label>Ngày tạo: </label> <%= string.Format("{0: dd/MM/yyyy hh:mm:ss}", dto.CreateDate) %></div>
                <div><label>Xác nhận: </label> <%= dto.ConfirmDate != null ? string.Format("{0: dd/MM/yyyy hh:mm:ss}", dto.ConfirmDate) : "" %></div>
                <div><label>Ngày gửi: </label> <%= dto.SendDate != null ? string.Format("{0: dd/MM/yyyy hh:mm:ss}", dto.SendDate) : "" %></div>
            </div> 
        </div>
    </div>
    <div class="col-md-4">
        <div class="panel panel-info"> 
            <div class="panel-heading"> 
                <h3 class="panel-title">Giao hàng</h3> 
            </div> 
            <div class="panel-body" style="font-size:12px">
                <%if (Data == null) {%>
                <div><label>Mã vận chuyển: </label> <%=dto.ShippingCode %></div>
                <div><label>Tổng tiền hàng: </label> <%=string.Format("{0: 0,0}", dto.TotalDue) %></div>
                <div><label>Phí vận chuyển: </label> <%=string.Format("{0: 0,0}", dto.DeliveryFee) %></div>
                <%} else { %>
                <div><label>Phí vận chuyển: </label> <%=string.Format("{0: 0,0}", Data.ServiceCost) %></div>
                <div><label>Trạng thái: </label> <%=Data.Status == "Finish" ? "Hoàn tất" : Data.Status%></div>
                <div><label>Dự kiến: </label><%=Data.DeliveryTime%></div>
                <div><label>Dịch vụ: </label><%=Data.Message%></div>
                <%} %>
            </div> 
        </div>
    </div>
    </div>
<%if ( dto.IsPaid == false)
                    { %>
<div class="row" style="padding:10px 0px 10px 0px;display:none">
    <div class="col-md-6 col-md-offset-3" style="text-align:center">
        <a class="btn btn-primary" href='<%=HREF.LinkComponent( "Payment", SettingsManager.Constants.SendOrder + "/" + dto.Id)%>'>Thanh toán ngay</a>
</div>
    </div>
<%} %>
<div class="col-md-12" style="display:none">
        <div class="panel panel-info"> 
            <div class="panel-heading"> 
                <h3 class="panel-title">Sản phẩm</h3> 
            </div> 
            <div class="panel-body" style="font-size:12px">
                <table class="table table-bordered table-striped"> 
                    <thead> <tr> <th>Mã</th> <th>Tên sản phẩm</th> <th>Số lượng</th></tr> </thead> 
                    <tbody> 
                <% foreach (var product in Products)
                            { 
                    %>
                
                        <tr> 
                            <td scope="row"> <code><%=product.ProductCode %></code> </td> 
                            <td><%=product.ProductName %></td>
                            <td><%=product.Quantity %></td>
                        </tr>  
                    <%} %>
                        </tbody> 
                </table>
            </div> 
        </div>
    </div>
<%} else if(dto == null && !string.IsNullOrEmpty(txtOrderId.Text)){%>
<div class="alert alert-danger" role="alert" style="margin:20px"> Không tìm thấy đơn hàng <strong><%=txtOrderId.Text %></strong></div>
<%} %> 