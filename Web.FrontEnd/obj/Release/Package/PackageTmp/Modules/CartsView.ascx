<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CartsView.ascx.cs" Inherits="Web.FrontEnd.Modules.CartsView" %>
 
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

<% var productIds = this.ProductPrices.Select(e => e.ProductId);%>
<%foreach(var id in productIds){%>
    <% var prices = this.ProductPrices.Where(e => e.ProductId == id);%>
    var arrQuatity<%=id %> = [<%= string.Join(",",prices.Select(e => e.Quantity))%>]
    var arrPrice<%=id %> = [<%= string.Join(",",prices.Select(e => e.Price))%>] 
<%}%>