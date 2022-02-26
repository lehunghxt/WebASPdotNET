<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/MemberLogin.ascx.cs" Inherits="Web.FrontEnd.Modules.MemberLogin" %>

<h3 class="lg-m-top-15">1. Khách hàng mới / Đăng nhập</h3>
<%if(this.UserContext == null){ %>
<div class="box-login-register-arround">
                <ul class="nav-register">
                    <li class="active">
                        <a href="javascript:;" alt="login-form" onclick="changeAction('LOGIN')">
                            <span>Đăng nhập</span>
                            <i>Đã là thành viên Banmuagi</i>
                        </a>
                    </li>
                    <li>
                        <a href="javascript:;" alt="register-form" onclick="changeAction('REGIS')">
                            <span>Tạo tài khoản</span>
                            <i>Dành cho khách hàng mới</i>
                        </a>
                    </li>
                </ul>

                <div class="register-content">
                    <div id="login-form" class="register-comm-for">
                        <div class="content" id="login_popup_form">
                            <input type="hidden" name="checkout_step" value="1" />
                            <div class="form-group" id="popup_login">
                                <label class="control-label">Email</label>
                                <input id="popup-login-email" type="text" class="form-control login" name="LoginUser">
                                <span class="help-block ajax-message"></span>






                            </div>

                            <div class="form-group" id="popup_password">
                                <label class="control-label">Mật khẩu</label>
                                <input type="password" id="login_password" class="form-control login" name="LoginPassword" placeholder="Nhập mật khẩu" autocomplete="off">
                                <span class="help-block ajax-message"></span>
                            </div>

                            <div class="login-ajax-captcha" style="display:none">
                                <div id="login-checkout-recaptcha"></div>
                                <span class="help-block ajax-message"></span>
                            </div>
                            <div class="form-group" id="error_captcha" style="margin-bottom: 15px">
                                <span class="help-block ajax-message"></span>
                            </div>

                            <div class="form-group last">
                                <p class="reset">Quên mật khẩu? Khôi phục mật khẩu <a data-toggle="modal" data-target="#reset-password-form" href="#">tại đây</a></p>
                                <button type="submit" <%--id="login_popup_submit"--%> class="btn btn-info btn-block">Đăng nhập</button>
                            </div>
                        </div>
                    </div>
                    <!-- login widget -->

                    <!-- Register widget -->
                    <div id="register-form" class="register-comm-for">
                        <div id="register_popup_form" class="content" >
                            <input type="hidden" name="verify" value="1">
                            <div form-group id="general_error">
                                <span></span>
                            </div>
                            <div class="form-group use-phone" id="register_phone">
                                <label class="control-label" for="email"><strong>Số điện thoại</strong></label>
                                <div class="input-wrap">
                                    <input type="text" class="form-control register register-email-input" name="Phone" id="phone_for_register" placeholder="Nhập số điện thoại">

                                </div>
                            </div>

                            <div class="form-group" id="register_email">
                                <label class="control-label" for="email"><strong>Email</strong></label>
                                <div class="input-wrap">
                                    <input type="text" class="form-control register register-email-input" name="Email" id="email_for_register" placeholder="Nhập email">
                                    <span class="help-block ajax-message"></span>
                                </div>
                            </div>
                            <div class="form-group" id="register_password">
                                <label class="control-label" for="pasword"><strong>Mật khẩu</strong></label>
                                <div class="input-wrap">
                                    <input type="password" class="form-control register" name="Password" id="password" placeholder="Mật khẩu từ 6 đến 32 ký tự" autocomplete="off">
                                    <span class="help-block ajax-message"></span>
                                </div>
                            </div>
                            <div class="form-group" id="register_password_again">
                                <label class="control-label" for="pasword"><strong>Mật khẩu</strong></label>
                                <div class="input-wrap">
                                    <input type="password" class="form-control register" name="PasswordConfirm" id="password_again" placeholder="Mật khẩu từ 6 đến 32 ký tự" autocomplete="off">
                                    <span class="help-block ajax-message"></span>
                                </div>
                            </div>
                            <div class="form-group" id="register_name">
                                <label class="control-label"><strong>Họ tên</strong></label>
                                <div class="input-wrap">
                                    <input type="text" class="form-control register" name="FullName" id="name" placeholder="Nhập họ tên">
                                    <span class="help-block ajax-message"></span>
                                </div>
                            </div>
                            <div class="form-group" id="register_address">
                                <label class="control-label"><strong>Địa chỉ</strong></label>
                                <div class="input-wrap">
                                    <input type="text" class="form-control register" name="Address" id="address" placeholder="Địa chỉ">
                                    <span class="help-block ajax-message"></span>
                                </div>
                            </div>
                            <div class="form-group policy-group">
                                <div class="input-wrap">
                                    <p class="policy">Khi bạn nhấn Đăng ký, bạn đã đồng ý thực hiện
                                        mọi giao dịch mua bán theo <a target="_blank" href="#">điều
                                            kiện sử dụng và chính sách của Banmuagi</a>.</p>
                                </div>
                            </div>
                            <div class="form-group last">
                                <button type="submit" <%--id="register_popup_submit"--%> class="btn btn-info btn-block">Đăng ký</button>
                            </div>
                            <input type="hidden" name="shippingStep" value="1" />
                        </div>
                    </div>
                    <!-- Register widget -->

                    <!-- reset password -->
                    <div class="modal" id="reset-password-form" tabindex="-1" role="dialog">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <i class="fa fa-times"></i>
                                    </button>
                                    <div class="head">
                                        <h2>Quên mật khẩu?</h2>
                                        <p>
                                            <span>Vui lòng gửi email. Chúng tôi sẽ gửi link khởi tạo
                                                mật khẩu mới qua email của bạn.</span>
                                        </p>
                                    </div>
                                </div>
                                <div class="modal-body">
                                    <div class="content" id="reset_popup_form">
                                        <div id="forgot_successful">
                                            <span></span>
                                        </div>
                                        <div class="form-group" id="forgot_pass">
                                            <input type="text" name="ResetEmail" id="email" class="form-control" value="" placeholder="Nhập email">
                                            <span class="help-block"></span>
                                        </div>
                                        <div class="form-group last">
                                            <button type="submit" id="reset_form_submit" class="btn btn-info" onclick="changeAction('RESET')">Gửi</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- end reset password -->

                </div>
            </div>

<input type="hidden" id="Action" name="Action" value="LOGIN"/>
<input type="hidden" name="LoginSave" value="true"/>

<script>
    function changeAction(action) {
        $("#Action").val(action);
    }
</script>
<%}
    else{%>
<div>Tên: <%= this.UserContext.FullName%></div>
<div>Số ĐT: <%= this.UserContext.Phone%></div>
<div>Email: <%= this.UserContext.Email%></div>
<div>Địa chỉ: <%= this.UserContext.Address%></div>
<%} %>
