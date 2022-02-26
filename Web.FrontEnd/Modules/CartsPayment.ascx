<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CartsPayment.ascx.cs" Inherits="Web.FrontEnd.Modules.CartsPayment" %>
 
<asp:Literal ID="lblMsg" runat="server"></asp:Literal>
<div id="hangtrongGio" style="text-align:center">
    <asp:Repeater ID="rptGioHang" runat="server">
        <ItemTemplate>
            <%#Eval("ProductID")%>
            <%#Eval("ProductName")%>
            <%#Eval("Image") %>
            <%#Eval("Quantity")%>
            <%#string.Format("{0:0,0}", Convert.ToDecimal(Eval("UnitPrice")))%>
            <%#string.Format("{0:0,0}", Convert.ToDecimal(Eval("TotalCost")))%>
        </ItemTemplate>
    </asp:Repeater>
    Tổng thành tiền: <asp:Label ID="lblTongTien" runat="server"></asp:Label>
</div>

    Thông tin đơn hàng: 
<input type="text" name="infoValue1" placeholder="Họ và tên"/>
<input type="text" name="infoValue2" placeholder="Số Điện thoại"/>
<input type="text" name="infoValue3" placeholder="Địa chỉ"/>
<input type="hidden" name="infoLable" value="Họ và tên|Số Điện thoại|Địa chỉ"/>
Họ tên:<asp:TextBox ID="txtHoTen" runat="server"></asp:TextBox>
Địa chỉ:<asp:TextBox ID="txtDiaChi" runat="server"></asp:TextBox>
Điện thoại:<asp:TextBox ID="txtDienThoai" runat="server"></asp:TextBox>
Note:<asp:TextBox ID="txtNote" runat="server"></asp:TextBox>
Email:<asp:TextBox ID="txtMail" runat="server"></asp:TextBox>
<input type="hidden" name="DeliveryFee"/>
<input type="hidden" name="ShippingId"/>
<input type="hidden" name="Voucher"/>
<input type="hidden" name="Point" id="Point"/>

<asp:Button ID="imbHoanTat" runat="server" OnClick="imbHoanTat_Click"/>

<% var productIds = this.ProductPrices.Select(e => e.ProductId);%>
<%foreach(var id in productIds){%>
    <% var prices = this.ProductPrices.Where(e => e.ProductId == id);%>
    var arrQuatity<%=id %> = [<%= string.Join(",",prices.Select(e => e.Quantity))%>]
    var arrPrice<%=id %> = [<%= string.Join(",",prices.Select(e => e.Price))%>] 
<%}%>