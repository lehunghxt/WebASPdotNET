<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberOrders.ascx.cs" Inherits="Web.FrontEnd.Modules.MemberOrders" %>

<input type="hidden" value ="0" id="OrderCancleId" name="OrderCancleId" />
<textarea name="CustomerNote" id="CustomerNote" rows="3" placeholder="Vui lòng cho biết lý do"></textarea>
<asp:Button ID="btnHuy" runat="server" OnClick="btnHuy_Click"/>