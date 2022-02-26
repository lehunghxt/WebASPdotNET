
using System;
using System.Collections.Generic;
using System.Linq;
using Library;
using Web.Data;
using Web.Data.DataAccess;
using Web.Model;

namespace Web.Business
{
    public class ProductBLL : BaseBLL
    {
        private ArticleDAL articleDAL;
        private ArticleLanguageDAL articleLanguageDAL;
        private ProductDAL productDAL;
        private ItemDAL itemDAL;
        private ItemCommentDAL commentDAL;
        private ItemImageDAL imageDAL;
        private SEODAL seoDAL;
        private AttributeDAL attributeDAL;
        private AttributeValueDAL attributeValueDAL;
        private AttributeCategoryDAL attributeCategoryDAL;
        private ProductAttributeDAL productAttributeDAL;
        private ProductPriceDAL productPriceDAL;
        private ProductColorDAL productColorDAL;
        private ProductGrouponDAL productGrouponDAL;
        private OrderDAL orderDAL;
        private OrderProductDAL orderCollectionDAL;
        private OrderTransactionDAL orderTransactionDAL;
        private WarehouseIODAL warehouseIODAL;
        private WarehouseIOColectionDAL warehouseCollectionDAL;
        private SupplierDAL supplierDAL;
        private CategoryDAL categoryDAL;
        private CategoryLanguageDAL categoryLanguageDAL;
        private VoucherDAL voucherDAL;
        private CustomerDAL customerDAL;
        private CustomerPointDAL customerPointDAL;
        private CompanyUserDAL userDAL;
        private ConfigMemberPointDAL pointDAL;
        private IProductAddOnDAL addonDAL;
        private IArticleRelatiedDAL relatiedDAL;
        private IWebConfigDAL configDAL;

        public ProductBLL(string connectionString = "")
            : base(connectionString)
        {
            articleDAL = new ArticleDAL(this.DatabaseFactory);
            articleLanguageDAL = new ArticleLanguageDAL(this.DatabaseFactory);
            productDAL = new ProductDAL(this.DatabaseFactory);
            itemDAL = new ItemDAL(this.DatabaseFactory);
            commentDAL = new ItemCommentDAL(this.DatabaseFactory);
            imageDAL = new ItemImageDAL(this.DatabaseFactory);
            seoDAL = new SEODAL(this.DatabaseFactory);
            attributeDAL = new AttributeDAL(this.DatabaseFactory);
            attributeCategoryDAL = new AttributeCategoryDAL(this.DatabaseFactory);
            attributeValueDAL = new AttributeValueDAL(this.DatabaseFactory);
            productAttributeDAL = new ProductAttributeDAL(this.DatabaseFactory);
            productPriceDAL = new ProductPriceDAL(this.DatabaseFactory);
            productColorDAL = new ProductColorDAL(this.DatabaseFactory);
            productGrouponDAL = new ProductGrouponDAL(this.DatabaseFactory);
            orderDAL = new OrderDAL(this.DatabaseFactory);
            orderCollectionDAL = new OrderProductDAL(this.DatabaseFactory);
            warehouseIODAL = new WarehouseIODAL(this.DatabaseFactory);
            warehouseCollectionDAL = new WarehouseIOColectionDAL(this.DatabaseFactory);
            supplierDAL = new SupplierDAL(this.DatabaseFactory);
            categoryDAL = new CategoryDAL(this.DatabaseFactory);
            categoryLanguageDAL = new CategoryLanguageDAL(this.DatabaseFactory);
            orderTransactionDAL = new OrderTransactionDAL(this.DatabaseFactory);
            voucherDAL = new VoucherDAL(this.DatabaseFactory);
            customerDAL = new CustomerDAL(this.DatabaseFactory);
            customerPointDAL = new CustomerPointDAL(this.DatabaseFactory);
            userDAL = new CompanyUserDAL(this.DatabaseFactory);
            pointDAL = new ConfigMemberPointDAL(this.DatabaseFactory);
            addonDAL = new ProductAddOnDAL(this.DatabaseFactory);
            relatiedDAL = new ArticleRelatiedDAL(this.DatabaseFactory);
            configDAL = new WebConfigDAL(this.DatabaseFactory);
        }

        #region product
        public List<DataSimpleModel> SearchDataSimple(int companyId, string languageId, string key, out int totalPage, int Skip = 0, int Take = 0)
        {
            var dtos = this.articleLanguageDAL.GetAll()
                .Where(e => e.Article.Item.CompanyId == companyId && e.LanguageId == languageId && e.Article.Item.IsPublished)
                .Select(e => new
                {
                    e.ArticleId,
                    e.Title,
                    e.Brief,
                    e.Contents,
                    e.Article.Product.Code,
                    e.Article.Image,
                    e.Article.CategoryId,
                    e.Article.Item.Orders,
                    e.Article.Item.ModifyDate
                })
                .ToList();

            var query = dtos.Where(e => (!string.IsNullOrEmpty(e.Code) && e.Code.Trim().ConvertToUnSign().Replace("-", "").Replace(" ", "").ToLower().Contains(key)) || e.Title.Trim().ConvertToUnSign().Replace("-", "").Replace(" ", "").ToLower().Contains(key));

            var selected = query.OrderBy(e => e.Orders)
                .ThenByDescending(e => e.ModifyDate)
                .Select(a => new DataSimpleModel
                {
                    ID = a.ArticleId,
                    CategoryId = a.CategoryId,
                    Title = a.Title,
                    Description = a.Brief,
                    ImagePath = a.Image
                });

            totalPage = selected.Count();
            if (Skip > 0) selected = selected.Skip(Skip);
            if (Take > 0) selected = selected.Take(Take);

            var data = selected.ToList();

            return data;
        }

        public List<ProductWebModel> Search(int companyId, string languageId, string key, out int totalPage, int Skip = 0, int Take = 0, bool showMinPrice = false)
        {
            var dtos = this.articleLanguageDAL.GetAll()
                .Where(e => e.Article.Item.CompanyId == companyId && e.LanguageId == languageId && e.Article.Item.IsPublished)
                .OrderBy(e => e.Article.Item.Orders).ThenByDescending(e => e.Article.Item.ModifyDate)
                .Select(a => new ProductWebModel
                {
                    ID = a.ArticleId,
                    Title = a.Title,
                    ImageName = a.Article.Image,
                    Brief = a.Brief,
                    Description = a.Contents,
                    Price = a.Article.Product.Price,
                    Sale = a.Article.Product.Sale,
                    VoteNumber = a.Article.Item.ItemVote == null ? 1 : a.Article.Item.ItemVote.VoteNumber,
                    VoteRate = a.Article.Item.ItemVote == null ? 1 : a.Article.Item.ItemVote.VoteRate,
                    ViewNumber = a.Article.Item.ItemView == null ? 1 : a.Article.Item.ItemView.Views,
                })
                .ToList();

            var query = dtos.Where(e => (!string.IsNullOrEmpty(e.Code) && e.Code.Trim().ConvertToUnSign().Replace("-", "").Replace(" ", "").ToLower().Contains(key)) || e.Title.Trim().ConvertToUnSign().Replace("-", "").Replace(" ", "").ToLower().Contains(key));

            totalPage = query.Count();
            if (Skip > 0) query = query.Skip(Skip);
            if (Take > 0) query = query.Take(Take);

            var data = query.ToList();
            var productOrders = this.orderCollectionDAL.GetAll().Where(e => e.Order.CompanyId == companyId).Select(e => new { e.Quantity, e.Order.CompanyId, e.ProductId }).ToList();
            var productIds = data.Select(d => d.ID).ToList();
            var productPrices = showMinPrice == false ? null : this.productPriceDAL.GetAll().Where(e => productIds.Contains(e.ProductId)).Select(e => new { e.ProductId, e.Price }).ToList();
            foreach (var dto in data)
            {
                dto.PayNumber = productOrders.Where(e => e.CompanyId == companyId && e.ProductId == dto.ID).Select(e => e.Quantity).DefaultIfEmpty(0).Sum();
                dto.MinPrice = dto.Sale == 0 ? dto.Price : dto.Sale;

                if (productPrices != null)
                {
                    var price = productPrices.Where(e => e.ProductId == dto.ID).Select(e => e.Price).DefaultIfEmpty(0).Min();
                    if (price != 0) dto.MinPrice = price;
                }
            }

            return data;
        }

        public OrderProductModel GetProductCartsById(int id, string language, int companyId)
        {
            var query = this.articleLanguageDAL.GetAll()
                .Where(c => c.ArticleId == id && c.LanguageId == language && c.Article.Item.IsPublished && c.Article.Item.CompanyId == companyId)
                .OrderBy(a => a.Article.Item.Orders)
                .Select(o => new OrderProductModel
                {
                    ProductId = o.ArticleId,
                    ProductCode = o.Article.Product.Code,
                    ProductName = o.Title,
                    ProductImage = o.Article.Image,
                    Price = o.Article.Product.Sale == 0 ? o.Article.Product.Price : o.Article.Product.Sale
                });

            var data = query.FirstOrDefault();

            return data;
        }

        public IList<ProductWebModel> GetListProductTheSameCategory(int productId, int companyId, out int totalPage, int skip = 0, int take = 0, string languageId = "vi-VN", bool showMinPrice = false)
        {
            var product = this.articleDAL.Get(e => e.Id == productId);
            if (product == null)
            {
                totalPage = 0;
                return new List<ProductWebModel>();
            } 

            var query = this.QueryProduct(companyId, product.CategoryId, false, productId, "");

            var articles = query.Select(a => new ProductWebModel
            {
                ID = a.Id,
                Code = a.Product.Code,
                ImageName = a.Image,
                CreateDate = a.Item.ModifyDate,
                Price = a.Product.Price,
                Sale = a.Product.Sale,
                SaleMin = a.Product.SaleMin,
                Quantity = a.Product.Quantity,
                VoteNumber = a.Item.ItemVote == null ? 1 : a.Item.ItemVote.VoteNumber,
                VoteRate = a.Item.ItemVote == null ? 1 : a.Item.ItemVote.VoteRate,
                ViewNumber = a.Item.ItemView == null ? 1 : a.Item.ItemView.Views
            }).ToList();

            totalPage = articles.Count();
            var r = articles.Skip(skip);
            if (take > 0) r = r.Take(take);

            var data = r.ToList();
            var productOrders = this.orderCollectionDAL.GetAll().Where(e => e.Order.CompanyId == companyId).Select(e => new { e.Quantity, e.Order.CompanyId, e.ProductId }).ToList();
            var productIds = data.Select(d => d.ID).ToList();

            var productPrices = showMinPrice == false ? null : this.productPriceDAL.GetAll().Where(e => productIds.Contains(e.ProductId)).Select(e => new { e.ProductId, e.Price }).ToList();
            var langs = articleLanguageDAL.GetAll()
                                .Where(e => productIds.Contains(e.ArticleId))
                                .Select(e => new {e.ArticleId, e.LanguageId, e.Title, e.Brief, e.Contents})
                                .ToList();

            var defaultLanguage = configDAL.GetAll().Where(e => e.Id == companyId)
                                              .Select(e => new { e.DefaultLanguage, e.DefaultLanguageIfNotSet })
                                              .FirstOrDefault();

            foreach (var dto in data)
            {
                dto.PayNumber = productOrders.Where(e => e.CompanyId == companyId && e.ProductId == dto.ID).Select(e => e.Quantity).DefaultIfEmpty(0).Sum();
                dto.MinPrice = dto.Sale == 0 ? dto.Price : dto.Sale;
                if (dto.Brief == null) dto.Brief = string.Empty;

                if (productPrices != null)
                {
                    var price = productPrices.Where(e => e.ProductId == dto.ID).Select(e => e.Price).DefaultIfEmpty(0).Min();
                    if (price != 0) dto.MinPrice = price;
                }

                var lang = langs.FirstOrDefault(e => e.ArticleId == dto.ID && e.LanguageId == languageId);
                if (lang != null)
                {
                    dto.Title = lang.Title;
                    dto.Brief = lang.Brief;
                    dto.Description = lang.Contents;
                }
                else if (defaultLanguage.DefaultLanguageIfNotSet)
                {
                    lang = langs.FirstOrDefault(e => e.ArticleId == dto.ID && e.LanguageId == defaultLanguage.DefaultLanguage);
                    if (lang == null) lang = langs.FirstOrDefault(e => e.ArticleId == dto.ID);
                    if (lang != null)
                    {
                        dto.Title = lang.Title;
                        dto.Brief = lang.Brief;
                        dto.Description = lang.Contents;
                    }
                }
                else
                {
                    dto.Title = dto.Brief = dto.Description = dto.Code;
                }
            }

            return data;
        }
        
        public IList<ProductWebModel> GetListProductWithImageAndPriceAndVote(int companyId, out int totalPage, int skip = 0, int take = 0, int categoryId = 0, bool inChildCat = false, int ortherId = 0, string colorId = "", string languageId = "vi-VN", bool showMinPrice = false, string orderBy = "", int atrId = 0, string atrValue = "", string search = "")
        {
            var query = this.QueryProduct(companyId, categoryId, inChildCat, ortherId, colorId, atrId, atrValue);

            var products = query.Select(a => new ProductWebModel
            {
                ID = a.Id,
                Code = a.Product.Code,
                ImageName = a.Image,
                CreateDate = a.Item.ModifyDate,
                Price = a.Product.Price,
                Sale = a.Product.Sale,
                SaleMin = a.Product.SaleMin,
                Quantity = a.Product.Quantity,
                VoteNumber = a.Item.ItemVote == null ? 1 : a.Item.ItemVote.VoteNumber,
                VoteRate = a.Item.ItemVote == null ? 1 : a.Item.ItemVote.VoteRate,
                ViewNumber = a.Item.ItemView == null ? 1 : a.Item.ItemView.Views,

                // luu tam tag phuc vu tim kiem
                ImagePath = a.Tag
            });

            var data = products.ToList();

            var productOrders = this.orderCollectionDAL.GetAll().Where(e => e.Order.CompanyId == companyId).Select(e => new { e.Quantity, e.Order.CompanyId, e.ProductId }).ToList();
            var productIds = data.Select(d => d.ID).ToList();
            var productPrices = showMinPrice == false ? null : this.productPriceDAL.GetAll().Where(e => productIds.Contains(e.ProductId)).Select(e => new { e.ProductId, e.Price }).ToList();
            var langs = articleLanguageDAL.GetAll()
                                .Where(e => productIds.Contains(e.ArticleId))
                                .Select(e => new { e.ArticleId, e.LanguageId, e.Title, e.Brief, e.Contents })
                                .ToList();

            var defaultLanguage = configDAL.GetAll().Where(e => e.Id == companyId)
                                              .Select(e => new { e.DefaultLanguage, e.DefaultLanguageIfNotSet })
                                              .FirstOrDefault();

            foreach (var dto in data)
            {
                dto.PayNumber = productOrders.Where(e => e.CompanyId == companyId && e.ProductId == dto.ID).Select(e => e.Quantity).DefaultIfEmpty(0).Sum();
                dto.MinPrice = dto.Sale == 0 ? dto.Price : dto.Sale;
                if (dto.Brief == null) dto.Brief = string.Empty;

                if (productPrices != null)
                {
                    var price = productPrices.Where(e => e.ProductId == dto.ID).Select(e => e.Price).DefaultIfEmpty(0).Min();
                    if (price != 0) dto.MinPrice = price;
                }

                var lang = langs.FirstOrDefault(e => e.ArticleId == dto.ID && e.LanguageId == languageId);
                if (lang != null)
                {
                    dto.Title = lang.Title;
                    dto.Brief = lang.Brief;
                    dto.Description = lang.Contents;
                }
                else if (defaultLanguage.DefaultLanguageIfNotSet)
                {
                    lang = langs.FirstOrDefault(e => e.ArticleId == dto.ID && e.LanguageId == defaultLanguage.DefaultLanguage);
                    if (lang == null) lang = langs.FirstOrDefault(e => e.ArticleId == dto.ID);
                    if (lang != null)
                    {
                        dto.Title = lang.Title;
                        dto.Brief = lang.Brief;
                        dto.Description = lang.Contents;
                    }
                }
                else
                {
                    dto.Title = dto.Brief = dto.Description = dto.Code;
                }
            }

            if (!string.IsNullOrEmpty(search))
            {
                var key = search.ToLower();
                if (search.Any(e => e == ' ')) search.ConvertToUnSign();
                data = data.Where(e => e.Title.ConvertToUnSign().ToLower().Contains(key) || e.ImagePath.ConvertToUnSign().ToLower().Contains(key) || e.Brief.ToLower().ConvertToUnSign().Contains(key)).ToList();
            }

            totalPage = data.Count;

            if (!string.IsNullOrEmpty(orderBy))
            {
                var os = data.OrderByDescending(o => o.ID);
                var orders = orderBy.Split(',').Select(o => o.Trim().ToUpper()).ToList();
                var first = true;
                foreach (var order in orders)
                {
                    switch (order)
                    {
                        case "ID": os = first ? data.OrderByDescending(o => o.ID) : os.ThenByDescending(o => o.ID); break;
                        case "PAYMENT": os = first ? data.OrderByDescending(o => o.PayNumber / ((DateTime.Now - o.CreateDate).TotalDays + 1)) : os.ThenByDescending(o => o.PayNumber / ((DateTime.Now - o.CreateDate).TotalDays + 1)); break;
                        case "VIEW": os = first ? data.OrderByDescending(o => o.ViewNumber / ((DateTime.Now - o.CreateDate).TotalDays + 1)) : os.ThenByDescending(o => o.ViewNumber / ((DateTime.Now - o.CreateDate).TotalDays + 1)); break;
                        case "VOTE": os = first ? data.OrderByDescending(o => o.Vote) : os.ThenByDescending(o => o.Vote); break;
                        case "DEAL": os = first ? data.OrderBy(o => (o.Sale == 0 || o.Price == 0) ? decimal.MaxValue : o.Sale / o.Price) : os.ThenBy(o => (o.Sale == 0 || o.Price == 0) ? decimal.MaxValue : o.Sale / o.Price); break;
                        case "PRICEMIN": os = first ? data.OrderBy(o => (o.Sale == null || o.Sale == 0) ? o.Price : o.Sale) : os.ThenBy(o => (o.Sale == null || o.Sale == 0) ? o.Price : o.Sale); break;
                        case "PRICEMAX": os = first ? data.OrderByDescending(o => (o.Sale == null || o.Sale == 0) ? o.Price : o.Sale) : os.ThenByDescending(o => (o.Sale == null || o.Sale == 0) ? o.Price : o.Sale); break;
                        default: break;
                    }

                    first = false;
                }

                data = os.ToList();
            }

            var r = data.Skip(skip);
            if (take > 0) r = r.Take(take);

            return r.ToList();
        }
        public IList<ProductGrouponModel> GetListGrouponWithImageAndPrice(int companyId, DateTime fromDate, out int totalPage, int skip = 0, int take = 0, int categoryId = 0, bool inChildCat = false, int ortherId = 0, string colorId = "", string languageId = "vi-VN", bool showMinPrice = false, string orderBy = "", int atrId = 0, string atrValue = "", string search = "")
        {
            var query = this.QueryProduct(companyId, categoryId, inChildCat, ortherId, colorId, atrId, atrValue, true);

            var products = query.Select(a => new ProductGrouponModel
            {
                ID = a.Id,

                Code = a.Product.Code,
                Image = a.Image,
                Price = a.Product.Price,
                SaleMin = a.Product.SaleMin,
                Tag = a.Tag,

                Quantity = a.Product.ProductGroupon.Quantity,
                StartDate = a.Product.ProductGroupon.StartDate,
                EndDate = a.Product.ProductGroupon.EndDate,
                Sale = a.Product.ProductGroupon.Price,

                VoteNumber = a.Item.ItemVote == null ? 1 : a.Item.ItemVote.VoteNumber,
                VoteRate = a.Item.ItemVote == null ? 1 : a.Item.ItemVote.VoteRate,
                ViewNumber = a.Item.ItemView == null ? 1 : a.Item.ItemView.Views,
            });

            var data = products.ToList();
            if (!string.IsNullOrEmpty(search))
            {
                var key = search.ToLower();
                if (search.Any(e => e == ' ')) search.ConvertToUnSign();
                data = data.Where(e => e.Title.ConvertToUnSign().ToLower().Contains(key) || e.Tag.ConvertToUnSign().ToLower().Contains(key) || e.Brief.ToLower().ConvertToUnSign().Contains(key)).ToList();
            }

            var productOrders = this.orderCollectionDAL.GetAll().Where(e => e.Order.CompanyId == companyId).Select(e => new { e.Quantity, e.Order.CompanyId, e.ProductId, e.Order.CreateDate }).ToList();
            var productIds = data.Select(d => d.ID).ToList();
            //var productPrices = showMinPrice == false ? null : this.productPriceDAL.GetAll().Where(e => productIds.Contains(e.ProductId)).Select(e => new { e.ProductId, e.Price }).ToList();
            var langs = articleLanguageDAL.GetAll()
                                .Where(e => productIds.Contains(e.ArticleId))
                                .Select(e => new { e.ArticleId, e.LanguageId, e.Title, e.Brief, e.Contents })
                                .ToList();

            var defaultLanguage = configDAL.GetAll().Where(e => e.Id == companyId)
                                              .Select(e => new { e.DefaultLanguage, e.DefaultLanguageIfNotSet })
                                              .FirstOrDefault();

            foreach (var dto in data)
            {
                dto.PayNumber = productOrders.Where(e => e.CompanyId == companyId && e.ProductId == dto.ID).Select(e => e.Quantity).DefaultIfEmpty(0).Sum();
                //dto.MinPrice = dto.Sale == 0 ? dto.Price : dto.Sale;
                if (dto.Brief == null) dto.Brief = string.Empty;

                //if (productPrices != null)
                //{
                //    var price = productPrices.Where(e => e.ProductId == dto.ID).Select(e => e.Price).DefaultIfEmpty(0).Min();
                //    if (price != 0) dto.MinPrice = price;
                //}

                var lang = langs.FirstOrDefault(e => e.ArticleId == dto.ID && e.LanguageId == languageId);
                if (lang != null)
                {
                    dto.Title = lang.Title;
                    dto.Brief = lang.Brief;
                    //dto.Description = lang.Contents;
                }
                else if (defaultLanguage.DefaultLanguageIfNotSet)
                {
                    lang = langs.FirstOrDefault(e => e.ArticleId == dto.ID && e.LanguageId == defaultLanguage.DefaultLanguage);
                    if (lang == null) lang = langs.FirstOrDefault(e => e.ArticleId == dto.ID);
                    if (lang != null)
                    {
                        dto.Title = lang.Title;
                        dto.Brief = lang.Brief;
                        //dto.Description = lang.Contents;
                    }
                }
            }

            foreach (var dto in data)
            {
                dto.PayNumber = productOrders.Where(e => e.CompanyId == companyId && e.ProductId == dto.ID && dto.StartDate <= e.CreateDate && e.CreateDate <= dto.EndDate).Select(e => e.Quantity).DefaultIfEmpty(0).Sum();
            }

            data = data.Where(e => e.PayNumber < e.Quantity).ToList();

            totalPage = data.Count;

            if (!string.IsNullOrEmpty(orderBy))
            {
                var os = data.OrderByDescending(o => o.ID);
                var orders = orderBy.Split(',').Select(o => o.Trim().ToUpper()).ToList();
                var first = true;
                foreach (var order in orders)
                {
                    switch (order)
                    {
                        case "ID": os = first ? data.OrderByDescending(o => o.ID) : os.ThenByDescending(o => o.ID); break;
                        case "PAYMENT": os = first ? data.OrderByDescending(o => o.PayNumber) : os.ThenByDescending(o => o.PayNumber); break;
                        case "VIEW": os = first ? data.OrderByDescending(o => o.ViewNumber) : os.ThenByDescending(o => o.ViewNumber); break;
                        case "VOTE": os = first ? data.OrderByDescending(o => o.Vote) : os.ThenByDescending(o => o.Vote); break;
                        case "DEAL": os = first ? data.OrderBy(o => (o.Sale == 0 || o.Price == 0) ? decimal.MaxValue : o.Sale / o.Price) : os.ThenBy(o => (o.Sale == 0 || o.Price == 0) ? decimal.MaxValue : o.Sale / o.Price); break;
                        case "PRICEMIN": os = first ? data.OrderBy(o => (o.Sale == null || o.Sale == 0) ? o.Price : o.Sale) : os.ThenBy(o => (o.Sale == null || o.Sale == 0) ? o.Price : o.Sale); break;
                        case "PRICEMAX": os = first ? data.OrderByDescending(o => (o.Sale == null || o.Sale == 0) ? o.Price : o.Sale) : os.ThenByDescending(o => (o.Sale == null || o.Sale == 0) ? o.Price : o.Sale); break;
                        default: break;
                    }

                    first = false;
                }

                data = os.ToList();
            }

            var r = data.Skip(skip);
            if (take > 0) r = r.Take(take);

            return r.ToList();
        }

        public IQueryable<ProductModel> GetProducts(int companyId, string language, int catId= 0)
        {
            var query = articleDAL.GetAll().Where(e => e.Item.CompanyId == companyId && e.Category.TypeId == "PRO");

            if (catId > 0)
            {
                var listCategory = GetAllChildId(catId, companyId);
                listCategory.Add(catId);
                query = query.Where(e => listCategory.Contains(e.CategoryId));
            }

            var products = query.Select(e => new ProductModel
            {
                ID = e.Id,               
                CategoryId = e.CategoryId,
                Image = e.Image,
                Orders = e.Item.Orders,
                Publish = e.Item.IsPublished,
                CategoryName = e.Category.CategoryLanguages.FirstOrDefault(c => c.LanguageId == language).Title,
                Code = e.Product.Code,
                Price = e.Product.Price,
                Sale = e.Product.Sale,
                Quantity = e.Product.Quantity,
                SaleMin = e.Product.SaleMin,
                VoteNumber = e.Product.Article.Item.ItemVote == null ? 1 : e.Product.Article.Item.ItemVote.VoteNumber,
                VoteRate = e.Product.Article.Item.ItemVote == null ? 1 : e.Product.Article.Item.ItemVote.VoteRate,
                ViewNumber = e.Product.Article.Item.ItemView == null ? 1 : e.Product.Article.Item.ItemView.Views,
            }).ToList();

            var ids = products.Select(e => e.ID).ToList();
            var langs = articleLanguageDAL.GetAll()
                                .Where(e => ids.Contains(e.ArticleId))
                                .Select(e => new ARTICLELANGUAGEModel
                                {
                                    ID = e.ArticleId,
                                    LANGUAGEID = e.LanguageId,
                                    TITLE = e.Title,
                                    BRIEF = e.Brief,
                                    CONTENT = e.Contents
                                }).ToList();

           foreach(var article in products)
            {
                var lang = langs.FirstOrDefault(e => e.ID == article.ID && e.LANGUAGEID == language);
                if (lang != null)
                {
                    article.LanguageId = lang.LANGUAGEID;
                    article.Title = lang.TITLE;
                    article.Brief = lang.BRIEF;
                    article.Contents = lang.CONTENT;
                }

                article.Languages = string.Join(", ", langs.Where(e => e.ID == article.ID).Select(e => e.LANGUAGEID));
            }

            return products.AsQueryable();
        }
        public IQueryable<ProductGrouponModel> GetGroupons(int companyId, string language, int catId = 0)
        {
            var query = productGrouponDAL.GetAll()
                                .Where(e => e.Product.Article.Item.CompanyId == companyId && e.Product.Article.Category.TypeId == "PRO");

            if (catId > 0)
            {
                var listCategory = GetAllChildId(catId, companyId);
                listCategory.Add(catId);
                query = query.Where(e => listCategory.Contains(e.Product.Article.CategoryId));
            }

            var products = query.Select(e => new ProductGrouponModel
            {
                ID = e.Id,
                CategoryId = e.Product.Article.CategoryId,
                Image = e.Product.Article.Image,
                Orders = e.Product.Article.Item.Orders,
                Publish = e.Product.Article.Item.IsPublished,
                CategoryName = e.Product.Article.Category.CategoryLanguages.FirstOrDefault(c => c.LanguageId == language).Title,
                Code = e.Product.Code,
                Price = e.Product.Price,
                SaleMin = e.Product.SaleMin,

                Quantity = e.Product.ProductGroupon.Quantity,
                EndDate = e.Product.ProductGroupon.EndDate,
                StartDate = e.Product.ProductGroupon.StartDate,
                Sale = e.Product.ProductGroupon.Price,

                VoteNumber = e.Product.Article.Item.ItemVote == null ? 1 : e.Product.Article.Item.ItemVote.VoteNumber,
                VoteRate = e.Product.Article.Item.ItemVote == null ? 1 : e.Product.Article.Item.ItemVote.VoteRate,
                ViewNumber = e.Product.Article.Item.ItemView == null ? 1 : e.Product.Article.Item.ItemView.Views,
            }).ToList();

            var ids = products.Select(e => e.ID).ToList();
            var langs = articleLanguageDAL.GetAll()
                                .Where(e => ids.Contains(e.ArticleId))
                                .Select(e => new ARTICLELANGUAGEModel
                                {
                                    ID = e.ArticleId,
                                    LANGUAGEID = e.LanguageId,
                                    TITLE = e.Title,
                                    BRIEF = e.Brief,
                                    CONTENT = e.Contents
                                }).ToList();
            var orderProducts = orderCollectionDAL.GetAll()
                    .Where(e => ids.Contains(e.ProductId))
                    .Select(e => new { e.ProductId, e.Quantity, e.Order.CreateDate })
                    .ToList();

            foreach (var product in products)
            {
                var lang = langs.FirstOrDefault(e => e.ID == product.ID && e.LANGUAGEID == language);
                if (lang != null)
                {
                    product.LanguageId = lang.LANGUAGEID;
                    product.Title = lang.TITLE;
                    product.Brief = lang.BRIEF;
                    product.Contents = lang.CONTENT;
                }

                product.Languages = string.Join(", ", langs.Where(e => e.ID == product.ID).Select(e => e.LANGUAGEID));

                product.PayNumber = orderProducts.Where(e => e.ProductId == product.ID && product.StartDate <= e.CreateDate && e.CreateDate <= product.EndDate)
                                           .Select(e => e.Quantity).DefaultIfEmpty(0).Sum();
            }

            return products.AsQueryable();
        }

        public IList<ProductAddOnModel> GetListProductAddOns(int id, int companyId, string language)
        {
            var idAddons = addonDAL.GetAll().Where(e => e.Id == id && e.Product.Article.Item.CompanyId == companyId).Select(e => new { e.ProductId, e.Price, e.Quantity }).ToList();
            var ids = idAddons.Select(e => e.ProductId).ToList();
            var langs = articleLanguageDAL.GetAll()
                                .Where(e => ids.Contains(e.ArticleId))
                                .Select(e => new ProductAddOnModel
                                {
                                    Id = e.ArticleId,
                                    Name = e.Title,
                                    ImagePath = e.Article.Image,
                                    ProductId = e.Article.CategoryId,
                                    Price = e.Article.Product.Price
                                }).ToList();
            foreach(var lang in langs)
            {
                var addon = idAddons.FirstOrDefault(e => e.ProductId == lang.Id);
                if (addon != null)
                {
                    lang.Sale = addon.Price;
                    lang.Quantity = addon.Quantity;
                }
            }

            return langs;
        }

        public IList<ProductAddOnModel> GetListRelatiedProducts(int id, int companyId, string language)
        {
            var ids = relatiedDAL.GetAll().Where(e => e.Id == id && e.Article.Item.CompanyId == companyId).Select(e => e.ArticleId).ToList();
            var langs = articleLanguageDAL.GetAll()
                                .Where(e => ids.Contains(e.ArticleId) && e.LanguageId == language && e.Article.Item.IsPublished)
                                .Select(e => new ProductAddOnModel
                                {
                                    Id = e.ArticleId,
                                    Name = e.Title,
                                    ImagePath = e.Article.Image,
                                    ProductId = e.Article.CategoryId,
                                    Price = e.Article.Product.Price,
                                    Sale = e.Article.Product.Sale,
                                    Quantity = e.Article.Product.SaleMin,
                                }).ToList();

            var id2 = relatiedDAL.GetAll().Where(e => e.ArticleId == id && e.Article.Item.CompanyId == companyId).Select(e => e.Id).ToList();
            var langs2 = articleLanguageDAL.GetAll()
                                .Where(e => id2.Contains(e.ArticleId) && e.LanguageId == language && e.Article.Item.IsPublished)
                                .Select(e => new ProductAddOnModel
                                {
                                    Id = e.ArticleId,
                                    Name = e.Title,
                                    ImagePath = e.Article.Image,
                                    ProductId = e.Article.CategoryId,
                                    Price = e.Article.Product.Price,
                                    Sale = e.Article.Product.Sale,
                                    Quantity = e.Article.Product.SaleMin,
                                }).ToList();

            var result = langs.Union(langs2);

            return result.ToList();
        }

        public ProductModel GetProduct(int companyId, string language, int productIdId)
        {
            var article = articleDAL.GetAll().Where(e => e.Id == productIdId && e.Item.CompanyId == companyId)
                                                .Select(e => new ProductModel
                                                {
                                                    ID = e.Id,
                                                    CategoryId = e.CategoryId,
                                                    Image = e.Image,
                                                    Tag = e.Tag,
                                                    Orders = e.Item.Orders,
                                                    Publish = e.Item.IsPublished,
                                                    CategoryName = e.Category.CategoryLanguages.FirstOrDefault(c => c.LanguageId == language).Title,
                                                    Code = e.Product.Code,
                                                    Price = e.Product.Price,
                                                    Sale = e.Product.Sale,
                                                    Quantity = e.Product.Quantity,
                                                    SaleMin = e.Product.SaleMin,
                                                    VoteNumber = e.Item.ItemVote == null ? 1 : e.Item.ItemVote.VoteNumber,
                                                    VoteRate = e.Item.ItemVote == null ? 1 : e.Item.ItemVote.VoteRate,
                                                    ViewNumber = e.Item.ItemView == null ? 1 : e.Item.ItemView.Views,
                                                }).FirstOrDefault();
            if (article == null) return null;
            var lang = articleLanguageDAL.GetAll().Where(e => e.ArticleId == productIdId && e.Article.Item.CompanyId == companyId && e.LanguageId == language).FirstOrDefault();
            if(lang !=null)
            {
                article.LanguageId = language;
                article.Title = lang.Title;
                article.Brief = lang.Brief;
                article.Contents = lang.Contents;
            };

            return article;
        }
        public GrouponModel GetGroupon(int companyId, int productIdId)
        {
            var groupon = productGrouponDAL.GetAll().Where(e => e.Id == productIdId && e.Product.Article.Item.CompanyId == companyId)
                                                .Select(e => new GrouponModel
                                                {
                                                    ID = e.Id,
                                                    EndDate = e.EndDate,
                                                    Price = e.Price,
                                                    Quantity = e.Quantity,
                                                    StartDate = e.StartDate
                                                }).FirstOrDefault();

            return groupon;
        }

        public int SaveProduct(ProductModel model,
                                List<string> imageDetailRemoves,
                                List<string> imageDetailAdds, 
                                List<string> colorRemoves, 
                                List<ProductColorModel> colorAdds, 
                                List<ProductPriceModel> prices,
                                List<ProductAttributeModel> attributes,
                                List<int> RelatiedProducts,
                                List<ProductAddOnModel> addOns,
                                int companyId, 
                                int userId)
        {
            var articleLanguage = this.articleLanguageDAL.AllIncludes(e => e.Article,
                                                                    e => e.Article.Item, 
                                                                    e => e.Article.Item.ItemImages,
                                                                    e => e.Article.ArticleRelatieds,
                                                                    e => e.Article.Product, 
                                                                    e => e.Article.Product.ProductAttributes, 
                                                                    e => e.Article.Product.ProductColors,
                                                                    e => e.Article.Product.ProductPrices,
                                                                    e => e.Article.Product.ProductAddOns)
                                            .FirstOrDefault(e => e.Article.Product.Id == model.ID && e.Article.Item.CompanyId == companyId && e.LanguageId == model.LanguageId);
            if (articleLanguage == null)
            {
                articleLanguage = new ArticleLanguage();
                articleLanguage.LanguageId = model.LanguageId;

                var article = this.articleDAL.AllIncludes(e => e.Item, e => e.Item.ItemImages, e => e.Product, e => e.Product.ProductAttributes, e => e.Product.ProductColors, e => e.Product.ProductPrices)
                                        .FirstOrDefault(e => e.Id == model.ID && e.Item.CompanyId == companyId);
                if (article == null)
                {
                    article = new Article();
                    article.Product = new Product();
                    article.Item = new Item();
                    article.Item.CompanyId = companyId;
                }
                else
                {
                    this.articleDAL.Update(article);
                    this.itemDAL.Update(article.Item);
                    this.productDAL.Update(article.Product);
                }

                articleLanguage.Article = article;
                articleLanguageDAL.Add(articleLanguage);
            }
            else
            {
                articleLanguageDAL.Update(articleLanguage);
                articleDAL.Update(articleLanguage.Article);
                itemDAL.Update(articleLanguage.Article.Item);
                productDAL.Update(articleLanguage.Article.Product);
            }

            while (string.IsNullOrEmpty(model.Code))
            {
                model.Code = model.Title[0] + GenerateRandomCode.RandomNumber(5);
                if (productDAL.GetAll().Any(e => e.Code == model.Code && e.Article.Item.CompanyId == companyId))
                    model.Code = string.Empty;
            }

            articleLanguage.LanguageId = model.LanguageId;
            articleLanguage.Brief = model.Brief;
            articleLanguage.Title = model.Title;
            articleLanguage.Contents = model.Contents;
            articleLanguage.Article.Tag = model.Tag;
            articleLanguage.Article.CategoryId = model.CategoryId;
            articleLanguage.Article.Image = model.Image;
            articleLanguage.Article.Item.ModifyDate = DateTime.Now;
            articleLanguage.Article.Item.ModifyByUser = userId;
            articleLanguage.Article.Item.IsPublished = model.Publish;
            articleLanguage.Article.Item.Orders = model.Orders;
            articleLanguage.Article.Product.Code = model.Code;
            articleLanguage.Article.Product.Price = model.Price;
            articleLanguage.Article.Product.Sale = model.Sale;
            articleLanguage.Article.Product.SaleMin = model.SaleMin;

            //remove image detail
            imageDAL.Delete(e => imageDetailRemoves.Contains(e.Id.ToString()));

            //add image detail
            foreach (var image in imageDetailAdds)
            {
                var itemImage = new ItemImage();
                itemImage.ImageName = image;
                articleLanguage.Article.Item.ItemImages.Add(itemImage);
            }

            // remove color
            productColorDAL.Delete(e => colorRemoves.Contains(e.Id.ToString()));

            // add color
            foreach (var colorModel in colorAdds)
            {
                var color = new ProductColor();
                color.ImageName = colorModel.ImageName;
                color.Name = colorModel.Name;
                color.Value = colorModel.Value;
                color.Price = colorModel.Price;
                articleLanguage.Article.Product.ProductColors.Add(color);
            }

            // Prices
            articleLanguage.Article.Product.ProductPrices.Clear();
            foreach (var priceModel in prices)
            {
                var price = new ProductPrice();
                price.Quantity = priceModel.Quantity;
                price.Price = priceModel.Price;
                articleLanguage.Article.Product.ProductPrices.Add(price);
            }

            //Attribute
            foreach(var attributeModel in attributes)
            {
                var attribute = articleLanguage.Article.Product.ProductAttributes.FirstOrDefault(e => e.AttributeId == attributeModel.ID);
                if (attribute == null)
                {
                    attribute = new ProductAttribute();
                    attribute.AttributeId = attributeModel.ID;
                    articleLanguage.Article.Product.ProductAttributes.Add(attribute);
                }
                else productAttributeDAL.Update(attribute);
                attribute.Value = attributeModel.Value;
            }

            //Relatied
            articleLanguage.Article.ArticleRelatieds.Clear();
            var refIds = relatiedDAL.GetMany(e => e.ArticleId == model.ID).Select(e => e.Id).ToList();
            foreach (var relatied in RelatiedProducts)
            {
                if (!refIds.Exists(e => e == relatied))
                {
                    var article = new ArticleRelatied();
                    article.ArticleId = relatied;
                    articleLanguage.Article.ArticleRelatieds.Add(article);
                }
            }

            //AddOns
            articleLanguage.Article.Product.ProductAddOns.Clear();
            foreach (var addon in addOns)
            {
                var product = new ProductAddOn();
                product.ProductId = addon.ProductId;
                product.Price = addon.Sale;
                product.Quantity = addon.Quantity;
                articleLanguage.Article.Product.ProductAddOns.Add(product);
            }

            this.SaveChanges();
            model.ID = articleLanguage.ArticleId;
            return model.ID;
        }
        public int SaveGroupon(GrouponModel groupon, 
                                ProductModel product,
                                List<string> imageDetailRemoves,
                                List<string> imageDetailAdds,
                                List<string> colorRemoves,
                                List<ProductColorModel> colorAdds,
                                List<ProductPriceModel> prices,
                                List<ProductAttributeModel> attributes,
                                List<int> RelatiedProducts,
                                List<ProductAddOnModel> addOns,
                                int companyId,
                                int userId)
        {
            var articleLanguage = this.articleLanguageDAL.AllIncludes(e => e.Article,
                                                                    e => e.Article.Item,
                                                                    e => e.Article.Item.ItemImages,
                                                                    e => e.Article.ArticleRelatieds,
                                                                    e => e.Article.Product,
                                                                    e => e.Article.Product.ProductAttributes,
                                                                    e => e.Article.Product.ProductColors,
                                                                    e => e.Article.Product.ProductPrices,
                                                                    e => e.Article.Product.ProductGroupon)
                                            .FirstOrDefault(e => e.Article.Product.Id == product.ID && e.Article.Item.CompanyId == companyId && e.LanguageId == product.LanguageId);
            if (articleLanguage == null)
            {
                articleLanguage = new ArticleLanguage();
                articleLanguage.LanguageId = product.LanguageId;

                var article = this.articleDAL.AllIncludes(e => e.Item, e => e.Item.ItemImages, e => e.Product, e => e.Product.ProductGroupon, e => e.Product.ProductAttributes, e => e.Product.ProductColors, e => e.Product.ProductPrices)
                                    .FirstOrDefault(e => e.Id == product.ID && e.Item.CompanyId == companyId);
                if (article == null)
                {
                    article = new Article();
                    article.Product = new Product();
                    article.Product.ProductGroupon = new ProductGroupon();
                    article.Item = new Item();
                    article.Item.CompanyId = companyId;
                }
                else
                {
                    this.articleDAL.Update(article);
                    this.itemDAL.Update(article.Item);
                    this.productDAL.Update(article.Product);

                    if (article.Product.ProductGroupon == null) article.Product.ProductGroupon = new ProductGroupon();
                    else this.productGrouponDAL.Update(article.Product.ProductGroupon);
                }

                articleLanguage.Article = article;
                articleLanguageDAL.Add(articleLanguage);
            }
            else
            {
                articleLanguageDAL.Update(articleLanguage);
                articleDAL.Update(articleLanguage.Article);
                itemDAL.Update(articleLanguage.Article.Item);
                productDAL.Update(articleLanguage.Article.Product);

                if (articleLanguage.Article.Product.ProductGroupon == null) articleLanguage.Article.Product.ProductGroupon = new ProductGroupon();
                else this.productGrouponDAL.Update(articleLanguage.Article.Product.ProductGroupon);
            }

            while (string.IsNullOrEmpty(product.Code))
            {
                product.Code = product.Title[0] + GenerateRandomCode.RandomNumber(5);
                if (productDAL.GetAll().Any(e => e.Code == product.Code && e.Article.Item.CompanyId == companyId))
                    product.Code = string.Empty;
            }

            articleLanguage.LanguageId = product.LanguageId;
            articleLanguage.Brief = product.Brief;
            articleLanguage.Title = product.Title;
            articleLanguage.Contents = product.Contents;
            articleLanguage.Article.Tag = product.Tag;
            articleLanguage.Article.CategoryId = product.CategoryId;
            articleLanguage.Article.Image = product.Image;
            articleLanguage.Article.Item.ModifyDate = DateTime.Now;
            articleLanguage.Article.Item.ModifyByUser = userId;
            articleLanguage.Article.Item.IsPublished = product.Publish;
            articleLanguage.Article.Item.Orders = product.Orders;
            articleLanguage.Article.Product.Code = product.Code;
            articleLanguage.Article.Product.Price = product.Price;
            articleLanguage.Article.Product.Sale = product.Sale;
            articleLanguage.Article.Product.SaleMin = product.SaleMin;

            //Groupon
            articleLanguage.Article.Product.ProductGroupon.Quantity = groupon.Quantity;
            articleLanguage.Article.Product.ProductGroupon.StartDate = groupon.StartDate;
            articleLanguage.Article.Product.ProductGroupon.EndDate = groupon.EndDate;
            articleLanguage.Article.Product.ProductGroupon.Price = groupon.Price;

            //remove image detail
            imageDAL.Delete(e => imageDetailRemoves.Contains(e.Id.ToString()));

            //add image detail
            foreach (var image in imageDetailAdds)
            {
                var itemImage = new ItemImage();
                itemImage.ImageName = image;
                articleLanguage.Article.Item.ItemImages.Add(itemImage);
            }

            // remove color
            productColorDAL.Delete(e => colorRemoves.Contains(e.Id.ToString()));

            // add color
            foreach (var colorModel in colorAdds)
            {
                var color = new ProductColor();
                color.ImageName = colorModel.ImageName;
                color.Name = colorModel.Name;
                color.Value = colorModel.Value;
                color.Price = colorModel.Price;
                articleLanguage.Article.Product.ProductColors.Add(color);
            }

            // Prices
            articleLanguage.Article.Product.ProductPrices.Clear();
            foreach (var priceModel in prices)
            {
                var price = new ProductPrice();
                price.Quantity = priceModel.Quantity;
                price.Price = priceModel.Price;
                articleLanguage.Article.Product.ProductPrices.Add(price);
            }

            //Attribute
            foreach (var attributeModel in attributes)
            {
                var attribute = articleLanguage.Article.Product.ProductAttributes.FirstOrDefault(e => e.AttributeId == attributeModel.ID);
                if (attribute == null)
                {
                    attribute = new ProductAttribute();
                    attribute.AttributeId = attributeModel.ID;
                    articleLanguage.Article.Product.ProductAttributes.Add(attribute);
                }
                else productAttributeDAL.Update(attribute);
                attribute.Value = attributeModel.Value;
            }

            this.SaveChanges();
            product.ID = groupon.ID = articleLanguage.ArticleId;
            return product.ID;
        }

        public void RemoveProduct(int id, int companyId)
        {
            var product = productDAL.AllIncludes(e => e.Article, 
                                                    e => e.Article.Item, 
                                                    e => e.Article.Item.ItemImages, 
                                                    e => e.Article.Product, 
                                                    e => e.Article.Product.ProductAttributes, 
                                                    e => e.Article.Product.ProductColors,
                                                    e => e.Article.Product.ProductPrices,
                                                    e => e.Article.Product.ProductGroupon,
                                                    e => e.Article.Product.OrderProducts)
                .FirstOrDefault(e => e.Id == id && e.Article.Item.CompanyId == companyId);
            if (product != null)
            {
                if (product.OrderProducts.Count > 0) throw new BusinessException("Sản phẩm đã được bán không thể xóa");
                itemDAL.Delete(e => e.Id == id);
                this.SaveChanges();
            }
            else { throw new Exception("Data does not exist"); }
        }

        public void RemoveGroupon(int id, int companyId)
        {
            var groupon = productGrouponDAL.GetAll()
                            .FirstOrDefault(e => e.Id == id && e.Product.Article.Item.CompanyId == companyId);
            if (groupon != null)
            {
                productGrouponDAL.Delete(e => e.Id == id);
                this.SaveChanges();
            }
            else { throw new Exception("Data does not exist"); }
        }

        private IQueryable<Article> QueryProduct(
            int companyId,
            int categoryId,
            bool inChildCat,
            int ortherId,
            string colors,
            int atrId = 0,
            string atrValue = "",
            bool groupon = false)
        {
            var query =
                this.articleDAL.GetAll()
                .Where(c => c.Item.IsPublished)
                .Where(c => c.Item.CompanyId == companyId)
                .Where(c => c.Product != null);

            var grouponids = this.productGrouponDAL.GetAll().Where(e => e.StartDate <= DateTime.Now && DateTime.Now <= e.EndDate).Select(e => e.Id).ToList();
            if (groupon) query = query.Where(c => c.Product.ProductGroupon != null && grouponids.Contains(c.Id));
            else query = query.Where(c => !grouponids.Contains(c.Id));

            if (ortherId > 0)
            {
                query = query.Where(language => language.Id != ortherId);
            }

            if (categoryId > 0)
            {
                IList<int> listCategory;

                if (inChildCat) listCategory = GetAllChildId(categoryId, companyId);
                else listCategory = new List<int>();

                listCategory.Add(categoryId);
                query = query.Where(e => listCategory.Contains(e.CategoryId));
            }

            if (!string.IsNullOrEmpty(colors))
            {
                var arrColor = colors.Split(',');
                foreach (var color in arrColor)
                {
                    if (!string.IsNullOrEmpty(color) && !color.StartsWith("#"))
                    {
                        var fullcolor = "#" + color;
                        var productIds = productColorDAL.GetAll().Where(e => e.Value == fullcolor).Select(e => e.ProductId).Distinct().ToList();
                        query = query.Where(e => productIds.Contains(e.Id));
                    }
                }
                
            }

            if (atrId > 0)
            {
                var attribute = attributeDAL.GetAll().FirstOrDefault(e => e.CompanyId == companyId && e.CategoryId == atrId);
                if (attribute != null) atrId = attribute.Id;
                var productIds = productAttributeDAL.GetAll()
                                            .Where(e => e.AttributeId == atrId && e.Value == atrValue)
                                            .Select(e => e.ProductId)
                                            .ToList();
                query = query.Where(e => productIds.Contains(e.Id));
            }

            query = query.OrderBy(o => o.Item.Orders).ThenByDescending(e => e.Id);

            return query;
        }
        #endregion

        #region Attribute
        public IQueryable<AttributeCategoryModel> GetAttributeCategories(int companyId)
        {
            var query = attributeCategoryDAL.GetAll().Where(e => e.Item.CompanyId == companyId);

            var categories = query.Select(e => new AttributeCategoryModel
            {
                Id = e.Id,
                Name = e.Name,
                Type = e.Type,
                CountAttributeValues = e.AttributeValues.Count
            });

            return categories;
        }

        public AttributeCategoryModel GetAttributeCategory(int id, int companyId)
        {
            var query = attributeCategoryDAL.GetAll().Where(e => e.Id == id && e.Item.CompanyId == companyId);

            var category = query.Select(e => new AttributeCategoryModel
            {
                Id = e.Id,
                Name = e.Name,
                Type = e.Type
            }).FirstOrDefault();

            return category;
        }

        public int AddAttributeCategory(AttributeCategoryModel model, int companyId, int userId)
        {
            var attribute = new AttributeCategory();
            attribute.Item = new Item();
            attribute.Item.Orders = 0;
            attribute.Item.CompanyId = companyId;
            attribute.Item.ModifyByUser = userId;
            attribute.Item.ModifyDate = DateTime.Now;
            attribute.Name = model.Name;
            if (string.IsNullOrEmpty(model.Type)) attribute.Type = "Text";
            else attribute.Type = model.Type;
            attributeCategoryDAL.Add(attribute);
            this.SaveChanges();
            model.Id = attribute.Id;
            return model.Id;
        }

        public void UpdateAttributeCategory(AttributeCategoryModel model, int companyId, int userId)
        {
            var attribute = attributeCategoryDAL.AllIncludes(e => e.AttributeValues, e => e.Item)
                                .Where(e => e.Id == model.Id && e.Item.CompanyId == companyId)
                                .FirstOrDefault();
            if (attribute != null)
            {
                attribute.Item.ModifyByUser = userId;
                attribute.Item.ModifyDate = DateTime.Now;
                attribute.Name = model.Name;
                if (attribute.AttributeValues.Count == 0)
                {
                    if (string.IsNullOrEmpty(model.Type)) attribute.Type = "Text";
                    else attribute.Type = model.Type;
                }
                attributeCategoryDAL.Update(attribute);
                this.SaveChanges();
            }
        }

        public void DeleteAttributeCategory(int id, int companyId)
        {
            var entity = this.attributeCategoryDAL.GetAll().FirstOrDefault(e => e.Id == id && e.Item.CompanyId == companyId);
            if (entity != null)
            {
                this.attributeCategoryDAL.Delete(entity);
                this.SaveChanges();
            }
            else { throw new Exception("Data does not exist"); }
        }

        public IQueryable<AttributeValueModel>GetAttributeValues(int companyId, int categoryId = 0)
        {
            var query = attributeValueDAL.GetAll().Where(e => e.AttributeCategory.Item.CompanyId == companyId);
            if(categoryId > 0) query = query.Where(e => e.AttributeCategory.Id == categoryId);

            var category = query.Select(e => new AttributeValueModel
                {
                    Id = e.Id,
                    Value = e.Value,
                    CategoryId = e.CategoryId,
                    Orders = e.Item.Orders
                })
                .OrderBy(e => e.Orders);

            return category;
        }

        public AttributeValueModel GetAttributeValue(int companyId, int attributeId = 0) 
        {
            var query = attributeValueDAL.GetAll().Where(e => e.AttributeCategory.Item.CompanyId == companyId && e.Id == attributeId);

            var attributes = query.Select(e => new AttributeValueModel
            {
                Id = e.Id,
                Value = e.Value,
                CategoryId = e.CategoryId,
                Orders = e.Item.Orders
            });

            return attributes.FirstOrDefault();
        }

        public int AddAttributeValue(AttributeValueModel model, int companyId, int userId)
        {
            var attribute = new AttributeValue();
            attribute.Item = new Item();
            attribute.Item.Orders = model.Orders;
            attribute.Item.CompanyId = companyId;
            attribute.Item.ModifyByUser = userId;
            attribute.Item.ModifyDate = DateTime.Now;
            attribute.CategoryId = model.CategoryId;
            attribute.Value = model.Value;
            attributeValueDAL.Add(attribute);
            this.SaveChanges();
            model.Id = attribute.Id;
            return model.Id;
        }

        public void UpdateAttributeValue(AttributeValueModel model, int companyId, int userId)
        {
            var attribute = attributeValueDAL.AllIncludes(e => e.Item)
                                .Where(e => e.Id == model.Id && e.Item.CompanyId == companyId)
                                .FirstOrDefault();
            if (attribute != null)
            {
                attribute.Value = model.Value;
                attribute.Item.Orders = model.Orders;
                attribute.Item.ModifyDate = DateTime.Now;
                attribute.Item.ModifyByUser = userId;
                attributeValueDAL.Update(attribute);
                this.SaveChanges();
            }
        }

        public void DeleteAttributeValue(int id, int companyId)
        {
            var entity = this.attributeValueDAL.GetAll().FirstOrDefault(e => e.Id == id && e.AttributeCategory.Item.CompanyId == companyId);
            if (entity != null)
            {
                this.attributeValueDAL.Delete(entity);
                this.SaveChanges();
            }
            else { throw new Exception("Data does not exist"); }
        }

        public IQueryable<DataSimpleModel> GetSimpleAttributes(int companyId, int categoryId)
        {
            var query = this.attributeValueDAL.GetAll()
                            .Where(e => e.AttributeCategory.Item.CompanyId == companyId && e.CategoryId == categoryId)
                            .OrderBy(e => e.Item.Orders)
                            .Select(e => new DataSimpleModel
                            { 
                                ID = e.Id,
                                Title = e.Value,
                                CategoryId = e.CategoryId,
                                Description = e.AttributeCategory.Type
                            });
            return query;
        }

        public IQueryable<AttributeModel> GetAttributes(int companyId)
        {
            var query = this.attributeDAL.GetAll()
                            .Where(e => e.CompanyId == companyId)
                            .Select(e => new AttributeModel
                            {
                                CategoryId = e.CategoryId,
                                ID = e.Id,
                                Name = e.Name,
                                Type = e.Type
                            });
            return query;
        }

        public int AddAttribute(AttributeModel model, int companyId)
        {
            var attribute = new Data.Attribute();
            attribute.CompanyId = companyId;
            attribute.CategoryId = model.CategoryId;
            attribute.Name = model.Name;
            attribute.Type = model.Type;
            attributeDAL.Add(attribute);
            this.SaveChanges();
            model.ID = attribute.Id;
            return model.ID;
        }

        public void UpdateAttribute(AttributeModel model, int companyId)
        {
            var attribute = attributeDAL.GetAll()
                                .Where(e => e.Id == model.ID && e.CompanyId == companyId)
                                .FirstOrDefault();
            if (attribute != null)
            {
                attribute.CategoryId = model.CategoryId;
                attribute.Name = model.Name;
                attribute.Type = model.Type;
                attributeDAL.Update(attribute);
                this.SaveChanges();
            }
        }

        public void DeleteAttribute(int id, int companyId)
        {
            var entity = this.attributeDAL.GetAll().FirstOrDefault(e => e.Id == id && e.CompanyId == companyId);
            if (entity != null)
            {
                this.attributeDAL.Delete(entity);
                this.SaveChanges();
            }
            else { throw new Exception("Data does not exist"); }
        }
        #endregion

        #region color and tag and attribute
        public IQueryable<ProductColorModel> GetColorByCategoryId(int companyId, int categoryId, bool inChildCat)
        {
                IList<int> listCategory;
                if (inChildCat) listCategory = GetAllChildId(categoryId, companyId);
                else listCategory = new List<int>();
                listCategory.Add(categoryId);

            var colors = productColorDAL.GetAll()
                            .Where(e => e.Product.Article.Item.CompanyId == companyId)
                            //.Where(e => listCategory.Contains(e.Product.Article.CategoryId))
                            .Select(e => new ProductColorModel
                            {
                                Id = e.Id,
                                ImageName = e.ImageName,
                                Name = e.Name,
                                Price = e.Price,
                                ProductId = e.ProductId,
                                Value = e.Value
                            });
            return colors;
        }

        public IList<ProductAttributeModel> GetAttributeByCategoryId(int companyId, int categoryId, bool inChildCat)
        {
            IList<int> listCategory;
            if (inChildCat) listCategory = GetAllChildId(categoryId, companyId);
            else listCategory = new List<int>();
            listCategory.Add(categoryId);

            var values = this.productAttributeDAL.GetAll()
                            .Where(e => e.Attribute.CompanyId == companyId && listCategory.Contains(e.Product.Article.CategoryId))
                            .Select(e => new ProductAttributeModel
                            {
                                ID = e.AttributeId,
                                Value = e.Value,
                                Type = e.ProductId.ToString()
                            }).ToList();

            var names = this.attributeDAL.GetAll()
                            .Where(e => e.CompanyId == companyId)
                            .Select(e => new ProductAttributeModel
                            {
                                CategoryId = e.CategoryId,
                                ID = e.Id,
                                Name = e.Name,
                                Type = e.Type
                            }).ToList();

            var categorys = attributeValueDAL.GetAll()
                                .Where(e => e.AttributeCategory.Item.CompanyId == companyId)
                                .Select(e => new { e.Id, e.Value })
                                .ToList();

            foreach(var value in values)
            {
                var att = names.FirstOrDefault(e => e.ID == value.ID);
                if (att != null)
                {
                    value.Name = att.Name;
                    value.CategoryId = att.CategoryId;
                }
                if (value.CategoryId > 0 && !string.IsNullOrEmpty(value.Value))
                {
                    if (att.Type == "ListCheck")
                    {
                        var temp = new List<string>();
                        foreach (var val in value.Value.Split(','))
                        {
                            var tempVal = categorys.Where(e => e.Id.ToString() == val)
                                                .Select(e => e.Value)
                                                .FirstOrDefault();
                            temp.Add(tempVal);
                        }
                        value.ValueName = string.Join(", ", temp);
                    }
                    else
                    {
                        value.ValueName = categorys.Where(e => e.Id.ToString() == value.Value)
                                                .Select(e => e.Value)
                                                .FirstOrDefault();
                    }
                }
            }
            return values;
        }

        public IList<string> GetTagByCategoryId(int companyId, int categoryId = 0, bool inChildCat = true)
        {
            IList<int> listCategory;
            if (inChildCat) listCategory = GetAllChildId(categoryId, companyId);
            else listCategory = new List<int>();
            listCategory.Add(categoryId);

            var tags = productDAL.GetAll()
                            .Where(e => e.Article.Item.CompanyId == companyId && listCategory.Contains(e.Article.CategoryId) && e.Article.Item.IsPublished)
                            .Select(e => e.Article.Tag)
                            .Distinct()
                            .ToList();

            var result = new List<string>();
            foreach (var group in tags)
            {
                var arrTags = group.Split(',');
                foreach (var onetag in arrTags)
                {
                    if (!string.IsNullOrEmpty(onetag.Trim()) && !result.Any(e => e == onetag.Trim()))
                    {
                        result.Add(onetag.Trim());
                    }
                }
            }

            return result;
        }
        #endregion

        #region order
        public IQueryable<OrderModel> GetOrders(int companyId, DateTime? fromDate = null, DateTime? toDate = null, string phone = "", int customerId = 0)
        {
            var queryOrders = this.orderDAL.GetAll()
               .Where(e => e.CompanyId == companyId);

            if (fromDate != null)
                queryOrders = queryOrders.Where(e => fromDate <= e.CreateDate || (e.LastUpdate != null && fromDate <= e.LastUpdate));

            if (toDate != null)
            {
                toDate = toDate.Value.AddDays(1);
                queryOrders = queryOrders.Where(e => e.CreateDate <= toDate || (e.LastUpdate != null && e.LastUpdate <= toDate));
            }

            if (!string.IsNullOrEmpty(phone)) queryOrders = queryOrders.Where(e => e.CustomerPhone == phone);
            if (customerId > 0) queryOrders = queryOrders.Where(e => e.CustomerId == customerId);

            var orders = queryOrders.Select(e => new OrderModel
            {
                CreateDate = e.CreateDate,
                CustomerId = e.CustomerId ?? 0,
                CustomerAddress = e.CustomerAddress,
                CustomerEmail = e.CustomerEmail,
                CustomerName = e.CustomerName,
                CustomerPhone = e.CustomerPhone,
                CustomerNote = e.CustomerNote,
                Note = e.Note,
                TotalDue = e.Due,
                Id = e.Id,
                Status = e.Status,
                ConfirmDate = e.ConfirmDate,
                SendDate = e.SendDate,
                LastUpdate = e.LastUpdate,
                DeliveryFee = e.DeliveryFee,
                IsPaid = e.IsPaid,
                PaidDate = e.PaidDate,
                ShippingCode = e.ShippingCode,
                ShippingId = e.ShippingId,
                TotalProduct = e.OrderProducts.Count
            });

            return orders;
        }

        public OrderModel GetOrder(int id, int companyId)
        {
            var queryOrders = this.orderDAL.GetAll().Where(e => e.Id == id && e.CompanyId == companyId);

            var orders = queryOrders.Select(e => new OrderModel
            {
                CreateDate = e.CreateDate,
                CustomerAddress = e.CustomerAddress,
                CustomerEmail = e.CustomerEmail,
                CustomerId = e.CustomerId ?? 0,
                CustomerName = e.CustomerName,
                CustomerPhone = e.CustomerPhone,
                CustomerNote = e.CustomerNote,
                CustomerPayDelivery = e.CustomerPayDelivery,
                Note = e.Note,
                TotalDue = e.Due,
                Id = e.Id,
                Status = e.Status,
                ConfirmDate = e.ConfirmDate,
                SendDate = e.SendDate,
                LastUpdate = e.LastUpdate,
                DeliveryFee = e.DeliveryFee,
                IsPaid = e.IsPaid,
                PaidDate = e.PaidDate,
                ShippingCode = e.ShippingCode,
                ShippingId = e.ShippingId,
                Voucher = e.Voucher,
                Point = e.Point ?? 0
            });

            return orders.FirstOrDefault();
        }

        public IQueryable<OrderProductModel> GetOrderProducts(List<int> orderIds, string language)
        {
            var query = orderCollectionDAL.GetAll()
                        .Where(e => orderIds.Contains(e.OrderId))
                        .Select(e => new OrderProductModel
                        {
                            IOId = e.OrderId,
                            Price = e.PriceUnit,
                            Quantity = e.Quantity,
                            ProductId = e.ProductId,
                            ProductProperties = e.Properties,
                            ProductCode = e.Product.Code,
                            ProductImage = e.Product.Article.Image,
                            ProductName = e.Product.Article.ArticleLanguages.FirstOrDefault(a => a.LanguageId == language).Title,
                            TotalCost = e.Quantity * e.PriceUnit
                        });

            return query;
        }

        public int SaveOrder(OrderModel model, int companyId, int? userId, List<OrderProductModel> products)
        {
            var order = orderDAL.AllIncludes(e => e.OrderProducts)
                        .FirstOrDefault(e => e.Id == model.Id && e.CompanyId == companyId);
            if (order == null)
            {
                order = new Order();
                order.CompanyId = companyId;
                order.CreateDate = DateTime.Now;
                order.Status = 0;
                if (model.CustomerId > 0) order.CustomerId = model.CustomerId;

                foreach (var product in products)
                {
                    var item = new OrderProduct();
                    item.ProductId = product.ProductId;
                    item.Quantity = product.Quantity;
                    item.PriceUnit = product.Price;
                    item.Properties = product.ProductProperties;
                    order.OrderProducts.Add(item);
                }

                orderDAL.Add(order);
            }
            else
            {

                foreach (var product in products)
                {
                    var proExit = order.OrderProducts.FirstOrDefault(e => e.ProductId == product.ProductId && e.Properties == product.ProductProperties);
                    if (proExit != null)
                    {
                        proExit.Quantity = product.Quantity;
                        proExit.PriceUnit = product.Price;
                    }
                    else
                    {
                        var item = new OrderProduct();
                        item.ProductId = product.ProductId;
                        item.Properties = product.ProductProperties;
                        item.Quantity = product.Quantity;
                        item.PriceUnit = product.Price;
                        order.OrderProducts.Add(item);
                    }
                }

                var removeproducts = new List<OrderProduct>();
                foreach (var p in order.OrderProducts)
                {
                    if (!products.Any(e => e.ProductId == p.ProductId && e.ProductProperties == p.Properties)) removeproducts.Add(p);
                }
                var ids = removeproducts.Select(e => e.Id).ToArray();
                orderCollectionDAL.Delete(e => ids.Contains(e.Id));

                orderDAL.Update(order);
            }

            order.CustomerAddress = model.CustomerAddress;
            order.CustomerEmail = model.CustomerEmail;
            order.CustomerName = model.CustomerName;
            order.CustomerPhone = model.CustomerPhone.Replace(" ", "").Replace(",", "").Replace(".", "");
            order.CustomerPayDelivery = model.CustomerPayDelivery;
            order.CustomerNote = model.CustomerNote;
            order.Due = order.OrderProducts.Sum(e => e.PriceUnit * e.Quantity);
            order.ShippingCode = model.ShippingCode;
            order.ShippingId = model.ShippingId;
            order.DeliveryFee = model.DeliveryFee;
            order.UpdateBy = userId;
            order.LastUpdate = DateTime.Now;

            if (!string.IsNullOrEmpty(model.Voucher) && order.Voucher != model.Voucher)
            {
                var voucher = voucherDAL.GetAll().FirstOrDefault(e => e.Code == model.Voucher && e.Item.IsPublished == true);
                if (voucher != null && voucher.EffectDate <= order.CreateDate && order.CreateDate <= voucher.ExpirDate && voucher.Quantity > 0)
                {
                    order.Voucher = model.Voucher;
                    voucher.Quantity -= 1;
                    voucherDAL.Update(voucher);
                }
            }

            
            if (order.CustomerId > 0)
            {
                var custemer = customerDAL.GetAll().FirstOrDefault(e => e.Id == order.CustomerId && e.CompanyId == companyId);
                if (model.Point > 0 && order.Point != model.Point)
                {
                    if ((custemer.Point < (model.Point - order.Point))) throw new BusinessException("Điểm thành viên không đủ");
                    if (order.Point > 0) custemer.Point += order.Point;
                    order.Point = model.Point;
                    custemer.Point -= model.Point;
                }

                customerDAL.Update(custemer);
            }

            this.SaveChanges();
            return order.Id;
        }

        public void Confirm(int companyId, int userId, int orderId)
        {
            var order = this.orderDAL.GetAll()
                .Where(e => e.CompanyId == companyId && e.Id == orderId && e.Status == 0)
                .FirstOrDefault();

            if (order != null)
            {
                order.Status = 1;
                order.LastUpdate = DateTime.Now;
                order.ConfirmDate = DateTime.Now;
                order.UpdateBy = userId;

                this.orderDAL.Update(order);
            }

            this.SaveChanges();
        }

        public void Update(int companyId, int userId, OrderModel dto)
        {
            var order = this.orderDAL.GetAll()
                .Where(e => e.CompanyId == companyId && e.Id == dto.Id && e.Status < 3)
                .FirstOrDefault();

            if (order != null)
            {
                order.CustomerName = dto.CustomerName;
                order.CustomerPhone = dto.CustomerPhone;
                order.CustomerAddress = dto.CustomerAddress;
                order.CustomerNote = dto.CustomerNote;
                order.DeliveryFee = dto.DeliveryFee;
                order.Due = dto.TotalDue;
                order.Note = dto.Note;
                order.ShippingCode = dto.ShippingCode;
                order.ShippingId = dto.ShippingId;

                if (order.Status == 0) order.Status = 1;
                order.LastUpdate = DateTime.Now;
                order.ConfirmDate = DateTime.Now;
                order.UpdateBy = userId;

                this.orderDAL.Update(order);
            }

            this.SaveChanges();
        }

        public void Paid(int companyId, int userId, int orderId)
        {
            var order = this.orderDAL.GetAll()
                .Where(e => e.CompanyId == companyId && e.Id == orderId)
                .FirstOrDefault();

            if (order != null)
            {
                order.IsPaid = true;
                order.LastUpdate = DateTime.Now;
                order.PaidDate = DateTime.Now;
                order.UpdateBy = userId;

                this.orderDAL.Update(order);
            }

            this.SaveChanges();
        }

        public void Send(int companyId, int userId, int orderId, string note = "")
        {
            var order = this.orderDAL.GetAll()
                .Where(e => e.CompanyId == companyId && e.Id == orderId && e.Status < 2)
                .FirstOrDefault();

            if (order != null)
            {
                order.Note = note;

                order.Status = 2;
                order.SendDate = DateTime.Now;
                order.LastUpdate = DateTime.Now;
                order.UpdateBy = userId;
                this.orderDAL.Update(order);
            }

            this.SaveChanges();
        }

        public void Delivery(int companyId, int userId, int orderId, int shippingId, string orderCode, decimal deliveryFee, string note, bool reciverPay, bool useCustomerPay)
        {
            var order = this.orderDAL.GetAll()
                .Where(e => e.CompanyId == companyId && e.Id == orderId)
                .FirstOrDefault();

            if (order != null)
            {
                order.ShippingId = shippingId;
                order.ShippingCode = orderCode;
                order.DeliveryFee = deliveryFee;
                order.Note = note;
                order.LastUpdate = DateTime.Now;
                order.UpdateBy = userId;
                if(!useCustomerPay)
                {
                    if (reciverPay) order.CustomerPayDelivery = deliveryFee;
                    else order.CustomerPayDelivery = 0;
                }
                this.orderDAL.Update(order);
            }

            this.SaveChanges();
        }

        public void Return(int companyId, int userId, int orderId, string note = "")
        {
            var query = this.orderDAL.GetAll()
                .Where(e => e.CompanyId == companyId && e.Id == orderId);

            if (userId > 0) query = query.Where(e => e.Status == 2);
            else query = query.Where(e => e.Status < 2);

            var order = query.FirstOrDefault();

            if (order != null) 
            {
                if (userId > 0) order.Note = note;
                else order.CustomerNote = note;

                order.Status = 4;
                order.LastUpdate = DateTime.Now;
                order.UpdateBy = userId;

                // hàng ko giao được thì trả điểm tích lũy lại cho khách
                if (order.Point > 0)
                {
                    var custemer = customerDAL.GetAll().FirstOrDefault(e => e.Id == order.CustomerId || ((e.Phone == order.CustomerPhone || e.Email == order.CustomerEmail) && e.CompanyId == companyId));
                    if (custemer != null)
                    {
                        custemer.Point += order.Point;
                        customerDAL.Update(custemer);
                    }
                }

                this.orderDAL.Update(order);

                //var orders = this.orderCollectionDAL.GetAll()
                //    .Where(e => e.OrderId == order.Id && e.Order.CompanyId == companyId)
                //    .Select(e => new { e.ProductId, e.Quantity })
                //    .ToList();
                //foreach (var o in orders)
                //{
                //    var product = this.productDAL.Get(e => e.Id == o.ProductId && e.Article.Item.CompanyId == companyId);
                //    if (product != null)
                //    {
                //        product.Quantity += o.Quantity;
                //        this.productDAL.Update(product);
                //    }
                //}
            }

            this.SaveChanges();
        }

        public void Recived(int companyId, int userId, int orderId, string note = "")
        {
            var order = this.orderDAL.GetAll()
                .Where(e => e.CompanyId == companyId && e.Id == orderId)
                .FirstOrDefault();

            if (order != null && order.Status == 2)
            {
                order.Status = 3;
                order.LastUpdate = DateTime.Now;
                order.UpdateBy = userId;
                this.orderDAL.Update(order);

                var orders = this.orderCollectionDAL.GetAll()
                    .Where(e => e.OrderId == order.Id && e.Order.CompanyId == companyId)
                    .Select(e => new { e.ProductId, e.Quantity })
                    .ToList();
                foreach (var o in orders)
                {
                    var product = this.productDAL.Get(e => e.Id == o.ProductId && e.Article.Item.CompanyId == companyId);
                    if (product != null)
                    {
                        product.Quantity -= o.Quantity;
                        this.productDAL.Update(product);
                    }                    
                }

                // giao hàng thành công thì tích điểm đơn hàng cho khách
                // lưu ý: điểm khách sử dụng cho đơn hàng này đã trừ lúc tạo đơn
                var custemer = customerDAL.GetAll().FirstOrDefault(e => e.Id == order.CustomerId || ((e.Phone == order.CustomerPhone || e.Email == order.CustomerEmail) && e.CompanyId == companyId));
                if (custemer != null)
                {
                    var configPoint = pointDAL.GetById(companyId);
                    if (configPoint != null)
                    {
                        decimal pointPlus = 0;
                        if (configPoint.OrderPercent > 0)
                        {
                            var point = order.Due * (configPoint.OrderPercent ?? 0) / 100;
                            pointPlus += point;
                        }
                        if (configPoint.ProductAttribute > 0)
                        {
                            var productIds = order.OrderProducts.Select(e => e.ProductId).ToList();
                            var attributes = productAttributeDAL.GetAll()
                                                    .Where(e => productIds.Contains(e.ProductId) && e.AttributeId == configPoint.ProductAttribute)
                                                    .Select(e => new { e.ProductId, e.Value })
                                                    .ToList();
                            foreach (var product in order.OrderProducts)
                            {
                                var value = attributes.Where(e => e.ProductId == product.ProductId)
                                                .Select(e => e.Value)
                                                .FirstOrDefault();
                                if (value != null)
                                {
                                    var point = 0;
                                    int.TryParse(value, out point);
                                    pointPlus += point * product.Quantity;
                                }
                            }
                        }
                        custemer.Point += pointPlus;
                        customerDAL.Update(custemer);

                        if(custemer.Id > 0)
                        {
                            var cusPoint = new CustomerPoint();
                            cusPoint.CustomerId = order.CustomerId ?? 0;
                            cusPoint.OrderId = order.Id;
                            cusPoint.Addition = pointPlus;
                            cusPoint.Subtraction = order.Point ?? 0;
                            customerPointDAL.Add(cusPoint);
                        }
                    }
                }
            }

            this.SaveChanges();
        }

        public void Delete(int companyId, List<int> ids)
        {
            var orders = this.orderDAL.GetAll()
                    .Where(e => e.CompanyId == companyId && ids.Contains(e.Id))
                    .ToList();
            foreach (var order in orders)
            {
                if (order.Point > 0 && order.CustomerId > 0)
                {
                    var custemer = customerDAL.GetAll().FirstOrDefault(e => e.Id == order.CustomerId || ((e.Phone == order.CustomerPhone || e.Email == order.CustomerEmail) && e.CompanyId == companyId));
                    if (custemer != null)
                    {
                        custemer.Point += order.Point;
                        customerDAL.Update(custemer);
                    }
                }

                this.orderCollectionDAL.Delete(e => e.OrderId == order.Id);
                this.orderDAL.Delete(order);
            }

            this.SaveChanges();
        }

        public void Payment(OrderTransactionModel dto)
        {
            var order = this.orderDAL.GetAll()
                .Where(o => o.Id == dto.OrderId)
                .FirstOrDefault();

            if (order == null) throw new BusinessException("Không tồn tại đơn hàng " + dto.OrderId);

            var totalAmount = this.orderTransactionDAL.GetAll().Where(e => e.OrderId == dto.OrderId).Select(e => e.Amount).DefaultIfEmpty(0).Sum();
            if ((totalAmount + dto.Amount) >= order.Due)
            {
                order.Status = 1;
                order.LastUpdate = DateTime.Now;
                this.orderDAL.Update(order);
            }

            var entity = new OrderTransaction();
            entity.Amount = dto.Amount;
            entity.OrderId = dto.OrderId;
            entity.OrderInfo = dto.OrderInfo;
            entity.OrderType = dto.OrderType;
            entity.RequestTime = dto.RequestTime;
            entity.ResponseMessage = dto.ResponseMessage;
            entity.ResponseTime = dto.ResponseTime;
            entity.ResponseCode = dto.ResponseCode;
            entity.Trans_Ref = dto.Trans_Ref;
            entity.Trans_Status = dto.Trans_Status;
            this.orderTransactionDAL.Add(entity);
            this.SaveChanges();
        }
        #endregion

        #region Supplier
        public IQueryable<SupplierModel> GetSuppliers(int companyId)
        {
            var query = supplierDAL.GetAll()
                        .Where(e => e.CompanyId == companyId)
                        .Select(e => new SupplierModel
                        {
                            Id = e.Id,
                            Address = e.Address,
                            Email = e.Email,
                            Name = e.Name,
                            Phone = e.Phone
                        });

            return query;
        }
        
        public int AddSupplier(SupplierModel model, int companyId)
        {
            try
            {
                var supplier = new Supplier();
                supplier.CompanyId = companyId;
                supplier.Address = model.Address;
                supplier.Email = model.Email;
                supplier.Name = model.Name;
                supplier.Phone = model.Phone;
                
                supplierDAL.Add(supplier);
                this.SaveChanges();
                model.Id = supplier.Id;
                return model.Id;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return 0;
        }

        public void UpdateSupplier(SupplierModel model, int companyId)
        {
            var supplier = this.supplierDAL.GetAll().FirstOrDefault(e => e.Id == model.Id && e.CompanyId == companyId);
            if (supplier == null) throw new BusinessException("Không tồn tại Nhà cung cấp");
            else supplierDAL.Update(supplier);
            
            supplier.Address = model.Address;
            supplier.Email = model.Email;
            supplier.Name = model.Name;
            supplier.Phone = model.Phone;

            this.SaveChanges();
        }
        public void DeleteSupplier(int id, int companyId)
        {
            var entity = this.supplierDAL.GetAll().FirstOrDefault(e => e.Id == id && e.CompanyId == companyId);
            if (entity != null)
            {
                this.supplierDAL.Delete(entity);
                this.SaveChanges();
            }
            else { throw new Exception("Data does not exist"); }
        }
        #endregion

        #region Warehouse
        public IQueryable<WarehouseModel> GetWarehouseIOs(int companyId)
        {
            var query = warehouseIODAL.GetAll()
                        .Where(e => e.CompanyId == companyId)
                        .Select(e => new WarehouseModel
                        {
                            Id = e.Id,
                            Code = e.Code,
                            Date = e.Date,
                            Description = e.Description,
                            TotalCount = e.WarehouseIOColections.Count,
                            TotalPrice = e.TotalPrice,
                            Type = e.Type,
                            SupplierId = e.SupplierId,
                            SupplierName = e.Supplier.Name
                        });

            return query;
        }

        public WarehouseModel GetWarehouseIO(int ioId, int companyId)
        {
            var query = warehouseIODAL.GetAll()
                        .Where(e => e.Id == ioId && e.CompanyId == companyId)
                        .Select(e => new WarehouseModel
                        {
                            Id = e.Id,
                            Code = e.Code,
                            Date = e.Date,
                            Description = e.Description,
                            TotalCount = e.WarehouseIOColections.Count,
                            TotalPrice = e.TotalPrice,
                            Type = e.Type,
                            SupplierId = e.SupplierId,
                            SupplierName = e.Supplier.Name
                        });

            return query.FirstOrDefault();
        }

        public IQueryable<OrderProductModel> GetWarehouseProducts(List<int> ioIds, string language)
        {
            var query = warehouseCollectionDAL.GetAll()
                        .Where(e => ioIds.Contains(e.IOId))
                        .Select(e => new OrderProductModel
                        {
                            IOId = e.IOId,
                            Price = e.Price,
                            Quantity = e.Quantity,
                            ProductId = e.ProductId,
                            ProductCode = e.Product.Code,
                            ProductImage = e.Product.Article.Image,
                            ProductName = e.Product.Article.ArticleLanguages.FirstOrDefault(a => a.LanguageId == language).Title
                        });

            return query;
        }

        public int SaveWarehouseIO(WarehouseModel model, int companyId, int userId, List<OrderProductModel> products)
        {
            var warehouse = warehouseIODAL.AllIncludes(e => e.WarehouseIOColections)
                        .FirstOrDefault(e => e.Id == model.Id && e.CompanyId == companyId);
            if (warehouse == null)
            {
                warehouse = new WarehouseIO();
                warehouse.CompanyId = companyId;

                foreach (var product in products)
                {
                    var productInventory = productDAL.GetAll().FirstOrDefault(e => e.Id == product.ProductId);
                    if (productInventory != null)
                    {
                        var item = new WarehouseIOColection();
                        item.ProductId = product.ProductId;
                        item.Quantity = product.Quantity;
                        item.Price = product.Price;
                        warehouse.WarehouseIOColections.Add(item);

                        if (model.Type) productInventory.Quantity += product.Quantity;
                        else productInventory.Quantity -= product.Quantity;
                    }
                }

                warehouseIODAL.Add(warehouse);
            }
            else
            {
                foreach (var product in products)
                {
                    var productInventory = productDAL.GetAll().FirstOrDefault(e => e.Id == product.ProductId);
                    if (productInventory != null)
                    {
                        var proExit = warehouse.WarehouseIOColections.FirstOrDefault(e => e.ProductId == product.ProductId);
                        if (proExit != null)
                        {
                            proExit.Quantity = product.Quantity;
                            proExit.Price = product.Price;
                        }
                        else
                        {
                            var item = new WarehouseIOColection();
                            item.ProductId = product.ProductId;
                            item.Quantity = product.Quantity;
                            item.Price = product.Price;
                            warehouse.WarehouseIOColections.Add(item);
                        }

                        if (model.Type) productInventory.Quantity += product.Quantity;
                        else productInventory.Quantity -= product.Quantity;
                    }
                }

                var productIds = products.Select(e => e.ProductId).ToList();
                var productRemoves = warehouse.WarehouseIOColections.Where(e => !productIds.Contains(e.ProductId)).ToList();
                foreach (var pr in productRemoves)
                {
                    warehouse.WarehouseIOColections.Remove(pr);
                    var productInventory = productDAL.GetAll().FirstOrDefault(e => e.Id == pr.ProductId);
                    if (productInventory != null)
                    {
                        if (model.Type) productInventory.Quantity -= pr.Quantity;
                        else productInventory.Quantity += pr.Quantity;
                    }
                }

                warehouseIODAL.Update(warehouse);
            }

            warehouse.SupplierId = model.SupplierId;
            warehouse.Code = model.Code;
            warehouse.Date = model.Date;
            warehouse.Description = model.Description;
            warehouse.LastUpdate = DateTime.Now;
            warehouse.UpdateBy = userId;
            warehouse.Type = model.Type;
            warehouse.TotalPrice = model.TotalPrice;
            
            this.SaveChanges();
            return warehouse.Id;
        }

        public void DeleteWarehouseIO(int ioId, int companyId)
        {
            var entity = this.warehouseIODAL.GetAll().FirstOrDefault(e => e.Id == ioId && e.CompanyId == companyId);
            if (entity != null)
            {
                var collection = warehouseCollectionDAL.AllIncludes(e => e.Product).Where(e => e.IOId == entity.Id).ToList();
                foreach(var item in collection)
                {
                    if(entity.Type) item.Product.Quantity -= item.Quantity;
                    else item.Product.Quantity += item.Quantity;
                    productDAL.Update(item.Product);
                }
                warehouseCollectionDAL.Delete(e => e.IOId == entity.Id);
                warehouseIODAL.Delete(entity);
                SaveChanges();
            }
            else { throw new Exception("Data does not exist"); }
        }
        #endregion
        
        #region Voucher
        public IQueryable<VoucherModel> GetVouchers(int companyId)
        {
            var query = voucherDAL.GetAll()
                        .Where(e => e.Item.CompanyId == companyId)
                        .Select(e => new VoucherModel
                        {
                            Id = e.Id,
                            Code = e.Code,
                            IsPercent = e.IsPercent,
                            Value = e.Value,
                            Quantity = e.Quantity,
                            EffectDate = e.EffectDate,
                            ExpirDate = e.ExpirDate,
                            Publish = e.Item.IsPublished
                        });

            return query;
        }

        public VoucherModel GetVoucher(string code, int companyId)
        {
            var query = voucherDAL.GetAll()
                        .Where(e => e.Code == code && e.Item.CompanyId == companyId)
                        .Select(e => new VoucherModel
                        {
                            Id = e.Id,
                            Code = e.Code,
                            IsPercent = e.IsPercent,
                            Value = e.Value,
                            Quantity = e.Quantity,
                            EffectDate = e.EffectDate,
                            ExpirDate = e.ExpirDate,
                            Publish = e.Item.IsPublished
                        });

            return query.FirstOrDefault();
        }

        public int SaveVoucher(VoucherModel model, int companyId, int userId)
        {
            var voucher = voucherDAL.AllIncludes(e => e.Item)
                        .FirstOrDefault(e => e.Id == model.Id && e.Item.CompanyId == companyId);
            if (voucher == null)
            {
                voucher = new Voucher();
                voucher.Item = new Item();

                voucherDAL.Add(voucher);
            }
            else
            {
                voucherDAL.Update(voucher);
                itemDAL.Update(voucher.Item);
            }
            
            voucher.Code = model.Code;
            voucher.IsPercent = model.IsPercent;
            voucher.Value = model.Value;
            voucher.Quantity = model.Quantity;
            voucher.EffectDate = model.EffectDate;
            voucher.ExpirDate = model.ExpirDate;
            voucher.Item.ModifyByUser = userId;
            voucher.Item.ModifyDate = DateTime.Now;
            voucher.Item.CompanyId = companyId;
            voucher.Item.IsPublished = model.Publish;

            this.SaveChanges();
            return voucher.Id;
        }

        public int AddVoucher(VoucherModel model, int companyId, int userId)
        {
            try
            {
                var checkExist = voucherDAL.GetAll().Any(e => e.Code == model.Code);
                if (checkExist) throw new BusinessException("Mã voucher đã tồn tại");

                var voucher = new Voucher();
                voucher.Item = new Item();
                voucher.Code = model.Code;
                voucher.IsPercent = model.IsPercent;
                voucher.Value = model.Value;
                voucher.Quantity = model.Quantity;
                voucher.EffectDate = model.EffectDate;
                voucher.ExpirDate = model.ExpirDate;
                voucher.Item.ModifyByUser = userId;
                voucher.Item.ModifyDate = DateTime.Now;
                voucher.Item.CompanyId = companyId;
                voucher.Item.IsPublished = model.Publish;

                voucherDAL.Add(voucher);
                this.SaveChanges();
                model.Id = voucher.Id;
                return model.Id;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return 0;
        }

        public void UpdateVoucher(VoucherModel model, int companyId, int userId)
        {
            var voucher = voucherDAL.AllIncludes(e => e.Item)
                        .FirstOrDefault(e => e.Id == model.Id && e.Item.CompanyId == companyId);
            if (voucher == null) throw new BusinessException("Không tồn tại Voucher");
            else
            {
                voucherDAL.Update(voucher);
                itemDAL.Update(voucher.Item);
            }

            voucher.Code = model.Code;
            voucher.IsPercent = model.IsPercent;
            voucher.Value = model.Value;
            voucher.Quantity = model.Quantity;
            voucher.EffectDate = model.EffectDate;
            voucher.ExpirDate = model.ExpirDate;
            voucher.Item.ModifyByUser = userId;
            voucher.Item.ModifyDate = DateTime.Now;
            voucher.Item.CompanyId = companyId;
            voucher.Item.IsPublished = model.Publish;

            this.SaveChanges();
        }
        #endregion

        #region Extention
        public string GetValueAttributeProduct(int productId, int attributeId, int companyId)
        {
            var value = this.productAttributeDAL.GetAll()
                            .Where(e => e.Attribute.CompanyId == companyId && e.ProductId == productId && e.AttributeId == attributeId)
                            .Select(e => e.Value)
                            .FirstOrDefault();

            return value;
        }

        public IQueryable<ProductAttributeModel> GetProductAttributes(int id, int companyId)
        {
            var attributes = this.attributeDAL.GetAll()
                            .Where(e => e.CompanyId == companyId)
                            .Select(e => new ProductAttributeModel
                            {
                                CategoryId = e.CategoryId,
                                ID = e.Id,
                                Name = e.Name,
                                Type = e.Type
                            }).ToList();

            var values = this.productAttributeDAL.GetAll()
                            .Where(e => e.Attribute.CompanyId == companyId && e.ProductId == id)
                            .Select(e => new ProductAttributeModel
                            {
                                ID = e.AttributeId,
                                Value = e.Value
                            }).ToList();

            var categorys = attributeValueDAL.GetAll()
                                .Where(e => e.AttributeCategory.Item.CompanyId == companyId)
                                .Select(e => new { e.Id, e.Value })
                                .ToList();

            foreach (var attribute in attributes)
            {
                var value = values.FirstOrDefault(e => e.ID == attribute.ID);
                if (value != null) attribute.Value = value.Value;
                if(attribute.CategoryId > 0 && !string.IsNullOrEmpty(attribute.Value))
                {
                    if (attribute.Type == "ListCheck")
                    {
                        var temp = new List<string>();
                        foreach (var val in attribute.Value.Split(','))
                        {
                            var tempVal = categorys.Where(e => e.Id.ToString() == val)
                                                .Select(e => e.Value)
                                                .FirstOrDefault();
                            temp.Add(tempVal);
                        }
                        attribute.ValueName = string.Join(", ", temp);
                    }
                    else
                    {
                        attribute.ValueName = categorys.Where(e => e.Id.ToString() == attribute.Value)
                                                .Select(e => e.Value)
                                                .FirstOrDefault();
                    }
                }
            }

            return attributes.AsQueryable();
        }

        public IQueryable<ProductPriceModel> GetProducePrices(int id)
        {
            var query = productPriceDAL.GetAll()
                            .Where(e => e.ProductId == id)
                            .Select(e => new ProductPriceModel
                            {
                                ProductId = e.ProductId,
                                Price = e.Price,
                                Quantity = e.Quantity
                            });
            return query;
        }

        public IList<ProductPriceModel> GetAllProducePrices(List<int> ids)
        {
            var query = productPriceDAL.GetAll()
                            .Where(e => ids.Contains(e.ProductId))
                            .Select(e => new ProductPriceModel
                            {
                                ProductId = e.ProductId,
                                Price = e.Price,
                                Quantity = e.Quantity
                            });

            var products = this.productDAL.GetAll().Where(e => ids.Contains(e.Id))
                .Select(e => new ProductPriceModel
                {
                    ProductId = e.Id,
                    Price = e.Sale > 0 ? e.Sale : e.Price,
                    Quantity = 1
                })
                .ToList();

            var data = query.ToList();
            data.AddRange(products);
            data = data.OrderBy(e => e.Quantity).ToList();

            return data;
        }

        public IQueryable<ProductColorModel> GetProduceColors(int id)
        {
            var query = productColorDAL.GetAll()
                            .Where(e => e.ProductId == id)
                            .Select(e => new ProductColorModel
                            {
                                Id = e.Id,
                                ProductId = e.ProductId,
                                Price = e.Price,
                                ImageName = e.ImageName,
                                Name = e.Name,
                                Value = e.Value
                            });
            return query;
        }
        #endregion

        #region statistic
        public ThongKeModel GetThongKe(int companyId, int alert)
        {
            var thongke = new ThongKeModel();

            var products = productDAL.GetAll().Where(e => e.Article.Item.CompanyId == companyId)
                .Select(e => new { e.Id, e.Quantity, Price = e.Sale > 0 ? e.Sale : e.Price })
                .ToList();
            thongke.SanPhamAm = products.Count(e => e.Quantity < 0);
            thongke.SanPhamDaHet = products.Count(e => e.Quantity == 0);
            thongke.SanPhamSapHet = products.Count(e => e.Quantity <= alert);
            thongke.TongTienTonKho = products.Where(e => e.Quantity > 0).Sum(e => e.Quantity * e.Price);

            var orders = this.orderDAL.GetAll().Where(e => e.CompanyId == companyId);
            thongke.DonHangMoi = orders.Count(e => e.Status == 0);
            thongke.DonHangChuaGui = orders.Count(e => e.Status == 1);
            thongke.TongTienBan = orders.Where(e => e.Status == 3).Select(x => x.Due + x.DeliveryFee).DefaultIfEmpty(0).Sum()
                - orders.Where(e => e.Status == 4).Select(x => x.DeliveryFee).DefaultIfEmpty(0).Sum();

            var warehouse = this.warehouseIODAL.GetAll().Where(e => e.CompanyId == companyId).Select(x => new { x.TotalPrice, x.Type });
            thongke.TongTienNhap = warehouse.Where(e => e.Type).Select(e => e.TotalPrice).DefaultIfEmpty(0).Sum() - warehouse.Where(e => !e.Type).Select(e => e.TotalPrice).DefaultIfEmpty(0).Sum();


            return thongke;
        }
        public IList<ThongKeLoiNhuanModel> GetStatisticCurrent(int companyId, int months)
        {
            DateTime date = DateTime.Now;
            date = date.AddMonths(-months + 1);

            return GetStatistic(companyId, date.Month, date.Year, months);
        }

        public IList<ThongKeLoiNhuanModel> GetStatistic(int companyId, int fromMonth, int fromYear, int months)
        {
            var data = new List<ThongKeLoiNhuanModel>();
            if (fromMonth <= 12 && fromMonth >= 0 && fromYear >= 0)
            {
                var fromDate = new DateTime(fromYear, fromMonth, 1);
                var toDate = fromDate.AddMonths(months);

                var orders = this.orderDAL.GetAll()
                    .Where(e => e.CompanyId == companyId && e.Status > 2 && fromDate <= e.CreateDate && e.CreateDate <= toDate)
                    .Select(e => new { e.CreateDate, e.DeliveryFee, e.Due, e.Status })
                    .ToList();
                var warehouseIOs = this.warehouseIODAL.GetAll()
                    .Where(e => e.CompanyId == companyId && fromDate <= e.Date && e.Date <= toDate)
                    .Select(x => new { x.Date, x.TotalPrice, x.Type });

                for (int i = 0; i < months; i++)
                {
                    toDate = fromDate.AddMonths(1);

                    var dto = new ThongKeLoiNhuanModel();
                    dto.Thang = fromDate.Year + "-" + fromDate.Month.ToString("D2");

                    var tongTraHang = orders.Where(e => e.Status == 4 && fromDate <= e.CreateDate && e.CreateDate < toDate)
                        .Select(x => x.Due + x.DeliveryFee)
                        .DefaultIfEmpty(0).Sum();
                    dto.TongThu = orders.Where(e => e.Status == 3 && fromDate <= e.CreateDate && e.CreateDate < toDate)
                        .Select(x => x.Due + x.DeliveryFee)
                        .DefaultIfEmpty(0).Sum() - tongTraHang;

                    var warehouseIOInMonth = warehouseIOs.Where(e => fromDate <= e.Date && e.Date < toDate);
                    dto.TongChi = warehouseIOInMonth.Where(e => e.Type).Select(e => e.TotalPrice).DefaultIfEmpty(0).Sum() - warehouseIOInMonth.Where(e => !e.Type).Select(e => e.TotalPrice).DefaultIfEmpty(0).Sum();

                    dto.LoiNhuan = dto.TongThu - dto.TongChi;

                    data.Add(dto);
                    fromDate = toDate;
                }
            }

            return data;
        }
        #endregion

        public IList<int> GetAllChildId(int parentId, int companyId = 0)
        {
            if (companyId == 0)
            {
                companyId = this.categoryDAL.GetAll().Where(e => e.Id == parentId).Select(e => e.Item.CompanyId).FirstOrDefault();
            }

            var categories = this.categoryDAL.GetAll()
                .Where(e => e.Item.CompanyId == companyId)
                .Select(e => new CategoryId { ID = e.Id, ParentId = e.ParentId ?? 0})
                .ToList();

            var childs = this.GetAllChildId(categories, parentId);
            return childs;
        }

        private IList<int> GetAllChildId(List<CategoryId> category, int parentId)
        {
            var subcats = category.Where(o => o.ParentId == parentId).ToList(); // lay tat ca con cua parentID
            if (!subcats.Any()) return new List<int>();
            var result = new List<int>();
            foreach (var subcat in subcats)
            {
                if (subcat != null)
                {
                    var temp = this.GetAllChildId(category, subcat.ID);

                    result.Add(subcat.ID);
                    if (temp != null && temp.Count > 0)
                    {
                        result.AddRange(temp);
                    }
                }
            }

            return result;
        }

        class CategoryId
        {
            public int ID { get; set; }
            public int ParentId { get; set; }
        }
    }
}
