<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/ContactDynamic.ascx.cs" Inherits="Web.FrontEnd.Modules.ContactDynamic" %>

<% var company = ((Web.Asp.UI.VITTemplate)this.Page.Master).Company;%>
<section class="f_cont min_wrap clearfix">
    <div class="fc_1">
                <p class="p_fc_1">Vui lòng điền vào mẫu liên hệ, chúng tôi sớm liên hệ với bạn trong thời gian sớm nhất. Hoặc có thể liên hệ trực tiếp tại</p>
                <h2 class="t_fc_1">VĂN PHÒNG TẠI</h2>
                <div class="m_fc_1">
                    <ul class="ul_m_fc_1">
                        <li>
                            <span>
                                <svg class="svg-inline--fa fa-map-marker-alt fa-w-12" aria-hidden="true" focusable="false" data-prefix="fas" data-icon="map-marker-alt" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 384 512" data-fa-i2svg=""><path fill="currentColor" d="M172.268 501.67C26.97 291.031 0 269.413 0 192 0 85.961 85.961 0 192 0s192 85.961 192 192c0 77.413-26.97 99.031-172.268 309.67-9.535 13.774-29.93 13.773-39.464 0zM192 272c44.183 0 80-35.817 80-80s-35.817-80-80-80-80 35.817-80 80 35.817 80 80 80z"></path></svg><!-- <i class="fas fa-map-marker-alt"></i> -->
                            </span>
                            Địa chỉ: <%=company.ADDRESS %></li>
                        <li>
                            <span>
                                <svg class="svg-inline--fa fa-phone-square fa-w-14" aria-hidden="true" focusable="false" data-prefix="fas" data-icon="phone-square" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" data-fa-i2svg=""><path fill="currentColor" d="M400 32H48C21.49 32 0 53.49 0 80v352c0 26.51 21.49 48 48 48h352c26.51 0 48-21.49 48-48V80c0-26.51-21.49-48-48-48zM94 416c-7.033 0-13.057-4.873-14.616-11.627l-14.998-65a15 15 0 0 1 8.707-17.16l69.998-29.999a15 15 0 0 1 17.518 4.289l30.997 37.885c48.944-22.963 88.297-62.858 110.781-110.78l-37.886-30.997a15.001 15.001 0 0 1-4.289-17.518l30-69.998a15 15 0 0 1 17.16-8.707l65 14.998A14.997 14.997 0 0 1 384 126c0 160.292-129.945 290-290 290z"></path></svg><!-- <i class="fas fa-phone-square"></i> -->
                            </span>
                            Điện thoại:  <%=company.PHONE %>                       </li>
                        <li>
                            <span>
                                <svg class="svg-inline--fa fa-mobile-alt fa-w-10" aria-hidden="true" focusable="false" data-prefix="fas" data-icon="mobile-alt" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 320 512" data-fa-i2svg=""><path fill="currentColor" d="M272 0H48C21.5 0 0 21.5 0 48v416c0 26.5 21.5 48 48 48h224c26.5 0 48-21.5 48-48V48c0-26.5-21.5-48-48-48zM160 480c-17.7 0-32-14.3-32-32s14.3-32 32-32 32 14.3 32 32-14.3 32-32 32zm112-108c0 6.6-5.4 12-12 12H60c-6.6 0-12-5.4-12-12V60c0-6.6 5.4-12 12-12h200c6.6 0 12 5.4 12 12v312z"></path></svg><!-- <i class="fas fa-mobile-alt"></i> -->
                            </span>
                            Hotline: <%=company.FAX %>                       </li>
                        <li>
                            <span>
                                <svg class="svg-inline--fa fa-envelope fa-w-16" aria-hidden="true" focusable="false" data-prefix="fa" data-icon="envelope" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" data-fa-i2svg=""><path fill="currentColor" d="M502.3 190.8c3.9-3.1 9.7-.2 9.7 4.7V400c0 26.5-21.5 48-48 48H48c-26.5 0-48-21.5-48-48V195.6c0-5 5.7-7.8 9.7-4.7 22.4 17.4 52.1 39.5 154.1 113.6 21.1 15.4 56.7 47.8 92.2 47.6 35.7.3 72-32.8 92.3-47.6 102-74.1 131.6-96.3 154-113.7zM256 320c23.2.4 56.6-29.2 73.4-41.4 132.7-96.3 142.8-104.7 173.4-128.7 5.8-4.5 9.2-11.5 9.2-18.9v-19c0-26.5-21.5-48-48-48H48C21.5 64 0 85.5 0 112v19c0 7.4 3.4 14.3 9.2 18.9 30.6 23.9 40.7 32.4 173.4 128.7 16.8 12.2 50.2 41.8 73.4 41.4z"></path></svg><!-- <i class="fa fa-envelope" aria-hidden="true"></i> -->
                            </span>
                            Email: <a href="mailto:<%=company.EMAIL %>"><%=company.EMAIL %></a>
                        </li>
                    </ul>                                






                </div>
            </div>

<input type="hidden" name="infoLable" value="Nội dung"/>
            <div class="fc_2">
                <div> 
                    <asp:Label ID="lblMessage" runat="server" ForeColor="#F35827"></asp:Label>
                <input style="display:none;" name="key_check" value="81df59b06616263b8908e3aecf2fa3d6">  
                    <ul class="ul_ct">
                        <li>
                            <asp:TextBox ID="txtName" CssClass="ipt_ct box-sizing-fix" name="name" required="required" runat="server" MaxLength="300"></asp:TextBox>
                        </li>
                        <li>
                            <asp:TextBox ID="txtEmail" CssClass="ipt_ct box-sizing-fix" runat="server" MaxLength="300" required="required" name="email"></asp:TextBox>
                        </li>
                        <li>
                            <asp:TextBox ID="txtPhone" CssClass="ipt_ct box-sizing-fix" name="phone" required="required" runat="server" MaxLength="300"></asp:TextBox>	
                        </li>
                        <li>
                            <asp:TextBox ID="txtAddress" CssClass="ipt_ct box-sizing-fix" name="address" required="required" runat="server" MaxLength="300"></asp:TextBox>
                        </li>
                        <li>
                            <textarea class="txt_ct box-sizing-fix" placeholder="Nội dung" name="infoValue0"></textarea>
                        </li> 
                        <li>
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
                        </li>
                    </ul>
                    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="btn_ct" Text="Gửi đi" />	
                </div>
            </div><!-- End .fc_2 -->
</section>