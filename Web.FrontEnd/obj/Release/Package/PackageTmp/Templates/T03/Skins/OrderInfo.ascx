<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/OrderInfo.ascx.cs" Inherits="Web.FrontEnd.Modules.OrderInfo" %>
 <%@ Import Namespace="Web.Asp.Provider"%>

<div class="row" style="padding:10px 0px 10px 0px">
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
                <div><label>Thanh toán: </label> <%=dto.IsPaid == true ? "Đã thanh toán" : dto.Status < 2 ? "<a href='"+HREF.LinkComponent( "Payment", SettingsManager.Constants.SendOrder + "/" + dto.Id)+"' style='color:red'>Thanh toán khi nhận hàng</a>" : "Thanh toán khi nhận hàng"%></div> 
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
                <div><label>Mã vận chuyển: </label> <%=dto.ShippingCode %></div>
                <div><label>Tổng tiền hàng: </label> <%=string.Format("{0: 0,0}", dto.TotalDue) %></div>
                <%if (Data == null) {%>
                    <%if (dto.DeliveryFee > dto.CustomerPayDelivery) { %>
                    <div><label>Phí vận chuyển thực tế: </label> <%=string.Format("{0: 0,0}", dto.DeliveryFee) %></div>
                    <div><label>Phí vận chuyển khách trả: </label> <%=string.Format("{0: 0,0}", dto.CustomerPayDelivery) %> (shop hỗ trợ <%=string.Format("{0: 0,0}", dto.DeliveryFee - dto.CustomerPayDelivery) %>)</div>
                    <%} else { %>
                    <div><label>Phí vận chuyển: </label> <%=string.Format("{0: 0,0}", dto.CustomerPayDelivery) %></div>
                    <%} %>
                <%} else { %>
                
                <div><label>Dịch vụ giao hàng: </label> <%=dto.ShippingId == 1 ? "Giao hàng nhanh" : "Giao hàng tiết kiệm"%></div>
                    <%if (Data.ServiceCost > dto.CustomerPayDelivery) { %>
                    <div><label>Phí vận chuyển thực tế: </label> <%=string.Format("{0: 0,0}", Data.ServiceCost) %></div>
                    <div><label>Phí vận chuyển khách trả: </label> <%=string.Format("{0: 0,0}", dto.CustomerPayDelivery) %> (shop hỗ trợ <%=string.Format("{0: 0,0}", Data.ServiceCost - dto.CustomerPayDelivery) %>)</div>
                    <%} else { %>
                    <div><label>Phí vận chuyển: </label> <%=string.Format("{0: 0,0}", dto.CustomerPayDelivery) %></div>
                    <%} %>
                <div><label>Trạng thái: </label> <%=Data.Status == "Finish" ? "Hoàn tất" : Data.Status%></div>
                <div><label>Ngày nhận: </label><%= string.Format("{0: dd/MM/yyyy hh:mm:ss}", Data.DeliveryTime) %> </div>
                <div><label>Ghi chú: </label><%=Data.Message%></div>
                <%} %>
            </div> 
        </div>
    </div>
<%if ( dto.IsPaid == false && dto.Status < 2)
                    { %>
<div class="row" style="padding:10px 0px 10px 0px">
    <div class="col-md-6 col-md-offset-3" style="text-align:center">
        <a class="btn btn-primary" href='<%=HREF.LinkComponent( "Payment", SettingsManager.Constants.SendOrder + "/" + dto.Id)%>'>Thanh toán ngay</a>
</div>
    </div>
<%} %>
<div class="col-md-12">
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