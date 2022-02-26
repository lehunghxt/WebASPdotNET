<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/Contact.ascx.cs" Inherits="VIT.Pre.FrontEnd.Modules.Contact" %>

<div class="row">
<div class="span3 content group">
    <div class="group post-979 page type-page status-publish hentry">
        <h1><%=this.Language["ContactInfo"] %></h1>
        <div id="contact-info-2" class="widget-2 widget-last widget contact-info">
            <div class="sidebar-nav">
                <ul>
                    <li>
                        <img src="/Templates/T0160/images/location.png" alt="Location">
                        <strong><%=this.Language["Address"] %>:</strong> <%=_company.Address %>
                    </li>
                    <li>
                        <img src="/Templates/T0160/images/tel.png" alt="Mobile">
                        <strong><%=this.Language["Phone"] %>:</strong> <%=_company.Phone %>
                    </li>
                    <li>
                        <img src="/Templates/T0160/images/fax.png" alt="Fax">
                        <strong>Fax:</strong> <%=_company.Fax %>
                    </li>
                    <li>
                        <img src="/Templates/T0160/images/email.png" alt="Email">
                        <strong>Email:</strong> <%=_company.Email %>
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
            <div class="usermessagea"><asp:Label ID="lblMessage" runat="server"></asp:Label></div>
            <fieldset>
                <ul>
                    <li class="text-field span6 nomargin">
                        <div class="input-prepend">
                            <label><%=Language["FullName"] %></label>
                            <asp:TextBox ID="txtTieuDe" CssClass="required" runat="server" MaxLength="300"></asp:TextBox>
                        </div>
                        <div class="msg-error"></div><div class="clear"></div>
                    </li>
                    <li class="text-field span6">
                        <div class="input-prepend">
                            <label>Email</label>
                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="300"  CssClass="required email-validate"></asp:TextBox>
                        </div>
                        <div class="msg-error"></div><div class="clear"></div>
                    </li>
                    <li class="text-field span12 nomargin">
                        <div class="input-prepend">
                            <label><%=Language["Content"] %></label>
                            <asp:TextBox ID="txtNoiDung" runat="server" TextMode="MultiLine" CssClass="required"></asp:TextBox>
                        </div>
                        <div class="msg-error"></div><div class="clear"></div>
                    </li>
                        <asp:UpdatePanel runat="server" ID="udpchange" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:UpdateProgress runat="server" ID="UdtProgress" AssociatedUpdatePanelID="udpchange">
                                    <ProgressTemplate>
                                        <%--Đang gửi...--%>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <li class="nomargin textarea-field first-of-line span12">
                                <div>
                                    <table style="margin-left:73px;">
                                        <tr>
                                            <td><img id="imgCaptcha" runat="server" alt="Captcha" width="150" /></td>
                                            <td valign="middle">
                                                <asp:Button ID="btnDoiMa" runat="server" Text="Change" CssClass="btn_eve changecaptcha" ViewStateMode="Disabled" onclick="btnDoiMa_Click" />

                                            </td>
                                        </tr>
                                    </table>
                                                                        
                                </div>
                                <div style="margin-top: 10px">
                                    <div class="table_left" style="float: left"><%=Language["ConfirmCode"]%>: </div>
                                    <asp:TextBox ID="txtCode" runat="server" CssClass="tab_input captcha"></asp:TextBox>
                                </div>
                                    </li>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    
                    <li class="submit-button span12">
                        <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" CssClass="button alignright" />
                        <div class="clear"></div>
                    </li>
                </ul>
            </fieldset>
        </div>
    </div>
     </div>
    </div>
