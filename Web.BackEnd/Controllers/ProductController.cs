namespace Web.Backend.Controllers
{
    using Asp.Provider;
    using Library;
    using Library.Web;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Web.Backend.Models;
    using Web.Business;
    using Web.Model;

    public class ProductController : BaseController
    {
        private ArticleBLL articleBLL;
        private CompanyBLL companyBLL;
        private ItemBLL itemBLL;
        private ProductBLL productBLL;
        private SEOBLL seoBLL;

        public ProductController()
        {
            articleBLL = new ArticleBLL();
            companyBLL = new CompanyBLL();
            itemBLL = new ItemBLL();
            productBLL = new ProductBLL();
            seoBLL = new SEOBLL();
        }

        public ActionResult Index(int catid = 0)
        {
            var model = new ProductViewModel();
            model.CatId = catid;
            model.Products = productBLL.GetProducts(this.User.CompanyId, this.LanguageId, model.CatId).OrderByDescending(e => e.ID).ToList();
            foreach(var product in model.Products)
            {
                product.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.Image;
            }

            model.Groupons = productBLL.GetGroupons(this.User.CompanyId, this.LanguageId, model.CatId).OrderByDescending(e => e.ID).ToList();
            foreach (var groupon in model.Groupons)
            {
                groupon.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + groupon.Image;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ProductViewModel model)
        {
            try
            {
                switch (model.Action)
                {
                    case "REMOVEPRO":
                        var folder = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage;
                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }

                        if (System.IO.File.Exists(folder + model.Image))
                            System.IO.File.Delete(folder + model.Image);

                        var images = itemBLL.GetImages(model.ProductId, this.User.CompanyId).ToList();
                        foreach (var image in images)
                        {
                            if (System.IO.File.Exists(folder + image.Image))
                                System.IO.File.Delete(folder + image.Image);
                        }

                        var colors = productBLL.GetProduceColors(model.ProductId).ToList();
                        foreach (var color in colors)
                        {
                            if (System.IO.File.Exists(folder + color.ImageName))
                                System.IO.File.Delete(folder + color.ImageName);
                        }

                        productBLL.RemoveProduct(model.ProductId, this.User.CompanyId);

                        seoBLL.Delete(model.ProductId);
                        break;
                    case "REMOVEPON":
                        productBLL.RemoveGroupon(model.ProductId, this.User.CompanyId);
                        break;
                    case "PUBLISH":
                        itemBLL.ChangePublish(model.ProductId, this.User.CompanyId);
                        break;
                    case "ORDER":
                        itemBLL.ChangeOrder(model.ProductId, this.User.CompanyId, model.ProductOrders);
                        break;
                }
            }
            catch (BusinessException ex)
            {

            } 

            model.Products = productBLL.GetProducts(this.User.CompanyId, this.LanguageId, model.CatId).OrderByDescending(e => e.ID).ToList();
            foreach (var product in model.Products)
            {
                product.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.Image;
            }

            model.Groupons = productBLL.GetGroupons(this.User.CompanyId, this.LanguageId, model.CatId).OrderByDescending(e => e.ID).ToList();
            foreach (var groupon in model.Groupons)
            {
                groupon.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + groupon.Image;
            }

            return View(model);
        }

        public ActionResult Detail(int id = 0, string language = "", int catid = 0)
        {
            var model = new ProductDetailViewModel();
            model.CatId = catid;
            
            if (!string.IsNullOrEmpty(language))  this.LanguageId = language;
            model.CompanyId = this.User.CompanyId;

            if (id > 0)
            {
                model.Product = productBLL.GetProduct(this.User.CompanyId, this.LanguageId, id);
                if (model.Product != null)
                    model.Product.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + model.Product.Image;
                else model.Product = new Model.ProductModel();
            }
            else model.Product = new Model.ProductModel();

            if (model.Product.ID == 0)
            {
                model.Product.Orders = 50;
                model.Product.Publish = true;
                model.Product.Tag = string.Empty;
            }
            model.Product.LanguageId = this.LanguageId;

            var categories = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, "PRO", model.CatId)
                                .ToList();
            model.Categories = SortTable(categories, 0).Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.Blank + e.NAME }).ToList();

            model.Languages = companyBLL.GetLanguage()
                                .Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();

            model.Prices = productBLL.GetProducePrices(model.Product.ID).ToList();

            model.Colors = productBLL.GetProduceColors(model.Product.ID).ToList();
            foreach (var color in model.Colors)
            {
                color.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + color.ImageName;
            }

            model.Images = itemBLL.GetImages(model.Product.ID, this.User.CompanyId).ToList();
            foreach (var image in model.Images)
            {
                image.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + image.Image;
            }

            model.Attributes = productBLL.GetProductAttributes(model.Product.ID, this.User.CompanyId).ToList();

            model.Products = productBLL.GetProducts(this.User.CompanyId, this.LanguageId, model.CatId).OrderByDescending(e => e.ID).ToList();
            foreach (var product in model.Products)
            {
                product.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.Image;
            }

            model.RelatiedProducts = articleBLL.GetRelatiedArticles(model.Product.ID, this.User.CompanyId, this.LanguageId);
            foreach (var product in model.RelatiedProducts)
            {
                product.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ImagePath;
            }

            model.AddOns = productBLL.GetListProductAddOns(model.Product.ID, this.User.CompanyId, this.LanguageId);
            foreach (var product in model.AddOns)
            {
                product.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ImagePath;
            }

            model.Tags = productBLL.GetTagByCategoryId(this.User.CompanyId);

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Detail(ProductDetailViewModel model)
        {
            model.CompanyId = this.User.CompanyId;
            
            var categories = articleBLL.GetCategoryByType(this.User.CompanyId, model.Product.LanguageId, "PRO", model.CatId)
                                .ToList();
            model.Categories = SortTable(categories, 0).Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.Blank + e.NAME }).ToList();

            model.Languages = companyBLL.GetLanguage()
                                .Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();

            model.Attributes = productBLL.GetProductAttributes(model.Product.ID, this.User.CompanyId).ToList();

            //lưu
            var folder = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            
            var imageDetailAdds = new List<string>();
            var productColors = new List<ProductColorModel>();
            if (Request.Files.Count > 0)
            {
                try
                {
                    var random = GenerateRandomCode.RandomNumber(5);
                    var logo = Request.Files["productimage"];
                    if (logo.ContentLength > 0)
                    {
                        var oldImage = model.Product.Image;
                        model.Product.Image = string.Format("{0}_{1}.{2}", model.Product.Title.ConvertToUnSign(), random, logo.FileName.Split('.')[1]);
                        logo.SaveAs(folder + model.Product.Image);

                        if (model.Product.Image != oldImage && System.IO.File.Exists(folder + oldImage))
                            System.IO.File.Delete(folder + oldImage);
                    }

                    var detailImages = Request.Files.GetMultiple("images");
                    for (int i = 0; i < detailImages.Count; i++)
                    {
                        if (detailImages[i].ContentLength > 0)
                        {
                            var name = string.Format("{0}_{1}_{2}.{3}", model.Product.Title.ConvertToUnSign(), i, random, detailImages[i].FileName.Split('.')[1]);
                            detailImages[i].SaveAs(folder + name);
                            imageDetailAdds.Add(name);
                        }
                    }
                                        
                    var colorNames = Request["color_name"].Split(',');
                    var colorValues = Request["color_value"].Split(',');
                    var colorPrices = Request["color_price"].Split(',');
                    for (int i = 0; i < colorNames.Length; i++)
                    {
                        var color = new ProductColorModel();
                        color.Name = colorNames[i];
                        color.Value = colorValues[i];
                        decimal price = 0;
                        decimal.TryParse(colorPrices[i], out price);
                        color.Price = price;

                        var colorImage = Request.Files["color_image_" + i];
                        if (colorImage.ContentLength > 0)
                        {
                            var name = string.Format("color_{0}_{1}_{2}.{3}", model.Product.Title.ConvertToUnSign(), i, random, colorImage.FileName.Split('.')[1]);
                            colorImage.SaveAs(folder + name);
                            color.ImageName = name;
                        }
                        productColors.Add(color);
                    }
                    
                }
                catch (Exception ex)
                {
                    ViewBag.Danger = "Gửi file thật bại";
                }
            }

            //xóa hình chi tiết
            var imageDetailRemoves = Request["imagedetailremove"].Split('|').Where(e => e !="").ToList();
            foreach (var image in imageDetailRemoves)
            {
                if (!string.IsNullOrEmpty(image) && System.IO.File.Exists(folder + image))
                    System.IO.File.Delete(folder + image);
            }
            var detailRemoves = Request["detailremove"].Split('|').Where(e => e != "").ToList();

            // xóa hình màu sắc
            var colorImageRemoves = Request["imageremove"].Split('|').Where(e => e != "").ToList();
            foreach (var image in colorImageRemoves)
            {
                if (!string.IsNullOrEmpty(image) && System.IO.File.Exists(folder + image))
                    System.IO.File.Delete(folder + image);
            }

            var colorRemoves = Request["colorremove"].Split('|').Where(e => e != "").ToList();
            var productPrices = new List<ProductPriceModel>();
            if (Request["quatities"] != null)
            {
                var quatities = Request["quatities"].Split(',');
                var prices = Request["prices"].Split(',');
                for (int i = 0; i < quatities.Length; i++)
                {
                    var price = new ProductPriceModel();
                    price.Quantity = Convert.ToInt32(quatities[i]);
                    price.Price = Convert.ToDecimal(prices[i]);
                    productPrices.Add(price);
                }
            }

            foreach(var attribute in model.Attributes)
            {
                var value = Request["attribute_" + attribute.ID];
                attribute.Value = value;
            }

            if (string.IsNullOrEmpty(model.Product.Tag)) model.Product.Tag = model.Product.Title;

            var relatiedProducts = new List<int>();
            if (Request["relatied_id"] != null)
            {
                var ids = Request["relatied_id"].Split(',');
                for (int i = 0; i < ids.Length; i++)
                {
                    var id = Convert.ToInt32(ids[i]);
                    relatiedProducts.Add(id);
                }
            }

            var productAddOns = new List<ProductAddOnModel>();
            if (Request["addon_id"] != null)
            {
                var addonids = Request["addon_id"].Split(',');
                for (int i = 0; i < addonids.Length; i++)
                {
                    var addon = new ProductAddOnModel();
                    addon.ProductId = Convert.ToInt32(addonids[i]);
                    addon.Sale = Convert.ToDecimal(Request["addon_price_"+ addonids[i]]);
                    addon.Quantity = Convert.ToInt32(Request["addon_quantity_" + addonids[i]]);
                    productAddOns.Add(addon);
                }
            }

            model.Product.ID = productBLL.SaveProduct(model.Product, detailRemoves, imageDetailAdds, colorRemoves, productColors, productPrices, model.Attributes.ToList(), relatiedProducts, productAddOns, this.User.CompanyId, this.User.UserId);
            new URL().ClearCache(SettingsManager.Constants.AllDataCache + this.User.CompanyId);
            if (model.Product.ID > 0)
                model.Product.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + model.Product.Image;

            // SEO link
            var url = new URL();
            //url.CreateSEO(model.Product.ID,
            //                model.Product.Title.Trim(),
            //                model.Product.Tag.Trim(),
            //                model.Product.Brief.Trim(),
            //                this.User.CompanyId,
            //                "Product",
            //                model.Product.CategoryId,
            //                this.LanguageId);

            var seo = new SEOLinkModel();
            seo.RefItem = model.Product.ID;
            
            seo.Url = url.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/" + model.Product.ID + "/" + model.Product.Title.ConvertToUnSign());

            var config = this.companyBLL.GetConfig(this.User.CompanyId);
            if (config.Hierarchy)
            {
                var cat = categories.FirstOrDefault(e => e.ID == model.Product.CategoryId);
                if (cat != null) seo.SeoUrl = string.Format("/{0}/{1}", cat.NAME.Trim().ConvertToUnSign(), model.Product.Title.Trim().ConvertToUnSign());
                else seo.SeoUrl = "/" + model.Product.Title.Trim().ConvertToUnSign();
            }
            else seo.SeoUrl = "/" + model.Product.Title.Trim().ConvertToUnSign();

            seo.Title = model.Product.Title.Trim();
            seo.MetaKeyWork = model.Product.Tag.Trim();
            seo.MetaDescription = model.Product.Brief.DeleteHTMLTag().Trim();
            seo.MetaDescription = seo.MetaDescription.Length > 200 ? seo.MetaDescription.Substring(0, 200) : seo.MetaDescription;

            seoBLL.Save(seo, this.User.CompanyId, model.Product.LanguageId);
            new URL().ClearCache(SettingsManager.Constants.AppCompanyDomainMapCache);
            // end SEO link

            model.Prices =productBLL.GetProducePrices(model.Product.ID).ToList();

            model.Colors = productBLL.GetProduceColors(model.Product.ID).ToList();
            foreach (var color in model.Colors)
            {
                color.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + color.ImageName;
            }

            model.Images = itemBLL.GetImages(model.Product.ID, this.User.CompanyId).ToList();
            foreach (var image in model.Images)
            {
                image.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + image.Image;
            }

            model.Products = productBLL.GetProducts(this.User.CompanyId, this.LanguageId, model.CatId).OrderByDescending(e => e.ID).ToList();
            foreach (var product in model.Products)
            {
                product.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.Image;
            }

            model.RelatiedProducts = articleBLL.GetRelatiedArticles(model.Product.ID, this.User.CompanyId, this.LanguageId);
            foreach (var product in model.RelatiedProducts)
            {
                product.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ImagePath;
            }

            model.AddOns = productBLL.GetListProductAddOns(model.Product.ID, this.User.CompanyId, this.LanguageId);
            foreach (var product in model.AddOns)
            {
                product.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ImagePath;
            }

            model.Tags = productBLL.GetTagByCategoryId(this.User.CompanyId);

            ModelState.Clear();
            return View(model);
        }

        public ActionResult Groupon(int id = 0, string language = "", int catid = 0)
        {
            var model = new ProductGrouponViewModel();
            model.CatId = catid;

            if (!string.IsNullOrEmpty(language)) this.LanguageId = language;
            model.CompanyId = this.User.CompanyId;

            if (id > 0)
            {
                model.Product = productBLL.GetProduct(this.User.CompanyId, this.LanguageId, id);
                if (model.Product != null)
                    model.Product.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + model.Product.Image;
                else model.Product = new Model.ProductModel();

                model.Groupon = productBLL.GetGroupon(this.User.CompanyId, id);
                if (model.Groupon == null)
                {
                    model.Groupon = new GrouponModel();
                    model.Groupon.Quantity = 1;
                    model.Groupon.StartDate = DateTime.Now;
                    model.Groupon.EndDate = DateTime.Now;
                    model.Groupon.Price = 0;
                }
            }
            else
            {
                model.Groupon = new GrouponModel();
                model.Groupon.Quantity = 1;
                model.Groupon.StartDate = DateTime.Now;
                model.Groupon.EndDate = DateTime.Now;
                model.Groupon.Price = 0;
            }

            if (model.Product.ID == 0)
            {
                model.Product.Orders = 50;
                model.Product.Publish = true;
                model.Product.Tag = string.Empty;
            }
            model.Product.LanguageId = this.LanguageId;

            var categories = articleBLL.GetCategoryByType(this.User.CompanyId, this.LanguageId, "PRO", model.CatId)
                                .ToList();
            model.Categories = SortTable(categories, 0).Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.Blank + e.NAME }).ToList();

            model.Languages = companyBLL.GetLanguage()
                                .Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();

            model.Prices = productBLL.GetProducePrices(model.Groupon.ID).ToList();

            model.Colors = productBLL.GetProduceColors(model.Groupon.ID).ToList();
            foreach (var color in model.Colors)
            {
                color.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + color.ImageName;
            }

            model.Images = itemBLL.GetImages(model.Groupon.ID, this.User.CompanyId).ToList();
            foreach (var image in model.Images)
            {
                image.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + image.Image;
            }

            model.Attributes = productBLL.GetProductAttributes(model.Groupon.ID, this.User.CompanyId).ToList();

            model.Products = productBLL.GetProducts(this.User.CompanyId, this.LanguageId, model.CatId).OrderByDescending(e => e.ID).ToList();
            foreach (var product in model.Products)
            {
                product.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.Image;
            }

            model.RelatiedProducts = articleBLL.GetRelatiedArticles(model.Product.ID, this.User.CompanyId, this.LanguageId);
            foreach (var product in model.RelatiedProducts)
            {
                product.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ImagePath;
            }

            model.AddOns = productBLL.GetListProductAddOns(model.Product.ID, this.User.CompanyId, this.LanguageId);
            foreach (var product in model.AddOns)
            {
                product.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ImagePath;
            }

            model.Tags = productBLL.GetTagByCategoryId(this.User.CompanyId);

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Groupon(ProductGrouponViewModel model)
        {
            model.CompanyId = this.User.CompanyId;

            var categories = articleBLL.GetCategoryByType(this.User.CompanyId, model.Product.LanguageId, "PRO", model.CatId)
                                .ToList();
            model.Categories = SortTable(categories, 0).Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.Blank + e.NAME }).ToList();

            model.Languages = companyBLL.GetLanguage()
                                .Select(e => new SelectListItem { Value = e.ID.ToString(), Text = e.NAME })
                                .ToList();

            model.Attributes = productBLL.GetProductAttributes(model.Groupon.ID, this.User.CompanyId).ToList();

            //lưu
            var folder = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/") + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var imageDetailAdds = new List<string>();
            var productColors = new List<ProductColorModel>();
            if (Request.Files.Count > 0)
            {
                try
                {
                    var random = GenerateRandomCode.RandomNumber(5);
                    var logo = Request.Files["grouponimage"];
                    if (logo.ContentLength > 0)
                    {
                        var oldImage = model.Product.Image;
                        model.Product.Image = string.Format("{0}_{1}.{2}", model.Product.Title.ConvertToUnSign(), random, logo.FileName.Split('.')[1]);
                        logo.SaveAs(folder + model.Product.Image);

                        if (model.Product.Image != oldImage && System.IO.File.Exists(folder + oldImage))
                            System.IO.File.Delete(folder + oldImage);
                    }

                    var detailImages = Request.Files.GetMultiple("images");
                    for (int i = 0; i < detailImages.Count; i++)
                    {
                        if (detailImages[i].ContentLength > 0)
                        {
                            var name = string.Format("{0}_{1}_{2}.{3}", model.Product.Title.ConvertToUnSign(), i, random, detailImages[i].FileName.Split('.')[1]);
                            detailImages[i].SaveAs(folder + name);
                            imageDetailAdds.Add(name);
                        }
                    }

                    var colorNames = Request["color_name"].Split(',');
                    var colorValues = Request["color_value"].Split(',');
                    var colorPrices = Request["color_price"].Split(',');
                    for (int i = 0; i < colorNames.Length; i++)
                    {
                        var color = new ProductColorModel();
                        color.Name = colorNames[i];
                        color.Value = colorValues[i];
                        decimal price = 0;
                        decimal.TryParse(colorPrices[i], out price);
                        color.Price = price;

                        var colorImage = Request.Files["color_image_" + i];
                        if (colorImage.ContentLength > 0)
                        {
                            var name = string.Format("color_{0}_{1}_{2}.{3}", model.Product.Title.ConvertToUnSign(), i, random, colorImage.FileName.Split('.')[1]);
                            colorImage.SaveAs(folder + name);
                            color.ImageName = name;
                        }
                        productColors.Add(color);
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Danger = "Gửi file thật bại";
                }
            }

            //xóa hình chi tiết
            var imageDetailRemoves = Request["imagedetailremove"].Split('|').Where(e => e != "").ToList();
            foreach (var image in imageDetailRemoves)
            {
                if (!string.IsNullOrEmpty(image) && System.IO.File.Exists(folder + image))
                    System.IO.File.Delete(folder + image);
            }
            var detailRemoves = Request["detailremove"].Split('|').Where(e => e != "").ToList();

            // xóa hình màu sắc
            var colorImageRemoves = Request["imageremove"].Split('|').Where(e => e != "").ToList();
            foreach (var image in colorImageRemoves)
            {
                if (!string.IsNullOrEmpty(image) && System.IO.File.Exists(folder + image))
                    System.IO.File.Delete(folder + image);
            }

            var colorRemoves = Request["colorremove"].Split('|').Where(e => e != "").ToList();
            var productPrices = new List<ProductPriceModel>();
            if (Request["quatities"] != null)
            {
                var quatities = Request["quatities"].Split(',');
                var prices = Request["prices"].Split(',');
                for (int i = 0; i < quatities.Length; i++)
                {
                    var price = new ProductPriceModel();
                    price.Quantity = Convert.ToInt32(quatities[i]);
                    price.Price = Convert.ToDecimal(prices[i]);
                    productPrices.Add(price);
                }
            }

            foreach (var attribute in model.Attributes)
            {
                var value = Request["attribute_" + attribute.ID];
                attribute.Value = value;
            }

            if (string.IsNullOrEmpty(model.Product.Tag)) model.Product.Tag = model.Product.Title;

            var relatiedProducts = new List<int>();
            if (Request["relatied_id"] != null)
            {
                var ids = Request["relatied_id"].Split(',');
                for (int i = 0; i < ids.Length; i++)
                {
                    var id = Convert.ToInt32(ids[i]);
                    relatiedProducts.Add(id);
                }
            }

            var productAddOns = new List<ProductAddOnModel>();
            if (Request["addon_id"] != null)
            {
                var addonids = Request["addon_id"].Split(',');
                for (int i = 0; i < addonids.Length; i++)
                {
                    var addon = new ProductAddOnModel();
                    addon.ProductId = Convert.ToInt32(addonids[i]);
                    addon.Sale = Convert.ToDecimal(Request["addon_price_" + addonids[i]]);
                    addon.Quantity = Convert.ToInt32(Request["addon_quantity_" + addonids[i]]);
                    productAddOns.Add(addon);
                }
            }

            //model.Product.ID = productBLL.SaveProduct(model.Product, detailRemoves, imageDetailAdds, colorRemoves, productColors, productPrices, model.Attributes.ToList(), relatiedProducts, productAddOns, this.User.CompanyId, this.User.UserId);
            model.Groupon.ID = model.Product.ID = productBLL.SaveGroupon(model.Groupon, model.Product, detailRemoves, imageDetailAdds, colorRemoves, productColors, productPrices, model.Attributes.ToList(), relatiedProducts, productAddOns, this.User.CompanyId, this.User.UserId);

            new URL().ClearCache(SettingsManager.Constants.AllDataCache + this.User.CompanyId);
            if (model.Product.ID > 0)
                model.Product.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + model.Product.Image;

            // SEO link
            var url = new URL();

            var seo = new SEOLinkModel();
            seo.RefItem = model.Groupon.ID;

            seo.Url = url.LinkComponent("Product", SettingsManager.Constants.SendProduct + "/" + model.Groupon.ID + "/" + model.Product.Title.ConvertToUnSign());

            var config = this.companyBLL.GetConfig(this.User.CompanyId);
            if (config.Hierarchy)
            {
                var cat = categories.FirstOrDefault(e => e.ID == model.Product.CategoryId);
                if (cat != null) seo.SeoUrl = string.Format("/{0}/{1}", cat.NAME.Trim().ConvertToUnSign(), model.Product.Title.Trim().ConvertToUnSign());
                else seo.SeoUrl = "/" + model.Product.Title.Trim().ConvertToUnSign();
            }
            else seo.SeoUrl = "/" + model.Product.Title.Trim().ConvertToUnSign();

            seo.Title = model.Product.Title.Trim();
            seo.MetaKeyWork = model.Product.Tag.Trim();
            seo.MetaDescription = model.Product.Brief.DeleteHTMLTag().Trim();
            seo.MetaDescription = seo.MetaDescription.Length > 200 ? seo.MetaDescription.Substring(0, 200) : seo.MetaDescription;

            seoBLL.Save(seo, this.User.CompanyId, model.Product.LanguageId);
            new URL().ClearCache(SettingsManager.Constants.AppCompanyDomainMapCache);
            // end SEO link

            model.Prices = productBLL.GetProducePrices(model.Groupon.ID).ToList();

            model.Colors = productBLL.GetProduceColors(model.Groupon.ID).ToList();
            foreach (var color in model.Colors)
            {
                color.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + color.ImageName;
            }

            model.Images = itemBLL.GetImages(model.Groupon.ID, this.User.CompanyId).ToList();
            foreach (var image in model.Images)
            {
                image.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + image.Image;
            }

            model.Products = productBLL.GetProducts(this.User.CompanyId, this.LanguageId, model.CatId).OrderByDescending(e => e.ID).ToList();
            foreach (var product in model.Products)
            {
                product.PathImage = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.Image;
            }

            model.RelatiedProducts = articleBLL.GetRelatiedArticles(model.Product.ID, this.User.CompanyId, this.LanguageId);
            foreach (var product in model.RelatiedProducts)
            {
                product.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ImagePath;
            }

            model.AddOns = productBLL.GetListProductAddOns(model.Product.ID, this.User.CompanyId, this.LanguageId);
            foreach (var product in model.AddOns)
            {
                product.ImagePath = "/" + string.Format(SettingsManager.AppSettings.FolderUpload, this.User.CompanyId) + SettingsManager.Constants.PathProductImage + product.ImagePath;
            }

            model.Tags = productBLL.GetTagByCategoryId(this.User.CompanyId);

            ModelState.Clear();
            return View(model);
        }

        public ActionResult Attribute()
        {
            var model = new AttributeViewModel();

            model.AttributeCategories = productBLL.GetAttributeCategories(this.User.CompanyId)
                                    .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
                                    .ToList();
            model.Attributes = productBLL.GetAttributes(this.User.CompanyId).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Attribute(AttributeViewModel model)
        {
            model.AttributeCategories = productBLL.GetAttributeCategories(this.User.CompanyId)
                                     .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
                                     .ToList();

            switch (model.Action)
            {
                case "ADD":
                    productBLL.AddAttribute(model.Attribute, this.User.CompanyId);
                    break;
                case "UPDATE":
                    productBLL.UpdateAttribute(model.Attribute, this.User.CompanyId);
                    break;
                case "REMOVE":
                    productBLL.DeleteAttribute(model.Attribute.ID, this.User.CompanyId);
                    break;
            }

            model.Attributes = productBLL.GetAttributes(this.User.CompanyId).ToList();

            return View(model);
        }

        public ActionResult AttributeCategory()
        {
            var model = new AttributeCategoryViewModel();
            model.Categories = productBLL.GetAttributeCategories(this.User.CompanyId).ToList();
            model.Values = productBLL.GetAttributeValues(this.User.CompanyId).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult AttributeCategory(AttributeCategoryViewModel model)
        {
            var seo = new SEOLinkModel();
            var url = new URL();
            var cat = new AttributeCategoryModel();

            switch (model.Action)
            {
                case "ADDCAT":
                    productBLL.AddAttributeCategory(model.Category, this.User.CompanyId, this.User.UserId);
                    break;
                case "UPDATECAT":
                    productBLL.UpdateAttributeCategory(model.Category, this.User.CompanyId, this.User.UserId);
                    break;
                case "REMOVECAT":
                    productBLL.DeleteAttributeCategory(model.Category.Id, this.User.CompanyId);

                    seoBLL.Delete(model.Category.Id);
                    new URL().ClearCache(SettingsManager.Constants.AppCompanyDomainMapCache);
                    new URL().ClearCache(SettingsManager.Constants.AllDataCache + this.User.CompanyId);
                    break;
                case "ADDVALUE":
                    model.Value.Id = productBLL.AddAttributeValue(model.Value, this.User.CompanyId, this.User.UserId);

                    // SEO link
                    cat = productBLL.GetAttributeCategory(model.Value.CategoryId, this.User.CompanyId);
                    seo.Url = url.LinkComponent("Products", SettingsManager.Constants.SendAttributeId + "/" + model.Value.CategoryId + "/" + SettingsManager.Constants.SendAttributeValue + "/" + model.Value.Id + "/" + model.Value.Value.ConvertToUnSign());
                    seo.SeoUrl = "/" + cat.Name.Trim().ConvertToUnSign() + "-" + model.Value.Value.Trim().ConvertToUnSign();
                    seo.Title = cat.Name.Trim() + " - " + model.Value.Value.Trim();
                    seo.MetaKeyWork = cat.Name.Trim() + ", " + model.Value.Value.Trim();
                    seo.MetaDescription = seo.MetaKeyWork;
                    seo.RefItem = model.Value.Id;

                    seoBLL.Save(seo, this.User.CompanyId, SettingsManager.Constants.DefauleLanguage);
                    new URL().ClearCache(SettingsManager.Constants.AppCompanyDomainMapCache);

                    break;
                case "UPDATEVALUE":
                    productBLL.UpdateAttributeValue(model.Value, this.User.CompanyId, this.User.UserId);

                    // SEO link
                    cat = productBLL.GetAttributeCategory(model.Value.CategoryId, this.User.CompanyId);
                    seo.Url = url.LinkComponent("Products", SettingsManager.Constants.SendAttributeId + "/" + model.Value.CategoryId + "/" + SettingsManager.Constants.SendAttributeValue + "/" + model.Value.Id + "/" + model.Value.Value.ConvertToUnSign());
                    seo.SeoUrl = "/" + cat.Name.Trim().ConvertToUnSign() + "-" + model.Value.Value.Trim().ConvertToUnSign();
                    seo.Title = cat.Name.Trim() + " - " + model.Value.Value.Trim();
                    seo.MetaKeyWork = cat.Name.Trim() + ", " + model.Value.Value.Trim();
                    seo.MetaDescription = seo.MetaKeyWork;
                    seo.RefItem = model.Value.Id;

                    seoBLL.Save(seo, this.User.CompanyId, SettingsManager.Constants.DefauleLanguage);
                    new URL().ClearCache(SettingsManager.Constants.AppCompanyDomainMapCache);

                    break;
                case "REMOVEVALUE":
                    productBLL.DeleteAttributeValue(model.Value.Id, this.User.CompanyId);

                    seoBLL.Delete(model.Value.Id);
                    new URL().ClearCache(SettingsManager.Constants.AppCompanyDomainMapCache);
                    new URL().ClearCache(SettingsManager.Constants.AllDataCache + this.User.CompanyId);
                    break;
            }

            model.Categories = productBLL.GetAttributeCategories(this.User.CompanyId).ToList();
            model.Values = productBLL.GetAttributeValues(this.User.CompanyId).ToList();

            return View(model);
        }

        public ActionResult Voucher()
        {
            var model = new VoucherViewModel();
            model.Vouchers = productBLL.GetVouchers(this.User.CompanyId).ToList();

            return View(model);
        }
 
        [HttpPost]
        public ActionResult Voucher(VoucherViewModel model)
        {
            switch (model.Action)
            {
                case "PUBLISH":
                    itemBLL.ChangePublish(model.Voucher.Id, this.User.CompanyId);
                    break;
                case "ADD":
                    productBLL.AddVoucher(model.Voucher, this.User.CompanyId, this.User.UserId);
                    break;
                case "UPDATE":
                    productBLL.UpdateVoucher(model.Voucher, this.User.CompanyId, this.User.UserId);
                    break;
                case "REMOVE":
                    itemBLL.Delete(model.Voucher.Id, this.User.CompanyId);
                    break;
            }

            model.Vouchers = productBLL.GetVouchers(this.User.CompanyId).ToList();

            return View(model);
        }

        private string GetCategoryName(string typeID)
        {
            switch (typeID.ToUpper())
            {
                case "ART": return "bài viết";
                case "PRO": return "sản phẩm";
                case "WH": return "kho hàng";
                case "SPO": return "hỗ trợ";
                case "LIN": return "liên kết";
                case "DOC": return "tài liệu";
                case "MID": return "media";
                default: return string.Empty;
            }
        }

        private IList<CATEGORYLANGUAGEModel> SortTable(IList<CATEGORYLANGUAGEModel> table, int parentId, string space = "", string distance = "....")
        {
            var rows = table.Where(dto => dto.PARENTID == parentId).ToList();
            if (!rows.Any()) return new List<CATEGORYLANGUAGEModel>();
            var sortData = new List<CATEGORYLANGUAGEModel>();
            foreach (var row in rows)
            {
                var spaceNext = space + distance;
                var dt = SortTable(table, row.ID, spaceNext, distance);
                row.Blank = space;
                //row.Title = space + row.Title;
                sortData.Add(row);

                if (dt.Count > 0) sortData.AddRange(dt);
            }
            return sortData;
        }

    }
}