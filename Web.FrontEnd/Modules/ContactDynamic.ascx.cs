using Library.Images;
using System;

using System.Collections.Generic;
using System.Linq;
using Web.Asp.Provider.Cache;
using Web.Asp.UI;
using Web.Business;

namespace Web.FrontEnd.Modules
{
    public partial class ContactDynamic : VITModule
    {
        #region khai bao cac bien
        private CustomerBLL _customerBLL;
        

        private int _productId;
        private bool _isOverwriteTitle;
        private Dictionary<string, string> MailContent;
        #endregion

        #region Event Method
        protected void Page_Load(object sender, EventArgs e)
        {
            this._customerBLL = new CustomerBLL();

            this.MailContent = new Dictionary<string, string>();
            btnDoiMa.Text = Language["ChangeCode"];
            
            if (this.GetValueParam<int>("CaptchaLength") > 0)
            {
                this.udpchange.Visible = true;
                this.LoadCaptChaImage();
            }
            else
            {
                this.udpchange.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var checkCaptchar = true;
                if (this.GetValueParam<int>("CaptchaLength") > 0)
                {
                    var code = this.Session["CaptchaImageText"].ToString();
                    if (this.txtCode.Text != code)
                    {
                        lblMessage.Text = "Mã xác nhận không đúng";
                        this.Session["CaptchaImageText"] = this.GenerateRandomCode();
                        this.LoadCaptChaImage();
                        checkCaptchar = false;
                    }
                }

                if (checkCaptchar)
                {
                    if (!string.IsNullOrEmpty(txtEmail.Text) && ktmail() == false)
                        lblMessage.Text = "<script>alert('Email không đúng định dạng');</script>";
                    else
                    {
                        var infoLable = this.GetValueRequest<string>("infoLable");
                        if(infoLable != null)
                        {
                            var infoLables = infoLable.Split('|').ToList();
                            int i = 0;
                            foreach(var lable in infoLables)
                            {
                                this.MailContent[lable] = this.GetValueRequest<string>("infoValue" + i++);
                            }
                        }
                        
                        lblMessage.Text = "Gửi mail thành công";
                        this.LoadCaptChaImage();
                        txtCode.Text = txtEmail.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is BusinessException)
                {
                    lblMessage.Text = "<script>alert('Gửi thông tin thất bại');</script>";
                }
            }
        }

        protected void btnDoiMa_Click(object sender, EventArgs e)
        {
            this.Session["CaptchaImageText"] = this.GenerateRandomCode();
            this.LoadCaptChaImage();
            this.udpchange.Update();
        }
        #endregion

        #region private method
        private bool ktmail()
        {
            int mail = txtEmail.Text.IndexOf("@");
            if (mail > 1)
                if (txtEmail.Text.IndexOf(".", mail) > 2)
                    return true;
            return false;
        }

        private void LoadCaptChaImage()
        {
            if (this.Session["CaptchaImageText"] == null) this.Session["CaptchaImageText"] = this.GenerateRandomCode();
            var att = new CaptchaImage(this.Session["CaptchaImageText"].ToString(), 500, 200);
            att.GenerateImage();
            this.imgCaptcha.Src = new EncodeImage().BitmapBase64Src(att.Image, System.Drawing.Imaging.ImageFormat.Png);
        }

        private string GenerateRandomCode()
        {
            return Library.GenerateRandomCode.RandomCode(this.GetValueParam<int>("CaptchaLength"));
        }
        #endregion

        private string Info()
        {
            string chuoi = "<p>Thông tin:</p>";
            chuoi += "<table cellpadding='3' cellspacing='3'>";
            chuoi += "<tr><td style='width:200px'>Tên:</td><td style='width:300px'>" + txtName.Text + "</td></tr>";
            chuoi += "<tr><td style='width:200px'>Email:</td><td style='width:300px'>" + txtEmail.Text + "</td></tr>";
            chuoi += "<tr><td style='width:200px'>Điện thoại:</td><td style='width:300px'>" + txtPhone.Text + "</td></tr>";
            chuoi += "<tr><td style='width:200px'>Địa chỉ:</td><td style='width:300px'>" + txtAddress.Text + "</td></tr>";

            foreach (var item in this.MailContent)
            {
                chuoi += "<tr><td style='width:200px'>" + item.Key + ":</td><td>" + item.Value + "</td></tr>";
            }
            
            chuoi += "</table><br /><br />";
            return chuoi;
        }
    }
}