<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/CartsPayment.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.CartsPayment" %>







<asp:Literal ID="lblMsg" runat="server"></asp:Literal>

<div id="hangtrongGio" style="text-align:center">
<p><style type="text/css">
.tbCart{background:#f7f7f7; margin:10px 0 0 10px;}
.tbCart td input[type=text]{width:30px; text-align:center;}
.tbCart td{background:#FAF9E5}
.tbCart td.left{text-align:left}
.tbCart td.right{text-align:right}
.tbCart tr.trhead td{text-align:center; background:#A0B222; color:#fff; font-weight:bold}	</style></p>
    <script src="../js/KiemTra.js" type="text/javascript"></script>
<table cellpadding="5" cellspacing="1" class="tbCart" width="672px">
    <tr class='trhead'>
        <td>#</td>
        <td>Mã</td>
        <td>Tên</td>
        <td>Hình</td>
        <td>Số lượng</td>
        <td>Đơn giá</td>
        <td>Tổng tiền</td>
    </tr>
    <asp:Repeater ID="rptGioHang" runat="server">
        <ItemTemplate>
            <tr id="row<%#Container.ItemIndex%2 %>">
                <td><%#Container.ItemIndex+1%></td>
                <td><%#Eval("ProductID")%></td>
                <td class='left'><%#Eval("ProductName")%></td>
                <td><img src="<%#Eval("Image") %>" width="40px"/></td>
                <td><input type="text" onKeyPress = "return keypress(event)" id="sl<%#Eval("ProductID")%>" value="<%#Eval("Quantity")%>" maxlength='3' onblur="TinhTongTien('<%#Eval("ProductID")%>')"/></td>
                <td class='right'><%#string.Format("{0:0,0}", Convert.ToDecimal(Eval("UnitPrice")))%><input type="hidden" id='DGia<%#Eval("ProductID")%>' value="<%#Eval("UnitPrice")%>" /></td>
                <td class='right'><label id="lbl<%#Eval("ProductID")%>"><%#string.Format("{0:0,0}", Convert.ToDecimal(Eval("TotalCost")))%> <input type="hidden" id='tongtiencot<%#Eval("ProductID")%>'  value="<%#Eval("TotalCost")%>" /> </label></td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>
<input type="hidden" id="tongtienHiden" name="tongtienHiden" value="<%=tongtienHiden.Replace(".","")%>"/>
<p style="text-align:right; color:Red;" >Tổng thành tiền: <label id="tongthanhtien"><%=tongtienHiden%></label> đồng.</p>
</div>

<table width="672px" cellpadding="10" cellspacing="10" style="background:#FAF9E5; margin:30px 0 0 10px;">
    <tr>
        <td colspan = "2" style="font-weight:bold">Thông tin đơn hàng</td>
    </tr>
    <tr>
        <td>Họ tên:</td>
        <td><asp:TextBox ID="txtHoTen" runat="server" CssClass="tab_input"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Địa chỉ:</td>
        <td><asp:TextBox ID="txtDiaChi" runat="server" CssClass="tab_input"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Điện thoại:</td>
        <td><asp:TextBox ID="txtDienThoai" runat="server" CssClass="tab_input"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Email:</td>
        <td><asp:TextBox ID="txtMail" runat="server" CssClass="tab_input"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Ghi chú:</td>
        <td><asp:TextBox ID="txtNote" TextMode="MultiLine" runat="server" CssClass="tab_input" Height="75"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Nhận hàng:</td>
        <td colspan="2">
            <asp:RadioButtonList ID="rdbPhuongThuc" runat="server" Width="350%">
                <asp:ListItem Selected="True">Nhận h&#224;ng trực tiếp từ trung t&#226;m</asp:ListItem>
                <asp:ListItem>Giao h&#224;ng tại nh&#224;</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td>Thanh toán:</td>
        <td>
            <asp:RadioButtonList ID="rdbHinhThuc" runat="server" Width="350%">
                <asp:ListItem Selected="True">Thanh toán bằng tiền mặt</asp:ListItem>
                <asp:ListItem>Chuyển khoản qua ngân hàng</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td colspan="2"><asp:Button ID="imbHoanTat" runat="server" OnClick="imbHoanTat_Click" CssClass="btn_eve" Text="Gửi đơn đặt hàng "/></td>
    </tr>
</table>

<script language="javascript" type="text/javascript">
    function TinhTongTien(id) {
        var tongtienold = $("#tongtienHiden").val();
        var tongtiencot = $("#tongtiencot" + id).val();
        var sl = $("#sl" + id).val();
        var dg = $("#DGia" + id).val();
        var t = sl * dg;

        var newt = (t + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
        newt = newt.substring(0, newt.length - 1);
        document.getElementById("lbl" + id).innerHTML = "<label id='lbl" + id + "'>" + newt + " <input type='hidden' id='tongtiencot" + id + "' value=" + newt.replace('.', '') + " /> </label>";
        var tongtien = (tongtienold - tongtiencot) + t;
        tongtien = (tongtien + '.').replace(/\d(?=(\d{3})+\.)/g, '$&.');
        tongtien = tongtien.substring(0, tongtien.length - 1);
        document.getElementById("tongthanhtien").innerHTML = "<label id='tongthanhtien'>" + tongtien + " </label>";
        $("#tongtienHiden").val(tongtien.replace('.', ''));

        // Send the rating information somewhere using Ajax or something like that.
        $.ajax({
            type: "POST",
            url: "<%=HREF.BaseUrl %>Components/Page/JsonPost.aspx/EditCarts",
            data: JSON.stringify({ productId: id, quanlity: sl }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data != "") {

                }
            }
        });
    }
</script>
    
    
    
    
    
    

    

