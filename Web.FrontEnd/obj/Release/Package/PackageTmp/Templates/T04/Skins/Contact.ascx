<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>

<section class="lib_feedback_form">
    <header class="panel-head uk-flex uk-flex-middle uk-flex-space-between" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background:" + this.GetValueParam<string>("HeaderBackground") %>">
		<h2 class="heading" title="<%=Title %>" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") > 0 ? "" : ";font-size:" + this.GetValueParam<string>("HeaderFontSize") + "px"%>"><%=Title %></h2>
    </header>
    <section class="panel-body uk-clearfix">
        <input type="hidden" name="infoLable" value="Nội dung"/>
        
  <div class="w3-panel w3-green" style="display: <%= lblMessage.Text == "" ? "none" : ""%>">
     <p>






        <asp:Label ID="lblMessage" runat="server"></asp:Label>
         </p>
    </div>                            
    <div class="rows">
                   				
			<label>Họ tên</label>
            <asp:TextBox ID="txtName" CssClass="input" name="name" required="required" runat="server" MaxLength="300"></asp:TextBox>
			
        <label>Điện thoại</label>
            <asp:TextBox ID="txtPhone" CssClass="input" name="name" required="required" runat="server" MaxLength="300"></asp:TextBox>				
			
        <label>Email</label>
			<asp:TextBox ID="txtEmail" CssClass="input" runat="server" MaxLength="300" required="required" name="email"></asp:TextBox>
					<label>Địa chỉ</label>
            <asp:TextBox ID="txtAddress" CssClass="input" name="name" required="required" runat="server" MaxLength="300"></asp:TextBox>											
		<hr />
        <label>Nội dung:</label>
			<textarea name="infoValue0" class="contents" placeholder="Nội dung"></textarea>

        <div style="display:none">
        <label>Hình mẫu</label> <asp:FileUpload ID="flu" runat="server"/>	
            </div>
        <hr />										
        
				<br /><br />
            <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="form_button" Text="Gửi đi" />					
					
								
    </div>

<asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:UpdateProgress runat="server" ID="UdtProgress" AssociatedUpdatePanelID="udpchange">
            <ProgressTemplate>
                <%--Đang gửi...--%>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <img id="imgCaptcha" runat="server" alt="Confirm Code" width="100" />
        Mã xác nhận: 
        <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
        <asp:Button ID="btnDoiMa" runat="server" Text="Đổi mã" CssClass="ChangeCode" ViewStateMode="Disabled" onclick="btnDoiMa_Click" />
    </ContentTemplate>
</asp:UpdatePanel>
        </section>
    </section>