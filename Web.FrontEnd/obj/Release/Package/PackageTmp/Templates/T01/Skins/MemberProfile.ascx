<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="../../../Modules/MemberProfile.ascx.cs" Inherits="Web.FrontEnd.Modules.MemberProfile" %>

<section id="user-profile">
    <h4>Thông tin tài khoản</h4>
    <div class="row mt-20">
        <div class="ml-10">
            <img src="https://dummyimage.com/96x96/8e8e91/000000" height="96px">
        </div>
        <div class="col-md-8">             
                <div class="form-group row">
                    <label for="inputFullName" class="col-sm-3 col-form-label">Họ tên</label>
                    <div class="col-sm-9">
                        <input type="text" name="FullName"  class="form-control" id="inputFullName" value ="<%=this.UserContext.FullName %>"/>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="inputPhone" class="col-sm-3 col-form-label">Số điện thoại</label>
                    <div class="col-sm-9">
                        <input type="text" name="Phone" class="form-control" id="inputPhone" value="<%=this.UserContext.Phone %>" />
                    </div>
                </div>
                <div class="form-group row">
                    <label for="inputEmail" class="col-sm-3 col-form-label">Email</label>
                    <div class="col-sm-9">
                        <input type="email" name="Email" class="form-control" id="inputEmail" value="<%=this.UserContext.Email %>"/>
                    </div>
                </div>
            <div class="form-group row">
                    <label for="inputAddress" class="col-sm-3 col-form-label">Địa chỉ</label>
                    <div class="col-sm-9">
                        <input type="text" name="Address" class="form-control" id="inputAddress" value="<%=this.UserContext.Address %>"/>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="inputBirthday" class="col-sm-3 col-form-label">Ngày sinh</label>
                    <div class="col-sm-9">
                         <input type="date" name="Birthday" class="form-control" id="inputBirthday" value="<%=this.UserContext.Birthday %>"/>
                    </div>

                </div>
            </div>
        </div>
    <hr style="margin:20px" />
    <div class="row mt-20">
        <div class="ml-10" style="width:96px"></div>
        <div class="col-md-8">
                <div class="form-group row">
                    <label for="inputPasswordOld" class="col-sm-3 col-form-label">Mật khẩu cũ</label>
                    <div class="col-sm-9">
                        <input name="PasswordOld" id="inputPasswordOld" type="password" class="form-control" placeholder="">
                    </div>
                </div>
                <div class="form-group row">
                    <label for="inputPasswordNew" class="col-sm-3 col-form-label">Mật khẩu mới</label>
                    <div class="col-sm-9">
                        <input name="PasswordNew" id="inputPasswordNew" type="password" class="form-control" placeholder="">
                    </div>
                </div>
                <div class="form-group row">
                    <label for="inputPasswordConfirm" class="col-sm-3 col-form-label">Nhập lại</label>
                    <div class="col-sm-9">
                        <input name="PasswordConfirm" id="inputPasswordConfirm" type="password" class="form-control" placeholder="">
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col-sm-10" style="text-align: center;">
                        <button type="submit" class="btn btn-primary" id="userInfoUpd">Cập
                            nhật</button>
                    </div>
                </div>
                                                        
        </div>
    </div>
</section>