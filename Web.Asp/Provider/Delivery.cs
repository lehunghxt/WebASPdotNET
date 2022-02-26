using Library;
using Library.Web;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Business;
using Web.Model;

namespace Web.Asp.Provider
{
    public class Delivery
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(Delivery));

        private CompanyBLL companyBLL;

        public Delivery()
        {
            companyBLL = new CompanyBLL();
        }

        public IList<DistrictModel> GetGHNDistrict()
        {
            var api = new ApiHelper();
            var urlDistrict = "https://console.ghn.vn/api/v1/apiv3/GetDistricts";

            var data = new PostGHNDto();
            data.Token = SettingsManager.AppSettings.GHNToken;
            try
            {
                var districts = api.PostGetObject<List<DistrictModel>>(urlDistrict, data);
                districts = districts.Where(o => o.IsRepresentative != true && o.SupportType != 1)
                                    .OrderBy(o => o.ProvinceName)
                                    .ThenBy(o => o.DistrictName)
                                    .ToList();
                return districts;
            }
            catch (Exception ex)
            {
                log.Error(ex.TraceInformation());
            }
            return new List<DistrictModel>();
        }

        public GHNFeeModel GetGHNFee(int companyId, string to, int weight)
        {
            var data = new PostGHNDto();
            data.Token = SettingsManager.AppSettings.GHNToken;
            var ghn = companyBLL.GetConfigGHN(companyId);
            if (ghn != null)
            {
                var arr = to.Split('-');
                var province = arr[0];
                var district = arr[1];

                var api = new ApiHelper();

                var urlService = "https://console.ghn.vn/api/v1/apiv3/FindAvailableServices";
                data.Token = ghn.GHNToken;
                data.FromDistrictID = ghn.GHNFromDistrict ?? 0;
                data.ToDistrictID = Convert.ToInt32(district);
                data.Weight = weight;
                var result = api.PostGetObject<List<GHNFeeModel>>(urlService, data);
                return result.OrderBy(o => o.ServiceFee).FirstOrDefault();
            }
            return new GHNFeeModel();
        }

        public GHNOrderInfoModel GetGHNOrder(int companyId, string orderCode)
        {
            try
            {
                var api = new ApiHelper();
                var data = new PostGHNCheckOrrderDto();

                var ghn = companyBLL.GetConfigGHN(companyId);
                if (!string.IsNullOrEmpty(ghn.GHNToken)) data.token = ghn.GHNToken;
                else
                {
                    var loginModel = new GHNLoginDto();
                    loginModel.token = SettingsManager.AppSettings.GHNToken;
                    loginModel.Email = ghn.GHNUserName;
                    loginModel.Password = ghn.GHNPassword;
                    var loginUrl = "https://console.ghn.vn/api/v1/apiv3/SignIn";
                    var ghnData = api.PostGetObject<GHNTokenDto>(loginUrl, loginModel);
                    if (ghnData != null && !string.IsNullOrEmpty(ghnData.Token))
                    {
                        data.token = ghnData.Token;
                    }
                }
                data.OrderCode = orderCode;
                var url = "https://console.ghn.vn/api/v1/apiv3/OrderInfo";
                var result = api.PostGetObject<GHNOrderInfoModel>(url, data);
                return result;
            }
            catch (Exception ex)
            {
                log.Error(ex.TraceInformation());
            }

            return new GHNOrderInfoModel();
        }

        public GHNOrderModel PostOrderGHN(int companyId, string language, OrderModel orderInShop, GHPostOrderModel delivery)
        {
            try
            {
                var api = new ApiHelper();
                var data = new PostGHNDto();

                var ghn = companyBLL.GetConfigGHN(companyId);
                if (!string.IsNullOrEmpty(ghn.GHNToken)) data.Token = ghn.GHNToken;
                else
                {
                    var loginModel = new GHNLoginDto();
                    loginModel.token = SettingsManager.AppSettings.GHNToken;
                    loginModel.Email = ghn.GHNUserName;
                    loginModel.Password = ghn.GHNPassword;
                    var loginUrl = "https://console.ghn.vn/api/v1/apiv3/SignIn";
                    var ghnData = api.PostGetObject<GHNTokenDto>(loginUrl, loginModel);
                    if (ghnData != null && !string.IsNullOrEmpty(ghnData.Token))
                    {
                        data.Token = ghnData.Token;
                    }
                }

                var district = Convert.ToInt32(delivery.ToDistrict.Split('-')[1]);

                data.FromDistrictID = ghn.GHNFromDistrict ?? 0;
                data.ToDistrictID = district;
                data.Weight = (int)delivery.Weight;
                //data.Length = delivery.Length;
                //data.Width = delivery.Width;
                //data.Height = delivery.Height;
                var urlService = "https://console.ghn.vn/api/v1/apiv3/FindAvailableServices";
                var services = api.PostGetObject<List<GHNFeeModel>>(urlService, data).OrderBy(o => o.ServiceFee).ToList();
                var service = services.FirstOrDefault();

                var company = companyBLL.GetCompanyInfo(companyId, language);

                var postData = new GHNCreateOrderModel();
                postData.token = data.Token;
                postData.ClientAddress = company.ADDRESS;
                postData.ClientContactName = company.DISPLAYNAME;
                postData.ClientContactPhone = company.PHONE;
                postData.FromDistrictID = ghn.GHNFromDistrict ?? 0;
                postData.CustomerName = orderInShop.CustomerName;
                postData.CustomerPhone = orderInShop.CustomerPhone;
                postData.ShippingAddress = orderInShop.CustomerAddress;
                postData.ToDistrictID = district;
                postData.Weight = (int)delivery.Weight;
                postData.Length = delivery.Length;
                postData.Width = delivery.Width;
                postData.Height = delivery.Height;
                postData.ServiceID = service.ServiceID;
                postData.NoteCode = "CHOXEMHANGKHONGTHU";
                postData.PaymentTypeID = delivery.ReceiverPay ? 2 : 1;
                postData.CoDAmount = (int)delivery.COD;
                postData.ClientHubID = 0;
                if (delivery.SendFromCenter)
                {
                    postData.ShippingOrderCosts = new List<ShippingOrderCostDto>();
                    postData.ShippingOrderCosts.Add(new ShippingOrderCostDto { ServiceID = 53337, ServiceType = 5 });
                }
                var urlCreate = "https://console.ghn.vn/api/v1/apiv3/CreateOrder";
                var order = api.PostGetObject<GHNOrderModel>(urlCreate, postData);
                return order;
            }
            catch (Exception ex)
            {
                log.Error(ex.TraceInformation());
            }

            return null;
        }

        public GHTKFeeModel GetGHTKFee(int companyId, string to)
        {
            var bll = new CompanyBLL();
            var ghtk = bll.GetConfigGHTK(companyId);
            if (ghtk != null)
            {
                var arr = to.Split('-');
                var province = arr[0];
                var district = arr[1];

                var url = "https://services.giaohangtietkiem.vn/services/shipment/fee";

                var api = new ApiHelper();
                api.Token = ghtk.GHTKToken;
                var param = new Dictionary<string, object>();
                param["weight"] = 100;
                param["pick_address_id"] = ghtk.GHTKAddressId;
                param["province"] = HttpUtility.UrlEncode(province.Trim());
                param["district"] = HttpUtility.UrlEncode(district.Trim());

                var data = new PostGHTKDto();
                data.weight = 100;
                data.pick_address_id = ghtk.GHTKAddressId;
                data.province = province.Trim();
                data.district = district.Trim();

                try
                {
                    var districts = api.PostGetObject<GHTKFeeModel>(url, data, param);
                    return districts;
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
            return new GHTKFeeModel();
        }

        public GHTKOrderInfoModel GetGHTKOrder(int companyId, string orderCode)
        {
            
            var data = new GHTKOrderInfoModel();
            var bll = new CompanyBLL();
            var ghtk = bll.GetConfigGHTK(companyId);
            if (ghtk != null)
            {
                try
                {
                    var api = new ApiHelper();
                    api.Token = ghtk.GHTKToken;

                    var url = "https://services.giaohangtietkiem.vn/services/shipment/v2/" + orderCode;
                    var result = api.PostGetObject<GHTKOrderInfoModel>(url, data);
                    return result;
                }
                catch (Exception ex)
                {
                    log.Error(ex.TraceInformation());
                }
            }

            return data;
        }

        public GHTKOrderModel PostOrderGHTK(int companyId, string language, OrderModel orderInShop, GHPostOrderModel delivery)
        {
            try
            {
                var ghtk = companyBLL.GetConfigGHTK(companyId);
                var api = new ApiHelper(ghtk.GHTKToken);

                var company = this.companyBLL.GetCompanyInfo(companyId, language);

                var postData = new GHTKCreateOrderModel();
                postData.order = new GHTKOrderDto();
                postData.order.pick_address_id = ghtk.GHTKAddressId;
                postData.order.pick_tel = company.PHONE;
                postData.order.pick_name = company.DISPLAYNAME;
                postData.order.pick_address = company.ADDRESS;
                postData.order.id = orderInShop.Id.ToString();
                postData.order.pick_money = delivery.COD;
                postData.order.name = orderInShop.CustomerName;
                postData.order.tel = orderInShop.CustomerPhone;
                postData.order.email = orderInShop.CustomerEmail;
                var areaSelected = delivery.ToDistrict.Split('-');
                postData.order.province = areaSelected[0].Trim();
                postData.order.district = areaSelected[1].Trim();
                postData.order.address = orderInShop.CustomerAddress; 
                postData.order.is_freeship = delivery.ReceiverPay ? 0 : 1;
                postData.order.note = orderInShop.Note;
                postData.order.weight_option = "gram";
                postData.order.total_weight = delivery.Weight;
                postData.order.pick_option = delivery.SendFromCenter ? "post" : "cod";
                postData.order.pick_district = postData.order.district;
                postData.order.pick_province = postData.order.province;

                var urlService = "https://services.giaohangtietkiem.vn/services/shipment/order";
                var result = api.PostGetObject<GHTKOrderModel>(urlService, postData);
                return result;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }

    class PostGHTKDto
    {
        public string pick_address_id { get; set; }
        public int weight { get; set; }
        public string province { get; set; }
        public string district { get; set; }
    }
    class PostGHNDto
    {
        public string Token { get; set; }
        public int FromDistrictID { get; set; }
        public int ToDistrictID { get; set; }
        public int Weight { get; set; }
        //public int Length { get; set; }
        //public int Width { get; set; }
        //public int Height { get; set; }
    }
    class PostGHNCheckOrrderDto
    {
        public string token { get; set; }
        public string OrderCode { get; set; }
    }
    class GHNTokenDto
    {
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string Token { get; set; }
    }
    class GHNLoginDto
    {
        public string token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
