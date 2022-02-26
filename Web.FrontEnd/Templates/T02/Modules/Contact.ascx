<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Contact.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.Contact" %>


    <div class="status alert alert-success" style="display: none">






        <asp:Label ID="lblMessage" runat="server" ForeColor="#F35827"></asp:Label>
    </div>                        
    <div class="page-content contact-form">
                            	
		<form id="contact-form" action="php/contact.php">
								
			<label>Họ tên</label>
            <asp:TextBox ID="txtTieuDe" name="name" required="required" runat="server" MaxLength="300"></asp:TextBox>
									
			<label>Email</label>
			<asp:TextBox ID="txtEmail" runat="server" MaxLength="300" required="required" name="email"></asp:TextBox>
												
			<label>Nội dung</label>
			<asp:TextBox ID="txtNoiDung" runat="server" name="message" required="required" TextMode="MultiLine" Rows="6"></asp:TextBox>
				
            <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="big" Text="Gửi đi" />					
									
        </form>
								
    </div>


<asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:UpdateProgress runat="server" ID="UdtProgress" AssociatedUpdatePanelID="udpchange">
            <ProgressTemplate>
                <%--Đang gửi...--%>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div>
            <table style="margin-left:73px;">
                <tr>
                    <td><img id="imgCaptcha" runat="server" alt="zicki ma xac nhan" width="150" /></td>
                    <td valign="middle"><asp:Button ID="btnDoiMa" runat="server" Text="Đổi mã" CssClass="btn_eve changecaptcha" ViewStateMode="Disabled" onclick="btnDoiMa_Click" /></td>
                </tr>
            </table>                                                   
        </div>
        <div class="controls" style="margin-top: 10px">
            <label class="col-sm-2 control-label" for="NoiDungLienHe">Mã xác nhận (*)</label>
            <asp:TextBox ID="txtCode" runat="server" CssClass="form-control captcha"></asp:TextBox>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>