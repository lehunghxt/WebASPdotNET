<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>

<section class="container">
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
                   				
	<div
              class="col-xs-12 col-sm-12 col-md-12 col-lg-12"
              style="padding: 5px"
            >
              <label class="col-xs-2 col-sm-2 col-md-2 col-lg-2">Họ tên</label>
              <div class="col-xs-10 col-sm-10 col-md-10 col-lg-10">
				<asp:TextBox ID="txtName" CssClass="form-control" name="name" required="required" runat="server" MaxLength="300"></asp:TextBox>
              </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12"style="padding: 5px"  >
              <label class="col-xs-2 col-sm-2 col-md-2 col-lg-2"
                >Điện thoại</label
              >
              <div class="col-xs-10 col-sm-10 col-md-10 col-lg-10">
                <asp:TextBox ID="txtPhone" CssClass="form-control" name="name" required="required" runat="server" MaxLength="300"></asp:TextBox>	
              </div>
            </div>

            <div
              class="col-xs-12 col-sm-12 col-md-12 col-lg-12"
              style="padding: 5px"
            >
              <label class="col-xs-2 col-sm-2 col-md-2 col-lg-2">Email</label>
              <div class="col-xs-10 col-sm-10 col-md-10 col-lg-10">
                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" MaxLength="300" required="required" name="email"></asp:TextBox>
              </div>
            </div>
            <div
              class="col-xs-12 col-sm-12 col-md-12 col-lg-12"
              style="padding: 5px"
            >
              <label class="col-xs-2 col-sm-2 col-md-2 col-lg-2">Địa chỉ</label>
              <div class="col-xs-10 col-sm-10 col-md-10 col-lg-10">
                <asp:TextBox ID="txtAddress" CssClass="form-control" name="name" required="required" runat="server" MaxLength="300"></asp:TextBox>	
              </div>
            </div>

            <div
              class="col-xs-12 col-sm-12 col-md-12 col-lg-12"
              style="padding: 5px"
            >
              <label class="col-xs-2 col-sm-2 col-md-2 col-lg-2"
                >Nội dung:</label
              >
              <div class="col-xs-10 col-sm-10 col-md-10 col-lg-10">
                <textarea name="infoValue0" class="contents" placeholder="Nội dung" style="width:100%;height:200px"></textarea>
              </div>
            </div>

            <div
              class="col-xs-12 col-sm-12 col-md-12 col-lg-12"
              style="padding: 10px 22px"
            >
      
			  <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="btn form_button pull-right" Text="Gửi đi" />		
            </div>				
					
								
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