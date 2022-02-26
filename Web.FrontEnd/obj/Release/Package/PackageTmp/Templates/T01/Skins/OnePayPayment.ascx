<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/OnePayPayment.ascx.cs" Inherits="Web.FrontEnd.Modules.OnePayPayment" %>

<div class="row" style="padding:10px 0px 10px 0px">
    <div class="col-md-8 col-md-offset-2">
        <div class="input-group custom-search-form">






            <asp:TextBox runat="server" ID="txtOrderId" TextMode="Number" ClientIDMode="Static" CssClass="form-control" placeholder="Nhập mã đơn hàng" Height="38" Width="100%"></asp:TextBox>
            <asp:LinkButton ID="btnSearch" runat="server" Onclick="btnSearch_Click" CssClass="input-group-btn">
                        <button class="btn btn-default" type="button" style="height:38px;">
                            <i class="fa fa-search"></i> Tìm
                        </button>
</asp:LinkButton>
              </div>     
        </div>
    </div>

<%if(dto!=null){ %>
 <div class="col-md-12">
<div class="bs-callout bs-callout-info" id="callout-help-text-accessibility">
    <h4>Thông tin đơn hàng</h4>
    <div class="row">
        <div class="col-md-4">
            <div><label>Mã đơn hàng: </label> <%=dto.Id %></div>
            <div><label>Tạo ngày: </label> <%=string.Format("{0: dd/MM/yyyy hh:mm:ss}", dto.CreateDate) %></div>
            <div><label>Tiền hàng: </label> <%=string.Format("{0:0,0}", dto.TotalDue) %></div>
             <div><label>Trạng thái: </label> <%=dto.IsPaid ? "Đã thanh toán" : "Chờ thanh toán" %></div>
            <div><label>Khách hàng: </label> <%=dto.CustomerName %></div>
            <div><label>Điện thoại: </label> <%=dto.CustomerPhone.Length > 5 ? "*****" + dto.CustomerPhone.Substring(5) : dto.CustomerPhone%></div>
            <div><label>Địa chỉ: </label> <%=dto.CustomerAddress %></div>
        </div>
        <div class="col-md-8">
            <table class="table table-bordered table-striped"> 
                    <thead> <tr> <th>Mã</th> <th>Tên sản phẩm</th> <th>Số lượng</th></tr> </thead> 
                    <tbody> 
                <%foreach (var product in Products)
                            {
                    %>
                
                        <tr> 
                            <td scope="row"> <code><%=product.ProductCode %></code> </td> 
                            <td><%=product.ProductName %></td>
                            <td><%=product.Quantity %></td>
                        </tr>  
                    <%}%>
                        </tbody> 
                </table>
        </div>
    </div>
</div>

     <%if (trans != null)
         {
             var status = string.Empty;
             if (trans.OrderType == "QT") status = Web.FrontEnd.DataSource.OnePayVisaStatusCollection.Where(e => e.Id == trans.ResponseCode).Select(e => e.Name).FirstOrDefault();
             else if (trans.OrderType == "ND") status = Web.FrontEnd.DataSource.OnePayATMStatusCollection.Where(e => e.Id == trans.ResponseCode).Select(e => e.Name).FirstOrDefault();
             %>
     <div class="bs-callout bs-callout-info">
    <h4>Thông tin thanh toán</h4>
    <div>
        <div><label>Thời gian bắt đầu giao dịch: </label> <%=string.Format("{0:dd/MM/yyyy}", trans.RequestTime) %></div>
        <div><label>Thời gian kết thúc giao dịch: </label> <%=string.Format("{0:dd/MM/yyyy}", trans.ResponseTime) %></div>
        <div><label>Mô tả kết quả: </label> <%=trans.ResponseMessage %></div>
        <div><label>Mã giao dịch: </label> <%=trans.Trans_Ref %></div>
        <div><label>Trạng thái: </label> <%=status %></div>
    </div>
</div>
     <%} %>

     <%if(!dto.IsPaid) { %>
<div class="bs-callout bs-callout-info" style="margin-bottom:30px">
    <h4>Chọn phương thức thanh toán</h4>
    <div>
        <input type="button" onclick="Action('ATM');" value="Internet Banking - thẻ thanh toán nội địa" class="btn btn-primary" />
        <input type="button" onclick="Action('VISA');" value="Thẻ Visa - Master - thẻ thanh toán quốc tế" class="btn btn-primary" /><br />
    </div>
</div>
<input type="hidden" id="action" name="action" value=""/>
<script type="text/javascript">
    function Action(id)
    {
        $("#action").val(id);
        $("form").submit();   
    }
</script>
<%} %>
     </div>
<%} %>