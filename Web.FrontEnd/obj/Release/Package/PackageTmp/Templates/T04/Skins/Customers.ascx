<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Customers.ascx.cs" Inherits="Web.FrontEnd.Modules.Customers" %>
<%@ Import Namespace="VIT.Library"%>

<div class="row" style="padding:10px 0px 10px 0px">
    <div class="col-md-8 col-md-offset-2">
        <div class="input-group custom-search-form">






            <asp:TextBox runat="server" ID="txtPhone" TextMode="Number" ClientIDMode="Static" CssClass="form-control" placeholder="Nhập số điện thoại" Height="38" Width="100%"></asp:TextBox>
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


<%if(dto.Count > 0 && !string.IsNullOrEmpty(txtPhone.Text)) { %>
<div class="col-md-12">
        <div class="panel panel-info"> 
            <div class="panel-heading"> 
                <h3 class="panel-title">Danh sách đơn hàng</h3> 
            </div> 
            <div class="panel-body" style="font-size:12px">
                <table class="table table-striped"> 
                    <thead> <tr> <th>#</th> <th>Tiền hàng</th> <th>Phí vận chuyển</th> <th>Ngày tạo</th> <th>Trạng thái</th></tr> </thead> 
                    <tbody> 
                <%foreach (var order in dto)
                            {%>
                        <tr> 
                            <th><a href="<%=HREF.LinkComponent("Product", "Cart", SettingsManager.Constants.SendOrder + "/" + order.ID) %>"><%=order.ID %></a></th>
                            <td><%=string.Format("{0: 0,0}", order.Due) %></td>
                            <td><%=string.Format("{0: 0,0}", order.ServiceFee) %></td>
                            <td><%=string.Format("{0: dd/MM/yyyy}", order.CreateDate) %></td>
                            <td><%=order.IsReturn ? "Khách không nhận" : order.IsRecieved ? "Khách đã nhận" : order.IsSend ? "Đã gửi hàng" : order.IsConfirm ? "Đã xác nhận" : "Đơn hàng mới"%></td>
                        </tr>  
                    <%} %>
                        </tbody> 
                </table>
            </div> 
        </div>
    </div>
<%} else if(dto.Count == 0 && !string.IsNullOrEmpty(txtPhone.Text)){%>
<div class="alert alert-danger" role="alert" style="margin:20px"> Không tìm thấy khách hàng <strong><%=txtPhone.Text %></strong></div>
<%} %>