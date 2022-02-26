namespace Web.Backend.Controllers
{
    using Asp.Provider;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Web.Backend.Models;
    using Web.Business;
    using Web.Model;

    public class WarehouseController : BaseController
    {
        private ArticleBLL articleBLL;
        private CompanyBLL companyBLL;
        private ItemBLL itemBLL;
        private ProductBLL productBLL;
        private CustomerBLL customerBLL;
        public WarehouseController()
        {
            articleBLL = new ArticleBLL();
            companyBLL = new CompanyBLL();
            itemBLL = new ItemBLL();
            productBLL = new ProductBLL();
            customerBLL = new CustomerBLL();
        }

        public ActionResult Index()
        {
            var model = new WarehouseIOViewModel();
            model.Warehouses = productBLL.GetWarehouseIOs(this.User.CompanyId).ToList();
            var ioIds = model.Warehouses.Select(e => e.Id).ToList();
            model.Products = productBLL.GetWarehouseProducts(ioIds, this.LanguageId).ToList();
            foreach(var product in model.Products)
            {
                product.ProductImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ProductImage;
            }
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(WarehouseIOViewModel model)
        {
            if(model.IOId > 0)
            {
                productBLL.DeleteWarehouseIO(model.IOId, this.User.CompanyId);
            }

            model.Warehouses = productBLL.GetWarehouseIOs(this.User.CompanyId).ToList();
            var ioIds = model.Warehouses.Select(e => e.Id).ToList();
            model.Products = productBLL.GetWarehouseProducts(ioIds, this.LanguageId).ToList();
            foreach (var product in model.Products)
            {
                product.ProductImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ProductImage;
            }

            return View(model);
        }

        public ActionResult Detail(int id = 0)
        {
            var model = new WarehouseIODetailViewModel();
            model.Warehouse = productBLL.GetWarehouseIO(id, this.User.CompanyId);
            if (model.Warehouse == null)
            {
                model.Warehouse = new WarehouseModel();
                model.Warehouse.Type = true;
                model.Warehouse.Date = DateTime.Now;
            }
            model.Suppliers = productBLL.GetSuppliers(this.User.CompanyId)
                                        .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
                                        .ToList();
            model.WarehouseProducts = productBLL.GetWarehouseProducts(new List<int> { id }, this.LanguageId).ToList();
            foreach (var product in model.WarehouseProducts)
            {
                product.ProductImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ProductImage;
            }
            model.Products = articleBLL.GetDataSimple(this.User.CompanyId, this.LanguageId, "PRO", 0, true);
            foreach (var product in model.Products)
            {
                product.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ImagePath;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Detail(WarehouseIODetailViewModel model)
        {
            model.Products = articleBLL.GetDataSimple(this.User.CompanyId, this.LanguageId, "PRO", 0, true);
            foreach (var product in model.Products)
            {
                product.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ImagePath;
            }

            model.Suppliers = productBLL.GetSuppliers(this.User.CompanyId)
                                        .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
                                        .ToList();

            var products = new List<OrderProductModel>();
            if(Request["product_id"] != null)
            {
                var ids = this.Request["product_id"].Split(',');
                var quantities = this.Request["product_quantity"].Split(',');
                var prices = this.Request["product_price"].Split(',');
                for (int i = 0; i < ids.Length; i++)
                {
                    try
                    {
                        var id = Convert.ToInt32(ids[i]);
                        if (!products.Any(e => e.ProductId == id))
                        {
                            var product = new OrderProductModel();
                            product.ProductId = id;
                            product.Quantity = Convert.ToInt32(quantities[i]);
                            product.Price = Convert.ToInt32(prices[i]);
                            products.Add(product);
                        }
                    }
                    catch
                    { }
                }
            }

            model.Warehouse.Id = productBLL.SaveWarehouseIO(model.Warehouse, this.User.CompanyId, this.User.UserId, products);

            model.Warehouse = productBLL.GetWarehouseIO(model.Warehouse.Id, this.User.CompanyId);
            if (model.Warehouse == null) model.Warehouse = new WarehouseModel();
            
            model.WarehouseProducts = productBLL.GetWarehouseProducts(new List<int> { model.Warehouse.Id }, this.LanguageId).ToList();
            foreach (var product in model.WarehouseProducts)
            {
                product.ProductImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ProductImage;
            }
            
            return View(model);
        }

        public ActionResult Orders(DateTime? fromdate = null, DateTime? todate = null)
        {
            var model = new OrderViewModel();
            model.FromDate = fromdate;
            model.ToDate = todate;
            model.Orders = productBLL.GetOrders(this.User.CompanyId, fromdate, todate).ToList();

            model.NewOrders = model.Orders.Where(e => e.Status == 0).ToList();
            model.TotalNew = model.NewOrders.Count;
            model.TotalNewDue = model.NewOrders.Sum(e => e.TotalDue);

            model.ConfirmOrders = model.Orders.Where(e => e.Status == 1).ToList();
            model.TotalConfirm = model.ConfirmOrders.Count;
            model.TotalConfirmDue = model.ConfirmOrders.Sum(e => e.TotalDue);

            model.SendOrders = model.Orders.Where(e => e.Status == 2).ToList();
            model.TotalSend = model.SendOrders.Count;
            model.TotalSendDue = model.SendOrders.Sum(e => e.TotalDue);

            model.RecievedOrders = model.Orders.Where(e => e.Status == 3).ToList();
            model.TotalRecieved = model.RecievedOrders.Count;
            model.TotalRecievedDue = model.RecievedOrders.Sum(e => e.TotalDue);

            model.ReturnOrders = model.Orders.Where(e => e.Status == 4).ToList();
            model.TotalReturn = model.ReturnOrders.Count;
            model.TotalReturnDue = model.ReturnOrders.Sum(e => e.TotalDue);

            var ioIds = model.Orders.Select(e => e.Id).ToList();
            model.Products = productBLL.GetOrderProducts(ioIds, this.LanguageId).ToList();
            foreach (var product in model.Products)
            {
                product.ProductImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ProductImage;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Orders(OrderViewModel model)
        {
            var request = Request["itemCheckbox"];
            if(request != null)
            {
                var ids = request.Split(',').Select(e => Convert.ToInt32(e)).ToList();
                productBLL.Delete(this.User.CompanyId, ids);
            }
            switch(model.Action)
            {
                case "CONFIRM":
                    productBLL.Confirm(this.User.CompanyId, this.User.UserId, model.OrderId);
                    break;
                case "SENT":
                    productBLL.Send(this.User.CompanyId, this.User.UserId, model.OrderId);
                    break;
                case "RETURN":
                    productBLL.Return(this.User.CompanyId, this.User.UserId, model.OrderId);
                    break;
                case "RECEIVE":
                    productBLL.Recived(this.User.CompanyId, this.User.UserId, model.OrderId);
                    break;
                case "DELETE":
                    if (model.OrderId > 0) productBLL.Delete(this.User.CompanyId, new List<int> { model.OrderId });
                    else
                    {
                        var check = Request["itemCheckbox"] == null ? string.Empty : Request["itemCheckbox"];
                        var ids = check.Split(',').Where(id => !string.IsNullOrEmpty(id)).Select(id => Convert.ToInt32(id)).ToList();
                        productBLL.Delete(this.User.CompanyId, ids);
                    }
                    
                    break;
            }
             
            model.Orders = productBLL.GetOrders(this.User.CompanyId, model.FromDate, model.ToDate).ToList();

            model.NewOrders = model.Orders.Where(e => e.Status == 0).ToList();
            model.TotalNew = model.NewOrders.Count;
            model.TotalNewDue = model.NewOrders.Sum(e => e.TotalDue);

            model.ConfirmOrders = model.Orders.Where(e => e.Status == 1).ToList();
            model.TotalConfirm = model.ConfirmOrders.Count;
            model.TotalConfirmDue = model.ConfirmOrders.Sum(e => e.TotalDue);

            model.SendOrders = model.Orders.Where(e => e.Status == 2).ToList();
            model.TotalSend = model.SendOrders.Count;
            model.TotalSendDue = model.SendOrders.Sum(e => e.TotalDue);

            model.RecievedOrders = model.Orders.Where(e => e.Status == 3).ToList();
            model.TotalRecieved = model.RecievedOrders.Count;
            model.TotalRecievedDue = model.RecievedOrders.Sum(e => e.TotalDue);

            model.ReturnOrders = model.Orders.Where(e => e.Status == 4).ToList();
            model.TotalReturn = model.ReturnOrders.Count;
            model.TotalReturnDue = model.ReturnOrders.Sum(e => e.TotalDue);

            var ioIds = model.Orders.Select(e => e.Id).ToList();
            model.Products = productBLL.GetOrderProducts(ioIds, this.LanguageId).ToList();
            foreach (var product in model.Products)
            {
                product.ProductImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ProductImage;
            }

            return View(model);
        }

        public ActionResult Order(int id = 0)
        {
            var model = new OrderDetailViewModel();

            model.Company = companyBLL.GetCompanyInfo(this.User.CompanyId, this.LanguageId);

            model.Order = productBLL.GetOrder(id, this.User.CompanyId);
            if (model.Order == null)
            {
                model.Order = new OrderModel();
                model.Order.CreateDate = DateTime.Now;
            }

            if (!string.IsNullOrEmpty(model.Order.Voucher))
            {
                var voucher = productBLL.GetVoucher(model.Order.Voucher, User.CompanyId);
                if (voucher != null && voucher.EffectDate <= model.Order.CreateDate && model.Order.CreateDate <= voucher.ExpirDate && voucher.Quantity > 0)
                {
                    if (voucher.IsPercent) model.VoucherFee = model.Order.TotalDue * voucher.Value / 100;
                    else model.VoucherFee = voucher.Value;
                }
            }

            if (model.Order.Point > 0)
            {
                var configPoint = companyBLL.GetConfigMemberPoint(User.CompanyId);
                if (configPoint != null)
                {
                    decimal point = 0;
                    if (configPoint.OrderPercent > 0)
                    {
                        point = model.Order.TotalDue * (configPoint.OrderPercent ?? 0) / 100;
                    }
                    if (configPoint.ProductAttribute > 0)
                    {
                        var productIds = model.OrderProducts.Select(e => e.ProductId).ToList();
                        foreach (var productId in productIds)
                        {
                            var value = productBLL.GetValueAttributeProduct(productId, configPoint.ProductAttribute ?? 0, User.CompanyId);
                            if (value != null)
                            {
                                var diemSP = 0;
                                int.TryParse(value, out diemSP);
                                if (diemSP > 0) point += diemSP;
                            }
                        }
                    }
                    if(point > 0)
                    {
                        model.PointFee = (configPoint.TranferPrice ?? 0) * point;
                    }
                }
            }

            model.TotalFee = model.Order.TotalDue - model.VoucherFee - model.PointFee;
            if (!string.IsNullOrEmpty(model.Order.ShippingCode)) model.TotalFee += model.Order.CustomerPayDelivery;

            model.OrderProducts = productBLL.GetOrderProducts(new List<int> { id }, this.LanguageId).ToList();
            foreach (var product in model.OrderProducts)
            {
                product.ProductImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ProductImage;
            }
            model.Products = productBLL.GetProducts(this.User.CompanyId, this.LanguageId).ToList();
            foreach (var product in model.Products)
            {
                product.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.Image;
            }
            
            model.Districts = new Delivery().GetGHNDistrict().ToList();
            model.Delivery.COD = model.Order.TotalDue - model.VoucherFee - model.PointFee;
            model.Delivery.Length = 10;
            model.Delivery.Width = 10;
            model.Delivery.Height = 10;
            return View(model);
        }

        [HttpPost]
        public ActionResult Order(OrderDetailViewModel model)
        {
            switch (model.Action)
            {
                case "UPDATE":
                    var products = new List<OrderProductModel>();
                    if (Request["product_id"] != null)
                    {
                        var ids = this.Request["product_id"].Split(',');
                        var quantities = this.Request["product_quantity"].Split(',');
                        var prices = this.Request["product_price"].Split(',');
                        var properties = this.Request["product_properties"].Split(','); 
                        for (int i = 0; i < ids.Length; i++)
                        {
                            try
                            {
                                var id = Convert.ToInt32(ids[i]);
                                if (!products.Any(e => e.ProductId == id && e.ProductProperties == properties[i]))
                                {
                                    var product = new OrderProductModel();
                                    product.ProductId = id;
                                    product.Quantity = Convert.ToInt32(quantities[i]);
                                    product.Price = Convert.ToInt32(prices[i]);
                                    product.ProductProperties = properties[i];
                                    products.Add(product);
                                }
                            }
                            catch
                            { }
                        }
                        model.Order.TotalDue = products.Select(e => e.Price * e.Quantity).DefaultIfEmpty(0).Sum();
                    }
                    model.Order.Id = productBLL.SaveOrder(model.Order, this.User.CompanyId, this.User.UserId, products);
                    break;
                case "CONFIRM":
                    productBLL.Confirm(this.User.CompanyId, this.User.UserId, model.Order.Id);
                    break;
                case "SENT":
                    productBLL.Send(this.User.CompanyId, this.User.UserId, model.Order.Id);
                    break;
                case "RETURN":
                    productBLL.Return(this.User.CompanyId, this.User.UserId, model.Order.Id);
                    break;
                case "RECEIVE":
                    productBLL.Recived(this.User.CompanyId, this.User.UserId, model.Order.Id);
                    break;
                case "DELETE":
                    productBLL.Delete(this.User.CompanyId, new List<int> { model.Order.Id });
                    break;
                case "DELIVERY":
                    var delivery = new Delivery(); 
                    if (model.UseCustomerDeliverPay)
                    { 
                        model.Delivery.COD += model.Order.CustomerPayDelivery;
                        model.Delivery.ReceiverPay = false;
                    } 

                    if (model.Order.ShippingId == 1)
                    {
                        var order = delivery.PostOrderGHN(User.CompanyId, this.LanguageId, model.Order, model.Delivery);
                        if (order != null)
                        {
                            model.Order.Note += "\n Dự kiến giao: " + order.ExpectedDeliveryTime;
                            this.productBLL.Delivery(User.CompanyId, User.UserId, model.Order.Id, model.Order.ShippingId, order.OrderCode, order.TotalServiceFee, model.Order.Note, model.Delivery.ReceiverPay, model.UseCustomerDeliverPay);
                        }
                    }
                    else if (model.Order.ShippingId == 2)
                    {
                        var order = delivery.PostOrderGHTK(User.CompanyId, this.LanguageId, model.Order, model.Delivery);
                        if (order != null && !string.IsNullOrEmpty(order.label))
                        {
                            model.Order.Note += "\n Dự kiến giao: " + order.estimated_deliver_time;
                            model.Order.ShippingCode = order.label;
                            model.Order.DeliveryFee = order.fee;
                            this.productBLL.Delivery(User.CompanyId, User.UserId, model.Order.Id, model.Order.ShippingId, order.label, order.fee, model.Order.Note, model.Delivery.ReceiverPay, model.UseCustomerDeliverPay);
                        } 
                    }
                    break;
            }

            model.Company = companyBLL.GetCompanyInfo(this.User.CompanyId, this.LanguageId);
            model.Order = productBLL.GetOrder(model.Order.Id, this.User.CompanyId);
            if (model.Order == null)
            {
                model.Order = new OrderModel();
                model.Order.CreateDate = DateTime.Now;
            }

            if (!string.IsNullOrEmpty(model.Order.Voucher))
            {
                var voucher = productBLL.GetVoucher(model.Order.Voucher, User.CompanyId);
                if (voucher != null && voucher.EffectDate <= model.Order.CreateDate && model.Order.CreateDate <= voucher.ExpirDate && voucher.Quantity > 0)
                {
                    if (voucher.IsPercent) model.VoucherFee = model.Order.TotalDue * voucher.Value / 100;
                    else model.VoucherFee = voucher.Value;
                }
            }

            if (model.Order.Point > 0)
            {
                var configPoint = companyBLL.GetConfigMemberPoint(User.CompanyId);
                if (configPoint != null)
                {
                    model.PointFee = (configPoint.TranferPrice ?? 0) * model.Order.Point;
                }
            }
            
            model.TotalFee = model.Order.TotalDue - model.VoucherFee - model.PointFee;
            if (!string.IsNullOrEmpty(model.Order.ShippingCode)) model.TotalFee += model.Order.CustomerPayDelivery;

            model.OrderProducts = productBLL.GetOrderProducts(new List<int> { model.Order.Id }, this.LanguageId).ToList();
            foreach (var product in model.OrderProducts)
            {
                product.ProductImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ProductImage;
            }
            model.Products = productBLL.GetProducts(this.User.CompanyId, this.LanguageId).ToList();
            foreach (var product in model.Products)
            {
                product.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.Image;
            }

            model.Districts = new Delivery().GetGHNDistrict().ToList();
            model.Delivery.COD = model.Order.TotalDue - model.VoucherFee - model.PointFee;

            ModelState.Clear();
            return View(model);
        }

        public ActionResult Supplier()
        {
            var model = new SupplierViewModel();
            model.Suppliers = productBLL.GetSuppliers(this.User.CompanyId).ToList();
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Supplier(SupplierViewModel model)
        {
            switch (model.Action)
            {
                case "ADD":
                    productBLL.AddSupplier(model.Supplier, this.User.CompanyId);
                    break;
                case "UPDATE":
                    productBLL.UpdateSupplier(model.Supplier, this.User.CompanyId);
                    break;
                case "REMOVE":
                    productBLL.DeleteSupplier(model.Supplier.Id, this.User.CompanyId);
                    break;
            }

            model.Suppliers = productBLL.GetSuppliers(this.User.CompanyId).ToList();
            return View(model);
        }

        public ActionResult Customers()
        {
            var model = new CustomerViewModel();
            model.Customers = customerBLL.GetCustomerOrders(this.User.CompanyId, null, null).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Customers(CustomerViewModel model)
        {
            model.Customers = customerBLL.GetCustomerOrders(this.User.CompanyId, model.FromDate, model.ToDate).ToList();
            return View(model);
        }

        public ActionResult Customer(string phone)
        {
            var model = new CustomerDetailViewModel();
            model.Customer = customerBLL.GetCustomer(this.User.CompanyId, phone);
            model.Orders = productBLL.GetOrders(this.User.CompanyId, null, null, phone).ToList();
            var ioIds = model.Orders.Select(e => e.Id).ToList();
            model.Products = productBLL.GetOrderProducts(ioIds, this.LanguageId).ToList();
            model.CusromerProducts = customerBLL.GetCustomerProducts(this.User.CompanyId, phone, this.LanguageId);
            foreach (var product in model.CusromerProducts)
            {
                product.ProductImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ProductImage;
            }
            return View(model);
        }
    }
}