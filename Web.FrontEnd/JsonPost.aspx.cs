namespace Web.FrontEnd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Services;
    using Library.Web;
    using log4net;
    using Web.Asp.Provider;
    using Web.Asp.UI;
    using Web.Business;
    using Web.Model;

    public partial class JsonPost : VITPage
    {
        //private ItemBLL _itemBLL;

        protected static readonly ILog log = LogManager.GetLogger(typeof(VITPage));

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region Vote & Like & Comment
        [WebMethod]
        public static ItemVoteModel UpdateVote(int Id, int Rate)
        {
            var _itemBLL = new ItemBLL();
            var vote = new ItemVoteModel();
            vote.Id = Id;
            vote.VoteRate = Rate;
            vote = _itemBLL.UpdateVote(vote);
            return vote;
        }

        [WebMethod]
        public static int Comment(int id, string name, string phone, string email, string content, string captcha)
        {
            var capServer = HttpContext.Current.Session["CaptchaImageText"];
            if (capServer == null || capServer.ToString() == captcha)
            {
                var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
                if (!domainCompanyMap.ContainsKey(HttpContext.Current.Request.Url.Authority))
                {
                    var company = (new CompanyBLL()).GetCompanyByDomain(HttpContext.Current.Request.Url.Authority);
                    domainCompanyMap[HttpContext.Current.Request.Url.Authority] = company;
                    HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
                }
                var config = domainCompanyMap[HttpContext.Current.Request.Url.Authority];

                content = content.Replace("<", "&#60;").Replace(">", "&#62;");
                content = content.ConvertBBCodeToHtml();

                var bll = new ItemBLL();
                var model = new ITEMCOMMENTModel();
                model.NAME = name;
                model.PHONE = phone;
                model.EMAIL = email;
                model.CONTENT = content;
                model.CLIENTID = MainCore.GetClientIpAddress();
                model.ITEMID = id;
                var newid = bll.CreateComment(model, config.ID);
                return newid;
            }
            else if(capServer != null && capServer.ToString() == captcha) return -1;

            return 0;
        }

        [WebMethod]
        public static ITEMLIKEModel Like(int id, bool like)
        {
            var history = (List<int>)HttpContext.Current.Session["LikeHistory"];
            if (history == null)
            {
                history = new List<int>();
                HttpContext.Current.Session["LikeHistory"] = history;
            }

            if (!history.Contains(id))
            {
                history.Add(id);
                var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
                if (!domainCompanyMap.ContainsKey(HttpContext.Current.Request.Url.Authority))
                {
                    var company = (new CompanyBLL()).GetCompanyByDomain(HttpContext.Current.Request.Url.Authority);
                    domainCompanyMap[HttpContext.Current.Request.Url.Authority] = company;
                    HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
                }
                var config = domainCompanyMap[HttpContext.Current.Request.Url.Authority];

                var bll = new ItemBLL();
                bll.UpdateLike(id, config.ID, like);
                var model = bll.GetLikes(id, config.ID);
                return model;
            }
            return new ITEMLIKEModel();
        }
        #endregion

        #region Carts
        [WebMethod]
        public static IList<OrderProductModel> GetCarts()
        {
            var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
            if (!domainCompanyMap.ContainsKey(HttpContext.Current.Request.Url.Authority))
            {
                var company = (new CompanyBLL()).GetCompanyByDomain(HttpContext.Current.Request.Url.Authority);
                domainCompanyMap[HttpContext.Current.Request.Url.Authority] = company;
                HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
            }
            var config = domainCompanyMap[HttpContext.Current.Request.Url.Authority];
            var giohang = HttpContext.Current.Session[SettingsManager.Constants.SessionGioHang + config.ID] as List<OrderProductModel>;
            if (giohang == null) giohang = new List<OrderProductModel>();

            return giohang;
        }

        [WebMethod]
        public static IList<OrderProductModel> AddProductsToCarts(int productId, int quantity, string properties)
        {
            var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
            if (!domainCompanyMap.ContainsKey(HttpContext.Current.Request.Url.Authority))
            {
                var company = (new CompanyBLL()).GetCompanyByDomain(HttpContext.Current.Request.Url.Authority);
                domainCompanyMap[HttpContext.Current.Request.Url.Authority] = company;
                HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
            }
            var config = domainCompanyMap[HttpContext.Current.Request.Url.Authority];
            var giohang = HttpContext.Current.Session[SettingsManager.Constants.SessionGioHang + config.ID] as List<OrderProductModel>;

            if (giohang == null) giohang = new List<OrderProductModel>();

            var productBLL = new ProductBLL();
            var prices = productBLL.GetAllProducePrices(new List<int> { productId }).ToList();

            var checkExist = giohang.Any(e => e.ProductId == productId && e.ProductProperties == properties);
            if (checkExist)
            {
                var item = giohang.Single(e => e.ProductId == productId && e.ProductProperties == properties);
                if (!item.IsAddOn)
                {
                    item.Quantity += quantity;
                    for (int i = prices.Count - 1; i >= 0; i--)
                    {
                        if (prices[i].Quantity <= item.Quantity)
                        {
                            item.Price = prices[i].Price;
                            item.TotalCost = item.Price * item.Quantity;
                            break;
                        }
                    }
                }
            }
            else
            {
                var product = productBLL.GetProductCartsById(productId, config.Language, config.ID);
                if (product != null)
                {
                    product.IsAddOn = false;
                    product.ProductImage = SettingsManager.AppSettings.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, config.ID) + SettingsManager.Constants.PathProductImage + product.ProductImage;
                    product.TotalCost = product.Price;
                    product.Quantity = 1;
                    product.ProductProperties = properties;

                    for (int i = prices.Count - 1; i >= 0; i--)
                    {
                        if (prices[i].Quantity <= quantity)
                        {
                            product.Price = prices[i].Price;
                            product.Quantity = quantity;
                            product.TotalCost = product.Price * product.Quantity;
                            break;
                        }
                    }

                    giohang.Add(product);

                    var addons = productBLL.GetListProductAddOns(productId, config.ID, config.Language);
                    foreach (var addon in addons)
                    {
                        var productAddOn = new OrderProductModel();
                        productAddOn.IsAddOn = true;
                        productAddOn.ProductId = addon.ProductId;
                        productAddOn.Price = addon.Price;
                        productAddOn.Quantity = addon.Quantity;
                        productAddOn.TotalCost = addon.Price * addon.Quantity;
                        productAddOn.ProductImage = SettingsManager.AppSettings.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, config.ID) + SettingsManager.Constants.PathProductImage + addon.ImagePath;
                        giohang.Add(productAddOn);
                    }
                }
            }
            HttpContext.Current.Session[SettingsManager.Constants.SessionGioHang + config.ID] = giohang;

            return giohang;
        }

        [WebMethod]
        public static IList<OrderProductModel> EditCarts(int productId, int quanlity, string properties)
        {
            var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
            if (!domainCompanyMap.ContainsKey(HttpContext.Current.Request.Url.Authority))
            {
                var company = (new CompanyBLL()).GetCompanyByDomain(HttpContext.Current.Request.Url.Authority);
                domainCompanyMap[HttpContext.Current.Request.Url.Authority] = company;
                HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
            }
            var config = domainCompanyMap[HttpContext.Current.Request.Url.Authority];
            var giohang = HttpContext.Current.Session[SettingsManager.Constants.SessionGioHang + config.ID] as List<OrderProductModel>;

            if (giohang == null) giohang = new List<OrderProductModel>();

            var checkExist = giohang.Any(e => e.ProductId == productId && e.ProductProperties == properties);
            if (checkExist)
            {
                var item = giohang.Single(e => e.ProductId == productId);
                item.Quantity = quanlity;
                item.TotalCost = item.Price * quanlity;

                var productBLL = new ProductBLL();
                var prices = productBLL.GetAllProducePrices(new List<int> { productId }).ToList();
                for (int i = prices.Count - 1; i >= 0; i--)
                {
                    if (prices[i].Quantity <= quanlity)
                    {
                        item.Price = prices[i].Price;
                        item.Quantity = quanlity;
                        item.TotalCost = item.Price * item.Quantity;
                        break;
                    }
                }
            }

            HttpContext.Current.Session[SettingsManager.Constants.SessionGioHang + config.ID] = giohang;

            return giohang;
        }

        [WebMethod]
        public static IList<OrderProductModel> RemoveProductFromCarts(int productId, string properties)
        {
            var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
            if (!domainCompanyMap.ContainsKey(HttpContext.Current.Request.Url.Authority))
            {
                var company = (new CompanyBLL()).GetCompanyByDomain(HttpContext.Current.Request.Url.Authority);
                domainCompanyMap[HttpContext.Current.Request.Url.Authority] = company;
                HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
            }
            var config = domainCompanyMap[HttpContext.Current.Request.Url.Authority];
            var giohang = HttpContext.Current.Session[SettingsManager.Constants.SessionGioHang + config.ID] as List<OrderProductModel>;

            if (giohang == null) giohang = new List<OrderProductModel>();

            var checkExist = giohang.Any(e => e.ProductId == productId && e.ProductProperties == properties);
            if (checkExist)
            {
                var item = giohang.Single(e => e.ProductId == productId && e.ProductProperties == properties);
                giohang.Remove(item);
            }

            HttpContext.Current.Session[SettingsManager.Constants.SessionGioHang + config.ID] = giohang;

            return giohang;
        }
        #endregion

        #region Point & Voucher
        [WebMethod]
        public static CustomerModel GetCustomer(int companyId, string phone)
        {
            if (phone == null) phone = string.Empty;
            phone = phone.Replace(" ", "").Replace(",", "").Replace(".", "");
            var bll = new CustomerBLL();
            var customer = bll.GetCustomer(companyId, phone);
            return customer;
        }

        [WebMethod]
        public static VoucherModel GetVoucher(int companyId, string code)
        {
            var bll = new ProductBLL();
            var voucher = bll.GetVoucher(code, companyId);
            if (voucher == null || (voucher.Publish == false)) return null;
            else if (voucher.EffectDate != null && voucher.EffectDate > DateTime.Now) return new VoucherModel();
            else if (voucher.ExpirDate != null && voucher.ExpirDate < DateTime.Now) return new VoucherModel();
            return voucher;
        }
        #endregion

        #region Delivery
        [WebMethod]
        public static IList<DistrictModel> GetGHNDistrict()
        {
            var peovider = new Delivery();
            return peovider.GetGHNDistrict();
        }

        [WebMethod]
        public static GHNFeeModel GetGHNFee(int companyId, string to, int weight)
        {
            var peovider = new Delivery();
            return peovider.GetGHNFee(companyId, to, weight);
        }

        [WebMethod]
        public static GHTKFeeModel GetGHTKFee(int companyId, string to)
        {
            var peovider = new Delivery();
            return peovider.GetGHTKFee(companyId, to);
        }
        #endregion

        #region Product
        [WebMethod]
        public static IList<DataSimpleModel> GetDataSimple(string datasource, int categoryId, int companyId, string language)
        {
            var articleBLL = new ArticleBLL();
            var productBLL = new ProductBLL();

            IList<DataSimpleModel> data = new List<DataSimpleModel>();
            
                switch (datasource)
                {
                    case "CAT":
                        data = articleBLL.GetDataSimple(companyId, language, datasource, categoryId, true);
                    foreach (var item in data)
                    {
                        if (!string.IsNullOrEmpty(item.ImagePath))
                            item.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, companyId) + SettingsManager.Constants.PathCategoryImage + item.ImagePath;
                    }
                    break;
                    case "ART":
                        data = articleBLL.GetDataSimple(companyId, language, datasource, categoryId, true);
                    foreach (var item in data)
                    {
                        if (!string.IsNullOrEmpty(item.ImagePath))
                            item.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, companyId) + SettingsManager.Constants.PathArticleImage + item.ImagePath;
                    }
                    break;
                    case "PRO":
                        data = articleBLL.GetDataSimple(companyId, language, datasource, categoryId, true);
                    foreach (var item in data)
                    {
                        if (!string.IsNullOrEmpty(item.ImagePath))
                            item.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, companyId) + SettingsManager.Constants.PathProductImage + item.ImagePath;
                    }
                    break;
                    case "PTG":
                        var arrP = productBLL.GetTagByCategoryId(companyId, categoryId, true).Distinct();

                        foreach (var onetag in arrP)
                        {
                            var itemP = new DataSimpleModel();
                            itemP.Title = onetag;
                            itemP.CategoryId = categoryId;
                            data.Add(itemP);
                        }
                        break;
                    case "ATG":
                        var arrA = articleBLL.GetTagByCategoryId(companyId, categoryId, true);
                        foreach (var onetag in arrA)
                        {
                            var itemA = new DataSimpleModel();
                            itemA.Title = onetag;
                            itemA.CategoryId = categoryId;
                            data.Add(itemA);
                        }
                        break;
                }

            return data.ToList();
        }
        #endregion

        [WebMethod]
        public static IList<ArticleBilingualModel> LoadMoreArticleBilinguals(string key, int cat, int skip, int take, string order, string tag)
        {
            var articleBLL = new ArticleBLL();

            if (HttpContext.Current.Session.SessionID != key) HttpContext.Current.Response.StatusCode = 401; 

            var domainCompanyMap = HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] as Dictionary<string, CompanyConfigModel> ?? new Dictionary<string, CompanyConfigModel>();
            if (!domainCompanyMap.ContainsKey(HttpContext.Current.Request.Url.Authority))
            {
                var company = (new CompanyBLL()).GetCompanyByDomain(HttpContext.Current.Request.Url.Authority);
                domainCompanyMap[HttpContext.Current.Request.Url.Authority] = company;
                HttpContext.Current.Application[SettingsManager.Constants.AppCompanyDomainMapCache] = domainCompanyMap;
            }
            var config = domainCompanyMap[HttpContext.Current.Request.Url.Authority];

            if (HttpContext.Current.Session != null)
            {
                var language = HttpContext.Current.Session[SettingsManager.Constants.SessionLanguage] as string;
                if (!string.IsNullOrEmpty(language)) config.Language = language;
            }

            var total = 0;
            var data = articleBLL.GetArticleBilinguals(config.ID, cat, config.Language.ToLower(), order, tag, out total, skip, take);
            foreach (var item in data)
            {
                if (!string.IsNullOrEmpty(item.ImagePath))
                    item.ImagePath = SettingsManager.AppSettings.DomainStore + "/" + string.Format(SettingsManager.AppSettings.FolderUpload, config.ID) + SettingsManager.Constants.PathArticleImage + item.ImagePath;
            }

            return data;
        }
    }
}