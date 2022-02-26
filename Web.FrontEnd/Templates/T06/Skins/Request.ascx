<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>

<section class=" container py-lg-4 py-md-3 py-sm-3 py-3">
    <header class="panel-head uk-flex uk-flex-middle uk-flex-space-between" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background:" + this.GetValueParam<string>("HeaderBackground") %>">
		<h3 class="text-center mb-md-4 mb-sm-3 mb-3 mb-2" title="<%=Title %>"><%=Title %></h3>
    </header>
    <section class="panel-body uk-clearfix">
        <input type="hidden" name="infoLable" value="Số lượng|Giá đề nghị|Mô tả mẫu"/>
        
 <div class="status alert alert-success" style="display: none">






        <asp:Label ID="lblMessage" runat="server" ForeColor="#F35827"></asp:Label>
    </div>                        
    <div class="row contact-form">
                   				
			<label><%=Language["FullName"] %></label>
            <asp:TextBox ID="txtName" CssClass="input" name="name" required="required" runat="server" MaxLength="300"></asp:TextBox>
			
        <label><%=Language["Phone"] %></label>
            <asp:TextBox ID="txtPhone" CssClass="input" name="name" required="required" runat="server" MaxLength="300"></asp:TextBox>				
			
        <label>Email</label>
			<asp:TextBox ID="txtEmail" CssClass="input" runat="server" MaxLength="300" required="required" name="email"></asp:TextBox>
					<label><%=Language["Address"] %></label>
            <asp:TextBox ID="txtAddress" CssClass="input" name="name" required="required" runat="server" MaxLength="300"></asp:TextBox>											
		<hr />
        
        <label><%=Language["Sample"] %></label> <asp:FileUpload ID="flu" runat="server"/>	
        <hr />
        <label><%=Language["Quantity"] %></label>
        <input type="text" name="infoValue0" class="input" placeholder="1000"/>
         <label><%=Language["SuggestedPrice"] %></label>
            <input type="text" name="infoValue1" class="input" placeholder="45.000 đ"/>											
        <label><%=Language["SampleDescripe"] %>:</label>
			<textarea name="infoValue2" class="contents" placeholder="Kích thước: 15cm x 25cm &#10;1000 màu xanh + 3000 màu đỏ &#10;Thêu logo công ty"></textarea>
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