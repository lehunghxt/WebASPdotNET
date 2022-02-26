namespace Web.FrontEnd.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    using Asp.UI;
    using Business;
    using Asp.Provider;
    using Model;
    using Library.Images;
    using System.Linq;
    using Library.Web;

    public partial class ItemComment : VITModule
    {
        private ItemBLL _itemBll;

        public int vId; 
         
        protected List<ITEMCOMMENTModel> Comments { get; set; }
 
        protected void Page_Load(object sender, EventArgs e)
        {
            this._itemBll = new ItemBLL();

            this.vId = this.GetValueParam<int>("ItemId");
            if(vId == 0) this.vId = this.GetValueRequest<int>(SettingsManager.Constants.SendArticle);
            if (vId == 0) this.vId = this.GetValueRequest<int>(SettingsManager.Constants.SendProduct);

            this.Comments = this._itemBll.GetComments(this.vId, Config.ID).ToList();
            foreach (var comment in this.Comments)
            {
                comment.CONTENT = comment.CONTENT.Replace("<br />", "").ConvertBBCodeToHtml();
                comment.CONTENT = comment.CONTENT.Replace("&#60;", "<").Replace("&#62;", ">");
            }

            if (!this.IsPostBack)
            {
                this.Session["CaptchaImageText"] = this.GenerateRandomCode();
            }

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

        protected void btnDoiMa_Click(object sender, EventArgs e)
        {
            this.Session["CaptchaImageText"] = this.GenerateRandomCode();
            this.LoadCaptChaImage();
            this.udpchange.Update();
        }
          
        protected void btnSendComment_Click(object sender, EventArgs e)
        {

                var alert = string.Empty;
                if (this.GetValueParam<int>("CaptchaLength") > 0)
                {
                    var code = this.Session["CaptchaImageText"].ToString();
                    if (this.txtCode.Text.Trim().ToUpper() != code)
                    {
                        alert = "Mã xác nhận không đúng";
                        this.Session["CaptchaImageText"] = this.GenerateRandomCode();
                        this.LoadCaptChaImage();
                    }
                }
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
    }
}