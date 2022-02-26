namespace Web.FrontEnd.Modules
{
    using Library;
    using Library.Web;
    using System;
    using System.Web;
    using System.Linq;
    using Web.Asp.UI;
    using Web.Business;
    using Web.Model;
    using Web.Asp.Provider;
    using System.Collections.Generic;

    public partial class OnePayPayment : VITModule
    {
        private ProductBLL productBLL;
        private CompanyBLL companyBLL;

        protected OrderModel dto;
        protected OrderTransactionModel trans;
        protected List<OrderProductModel> Products;

        private string PaymentComponent
        {
            get
            {
                return "Payment";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.productBLL = new ProductBLL();
            companyBLL = new CompanyBLL();

            var onePay = companyBLL.GetConfigOnePay(Config.ID);

            var orderId = this.GetValueRequest<int>(SettingsManager.Constants.SendOrder);
            var responseOrderId = this.GetValueRequest<int>("order_id");
            if (orderId == 0) orderId = responseOrderId;
            if (orderId > 0)
            {
                txtOrderId.Text = orderId.ToString();
                dto = this.productBLL.GetOrder(orderId, this.Config.ID);
                Products = productBLL.GetOrderProducts(new List<int> { orderId }, Config.Language).ToList();
                foreach (var product in Products)
                {
                    product.ProductImage = HREF.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, Config.ID) + SettingsManager.Constants.PathProductImage + product.ProductImage;
                }
            }

            var api = new ApiHelper();
            var action = this.GetValueRequest<string>("action");
            if(!string.IsNullOrEmpty(action) && dto != null)
            {
                var amount = dto.TotalDue + dto.CustomerPayDelivery;
                if (!string.IsNullOrEmpty(dto.Voucher))
                {
                    var voucher = productBLL.GetVoucher(dto.Voucher, Config.ID);
                    if(voucher != null && voucher.Publish && voucher.EffectDate <= DateTime.Now && DateTime.Now <= voucher.ExpirDate && voucher.Value > 0)
                    {
                        if (voucher.IsPercent) amount -= dto.TotalDue * voucher.Value / 100;
                        else amount -= voucher.Value;
                    }
                }

                if (dto.Point > 0)
                {
                    var point = companyBLL.GetConfigMemberPoint(Config.ID);
                    if (point != null && point.TranferPrice > 0)
                    {
                        amount -= dto.Point * (point.TranferPrice ?? 0);
                    }
                }

                action = action.ToUpper();
            switch(action)
            {
                case "VISA":
                    var urlVisa = "https://api.pay.truemoney.com.vn/visa-charging/api/handle/request";
                    var visacharge = new VisaChargingModel();
                    visacharge.access_key = onePay.AccessKey;
                    visacharge.amount = amount;
                    visacharge.cus_email = dto.CustomerEmail;
                    visacharge.cus_fname = dto.CustomerName;
                    visacharge.cus_phone = dto.CustomerPhone;
                    visacharge.order_id = dto.Id;
                    visacharge.order_info = "Thanh toan don hang " + dto.Id;
                    visacharge.return_url = "http://" + HREF.Domain + "/Templates/T01/Components/"+ PaymentComponent + ".aspx";

                    var signature = string.Format("access_key={0}&amount={1}&order_id={2}&order_info={3}",
                        visacharge.access_key,
                        visacharge.amount,
                        visacharge.order_id,
                        visacharge.order_info);
                    visacharge.signature = Encrypt.HMACSHA256(signature, onePay.Secret);

                    var urlFull = string.Format("{0}?access_key={1}&amount={2}&order_id={3}&order_info={4}&return_url={5}&signature={6}",
                            urlVisa,
                            visacharge.access_key,
                            visacharge.amount,
                            visacharge.order_id,
                            HttpUtility.UrlEncode(visacharge.order_info),
                            visacharge.return_url,
                            visacharge.signature);
                    var Data = api.PostGetObject<VisaChargingResponseModel>(urlFull, visacharge);

                    Response.Redirect(Data.pay_url);
                    break;
                case "ATM":
                    var urlATM = "https://api.pay.truemoney.com.vn/bank-charging/service/v2";
                    var atmCharge = new ATMChargingModel();
                    atmCharge.access_key = onePay.AccessKey;
                    atmCharge.amount = amount;
                    atmCharge.command = "request_transaction";
                    atmCharge.order_id = dto.Id;
                    atmCharge.order_info = "Thanh toan don hang " + dto.Id;
                    atmCharge.return_url = "http://" + HREF.Domain + "/Templates/T01/Components/" + PaymentComponent + ".aspx";

                    var signatureATM = string.Format("access_key={0}&amount={1}&command={2}&order_id={3}&order_info={4}&return_url={5}",
                        atmCharge.access_key,
                        atmCharge.amount,
                        atmCharge.command,
                        atmCharge.order_id,
                        atmCharge.order_info,
                        atmCharge.return_url);
                    atmCharge.signature = Encrypt.HMACSHA256(signatureATM, onePay.Secret);

                    var urlAMTFull = string.Format("{0}?access_key={1}&amount={2}&command={3}&order_id={4}&order_info={5}&return_url={6}&signature={7}",
                            urlATM,
                            atmCharge.access_key,
                            atmCharge.amount,
                            atmCharge.command,
                            atmCharge.order_id,
                            HttpUtility.UrlEncode(atmCharge.order_info),
                            atmCharge.return_url,
                            atmCharge.signature);

                    var DataAMT = api.PostGetObject<VisaChargingResponseModel>(urlAMTFull, atmCharge);
                    Response.Redirect(DataAMT.pay_url);
                    break;
                }
            }

            // xử lý kết quả
            if (responseOrderId > 0)
            {
                trans = new OrderTransactionModel();
                trans.Amount = this.GetValueRequest<decimal>("amount");
                trans.OrderId = this.GetValueRequest<int>("order_id");
                trans.OrderInfo = this.GetValueRequest<string>("order_info");
                trans.OrderType = this.GetValueRequest<string>("order_type");
                trans.RequestTime = this.GetValueRequest<DateTime>("request_time");
                trans.ResponseTime = this.GetValueRequest<DateTime>("response_time");
                trans.ResponseMessage = this.GetValueRequest<string>("response_message");
                trans.Trans_Status = this.GetValueRequest<string>("trans_status");
                trans.Trans_Ref = this.GetValueRequest<string>("trans_ref");
                trans.ResponseCode = this.GetValueRequest<string>("response_code");

                //neu ATM thì xử lý tiếp
                if (trans.OrderType == "ND")
                {
                    var atmCommit = new ATMCommitModel();
                    atmCommit.access_key = onePay.AccessKey;
                    atmCommit.trans_ref = trans.Trans_Ref;
                    atmCommit.command = "request_transaction";
                    var signatureATM = string.Format("access_key={0}&command={1}&trans_ref={2}",
                        atmCommit.access_key,
                        atmCommit.command,
                        atmCommit.trans_ref);
                    atmCommit.signature = Encrypt.HMACSHA256(signatureATM, onePay.Secret);

                    var urlCommit = "http://api.1pay.vn/bank-charging/service/v2";
                    var DataATMCommit = api.PostGetObject<ATMCommitResponseModel>(urlCommit, atmCommit);
                    this.productBLL.Payment(trans);
                    if (DataATMCommit.response_code == "00") this.productBLL.Payment(trans);
                }
                else if (trans.OrderType == "QT")
                { if (trans.ResponseCode == "00") this.productBLL.Payment(trans); }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            HREF.RedirectComponent(PaymentComponent, SettingsManager.Constants.SendOrder + "/" + txtOrderId.Text);
        }
    }
}