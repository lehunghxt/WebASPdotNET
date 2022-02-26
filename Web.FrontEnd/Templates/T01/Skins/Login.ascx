<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/MemberLogin.ascx.cs" Inherits="Web.FrontEnd.Modules.MemberLogin" %>

<input type="hidden" name="Action" value="LOGIN"/>
<input type="hidden" name="LoginSave" value="true"/>

<div class="col-full">
                <div class="row">
                    <div id="primary" class="content-area">
                        <main id="main" class="site-main">
                            <section class="section-landscape-products-carousel">
                                <div class="log-in ptb-70 ptb-sm-60">
                                    <div class="container">
                                        <div class="row">
                                            <!-- New Customer Start -->
                                            <div class="col-md-6">
                                                <div class="well mb-sm-30">
                                                    <div class="new-customer">
                                                        <h3 class="custom-title">Bạn chưa có tài khoản ?</h3>
                                                        <p class="mtb-10"><strong>Đăng ký tài khoản</strong></p>
                                                        <p>Tạo tài khoản để theo dõi đơn hàng, lưu danh sách sản phẩm yêu thích, nhận nhiều ưu đãi hấp dẫn. Ấn nút đăng ký dưới đây để đăng ký !</p>
                                                        <a class="customer-btn" href="<%=HREF.LinkComponent("Signup") %>">Đăng ký</a>






                                                    </div>
                                                </div>
                                            </div>
                                            <!-- New Customer End -->
                                            <!-- Returning Customer Start -->
                                            <div class="col-md-6">
                                                <div class="well">
                                                    <div class="return-customer">
                                                        <h3 class="mb-10 custom-title"><%=Title %></h3>
                                                        <p class="mb-10"><strong>Đăng nhập để theo dõi đơn hàng, lưu danh sách sản phẩm yêu thích, nhận nhiều ưu đãi hấp dẫn.</strong></p>
                                                        <form action="#">
                                                            <div class="form-group">
                                                                <label>Email</label>
                                                                <input type="text" name="LoginUser" placeholder="Nhập Email của bạn..." id="input-email" class="form-control">
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Mật Khẩu</label>
                                                                <input type="password" name="LoginPassword" placeholder="Mật Khẩu..." id="input-password" class="form-control">
                                                            </div>
                                                            <p class="lost-password"><a href="forgot-password.html">Quên mật khẩu ?</a></p>
                                                            <input type="submit" value="<%=Title %>" class="return-customer-btn">
                                                        </form>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Returning Customer End -->
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