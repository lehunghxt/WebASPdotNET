<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/GHNGetOrderInfo.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.GHNGetOrderInfo" %>
<%@ Register TagPrefix="VIT" Namespace="VIT.Web.Controls" Assembly="VIT.Web" %>
<%@ Import Namespace="VIT.Library"%>

<div class="row" style="padding:10px 0px 10px 0px">
    <div class="col-md-8 col-md-offset-2">
        <div class="input-group custom-search-form">






            <asp:TextBox runat="server" ID="txtOrderId" TextMode="Number" ClientIDMode="Static" CssClass="form-control" placeholder="Nhập mã đơn hàng" Height="38" Width="100%"></asp:TextBox>
            <asp:LinkButton ID="btnSearch" runat="server" Onclick="btnSearch_Click" CssClass="input-group-btn">
                        <button class="btn btn-default" type="button" style="height:38px;background:<%=this.Skin.BackgroundColor%>;border: 1px solid <%=this.Skin.BackgroundColor%>;color:<%=this.Skin.FontColor%>">
                            <i class="fa fa-search"></i> Tìm
                        </button>
                <%--<%if (this.UserContext == null)
                  {%>
                <a href="<%=HREF.LinkComponent("Member", "Login")%>" style="margin: 8px;position: absolute;width: 100px;text-align: right;" title="Đăng nhập để xem tất cả nguồn hàng tốt nhất"><i class="glyphicon glyphicon-log-in"></i> Đăng nhập</a>
                <%} else {%>
                <a href="<%=HREF.LinkComponent("Member", "Login", "logout/1")%>" style="margin: 8px;position: absolute;width: 100px;text-align: right;"><i class="glyphicon glyphicon-log-out"></i> Thoát</a>
                <%}%>--%>
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
                <div><label>Mã đơn hàng: </label> <%=dto.ID %></div>
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
                <div><label>Trạng thái: </label> <%=this.dto.IsReturn ? "Khách không nhận" : this.dto.IsRecieved ? "Khách đã nhận" : this.dto.IsSend ? "Đã gửi hàng" : this.dto.IsConfirm ? "Đã xác nhận" : "Đơn hàng mới"%></div>
                <%if (dto.Method.ToLower().Contains("ngân hàng"))
                    { %>
                <div><label>Thanh toán: </label> <%=dto.IsPayment == true ? "Đã thanh toán" : "<a href='"+HREF.LinkComponent("Product", "Payment", SettingsManager.Constants.SendOrder + "/" + dto.ID)+"' style='color:red'>Chờ thanh toán</a>"%></div>
                <%} %>
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
                <div><label>Tổng tiền hàng: </label> <%=string.Format("{0: 0,0}", dto.Due) %></div>
                <div><label>Phí vận chuyển: </label> <%=string.Format("{0: 0,0}", dto.ServiceFee) %></div>
                <%} else { %>
                <div><label>Mã vận chuyển: </label> <%=Data.OrderCode %></div>
                <div><label>COD (thu hộ): </label> <%=string.Format("{0: 0,0}", Data.CoDAmount) %></div>
                <div><label>Phí vận chuyển: </label> <%=string.Format("{0: 0,0}", Data.TotalServiceCost) %></div>
                <div><label>Trạng thái: </label> <%=Data.CurrentStatus == "Finish" ? "Hoàn tất" : Data.CurrentStatus%></div>
                <div><label>Dịch vụ: </label><%=Data.ServiceName%></div>
                <div><label>Dự kiến: </label><%=Data.OriginServiceName%></div>
                <%} %>
            </div> 
        </div>
    </div>
<%if (dto.Method.ToLower().Contains("ngân hàng") && dto.IsPayment == false)
                    { %>
<div class="row" style="padding:10px 0px 10px 0px">
    <div class="col-md-6 col-md-offset-3" style="text-align:center">
        <a class="btn btn-primary" href='<%=HREF.LinkComponent("Product", "Payment", SettingsManager.Constants.SendOrder + "/" + dto.ID)%>'>Thanh toán ngay</a>
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
                <%if (!string.IsNullOrEmpty(dto.Products))
                    {
                        var products = dto.Products.Split(',');
                        if (products.Length > 0)
                        {
                            foreach (var product in products)
                            {
                                var arr = product.Split(':');
                                var info = arr[1].Split('-');
                    %>
                
                        <tr> 
                            <th scope="row"> <code><%=arr[0] %></code> </th> 
                            <td><%=info[2] %></td>
                            <td><%=info[1] %></td>
                        </tr>  
                    <%}
        }
    } %>
                        </tbody> 
                </table>
            </div> 
        </div>
    </div>
<%} else if(dto == null && !string.IsNullOrEmpty(txtOrderId.Text)){%>
<div class="alert alert-danger" role="alert" style="margin:20px"> Không tìm thấy đơn hàng <strong><%=txtOrderId.Text %></strong></div>
<%} %>