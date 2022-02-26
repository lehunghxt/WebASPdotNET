<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/MemberLogin.ascx.cs" Inherits="Web.FrontEnd.Modules.MemberLogin" %>

<input type="hidden" name="Action" value="REGIS"/>

  <div class="col-full">
                <div class="row">
                    <div id="primary" class="content-area">
                        <main id="main" class="site-main">
                            <section class="section-landscape-products-carousel">
                                <div class="register-account ptb-70 ptb-sm-60">
                                    <div class="container">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="register-title">
                                                    <h3 class="mb-10">Đăng ký tài khoản</h3>
                                                    <p class="mb-10">Nếu bạn đã có tài khoản ? <a href="login.html"> Đăng nhập tại đây</a>.</p>






                                                </div>
                                            </div>
                                        </div>
                                        <!-- Row End -->
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <form class="form-register" action="#">
                                                    <fieldset>
                                                        <legend>Thông Tin</legend>
                                                        <div class="form-group d-md-flex align-items-md-center">
                                                            <label class="control-label col-md-2" for="l-name"><span class="require">*</span>Họ và Tên :</label>
                                                            <div class="col-md-10">
                                                                <input type="text" name="FullName" class="form-control" id="l-name" placeholder="Họ và Tên...">
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-md-flex align-items-md-center">
                                                            <label class="control-label col-md-2" for="email"><span class="require">*</span>Email :</label>
                                                            <div class="col-md-10">
                                                                <input type="email" name="Email" class="form-control" id="email" placeholder="Email...">
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-md-flex align-items-md-center">
                                                            <label class="control-label col-md-2" for="number"><span class="require">*</span>Điện thoại :</label>
                                                            <div class="col-md-10">
                                                                <input type="tel" name="Phone" class="form-control" id="number" placeholder="Điện Thoại...">
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-md-flex align-items-md-center">
                                                            <label class="control-label col-md-2" for="number"><span class="require">*</span>Địa chí :</label>
                                                            <div class="col-md-10">
                                                                <input type="text" name="Address" class="form-control" id="address" placeholder="Địa chỉ...">
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                    <fieldset>
                                                        <legend>Mật khẩu của bạn :</legend>
                                                        <div class="form-group d-md-flex align-items-md-center">
                                                            <label class="control-label col-md-2" for="pwd"><span class="require">*</span>Mật Khẩu:</label>
                                                            <div class="col-md-10">
                                                                <input type="password" class="form-control" name="Password" id="pwd" placeholder="Mật Khẩu...">
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-md-flex align-items-md-center">
                                                            <label class="control-label col-md-2" for="pwd-confirm"><span class="require">*</span>Xác nhận mật khẩu : </label>
                                                            <div class="col-md-10">
                                                                <input type="password" class="form-control" name="PasswordConfirm" id="pwd-confirm" placeholder="Xác nhận mật khẩu...">
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                    <fieldset class="newsletter-input">
                                                        <legend>Nhận Bản Tin khuyến mãi</legend>
                                                        <div class="form-group d-md-flex align-items-md-center">
                                                            <label class="col-md-2 control-label">Đăng ký</label>
                                                            <div class="col-md-10 radio-button">
                                                                <label class="radio-inline"><input type="radio" name="optradio">Có</label>
                                                                <label class="radio-inline"><input type="radio" name="optradio">Không</label>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                    <div class="terms">
                                                        <div class="float-md-right">
                                                            <span>Đồng ý các điều khoản sử của <a href="#" class="agree"><b>Banmuagi?</b></a></span>
                                                            <input type="checkbox" name="agree" value="1" required="required"> &nbsp;
                                                            <input type="submit" value="Đăng Ký" class="return-customer-btn">
                                                        </div>
                                                    </div>
                                                </form>
                                            </div>
                                        </div>
                                        <!-- Row End -->
                                    </div>
                                    <!-- Container End -->
                                </div>
                            </section>

                        </main>
                    </div>
                </div>
            </div>