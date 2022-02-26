<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>

<%var template = this.Page.Master as Web.Asp.UI.VITTemplate; %>
<div class="row">
    <div class="span3 content group">
    <div class="group post-979 page type-page status-publish hentry">
        <h1><%=this.Language["ContactInfo"] %></h1>
        <div id="contact-info-2" class="widget-2 widget-last widget contact-info">
            <div class="sidebar-nav">
                <ul>
                    <li>
                        <img src="/Templates/T09/images/location.png" alt="Location">
                        <strong><%=this.Language["Address"] %>:</strong> <%=template.Company.ADDRESS %>
                    </li>
                    <li>
                        <img src="/Templates/T09/images/tel.png" alt="Mobile">
                        <strong><%=this.Language["Phone"] %>:</strong> <%=template.Company.PHONE %>
                    </li>
                    <li>
                        <img src="/Templates/T09/images/fax.png" alt="Fax">
                        <strong>Fax:</strong> <%=template.Company.FAX %>
                    </li>
                    <li>
                        <img src="/Templates/T09/images/email.png" alt="Email">
                        <strong>Email:</strong> <%=template.Company.EMAIL %>
                    </li>
                </ul>






            </div>
        </div>
    </div>
</div>

 <div class="span6 content group">
    <div class="group post-979 page type-page status-publish hentry">
        <h1><%=this.Language["SentInfo"] %></h1>
        <div id="contact-form-contact-form" class="contact-form row-fluid">
            <div class="usermessagea"><asp:Label ID="Label1" runat="server"></asp:Label></div>
            <fieldset>
        <input type="hidden" name="infoLable" value="Số lượng|Giá đề nghị|Mô tả mẫu"/>
        
 <div class="status alert alert-success" style="display: none">
        <asp:Label ID="lblMessage" runat="server" ForeColor="#F35827"></asp:Label>
    </div>                        
    <div class="sidebar-nav">
                   				
			<label><%=Language["FullName"] %></label>
            <asp:TextBox ID="txtName" Width="100%" CssClass="input" name="name" required="required" runat="server" MaxLength="300"></asp:TextBox>
			
        <label><%=Language["Phone"] %></label>
            <asp:TextBox ID="txtPhone" Width="100%" CssClass="input" name="name" required="required" runat="server" MaxLength="300"></asp:TextBox>				
			
        <label>Email</label>
			<asp:TextBox ID="txtEmail" Width="100%" CssClass="input" runat="server" MaxLength="300" required="required" name="email"></asp:TextBox>
					<label><%=Language["Address"] %></label>
            <asp:TextBox ID="txtAddress" Width="100%" CssClass="input" name="name" required="required" runat="server" MaxLength="300"></asp:TextBox>											
		<hr />
        
        <label><%=Language["Sample"] %></label> <asp:FileUpload ID="flu" runat="server"/>	
        <hr />
        <label><%=Language["Quantity"] %></label>
        <input type="text" style="width:100%" name="infoValue0" class="input" placeholder="1000"/>
         <label><%=Language["SuggestedPrice"] %></label>
            <input type="text" style="width:100%" name="infoValue1" class="input" placeholder="45.000 đ"/>											
        <label><%=Language["SampleDescripe"] %>:</label>
			<textarea name="infoValue2" style="width:100%" class="contents" placeholder="Kích thước: 15cm x 25cm &#10;1000 màu xanh + 3000 màu đỏ &#10;Thêu logo công ty"></textarea>
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
                </fieldset>
        </div>
    </div>
</div>
    </div>