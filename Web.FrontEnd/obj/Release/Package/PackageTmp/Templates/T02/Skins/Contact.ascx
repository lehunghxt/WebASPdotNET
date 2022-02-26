<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>

<section class="homepage-artcategory home-news">
    <header class="panel-head uk-flex uk-flex-middle uk-flex-space-between" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderBackground")) ? "" : ";background:" + this.GetValueParam<string>("HeaderBackground") %>">
		<h2 class="heading" title="<%=Title %>" style="<%=string.IsNullOrEmpty(this.GetValueParam<string>("HeaderFontColor")) ? "" : ";color:" + this.GetValueParam<string>("HeaderFontColor") %><%=this.GetValueParam<int>("HeaderFontSize") > 0 ? "" : ";font-size:" + this.GetValueParam<string>("HeaderFontSize") + "px"%>"><%=Title %></h2>
    </header>
    <section class="panel-body uk-clearfix">
<input type="hidden" name="infoLable" value="Nội dung"/>
<div style="display:none">






Upload <asp:FileUpload ID="flu" runat="server" /> <br />
Phone <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox> <br />
Address <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox> <br />
</div>
 <div class="status alert alert-success" style="display: none">
        <asp:Label ID="lblMessage" runat="server" ForeColor="#F35827"></asp:Label>
    </div>                        
    <div class="page-content contact-form">
                            	
		<form id="contact-form" action="php/contact.php">
								
			<label>Họ tên</label>
            <asp:TextBox ID="txtName" name="name" required="required" runat="server" MaxLength="300"></asp:TextBox>
									
			<label>Email</label>
			<asp:TextBox ID="txtEmail" runat="server" MaxLength="300" required="required" name="email"></asp:TextBox>
												
			<label>Nội dung</label>
			<textarea name="infoValue1"></textarea>
				
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
        <img id="imgCaptcha" runat="server" alt="Confirm Code" width="100" />
        Mã xác nhận: 
        <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
        <asp:Button ID="btnDoiMa" runat="server" Text="Đổi mã" CssClass="ChangeCode" ViewStateMode="Disabled" onclick="btnDoiMa_Click" />
    </ContentTemplate>
</asp:UpdatePanel>
        </section>
    </section>