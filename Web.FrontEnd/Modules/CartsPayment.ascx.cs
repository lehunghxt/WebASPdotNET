namespace Web.FrontEnd.Modules
{
    using Library;
    using Library.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using Web.Asp.Provider;
    using Web.Asp.Provider.Cache;
    using Web.Asp.UI;
    using Web.Business;
    using Web.Model;

    public partial class CartsPayment : VITModule
    {
        private ProductBLL _bll;
        private CompanyBLL _companyBLL;

        public string tongtienHiden="";
        protected List<OrderProductModel> Carts { get; set; }
        protected List<ProductPriceModel> ProductPrices { get; set; }
        protected ConfigGHTKModel GHTK { get; set; }
        private Dictionary<string, string> MailContent;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._bll = new ProductBLL();
            this._companyBLL = new CompanyBLL();
            this.MailContent = new Dictionary<string, string>();

            Carts = Session[SettingsManager.Constants.SessionGioHang + this.Config.ID] as List<OrderProductModel>;
            if (Carts == null) Carts = new List<OrderProductModel>();

            var productIds = Carts.Select(p => p.ProductId).Distinct().ToList();
            ProductPrices = this._bll.GetAllProducePrices(productIds).ToList();
           
            var total = Carts.Select(o => o.TotalCost).Sum();
            tongtienHiden = string.Format("{0:0,0}", total);

            this.GHTK = _companyBLL.GetConfigGHTK(Config.ID);

            if (!Page.IsPostBack)
            {
                this.GetCookie();
            }
        }

        protected void imbHoanTat_Click(object sender, EventArgs e)
        {
            this.SetCookie();

            Carts = Session[SettingsManager.Constants.SessionGioHang + this.Config.ID] as List<OrderProductModel>;
            if (Carts == null) Carts = new List<OrderProductModel>();
            Carts = Carts.Where(o => o.Quantity > 0).ToList();
            if (Carts.Count == 0) lblMsg.Text = "<script>alert('Giỏ hàng hiện không có sản phẩm nào, vui lòng chọn sản phẩm vào giỏ');</script>";
            else
            {
                try
                {
                    // gui mail
                    var infoLable = this.GetValueRequest<string>("infoLable");
                    if (infoLable != null)
                    {
                        var infoLables = infoLable.Split('|').ToList();
                        int i = 0;
                        foreach (var lable in infoLables)
                        {
                            this.MailContent[lable] = this.GetValueRequest<string>("infoValue" + i++);
                        }
                    }

                    var noidung = this.Info(Carts);

                    var order = new OrderModel();
                    order.CreateDate = DateTime.Now;
                    if (this.UserContext != null)
                    {
                        if (order.Point > UserContext.Point) throw new Exception("Không đủ điểm");
                        order.CustomerId = this.UserContext.UserId;
                    }
                    order.CustomerAddress = txtDiaChi.Text.Trim();
                    order.CustomerEmail = txtMail.Text.Trim();
                    order.CustomerName = txtHoTen.Text.Trim();
                    order.CustomerNote = txtNote.Text.Trim();
                    order.CustomerPhone = txtDienThoai.Text.Trim();
                    order.ShippingId = this.GetValueRequest<int>("ShippingId");
                    order.CustomerPayDelivery = this.GetValueRequest<decimal>("DeliveryFee");
                    order.Voucher = this.GetValueRequest<string>("Voucher");
                    order.Point = this.GetValueRequest<int>("PointUse");
                    

                    order.Id = this._bll.SaveOrder(order,
                        this.Config.ID,
                        null,
                        Carts);

                    if (order.Id > 0)
                    {
                        Session[SettingsManager.Constants.SessionGioHang + this.Config.ID] = new List<OrderProductModel>();
                        if (!string.IsNullOrEmpty(this.GetValueParam<string>("ComponentResult")))
                            HREF.RedirectComponent(this.GetValueParam<string>("ComponentResult"), SettingsManager.Constants.SendOrder + "/" + order.Id);

                        lblMsg.Text = "<div class='alert alert-success' role='alert'> Đặt hàng thành công. Mã đơn hàng của bạn là <strong>" + order.Id + "</strong>. <br />";
                        lblMsg.Text += "Bạn có thể theo dõi đơn hàng tại đây: <a href='" + HREF.LinkComponent("OrderInfo", SettingsManager.Constants.SendOrder + "/" + order.Id) + "'>Tra cứu đơn hàng</a></div><script>alert('Gửi đơn đặt hàng thành công');</script>";

                        var webConfig = CacheProvider.GetCache<WEBCONFIGModel>(CacheProvider.Keys.Web, this.Config.ID);
                        if (webConfig == null)
                        {
                            webConfig = this._companyBLL.GetConfig(this.Config.ID);
                            CacheProvider.SetCache(webConfig, CacheProvider.Keys.Web, this.Config.ID);
                        }

                        if (webConfig != null)
                        {
                            try
                            {
                                MailManager mail = new MailManager();
                                mail.EnableSSL = webConfig.MailEnableSSL;
                                mail.Host = webConfig.MailServer;
                                mail.Port = webConfig.MailPort ?? 57;
                                mail.From = webConfig.MailAccount;
                                mail.Password = webConfig.MailPassword;
                                mail.To = webConfig.MailAccount + ";" + txtMail.Text;
                                mail.Title = this.Title;
                                mail.Content = noidung;
                                mail.SendEmail();
                            }
                            catch (Exception ex)
                            {
                                var error = "Không gửi được mail thông báo: " + ex.Message;
                                lblMsg.Text += "<div class='alert alert-danger' role='alert'> " + error + " </div><script>alert(\"" + error + "\");</script>";
                            }
                        }

                        var paymentMethod = this.GetValueRequest<string>("thanhtoan");
                        if (!string.IsNullOrEmpty(paymentMethod) && paymentMethod == "Online")
                        {
                            var action = this.GetValueRequest<string>("PaymentMethod");
                            if (!string.IsNullOrEmpty(action))
                                HREF.RedirectComponent("Payment", SettingsManager.Constants.SendOrder + "/" + order.Id + "/action/" + action);
                            else
                                HREF.RedirectComponent("Payment", SettingsManager.Constants.SendOrder + "/" + order.Id);
                        }
                    }
                }
                catch (BusinessException ex)
                {
                    lblMsg.Text += "<div class='alert alert-danger' role='alert'> " + ex.Message + " </div><script>alert(\"" + ex.Message + "\");</script>";
                }
            }
        }

        private string Info(List<OrderProductModel> carts)
        {
            string chuoi = "<p>Thông tin:</p>";
            chuoi += "<table cellpadding='3' cellspacing='3'>";
            chuoi += "<tr><td style='width:200px'>Tên:</td><td style='width:300px'>" + txtHoTen.Text + "</td></tr>";
            chuoi += "<tr><td style='width:200px'>Điện thoại:</td><td style='width:300px'>" + txtDienThoai.Text + "</td></tr>";
            chuoi += "<tr><td style='width:200px'>Địa chỉ:</td><td style='width:300px'>" + txtDiaChi.Text + "</td></tr>";
            chuoi += "<tr><td style='width:200px'>Ghi chú:</td><td style='width:300px'>" + txtNote.Text + "</td></tr>";

            foreach (var item in this.MailContent)
            {
                chuoi += "<tr><td style='width:200px'>" + item.Key + ":</td><td>" + item.Value + "</td></tr>";
            }

            chuoi += "</table><br /><br />";


            chuoi += "<table cellpadding='5' cellspacing='0' border='1' class='tbCart' width='95%' align='center' style='margin-top: 10px'>";
            chuoi += "<tr class='trhead'><td>Mã</td><td>Tên</td><td>Hình</td><td>Số lượng</td><td>Đơn giá</td><td>Tổng tiền</td></tr>";
            foreach (var sp in carts)
            {
                chuoi += string.Format("<tr><td style='text-align:center'>{0}</td>", string.IsNullOrEmpty(sp.ProductCode) ? sp.ProductId.ToString() : sp.ProductCode);
                chuoi += string.Format("<td style='text-align:center'>{0}</td>", sp.ProductName + (string.IsNullOrEmpty(sp.ProductProperties) ? "" : "(" + sp.ProductProperties + ")"));
                chuoi += string.Format("<td style='text-align:center'><img src='{0}' width='40px'/></td>", sp.ProductImage);
                chuoi += string.Format("<td style='text-align:center'>{0}</td>", sp.Quantity);
                chuoi += string.Format("<td style='text-align:right'>{0}</td>", string.Format("{0:0,0}", sp.Price));
                chuoi += string.Format("<td style='text-align:right'>{0}</td></tr>", string.Format("{0:0,0}", sp.TotalCost));
            }

            chuoi += "</table><br /><br />";
            chuoi += "<center><table>";
            chuoi += "<tr><td style='width:200px'>Tổng tiền:</td><td style='width:300px'>" + string.Format("{0:0,0}", carts.Sum(e => e.TotalCost)) + "</td></tr>";
            chuoi += "</table></center>";

            return chuoi;
        }

        protected decimal GetPrice(int productId, int quatity)
        {
            var price = this.ProductPrices.Where(e => e.ProductId == productId && e.Quantity < quatity)
                .OrderByDescending(e => e.Quantity)
                .Select(e => e.Price)
                .FirstOrDefault();

            return price;
        }

        private void GetCookie()
        {
            if (Request.Cookies["CartsPayment"] != null)
            {
                txtHoTen.Text = Server.UrlDecode(Request.Cookies["CartsPayment"]["CustomerName"]);
                txtDiaChi.Text = Server.UrlDecode(Request.Cookies["CartsPayment"]["CustomerAddress"]);
                txtDienThoai.Text = Server.UrlDecode(Request.Cookies["CartsPayment"]["CustomerPhone"]);
                txtNote.Text = Server.UrlDecode(Request.Cookies["CartsPayment"]["CustomerNote"]);
            }
        }

        private void SetCookie()
        {
            Response.Cookies["CartsPayment"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["CartsPayment"]["CustomerName"] = Server.UrlEncode(txtHoTen.Text);
            Response.Cookies["CartsPayment"]["CustomerAddress"] = Server.UrlEncode(txtDiaChi.Text);
            Response.Cookies["CartsPayment"]["CustomerPhone"] = Server.UrlEncode(txtDienThoai.Text);
            Response.Cookies["CartsPayment"]["CustomerMail"] = Server.UrlEncode("");
            Response.Cookies["CartsPayment"]["CustomerNote"] = Server.UrlEncode(txtNote.Text);
            Response.Cookies["CartsPayment"].Expires = DateTime.Now.AddDays(30);
        }

       //private void Payment(string action, OrderModel dto, string accessKey, string secret)
       // {
       //     var productBLL = new ProductBLL();        
       //         var api = new ApiHelper();
       //     switch (action)
       //     {
       //         case "VISA":
       //             var urlVisa = "https://api.pay.truemoney.com.vn/visa-charging/api/handle/request";
       //             var visacharge = new VisaChargingModel();
       //             visacharge.access_key = accessKey;
       //             visacharge.amount = dto.TotalDue;
       //             visacharge.cus_email = dto.CustomerEmail;
       //             visacharge.cus_fname = dto.CustomerName;
       //             visacharge.cus_phone = dto.CustomerPhone;
       //             visacharge.order_id = dto.Id;
       //             visacharge.order_info = "Thanh toan don hang " + dto.Id;
       //             visacharge.return_url = "http://" + HREF.Domain + "/Templates/T01/Components/PaymentResult.aspx";

       //             var signature = string.Format("access_key={0}&amount={1}&order_id={2}&order_info={3}",
       //                 visacharge.access_key,
       //                 visacharge.amount,
       //                 visacharge.order_id,
       //                 visacharge.order_info);
       //             visacharge.signature = Encrypt.HMACSHA256(signature, secret);

       //             var urlFull = string.Format("{0}?access_key={1}&amount={2}&order_id={3}&order_info={4}&return_url={5}&signature={6}",
       //                     urlVisa,
       //                     visacharge.access_key,
       //                     visacharge.amount,
       //                     visacharge.order_id,
       //                     HttpUtility.UrlEncode(visacharge.order_info),
       //                     visacharge.return_url,
       //                     visacharge.signature);
       //             var Data = api.PostGetObject<VisaChargingResponseModel>(urlFull, visacharge);

       //             Response.Redirect(Data.pay_url);
       //             break;
       //         case "ATM":
       //             var urlATM = "https://api.pay.truemoney.com.vn/bank-charging/service/v2";
       //             var atmCharge = new ATMChargingModel();
       //             atmCharge.access_key = accessKey;
       //             atmCharge.amount = dto.TotalDue;
       //             atmCharge.command = "request_transaction";
       //             atmCharge.order_id = dto.Id;
       //             atmCharge.order_info = "Thanh toan don hang " + dto.Id;
       //             atmCharge.return_url = "http://" + HREF.Domain + "/Templates/T01/Components/PaymentResult.aspx";

       //             var signatureATM = string.Format("access_key={0}&amount={1}&command={2}&order_id={3}&order_info={4}&return_url={5}",
       //                 atmCharge.access_key,
       //                 atmCharge.amount,
       //                 atmCharge.command,
       //                 atmCharge.order_id,
       //                 atmCharge.order_info,
       //                 atmCharge.return_url);
       //             atmCharge.signature = Encrypt.HMACSHA256(signatureATM, secret);

       //             var urlAMTFull = string.Format("{0}?access_key={1}&amount={2}&command={3}&order_id={4}&order_info={5}&return_url={6}&signature={7}",
       //                     urlATM,
       //                     atmCharge.access_key,
       //                     atmCharge.amount,
       //                     atmCharge.command,
       //                     atmCharge.order_id,
       //                     HttpUtility.UrlEncode(atmCharge.order_info),
       //                     atmCharge.return_url,
       //                     atmCharge.signature);

       //             var DataAMT = api.PostGetObject<VisaChargingResponseModel>(urlAMTFull, atmCharge);
       //             Response.Redirect(DataAMT.pay_url);
       //             break;
       //     }

       //     // xử lý kết quả
       //     if (dto.Id > 0)
       //     {
       //         var trans = new OrderTransactionModel();
       //         trans.Amount = this.GetValueRequest<decimal>("amount");
       //         trans.OrderId = this.GetValueRequest<int>("order_id");
       //         trans.OrderInfo = this.GetValueRequest<string>("order_info");
       //         trans.OrderType = this.GetValueRequest<string>("order_type");
       //         trans.RequestTime = this.GetValueRequest<DateTime>("request_time");
       //         trans.ResponseTime = this.GetValueRequest<DateTime>("response_time");
       //         trans.ResponseMessage = this.GetValueRequest<string>("response_message");
       //         trans.Trans_Status = this.GetValueRequest<string>("trans_status");
       //         trans.Trans_Ref = this.GetValueRequest<string>("trans_ref");
       //         trans.ResponseCode = this.GetValueRequest<string>("response_code");

       //         //neu ATM thì xử lý tiếp
       //         if (trans.OrderType == "ND")
       //         {
       //             var atmCommit = new AMTCommitModel();
       //             atmCommit.access_key = accessKey;
       //             atmCommit.trans_ref = trans.Trans_Ref;
       //             atmCommit.command = "request_transaction";
       //             var signatureATM = string.Format("access_key={0}&command={1}&trans_ref={2}",
       //                 atmCommit.access_key,
       //                 atmCommit.command,
       //                 atmCommit.trans_ref);
       //             atmCommit.signature = Encrypt.HMACSHA256(signatureATM, secret);

       //             var urlCommit = "https://api.pay.truemoney.com.vn/bank-charging/service/v2";
       //             var DataATMCommit = api.PostGetObject<ATMCommitResponseModel>(urlCommit, atmCommit);
       //             //this._orderBLL.Payment(trans);
       //             if (DataATMCommit.response_code == "00") productBLL.Payment(trans);
       //         }
       //         else if (trans.OrderType == "QT")
       //         { if (trans.ResponseCode == "00") productBLL.Payment(trans); }
       //     }
       // }
    }
}