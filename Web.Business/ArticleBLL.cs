
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.Script.Serialization;
using Web.Data;
using Web.Data.DataAccess;
using Web.Model;
using System.Data.Entity;

namespace Web.Business
{
    public class ArticleBLL : BaseBLL
    {
        private CategoryDAL categoryDAL;
        private CategoryLanguageDAL categoryLanguageDAL;
        private ICategoryTypeDAL categoryTypeDAL;
        private ArticleDAL articleDAL;
        private ArticleLanguageDAL articleLanguageDAL;
        private IArticleLinkDAL linkDAL;
        private ItemDAL itemDAL;
        private ItemCommentDAL commentDAL;
        private ISEODAL seoDAL;
        private IFileDAL fileDAL;
        private IFileDocumentDAL documentDAL;
        private IArticleRelatiedDAL relatiedDAL;
        private IWebConfigDAL configDAL;
        private readonly SEODAL seoDal;

        public ArticleBLL(string connectionString = "")
            : base(connectionString)
        {
            categoryDAL = new CategoryDAL(this.DatabaseFactory);
            categoryLanguageDAL = new CategoryLanguageDAL(this.DatabaseFactory);
            categoryTypeDAL = new CategoryTypeDAL(this.DatabaseFactory);
            articleDAL = new ArticleDAL(this.DatabaseFactory);
            articleLanguageDAL = new ArticleLanguageDAL(this.DatabaseFactory);
            linkDAL = new ArticleLinkDAL(this.DatabaseFactory);
            itemDAL = new ItemDAL(this.DatabaseFactory);
            commentDAL = new ItemCommentDAL(this.DatabaseFactory);
            seoDAL = new SEODAL(this.DatabaseFactory);
            fileDAL = new FileDAL(this.DatabaseFactory);
            documentDAL = new FileDocumentDAL(this.DatabaseFactory);
            relatiedDAL = new ArticleRelatiedDAL(this.DatabaseFactory);
            configDAL = new WebConfigDAL(this.DatabaseFactory);
            seoDal = new SEODAL(this.DatabaseFactory);
        }

        #region categoty
        public IQueryable<CATEGORYLANGUAGEModel> GetCategoryByType(int companyId, string language, string type = "", int catId = 0)
        {
            var query = categoryDAL.GetAll().Where(e => e.Item.CompanyId == companyId);
            if (!string.IsNullOrEmpty(type)) query = query.Where(e => e.TypeId == type);

            if (catId > 0)
            {
                var listCategory = GetAllChildId(catId, companyId);
                listCategory.Add(catId);
                query = query.Where(e => listCategory.Contains(e.Id));
            }

            var categories = query.Select(e => new CATEGORYLANGUAGEModel
            {
                ID = e.Id,
                PARENTID = e.ParentId ?? 0,
                TYPEID = e.TypeId,
                IMAGE = e.Image,
                ORDERS = e.Item.Orders,
                PUBLISH = e.Item.IsPublished,
            }).ToList();
            var ids = categories.Select(e => e.ID).ToList();

            var langs = categoryLanguageDAL.GetAll()
                                .Where(e => ids.Contains(e.CategoryId))
                                .Select(e => new CATEGORYLANGUAGEModel
                                {
                                    ID = e.CategoryId,
                                    NAME = e.Title,
                                    DESCRIPTION = e.Description,
                                    Content = e.Contents,
                                    LANGUAGEID = e.LanguageId
                                }).ToList();
            
            var defaultLanguage = configDAL.GetAll().Where(e => e.Id == companyId)
                                                .Select(e => new { e.DefaultLanguage, e.DefaultLanguageIfNotSet})
                                                .FirstOrDefault();

            foreach (var cat in categories)
            {
                var lang = langs.FirstOrDefault(e => e.ID == cat.ID && e.LANGUAGEID == language);
                if (lang != null)
                {
                    cat.LANGUAGEID = lang.LANGUAGEID;
                    cat.NAME = lang.NAME;
                    cat.DESCRIPTION = lang.DESCRIPTION;
                    cat.Content = lang.Content;
                }
                else if (defaultLanguage.DefaultLanguageIfNotSet)
                {
                    lang = langs.FirstOrDefault(e => e.ID == cat.ID && e.LANGUAGEID == defaultLanguage.DefaultLanguage);
                    if (lang == null) lang = langs.FirstOrDefault(e => e.ID == cat.ID);
                    if (lang != null)
                    {
                        cat.LANGUAGEID = lang.LANGUAGEID;
                        cat.NAME = lang.NAME;
                        cat.DESCRIPTION = lang.DESCRIPTION;
                        cat.Content = lang.Content;
                    }
                }

                cat.Languages = string.Join(", ", langs.Where(e => e.ID == cat.ID).Select(e => e.LANGUAGEID));
            }

            //var sql = new SQLSupport("data source=171.244.32.187;initial catalog=Vdoni;user id=sa;password=Ha8gr6LBQ6");

            //    var Articles = sql.ExecuteDataset(false, "select p.*, ABC=c.Title from Product p join CategoryLanguage c on p.CategoryId = c.CategoryId where Id in (select Id from Item where CompanyId = 177) and c.LanguageId = 'vi-VN'").Tables[0];
            //    foreach (DataRow artr in Articles.Rows)
            //    {
            //        var art = new Article();
            //    art.Item = new Item();
            //    art.Item.CompanyId = 12;
            //    art.Item.IsPublished = true;
            //    art.Item.ModifyDate = DateTime.Now;
            //        art.Image = artr["Image"].ToString();
            //        art.CategoryId = langs.FirstOrDefault(e => e.NAME == artr["ABC"].ToString()).ID;
            //        art.DisplayDate = DateTime.Now;
            //        art.Tag = artr["Tag"].ToString();
            //    art.Product = new Product();
            //    art.Product.Code = "NT";
            //    art.Product.Price = 0;
            //    art.Product.Sale = 0;

            //    var catlangs = sql.ExecuteDataset(false, "select * from ProductLanguage where ProductId = " + artr["Id"]).Tables[0];
            //        foreach (DataRow langr in catlangs.Rows)
            //        {
            //            var lang = new ArticleLanguage();
            //            lang.LanguageId = langr["LanguageId"].ToString();
            //            lang.Title = langr["Title"].ToString();
            //        lang.Brief = langr["Brief"].ToString();
            //        lang.Contents = langr["Description"].ToString();

            //            art.ArticleLanguages.Add(lang);
            //        }
            //    (new ArticleDAL(this.DatabaseFactory)).Add(art);
            //    this.SaveChanges();
            //}

            return categories.AsQueryable();
        }
        public CATEGORYLANGUAGEModel GetCategoryById(int companyId, string language, int id)
        {
            var category = categoryDAL.GetAll()
                                .Where(e => e.Item.CompanyId == companyId && e.Id == id)
                                .Select(e => new CATEGORYLANGUAGEModel
            {
                ID = e.Id,
                LANGUAGEID = language,
                PARENTID = e.ParentId ?? 0,
                TYPEID = e.TypeId,
                IMAGE = e.Image,
                ORDERS = e.Item.Orders,
                PUBLISH = e.Item.IsPublished,
            }).FirstOrDefault();
            if (category == null) return null;

            var catLang = categoryLanguageDAL.GetAll()
                                .Where(e => e.Category.Item.CompanyId == companyId && e.LanguageId == language && e.CategoryId == id)
                                .FirstOrDefault();

            var defaultLanguage = configDAL.GetAll().Where(e => e.Id == companyId)
                                                .Select(e => new { e.DefaultLanguage, e.DefaultLanguageIfNotSet })
                                                .FirstOrDefault();

            if (catLang != null)
            {
                category.DESCRIPTION = catLang.Description;
                category.NAME = catLang.Title;
                category.Content = catLang.Contents;
            }
            else if (defaultLanguage.DefaultLanguageIfNotSet)
            {
                catLang = categoryLanguageDAL.GetAll()
                                .Where(e => e.Category.Item.CompanyId == companyId && e.LanguageId == defaultLanguage.DefaultLanguage && e.CategoryId == id)
                                .FirstOrDefault();
                if (catLang == null) catLang = categoryLanguageDAL.GetAll()
                                .Where(e => e.Category.Item.CompanyId == companyId && e.CategoryId == id)
                                .FirstOrDefault();
                if (catLang != null)
                {
                    category.DESCRIPTION = catLang.Description;
                    category.NAME = catLang.Title;
                    category.Content = catLang.Contents;
                }
            }

            return category;
        }

        public IQueryable<CategoryTypeModel> GetCategoryTypes()
        {
            var query = categoryTypeDAL.GetAll()
                        .Select(e => new CategoryTypeModel
                        {
                            ID = e.Id,
                            Name = e.TypeName
                        });
            return query;
        }

        public int AddCategory(CATEGORYLANGUAGEModel model, int companyId, int userId)
        {
            try
            {
                var categoryLanguage = new CategoryLanguage();
                categoryLanguage.LanguageId = model.LANGUAGEID;
                categoryLanguage.Description = model.DESCRIPTION;
                categoryLanguage.Contents = model.Content;
                categoryLanguage.Title = model.NAME;
                categoryLanguage.Category = new Category();
                categoryLanguage.Category.ParentId = model.PARENTID;
                categoryLanguage.Category.TypeId = model.TYPEID;
                categoryLanguage.Category.Image = model.IMAGE;
                categoryLanguage.Category.Item = new Item();
                categoryLanguage.Category.Item.CompanyId = companyId;
                categoryLanguage.Category.Item.ModifyDate = DateTime.Now;
                categoryLanguage.Category.Item.IsPublished = model.PUBLISH;
                categoryLanguage.Category.Item.Orders = model.ORDERS;
                categoryLanguage.Category.Item.ModifyByUser = userId;

                categoryLanguageDAL.Add(categoryLanguage);
                this.SaveChanges();
                model.ID = categoryLanguage.CategoryId;
                return model.ID;
            }catch(Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return 0;
        }

        public int UpdateCategory(CATEGORYLANGUAGEModel model, int companyId, int userId)
        {
            var categoryLanguage = this.categoryLanguageDAL.AllIncludes(e => e.Category, e => e.Category.Item).FirstOrDefault(e => e.CategoryId == model.ID && e.Category.Item.CompanyId == companyId && e.LanguageId == model.LANGUAGEID);
            if (categoryLanguage == null)
            {
                var category = this.categoryDAL.AllIncludes(e => e.Item).FirstOrDefault(e => e.Id == model.ID && e.Item.CompanyId == companyId);
                if(category == null) throw new BusinessException("Không tồn tại Danh mục");
                else
                {
                    categoryLanguage = new CategoryLanguage();
                    categoryLanguage.Category = category;
                    categoryLanguage.LanguageId = model.LANGUAGEID;
                    categoryLanguageDAL.Add(categoryLanguage);
                }
            } else categoryLanguageDAL.Update(categoryLanguage);

            categoryLanguage.Description = model.DESCRIPTION;
            categoryLanguage.Contents = model.Content;
            categoryLanguage.Title = model.NAME;
            categoryLanguage.Category.ParentId = model.PARENTID;
            categoryLanguage.Category.Image = model.IMAGE;
            categoryLanguage.Category.Item.ModifyDate = DateTime.Now;
            categoryLanguage.Category.Item.ModifyByUser = userId;
            categoryLanguage.Category.Item.IsPublished = model.PUBLISH;
            categoryLanguage.Category.Item.Orders = model.ORDERS;

            categoryDAL.Update(categoryLanguage.Category);
            itemDAL.Update(categoryLanguage.Category.Item);

            this.SaveChanges();
            return model.ID;
        }

        public void RemoveCategory(int id, int companyId)
        {
            var entity = this.itemDAL.AllIncludes(e => e.Category.Articles).FirstOrDefault(e => e.Id == id && e.CompanyId == companyId);
            if (entity != null)
            {
                this.articleDAL.Delete(e => e.CategoryId == id);
                this.itemDAL.Delete(entity);
                this.SaveChanges();
            }
            else { throw new Exception("Data does not exist"); }
        }
        #endregion

        #region article
        public List<DataSimpleModel> SearchDataSimple(int companyId, string languageId, string key, out int totalPage, int Skip = 0, int Take = 0)
        {
            var list = this.articleLanguageDAL.GetAll()
                .Where(e => e.Article.Item.CompanyId == companyId && e.LanguageId == languageId && e.Article.Item.IsPublished)
                .Select(e => new
                {
                    e.ArticleId,
                    e.Title,
                    e.Brief,
                    e.Contents,
                    e.Article.Image,
                    e.Article.CategoryId,
                    e.Article.Item.Orders,
                    e.Article.Item.ModifyDate,
                    e.Article.Tag
                })
                .ToList();
            var query = list.Where(e => e.Title.Trim().ConvertToUnSign().ToLower().Contains(key) || e.Tag.ConvertToUnSign().ToLower().Contains(key) || e.Brief.Trim().ConvertToUnSign().ToLower().Contains(key));

            var articles = query.OrderBy(e => e.Orders)
                .ThenByDescending(e => e.ModifyDate)
                .Select(a => new DataSimpleModel
            {
                ID = a.ArticleId,
                CategoryId = a.CategoryId,
                Title = a.Title,
                Description = a.Brief,
                ImagePath = a.Image,
                TargetTag = a.Tag
            });

            totalPage = articles.Count();
            if (Skip > 0) articles = articles.Skip(Skip);
            if (Take > 0) articles = articles.Take(Take);

            var data = articles.ToList();

            return data;
        }

        public List<ArticleBilingualModel> GetArticleBilinguals(int companyId, int categoryId, string language, string order, string tag, out int totalPage, int Skip = 0, int Take = 0)
        {
            var query = articleDAL.GetAll().Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished)
                                           .Where(e => e.Category.TypeId == "ART" && e.CategoryId == categoryId);

            if (!string.IsNullOrEmpty(tag))
            {
                tag = tag.Replace("-", " ").ToLower();
                query = query.Where(e => e.Tag.ToLower().Contains(tag));
            }

             var articles = query.Select(e => new ArticleBilingualModel
            {
                ID = e.Id,
                CategoryId = e.CategoryId,
                ImagePath = e.Image,
                HasComment = e.HasComment ?? true,
                Views = e.Item.ItemView == null ? 0 : e.Item.ItemView.Views,
                TargetTag = e.Tag,
                CreateDate = e.DisplayDate ?? e.Item.ModifyDate,
                 Title1 = "",
                 Description1 = "",
                 Title2 = "",
                 Description2 = ""
             }).ToList();

            var ids = articles.Select(e => e.ID).ToList();
            var langs = articleLanguageDAL.GetAll()
                                .Where(e => ids.Contains(e.ArticleId))
                                .Select(e => new
                                {
                                    ID = e.ArticleId,
                                    LANGUAGEID = e.LanguageId,
                                    Title = e.Title,
                                    Brief = e.Brief,
                                    Contents = e.Contents,
                                }).ToList();

            var lstSEO = this.seoDal.GetAll()
                .Where(e => e.CompanyId == companyId && (e.RefItem == null || (e.RefItem > 0 && ids.Contains(e.RefItem ?? 0))))
                .Select(o => new SEOLinkModel
                {
                    SeoUrl = o.SEOURL,
                    Url = o.URL
                })
                .ToList();

            foreach (var article in articles)
            {
                var lang1 = langs.FirstOrDefault(e => e.ID == article.ID && e.LANGUAGEID.ToLower() == language);
                if (lang1 != null)
                {
                    article.Title1 = lang1.Title;
                    article.Description1 = lang1.Brief;
                    article.Content1 = lang1.Contents;
                }

                var lang2 = langs.FirstOrDefault(e => e.ID == article.ID && e.LANGUAGEID.ToLower() != language);
                if (lang2 != null)
                {
                    article.Title2 = lang2.Title;
                    article.Description2 = lang2.Brief;
                    article.Content2 = lang2.Contents;
                }
                
                article.Link = "/" + article.Title1.ConvertToUnSign() + "-vit-sart-" + article.ID + "-article";
                var link = lstSEO.FirstOrDefault(l => l.Url.ToLower() == article.Link.ToLower());
                if (link != null) article.Link = link.SeoUrl;
            }

            var data = articles.Where(e => !string.IsNullOrEmpty(e.Title1));

            totalPage = data.Count();
            switch (order)
            {
                case "recent":
                    data = data.OrderByDescending(e => e.CreateDate);
                    break;
                case "random":
                    var random = data.ToList();
                    if (totalPage > Take) data = random.GetRandomFromList(Take);
                    return data.ToList();
                    break;
                case "mostviewed":
                    var tomorrow = DateTime.Now.AddDays(1).Date;
                    data = data.OrderByDescending(e => e.Views / (tomorrow - e.CreateDate).Days);
                    break;
            }
            
            if (Skip > 0) data = data.Skip(Skip);
            if (Take > 0) data = data.Take(Take);

            try {
                return data.ToList();
            }
           catch (Exception ex)
            {
                return new List<ArticleBilingualModel>();
            }
        }
        public ArticleBilingualModel GetArticleBilingual(int id, int companyId, string language)
        {
            var query = articleDAL.GetAll().Where(e => e.Item.CompanyId == companyId && e.Id == id);
            var article = query.Select(e => new ArticleBilingualModel
            {
                ID = e.Id,
                CategoryId = e.CategoryId,
                ImagePath = e.Image,
                HasComment = e.HasComment ?? true,
                Views = e.Item.ItemView == null ? 0 : e.Item.ItemView.Views,
                TargetTag = e.Tag,
                CreateDate = e.DisplayDate ?? e.Item.ModifyDate,
                Title1 = "",
                Description1 = "",
                Title2 = "",
                Description2 = ""
            }).FirstOrDefault();

            if (article != null)
            {
                var langs = articleLanguageDAL.GetAll()
                                    .Where(e => e.ArticleId == article.ID)
                                    .Select(e => new
                                    {
                                        ID = e.ArticleId,
                                        LANGUAGEID = e.LanguageId,
                                        Title = e.Title,
                                        Brief = e.Brief,
                                        Contents = e.Contents,
                                    }).ToList();

                var lang1 = langs.FirstOrDefault(e => e.ID == article.ID && e.LANGUAGEID.ToLower() == language);
                if (lang1 != null)
                {
                    article.Title1 = lang1.Title;
                    article.Description1 = lang1.Contents;
                }

                var lang2 = langs.FirstOrDefault(e => e.ID == article.ID && e.LANGUAGEID.ToLower() != language);
                if (lang2 != null)
                {
                    article.Title2 = lang2.Title;
                    article.Description2 = lang2.Contents;
                }
            }
            return article;
        }

        public IQueryable<ARTICLELANGUAGEModel> GetAllArticles(int companyId, string language, int catId = 0)
        {
            //SQLSupport sql = new SQLSupport("data source=.;initial catalog=Vdoni;user id=sa;password=290893326;");
            //var data = sql.ExecuteDataset(false, "select a.id,a.Tag,a.DisplayDate,a.HasComment,l.Title,l.Brief,l.Contents,l.ArticleId,l.LanguageId from [dbo].[ArticleLanguage] l join[dbo].[Article] a on l.ArticleId = a.Id where ArticleId in (select id from[dbo].[Item] where CompanyId = 80) and l.LanguageId = 'vi-VN'");

            //foreach(DataRow row in data.Tables[0].Rows)
            //{
            //    var a = new ARTICLELANGUAGEModel();
            //    a.BRIEF = row["Brief"].ToString();
            //    a.CATEGORYID = 2619;
            //    a.CONTENT = row["Contents"].ToString();
            //    a.DISPLAYDATE = Convert.ToDateTime(row["DisplayDate"]);
            //    a.HASCOMMENT = true;
            //    a.LANGUAGEID = language;
            //    a.PUBLISH = true;
            //    a.TITLE = row["Title"].ToString();
            //    a.TAG = row["Tag"].ToString();
            //    AddArticle(a, new List<int>(), companyId, 10);
            //}
            //this.SaveChanges();

            var query = articleDAL.GetAll().Where(e => e.Item.CompanyId == companyId)
                                            .Where(e => e.Category.TypeId == "ART" && e.Item.CompanyId == companyId);

            if (catId > 0)
            {
                var listCategory = GetAllChildId(catId, companyId);
                listCategory.Add(catId);
                query = query.Where(e => listCategory.Contains(e.CategoryId));
            }

            var articles = query.Select(e => new ARTICLELANGUAGEModel
            {
                ID = e.Id,               
                CATEGORYID = e.CategoryId,
                IMAGE = e.Image,
                ORDERS = e.Item.Orders,
                PUBLISH = e.Item.IsPublished,
                DISPLAYDATE = e.DisplayDate ?? e.Item.ModifyDate,
                HASCOMMENT = e.HasComment ?? true,
                TAG = e.Tag,
                CategoryName = e.Category.CategoryLanguages.FirstOrDefault(c => c.LanguageId == language).Title,
                Views = e.Item.ItemView == null ? 0 : e.Item.ItemView.Views,
                Comments = e.Item.ItemComments.Count,
                TITLE = "",
                BRIEF = "",
                CONTENT = ""
            }).ToList();

            var ids = articles.Select(e => e.ID).ToList();
            var langs = articleLanguageDAL.GetAll()
                                .Where(e => ids.Contains(e.ArticleId))
                                .Select(e => new ARTICLELANGUAGEModel
                                {
                                    ID = e.ArticleId,
                                    LANGUAGEID = e.LanguageId,
                                    TITLE = e.Title,
                                    BRIEF = e.Brief,
                                    CONTENT = e.Contents,
                                }).ToList();

            var defaultLanguage = configDAL.GetAll().Where(e => e.Id == companyId)
                                               .Select(e => new { e.DefaultLanguage, e.DefaultLanguageIfNotSet })
                                               .FirstOrDefault();

            foreach (var article in articles)
            {
                var lang = langs.FirstOrDefault(e => e.ID == article.ID && e.LANGUAGEID == language);
                if (lang != null)
                {
                    article.LANGUAGEID = lang.LANGUAGEID;
                    article.TITLE = lang.TITLE;
                    article.BRIEF = lang.BRIEF;
                    article.CONTENT = lang.CONTENT;
                }
                else if (defaultLanguage.DefaultLanguageIfNotSet)
                {
                    lang = langs.FirstOrDefault(e => e.ID == article.ID && e.LANGUAGEID == defaultLanguage.DefaultLanguage);
                    if (lang == null) lang = langs.FirstOrDefault(e => e.ID == article.ID);
                    if (lang != null)
                    {
                        article.LANGUAGEID = lang.LANGUAGEID;
                        article.TITLE = lang.TITLE;
                        article.BRIEF = lang.BRIEF;
                        article.CONTENT = lang.CONTENT;
                    }
                }

                article.Languages = string.Join(", ", langs.Where(e => e.ID == article.ID).Select(e => e.LANGUAGEID));
            }

            return articles.AsQueryable();
        }

        public IList<ArticleModel> GetArticles(int companyId, string language, int categoryId = 0, bool inChildCat = false, string orderBy = "", string search = "", int take = 0)
        {
            var query = articleDAL.GetAll().Where(e => e.Item.CompanyId == companyId && e.Category.TypeId == "ART");

            if (categoryId > 0)
            {
                IList<int> listCategory;

                if (inChildCat) listCategory = GetAllChildId(categoryId, companyId);
                else listCategory = new List<int>();

                listCategory.Add(categoryId);
                query = query.Where(e => listCategory.Contains(e.CategoryId));
            }
            
            if (!string.IsNullOrEmpty(orderBy))
            {
                var os = query.OrderByDescending(o => o.Id);
                var orders = orderBy.Split('|').Select(o => o.Trim().ToUpper()).ToList();
                var first = true;
                foreach (var order in orders)
                {
                    switch (order)
                    {
                        case "ID": os = first ? query.OrderByDescending(o => o.Id) : os.ThenByDescending(o => o.Id); break;
                        case "VIEW":
                            var tomorrow = DateTime.Now.AddDays(1).Date;
                            var today = DateTime.Now.Date;

                            os = first ? query.OrderByDescending(e => e.Item.ItemView.Views / DbFunctions.DiffDays((DbFunctions.TruncateTime(e.Item.ModifyDate) ?? today), tomorrow)) 
                                : os.ThenByDescending(e => e.Item.ItemView.Views  / DbFunctions.DiffDays((DbFunctions.TruncateTime(e.Item.ModifyDate) ?? today), tomorrow));
                            break;
                        case "VOTE": os = first ? query.OrderByDescending(o => o.Item.ItemVote.VoteNumber) : os.ThenByDescending(o => o.Item.ItemVote.VoteNumber); break;
                        default: break;
                    }

                    first = false;
                }

                query = os;
            }

            if (take > 0) query = query.Skip(0).Take(take);

            var articles = query.Select(e => new ArticleModel
            {
                ID = e.Id,
                CategoryId = e.CategoryId,
                ImagePath = e.Image,
                HasComment = e.HasComment ?? false,
                CreateDate = e.DisplayDate ?? e.Item.ModifyDate,
                TargetTag = e.Tag,
                Title = "",
                Description = "",
            }).ToList();

            var ids = articles.Select(e => e.ID).ToList();
            var langs = articleLanguageDAL.GetAll()
                                .Where(e => ids.Contains(e.ArticleId))
                                .Select(e => new ArticleModel
                                {
                                    ID = e.ArticleId,
                                    Title = e.Title,
                                    Description = e.Brief,
                                    LanguageId = e.LanguageId
                                }).ToList();

            var defaultLanguage = configDAL.GetAll().Where(e => e.Id == companyId)
                                               .Select(e => new { e.DefaultLanguage, e.DefaultLanguageIfNotSet })
                                               .FirstOrDefault();

            foreach (var article in articles)
            {
                var lang = langs.FirstOrDefault(e => e.ID == article.ID && e.LanguageId == language);
                if (lang != null)
                {
                    article.LanguageId = lang.LanguageId;
                    article.Title = lang.Title;
                    article.Description = lang.Description;
                }
                else if (defaultLanguage.DefaultLanguageIfNotSet)
                {
                    lang = langs.FirstOrDefault(e => e.ID == article.ID && e.LanguageId == defaultLanguage.DefaultLanguage);
                    if (lang == null) lang = langs.FirstOrDefault(e => e.ID == article.ID);
                    if (lang != null)
                    {
                        article.LanguageId = lang.LanguageId;
                        article.Title = lang.Title;
                        article.Description = lang.Description;
                    }
                }
            }

            if (!string.IsNullOrEmpty(search))
            {
                var key = search.ToLower();
                if (search.Any(e => e == ' ')) search.ConvertToUnSign();
                articles = articles.Where(e => e.Title.ConvertToUnSign().ToLower().Contains(key) || e.TargetTag.ConvertToUnSign().ToLower().Contains(key) || e.Description.ToLower().ConvertToUnSign().Contains(key)).ToList();
            }

            return articles;
        }

        public IQueryable<ARTICLELANGUAGEModel> GetOtherArticles(int id, int companyId, string language, int top)
        {
            var articleRoot = articleDAL.GetAll().Where(e => e.Id == id && e.Item.CompanyId == companyId)
                                                .Select(e => new { e.CategoryId, e.Item.ModifyDate }).FirstOrDefault();
            if (articleRoot == null) return new List<ARTICLELANGUAGEModel>().AsQueryable();
            var query = articleDAL.GetAll().Where(e => e.Id != id && e.CategoryId == articleRoot.CategoryId && e.Item.CompanyId == companyId && e.Item.ModifyDate < articleRoot.ModifyDate);
            
            var articles = query.OrderBy(e => e.Item.Orders).ThenByDescending(e => e.Item.ModifyDate)
                .Select(e => new ARTICLELANGUAGEModel
            {
                ID = e.Id,
                CATEGORYID = e.CategoryId,
                IMAGE = e.Image,
                ORDERS = e.Item.Orders,
                PUBLISH = e.Item.IsPublished,
                DISPLAYDATE = e.DisplayDate ?? e.Item.ModifyDate,
                HASCOMMENT = e.HasComment ?? true,
                TAG = e.Tag,
                CategoryName = e.Category.CategoryLanguages.FirstOrDefault(c => c.LanguageId == language).Title,
                Views = e.Item.ItemView == null ? 0 : e.Item.ItemView.Views,
                Comments = e.Item.ItemComments.Count,
                    TITLE = "",
                    BRIEF = "",
                    CONTENT = ""
                }).ToList();

            if (top > 0) articles = articles.Skip(0).Take(top).ToList();

            var ids = articles.Select(e => e.ID).ToList();
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

            var defaultLanguage = configDAL.GetAll().Where(e => e.Id == companyId)
                                              .Select(e => new { e.DefaultLanguage, e.DefaultLanguageIfNotSet })
                                              .FirstOrDefault();

            foreach (var article in articles)
            {
                var lang = langs.FirstOrDefault(e => e.ID == article.ID && e.LANGUAGEID == language);
                if (lang != null)
                {
                    article.LANGUAGEID = lang.LANGUAGEID;
                    article.TITLE = lang.TITLE;
                    article.BRIEF = lang.BRIEF;
                    article.CONTENT = lang.CONTENT;
                }
                else if (defaultLanguage.DefaultLanguageIfNotSet)
                {
                    lang = langs.FirstOrDefault(e => e.ID == article.ID && e.LANGUAGEID == defaultLanguage.DefaultLanguage);
                    if (lang == null) lang = langs.FirstOrDefault(e => e.ID == article.ID);
                    if (lang != null)
                    {
                        article.LANGUAGEID = lang.LANGUAGEID;
                        article.TITLE = lang.TITLE;
                        article.BRIEF = lang.BRIEF;
                        article.CONTENT = lang.CONTENT;
                    }
                }

                article.Languages = string.Join(", ", langs.Where(e => e.ID == article.ID).Select(e => e.LANGUAGEID));
            }

            return articles.AsQueryable();
        }

        public IList<DataSimpleModel> GetRelatiedArticles(int id, int companyId, string language)
        {
            var idRelatied = relatiedDAL.GetAll().Where(e => e.Id == id && e.Article.Item.CompanyId == companyId).Select(e => e.ArticleId).ToList();
            
            var langs = articleLanguageDAL.GetAll()
                                .Where(e => idRelatied.Contains(e.ArticleId) && e.LanguageId == language)
                                .Select(e => new DataSimpleModel
                                {
                                    ID = e.ArticleId,
                                    Title = e.Title,
                                    Description = e.Brief,
                                    TargetTag = e.Article.Tag,
                                    CategoryId = e.Article.CategoryId,
                                    ImagePath = e.Article.Image
                                }).ToList();

            var idRelatied2 = relatiedDAL.GetAll().Where(e => e.ArticleId == id && e.Article.Item.CompanyId == companyId).Select(e => e.Id).ToList();
            var langs2 = articleLanguageDAL.GetAll()
                                .Where(e => idRelatied2.Contains(e.ArticleId) && e.LanguageId == language)
                                .Select(e => new DataSimpleModel
                                {
                                    ID = e.ArticleId,
                                    Title = e.Title,
                                    Description = e.Brief,
                                    TargetTag = e.Article.Tag,
                                    CategoryId = e.Article.CategoryId,
                                    ImagePath = e.Article.Image,
                                    URL = "ref"
                                }).ToList();

            langs.AddRange(langs2);

            return langs;
        }
        
        public ARTICLELANGUAGEModel GetArticle(int companyId, string language, int articleId)
        {
            var article = articleDAL.GetAll().Where(e => e.Id == articleId && e.Item.CompanyId == companyId)
                                                .Select(e => new ARTICLELANGUAGEModel
                                                {
                                                    ID = e.Id,
                                                    CATEGORYID = e.CategoryId,
                                                    LANGUAGEID = language,
                                                    IMAGE = e.Image,
                                                    ORDERS = e.Item.Orders,
                                                    PUBLISH = e.Item.IsPublished,
                                                    DISPLAYDATE = e.DisplayDate ?? DateTime.Now,
                                                    HASCOMMENT = e.HasComment ?? true,
                                                    TAG = e.Tag,
                                                    CategoryName = e.Category.CategoryLanguages.FirstOrDefault(c => c.LanguageId == language).Title,
                                                    Views = e.Item.ItemView == null ? 0 : e.Item.ItemView.Views,
                                                    Comments = e.Item.ItemComments.Count,
                                                    TITLE = "",
                                                    BRIEF = "",
                                                    CONTENT = ""
                                                }).FirstOrDefault();
            if (article == null) return null;

            var defaultLanguage = configDAL.GetAll().Where(e => e.Id == companyId)
                                             .Select(e => new { e.DefaultLanguage, e.DefaultLanguageIfNotSet })
                                             .FirstOrDefault();

            var lang = articleLanguageDAL.GetAll().Where(e => e.ArticleId == articleId && e.Article.Item.CompanyId == companyId && e.LanguageId == language).FirstOrDefault();
            if(lang !=null)
            {
                article.TITLE = lang.Title;
                article.BRIEF = lang.Brief;
                article.CONTENT = lang.Contents;
            }
            else if (defaultLanguage.DefaultLanguageIfNotSet)
            {
                lang = articleLanguageDAL.GetAll().FirstOrDefault(e => e.ArticleId == articleId && e.Article.Item.CompanyId == companyId && e.LanguageId == defaultLanguage.DefaultLanguage);
                if (lang == null) lang = articleLanguageDAL.GetAll().FirstOrDefault(e => e.ArticleId == articleId && e.Article.Item.CompanyId == companyId);
                if (lang != null)
                {
                    article.TITLE = lang.Title;
                    article.BRIEF = lang.Brief;
                    article.CONTENT = lang.Contents;
                }
            }

            return article;
        }

        public IList<string> GetTagByCategoryId(int companyId, int categoryId = 0, bool inChildCat = true)
        {
            IList<int> listCategory;
            if (inChildCat) listCategory = GetAllChildId(categoryId, companyId);
            else listCategory = new List<int>();
            listCategory.Add(categoryId);

            var tags = articleDAL.GetAll()
                            .Where(e => e.Item.CompanyId == companyId && listCategory.Contains(e.CategoryId) && e.Category.TypeId == "ART" && e.Item.IsPublished)
                            .Select(e => e.Tag)
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

        public int AddArticle(ARTICLELANGUAGEModel model, List<int> RelatiedArticles, int companyId, int userId)
        {
            var articleLanguage = new ArticleLanguage();

            articleLanguage.LanguageId = model.LANGUAGEID;
            articleLanguage.Brief = model.BRIEF;
            articleLanguage.Title = model.TITLE;
            articleLanguage.Contents = model.CONTENT;
            articleLanguage.Article = new Article();
            articleLanguage.Article.Tag = model.TAG;
            articleLanguage.Article.CategoryId = model.CATEGORYID;
            articleLanguage.Article.DisplayDate = model.DISPLAYDATE;
            articleLanguage.Article.HasComment = model.HASCOMMENT;
            articleLanguage.Article.Image = model.IMAGE;
            articleLanguage.Article.Item = new Item();
            articleLanguage.Article.Item.CompanyId = companyId;
            articleLanguage.Article.Item.ModifyDate = DateTime.Now;
            articleLanguage.Article.Item.IsPublished = model.PUBLISH;
            articleLanguage.Article.Item.Orders = model.ORDERS;
            articleLanguage.Article.Item.ModifyByUser = userId;

            articleLanguageDAL.Add(articleLanguage);

            //Relatied
            //articleLanguage.Article.ArticleRelatieds.Clear();
            foreach (var relatied in RelatiedArticles)
            {
                var article = new ArticleRelatied();
                article.ArticleId = relatied;
                articleLanguage.Article.ArticleRelatieds.Add(article);
            }

            this.SaveChanges();
            model.ID = articleLanguage.ArticleId;
            return model.ID;
        }

        public int UpdateArticle(ARTICLELANGUAGEModel model, List<int> RelatiedArticles, int companyId, int userId)
        {
            var articleLanguage = this.articleLanguageDAL.AllIncludes(e => e.Article, e => e.Article.Item, e => e.Article.ArticleRelatieds).FirstOrDefault(e => e.ArticleId == model.ID && e.Article.Item.CompanyId == companyId && e.LanguageId == model.LANGUAGEID);
            if (articleLanguage == null)
            {
                var article = this.articleDAL.AllIncludes(e => e.Item).FirstOrDefault(e => e.Id == model.ID && e.Item.CompanyId == companyId);
                if (article == null) throw new BusinessException("Không tồn tại Danh mục");
                else
                {
                    articleLanguage = new ArticleLanguage();
                    articleLanguage.Article = article;
                    articleLanguage.LanguageId = model.LANGUAGEID;
                    articleLanguageDAL.Add(articleLanguage);
                }
            }
            else articleLanguageDAL.Update(articleLanguage);
            
            articleLanguage.Brief = model.BRIEF;
            articleLanguage.Title = model.TITLE;
            articleLanguage.Contents = model.CONTENT;
            articleLanguage.Article.Tag = model.TAG;
            articleLanguage.Article.CategoryId = model.CATEGORYID;
            articleLanguage.Article.DisplayDate = model.DISPLAYDATE;
            articleLanguage.Article.HasComment = model.HASCOMMENT;
            articleLanguage.Article.Image = model.IMAGE;
            articleLanguage.Article.Item.ModifyDate = DateTime.Now;
            articleLanguage.Article.Item.ModifyByUser = userId;
            articleLanguage.Article.Item.IsPublished = model.PUBLISH;
            articleLanguage.Article.Item.Orders = model.ORDERS;

            //Relatied
            articleLanguage.Article.ArticleRelatieds.Clear();
            var refIds = relatiedDAL.GetMany(e => e.ArticleId == model.ID).Select(e => e.Id).ToList();
            foreach (var relatied in RelatiedArticles)
            {
                if (!refIds.Exists(e => e == relatied))
                {
                    var article = new ArticleRelatied();
                    article.ArticleId = relatied;
                    articleLanguage.Article.ArticleRelatieds.Add(article);
                }
            }

            articleDAL.Update(articleLanguage.Article);
            itemDAL.Update(articleLanguage.Article.Item);
            
            this.SaveChanges();
            return model.ID;
        }
        #endregion

        #region URL
        public IQueryable<ARTICLEURLModel> GetLinks(int companyId, string language, int catId = 0)
        {
            var query = articleDAL.GetAll()
                                .Where(e => e.Category.TypeId == "LIN" && e.Item.CompanyId == companyId);

            if (catId > 0)
            {
                var listCategory = GetAllChildId(catId, companyId);
                listCategory.Add(catId);
                query = query.Where(e => listCategory.Contains(e.CategoryId));
            }

            var links = query.Select(e => new ARTICLEURLModel
            {
                ID = e.Id,
                LANGUAGEID = language,
                IMAGE = e.Image,
                ORDERS = e.Item.Orders,
                PUBLISH = e.Item.IsPublished,
                URL = e.ArticleLink.URL,
                TARGET = e.ArticleLink.Target,
                CATEGORYID = e.CategoryId,
                CategoryName = e.Category.CategoryLanguages.FirstOrDefault(c => c.LanguageId == language).Title,
                TITLE = "",
                BRIEF = "",
                CONTENT = ""
            }).ToList();

            var langs = articleLanguageDAL.GetAll()
                                .Where(e => e.Article.Category.TypeId == "LIN" && e.Article.Item.CompanyId == companyId)
                                .Select(e => new ARTICLEURLModel
                                {
                                    ID = e.ArticleId,
                                    TITLE = e.Title,
                                    BRIEF = e.Brief,
                                    LANGUAGEID = e.LanguageId
                                }).ToList();

            foreach (var lin in links)
            {
                var lang = langs.FirstOrDefault(e => e.ID == lin.ID && e.LANGUAGEID == language);
                if (lang != null)
                {
                    lin.TITLE = lang.TITLE;
                    lin.BRIEF = lang.BRIEF;
                }

                lin.Languages = string.Join(", ", langs.Where(e => e.ID == lin.ID).Select(e => e.LANGUAGEID));
            }

            return links.AsQueryable();
        }

        public int AddLink(ARTICLEURLModel model, int companyId, int userId)
        {
            var articleLanguage = new ArticleLanguage();

            articleLanguage.LanguageId = model.LANGUAGEID;
            articleLanguage.Brief = model.BRIEF;
            articleLanguage.Title = model.TITLE;
            articleLanguage.Article = new Article();
            articleLanguage.Article.CategoryId = model.CATEGORYID;
            articleLanguage.Article.Image = model.IMAGE;
            articleLanguage.Article.Item = new Item();
            articleLanguage.Article.Item.CompanyId = companyId;
            articleLanguage.Article.Item.ModifyDate = DateTime.Now;
            articleLanguage.Article.Item.IsPublished = model.PUBLISH;
            articleLanguage.Article.Item.Orders = model.ORDERS;
            articleLanguage.Article.Item.ModifyByUser = userId;
            articleLanguage.Article.ArticleLink = new ArticleLink();
            articleLanguage.Article.ArticleLink.URL = model.URL;
            articleLanguage.Article.ArticleLink.Target = model.TARGET;

            articleLanguageDAL.Add(articleLanguage);
            this.SaveChanges();
            model.ID = articleLanguage.ArticleId;
            return model.ID;
        }

        public int UpdateLink(ARTICLEURLModel model, int companyId, int userId)
        {
            var articleLanguage = this.articleLanguageDAL.AllIncludes(e => e.Article, e => e.Article.ArticleLink, e => e.Article.Item).FirstOrDefault(e => e.ArticleId == model.ID && e.Article.Item.CompanyId == companyId && e.LanguageId == model.LANGUAGEID);
            if (articleLanguage == null)
            {
                var article = this.articleDAL.AllIncludes(e => e.ArticleLink, e => e.Item).FirstOrDefault(e => e.Id == model.ID && e.Item.CompanyId == companyId);
                if (article == null) throw new BusinessException("Không tồn tại Tài liệu");
                else
                {
                    articleLanguage = new ArticleLanguage();
                    articleLanguage.Article = article;
                    articleLanguage.LanguageId = model.LANGUAGEID;
                    articleLanguageDAL.Add(articleLanguage);
                }
            }
            else articleLanguageDAL.Update(articleLanguage);

            articleLanguage.Brief = model.BRIEF;
            articleLanguage.Title = model.TITLE;
            articleLanguage.Article.CategoryId = model.CATEGORYID;
            articleLanguage.Article.Image = model.IMAGE;
            articleLanguage.Article.Item.ModifyDate = DateTime.Now;
            articleLanguage.Article.Item.ModifyByUser = userId;
            articleLanguage.Article.Item.IsPublished = model.PUBLISH;
            articleLanguage.Article.Item.Orders = model.ORDERS;
            articleLanguage.Article.ArticleLink.URL = model.URL;
            articleLanguage.Article.ArticleLink.Target = model.TARGET;
            
            articleDAL.Update(articleLanguage.Article);
            linkDAL.Update(articleLanguage.Article.ArticleLink);
            itemDAL.Update(articleLanguage.Article.Item);

            this.SaveChanges();
            return model.ID;
        }
        #endregion

        #region Media
        public IQueryable<DocumentModel> GetDocuments(int companyId, string language, int catId = 0)
        {
            var query = articleDAL.GetAll()
                                .Where(e => e.Category.TypeId == "DOC" && e.Item.CompanyId == companyId);

            if (catId > 0)
            {
                var listCategory = GetAllChildId(catId, companyId);
                listCategory.Add(catId);
                query = query.Where(e => listCategory.Contains(e.CategoryId));
            }

            var documents = query.Select(e => new DocumentModel
                                {
                                    ID = e.Id,
                                    LANGUAGEID = language,
                                    IMAGE = e.Image,
                                    ORDERS = e.Item.Orders,
                                    PUBLISH = e.Item.IsPublished,
                                    CATEGORYID = e.CategoryId,
                                    CategoryName = e.Category.CategoryLanguages.FirstOrDefault(c => c.LanguageId == language).Title,
                                    FileName = e.File.FileName,
                                    FileUrl = e.File.FileUrl,
                                    Size = e.File.Size,
                                    Author = e.File.FileDocument.Author,
                                    Pages = e.File.FileDocument.Pages,
                TITLE = "",
                BRIEF = ""
            }).ToList();

            var langs = articleLanguageDAL.GetAll()
                                .Where(e => e.Article.Category.TypeId == "DOC" && e.Article.Item.CompanyId == companyId)
                                .Select(e => new DocumentModel
                                {
                                    ID = e.ArticleId,
                                    TITLE = e.Title,
                                    BRIEF = e.Brief,
                                    LANGUAGEID = e.LanguageId
                                }).ToList();

            foreach (var dov in documents)
            {
                var lang = langs.FirstOrDefault(e => e.ID == dov.ID && e.LANGUAGEID == language);
                if (lang != null)
                {
                    dov.TITLE = lang.TITLE;
                    dov.BRIEF = lang.BRIEF;
                }

                dov.Languages = string.Join(", ", langs.Where(e => e.ID == dov.ID).Select(e => e.LANGUAGEID));
            }

            return documents.AsQueryable();
        }

        public DocumentModel GetDocument(int companyId, string language, int documentId)
        {
            var document = articleDAL.GetAll()
                                .Where(e => e.Id == documentId && e.Category.TypeId == "DOC" && e.Item.CompanyId == companyId)
                                .Select(e => new DocumentModel
                                {
                                    ID = e.Id,
                                    LANGUAGEID = language,
                                    IMAGE = e.Image,
                                    ORDERS = e.Item.Orders,
                                    PUBLISH = e.Item.IsPublished,
                                    CATEGORYID = e.CategoryId,
                                    CategoryName = e.Category.CategoryLanguages.FirstOrDefault(c => c.LanguageId == language).Title,
                                    FileName = e.File.FileName,
                                    FileUrl = e.File.FileUrl,
                                    Size = e.File.Size,
                                    Type = e.File.Type,
                                    Author = e.File.FileDocument.Author,
                                    Pages = e.File.FileDocument.Pages,
                                    Views = e.Item.ItemView == null ? 0 : e.Item.ItemView.Views,
                                }).FirstOrDefault();

            if (document == null) return null;
            var lang = articleLanguageDAL.GetAll()
                                .Where(e => e.ArticleId == documentId && e.Article.Item.CompanyId == companyId && e.LanguageId == language)
                                .Select(e => new DocumentModel
                                {
                                    ID = e.ArticleId,
                                    TITLE = e.Title,
                                    BRIEF = e.Brief,
                                    CONTENT = e.Contents,
                                    LANGUAGEID = e.LanguageId
                                }).FirstOrDefault();


            if (lang != null)
            {
                document.TITLE = lang.TITLE;
                document.BRIEF = lang.BRIEF;
                document.CONTENT = lang.CONTENT;
            }

            return document;
        }

        public int AddDocument(DocumentModel model, int companyId, int userId)
        {
            var articleLanguage = new ArticleLanguage();

            articleLanguage.LanguageId = model.LANGUAGEID;
            articleLanguage.Brief = model.BRIEF;
            articleLanguage.Contents = model.CONTENT;
            articleLanguage.Title = model.TITLE.Trim();
            articleLanguage.Article = new Article();
            articleLanguage.Article.CategoryId = model.CATEGORYID;
            articleLanguage.Article.Image = model.IMAGE;
            articleLanguage.Article.Item = new Item();
            articleLanguage.Article.Item.CompanyId = companyId;
            articleLanguage.Article.Item.ModifyDate = DateTime.Now;
            articleLanguage.Article.Item.IsPublished = model.PUBLISH;
            articleLanguage.Article.Item.Orders = model.ORDERS;
            articleLanguage.Article.Item.ModifyByUser = userId;
            articleLanguage.Article.File = new File();
            articleLanguage.Article.File.FileName = model.FileName;
            articleLanguage.Article.File.FileUrl = model.FileUrl;
            articleLanguage.Article.File.Size = model.Size;
            articleLanguage.Article.File.Type = model.Type;
            if (!string.IsNullOrEmpty(model.Author) || model.Pages > 0)
            {
                articleLanguage.Article.File.FileDocument = new FileDocument();
                articleLanguage.Article.File.FileDocument.Author = model.Author;
                articleLanguage.Article.File.FileDocument.Pages = model.Pages;
            }

            articleLanguageDAL.Add(articleLanguage);
            this.SaveChanges();
            model.ID = articleLanguage.ArticleId;
            return model.ID;
        }

        public int UpdateDocument(DocumentModel model, int companyId, int userId)
        {
            var articleLanguage = this.articleLanguageDAL.AllIncludes(e => e.Article, e => e.Article.File, e => e.Article.File.FileDocument, e => e.Article.Item)
                                                        .FirstOrDefault(e => e.ArticleId == model.ID && e.Article.Item.CompanyId == companyId && e.LanguageId == model.LANGUAGEID);
            if (articleLanguage == null)
            {
                var article = this.articleDAL.AllIncludes(e => e.File, e => e.File.FileDocument, e => e.Item).FirstOrDefault(e => e.Id == model.ID && e.Item.CompanyId == companyId);
                if (article == null) throw new BusinessException("Không tồn tại Tài liệu");
                else
                {
                    articleLanguage = new ArticleLanguage();
                    articleLanguage.Article = article;
                    articleLanguage.LanguageId = model.LANGUAGEID;
                    articleLanguageDAL.Add(articleLanguage);
                }
            }
            else articleLanguageDAL.Update(articleLanguage);

            articleLanguage.Brief = model.BRIEF;
            articleLanguage.Contents = model.CONTENT;
            articleLanguage.Title = model.TITLE.Trim();
            articleLanguage.Article.CategoryId = model.CATEGORYID;
            articleLanguage.Article.Image = model.IMAGE;
            articleLanguage.Article.Item.ModifyDate = DateTime.Now;
            articleLanguage.Article.Item.ModifyByUser = userId;
            articleLanguage.Article.Item.IsPublished = model.PUBLISH;
            articleLanguage.Article.Item.Orders = model.ORDERS;
            articleLanguage.Article.File.FileName = model.FileName;
            articleLanguage.Article.File.FileUrl = model.FileUrl;

            if(model.Size > 0) articleLanguage.Article.File.Size = model.Size;

            if (articleLanguage.Article.File.FileDocument != null)
            {
                articleLanguage.Article.File.FileDocument.Author = model.Author;
                articleLanguage.Article.File.FileDocument.Pages = model.Pages;
                documentDAL.Update(articleLanguage.Article.File.FileDocument);
            } else if (!string.IsNullOrEmpty(model.Author) || model.Pages > 0)
            {
                articleLanguage.Article.File.FileDocument = new FileDocument();
                articleLanguage.Article.File.FileDocument.Author = model.Author;
                articleLanguage.Article.File.FileDocument.Pages = model.Pages;
            }

            articleDAL.Update(articleLanguage.Article);
            itemDAL.Update(articleLanguage.Article.Item);
            fileDAL.Update(articleLanguage.Article.File);

            this.SaveChanges();
            return model.ID;
        }

        public int AddAlbum(CATEGORYLANGUAGEModel model, int companyId, int userId, Dictionary<string, int> fileNames)
        {
            try
            {
                var categoryLanguage = new CategoryLanguage();
                categoryLanguage.LanguageId = model.LANGUAGEID;
                categoryLanguage.Description = model.DESCRIPTION;
                categoryLanguage.Title = model.NAME;
                categoryLanguage.Category = new Category();
                categoryLanguage.Category.ParentId = model.PARENTID;
                categoryLanguage.Category.TypeId = "MID";
                categoryLanguage.Category.Image = model.IMAGE;
                categoryLanguage.Category.Item = new Item();
                categoryLanguage.Category.Item.CompanyId = companyId;
                categoryLanguage.Category.Item.ModifyDate = DateTime.Now;
                categoryLanguage.Category.Item.IsPublished = model.PUBLISH;
                categoryLanguage.Category.Item.Orders = model.ORDERS;
                categoryLanguage.Category.Item.ModifyByUser = userId;

                foreach(var name in fileNames)
                {
                    var article = new Article();
                    article.Image = name.Key;
                    article.File = new Web.Data.File();
                    article.File.FileName = name.Key;
                    article.File.Size = name.Value;
                    article.File.Type = name.Key.Split('_')[0];
                    article.Item = new Item();
                    article.Item.CompanyId = companyId;
                    article.Item.ModifyDate = DateTime.Now;
                    article.Item.IsPublished = true;
                    article.Item.Orders = model.ORDERS;
                    article.Item.ModifyByUser = userId;

                    var articleLanguage = new ArticleLanguage();
                    articleLanguage.LanguageId = model.LANGUAGEID;
                    articleLanguage.Brief = model.DESCRIPTION;
                    articleLanguage.Title = model.NAME;
                    article.ArticleLanguages.Add(articleLanguage);
                    
                    categoryLanguage.Category.Articles.Add(article);
                }

                categoryLanguageDAL.Add(categoryLanguage);
                this.SaveChanges();
                model.ID = categoryLanguage.CategoryId;
                return model.ID;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return 0;
        }
        #endregion

        public DataSimpleModel GetDataSingle(int companyId, string language, string source, int id)
        {
            DataSimpleModel data = new DataSimpleModel();
            var defaultLanguage = configDAL.GetAll().Where(e => e.Id == companyId)
                                                        .Select(e => new { e.DefaultLanguage, e.DefaultLanguageIfNotSet })
                                                        .FirstOrDefault();
            switch (source)
            {
                case "CAT":
                    data = categoryDAL.GetAll()
                               .Where(e => e.Item.CompanyId == companyId && e.Id == id)
                               .Select(e => new DataSimpleModel
                               {
                                   ID = e.Id,
                                   CategoryId = e.ParentId ?? 0,
                                   ImagePath = e.Image,
                                   CreateDate = e.Item.ModifyDate,
                                   TargetTag = e.TypeId,
                                   Description = "",
                                   Title = ""
                               }).FirstOrDefault();
                    if (data == null) return null;

                    var catLang = categoryLanguageDAL.GetAll()
                                        .Where(e => e.Category.Item.CompanyId == companyId && e.LanguageId == language && e.CategoryId == id)
                                        .FirstOrDefault();
                    
                    if (catLang != null)
                    {
                        data.Description = catLang.Description;
                        data.Title = catLang.Title;
                    }
                    else if (defaultLanguage.DefaultLanguageIfNotSet)
                    {
                        catLang = categoryLanguageDAL.GetAll()
                                        .Where(e => e.Category.Item.CompanyId == companyId && e.LanguageId == defaultLanguage.DefaultLanguage && e.CategoryId == id)
                                        .FirstOrDefault();
                        if (catLang == null) catLang = categoryLanguageDAL.GetAll()
                                        .Where(e => e.Category.Item.CompanyId == companyId && e.CategoryId == id)
                                        .FirstOrDefault();
                        if (catLang != null)
                        {
                            data.Description = catLang.Description;
                            data.Title = catLang.Title;
                        }
                    }

                    break;
                case "PRO":
                    data = articleDAL.GetAll()
                              .Where(e => e.Item.CompanyId == companyId && e.Id == id)
                              .Select(e => new DataSimpleModel
                              {
                                  ID = e.Id,
                                  CategoryId = e.CategoryId,
                                  ImagePath = e.Image,
                                  CreateDate = e.Item.ModifyDate,
                                  TargetTag = e.Tag,
                                  URL = e.Product.Code,
                                  Description = "",
                                  Title = ""
                              }).FirstOrDefault();
                    if (data == null) return null;

                    var proLang = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && e.LanguageId == language && e.ArticleId == id)
                                        .FirstOrDefault();

                    if (proLang != null)
                    { 
                        data.Description = proLang.Brief;
                        data.Title = proLang.Title;
                    }
                    else if (defaultLanguage.DefaultLanguageIfNotSet)
                    {
                        proLang = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && e.LanguageId == defaultLanguage.DefaultLanguage && e.ArticleId == id)
                                        .FirstOrDefault();
                        if (proLang == null) proLang = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && e.ArticleId == id)
                                        .FirstOrDefault();
                        if (proLang != null)
                        {
                            data.Description = proLang.Brief;
                            data.Title = proLang.Title;
                        }
                    }
                    break;
                case "ART":
                    data = articleDAL.GetAll()
                              .Where(e => e.Item.CompanyId == companyId && e.Id == id)
                              .Select(e => new DataSimpleModel
                              {
                                  ID = e.Id,
                                  CategoryId = e.CategoryId,
                                  ImagePath = e.Image,
                                  CreateDate = e.Item.ModifyDate,
                                  TargetTag = e.Tag,
                                  Description = "",
                                  Title = ""
                              }).FirstOrDefault();
                    if (data == null) return null;

                    var artLang = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && e.LanguageId == language && e.ArticleId == id)
                                        .FirstOrDefault();

                    if (artLang != null)
                    {
                        data.Description = artLang.Brief;
                        data.Title = artLang.Title;
                    }
                    else if (defaultLanguage.DefaultLanguageIfNotSet)
                    {
                        proLang = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && e.LanguageId == defaultLanguage.DefaultLanguage && e.ArticleId == id)
                                        .FirstOrDefault();
                        if (proLang == null) proLang = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && e.ArticleId == id)
                                        .FirstOrDefault();
                        if (proLang != null)
                        {
                            data.Description = proLang.Brief;
                            data.Title = proLang.Title;
                        }
                    }
                    break;
                case "DOC":
                    data = articleDAL.GetAll()
                              .Where(e => e.Item.CompanyId == companyId && e.Id == id)
                              .Select(e => new DataSimpleModel
                              {
                                  ID = e.Id,
                                  CategoryId = e.CategoryId,
                                  ImagePath = e.Image,
                                  CreateDate = e.Item.ModifyDate,
                                  TargetTag = e.File.FileName,
                                  URL = e.File.FileUrl,
                                  Description = "",
                                  Title = ""
                              }).FirstOrDefault();
                    if (data == null) return null;

                    var docLang = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && e.LanguageId == language && e.ArticleId == id)
                                        .FirstOrDefault();

                    if (docLang != null)
                    {
                        data.Description = docLang.Brief;
                        data.Title = docLang.Title;
                    }
                    else if (defaultLanguage.DefaultLanguageIfNotSet)
                    {
                        proLang = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && e.LanguageId == defaultLanguage.DefaultLanguage && e.ArticleId == id)
                                        .FirstOrDefault();
                        if (proLang == null) proLang = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && e.ArticleId == id)
                                        .FirstOrDefault();
                        if (proLang != null)
                        {
                            data.Description = proLang.Brief;
                            data.Title = proLang.Title;
                        }
                    }
                    break;
                case "LIN":
                    data = articleDAL.GetAll()
                              .Where(e => e.Item.CompanyId == companyId && e.Id == id)
                              .Select(e => new DataSimpleModel
                              {
                                  ID = e.Id,
                                  CategoryId = e.CategoryId,
                                  ImagePath = e.Image,
                                  CreateDate = e.Item.ModifyDate,
                                  TargetTag = e.ArticleLink.Target,
                                  URL = e.ArticleLink.URL,
                                  Description = "",
                                  Title = ""
                              }).FirstOrDefault();
                    if (data == null) return null;

                    var linLang = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && e.LanguageId == language && e.ArticleId == id)
                                        .FirstOrDefault();

                    if (linLang != null)
                    {
                        data.Description = linLang.Brief;
                        data.Title = linLang.Title;
                    }
                    else if (defaultLanguage.DefaultLanguageIfNotSet)
                    {
                        linLang = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && e.LanguageId == defaultLanguage.DefaultLanguage && e.ArticleId == id)
                                        .FirstOrDefault();
                        if (linLang == null) linLang = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && e.ArticleId == id)
                                        .FirstOrDefault();
                        if (linLang != null)
                        {
                            data.Description = linLang.Brief;
                            data.Title = linLang.Title;
                        }
                    }
                    break;
                case "MID":
                    data = articleDAL.GetAll()
                             .Where(e => e.Item.CompanyId == companyId && e.Id == id)
                             .Select(e => new DataSimpleModel
                             {
                                 ID = e.Id,
                                 CategoryId = e.CategoryId,
                                 ImagePath = e.File.FileName,
                                 CreateDate = e.Item.ModifyDate,
                                 TargetTag = e.File.FileName,
                                 URL = e.File.FileUrl,
                                 Description = e.File.Embed,
                                 Title = ""
                             }).FirstOrDefault();

                    var midLang = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && e.LanguageId == language && e.ArticleId == id)
                                        .FirstOrDefault();

                    if (midLang != null)
                    {
                        data.Title = midLang.Title;
                    }
                    else if (defaultLanguage.DefaultLanguageIfNotSet)
                    {
                        linLang = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && e.LanguageId == defaultLanguage.DefaultLanguage && e.ArticleId == id)
                                        .FirstOrDefault();
                        if (linLang == null) linLang = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && e.ArticleId == id)
                                        .FirstOrDefault();
                        if (linLang != null)
                        {
                            data.Title = midLang.Title;
                        }
                    }

                    break;
            }
            return data;
        }

        public IList<DataSimpleModel> GetDataSimple(int companyId, string language, string source, int categoryId, bool includeChildCat = false, int top = 0, string order = "recent")
        {
            var data = new List<DataSimpleModel>();
            var defaultLanguage = configDAL.GetAll().Where(e => e.Id == companyId)
                                                        .Select(e => new { e.DefaultLanguage, e.DefaultLanguageIfNotSet })
                                                        .FirstOrDefault();
            IList<int> listCategory = new List<int>();
            switch(source)
            {
                case "CAT":
                    var queryCat = categoryDAL.GetAll()
                              .Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished)
                              .OrderByDescending(o => o.Id)
                              .Select(e => new DataSimpleModel
                              {
                                  ID = e.Id,
                                  CategoryId = e.ParentId ?? 0,
                                  ImagePath = e.Image,
                                  CreateDate = e.Item.ModifyDate,
                                  TargetTag = e.TypeId,
                                  Title = "",
                                  Description = ""
                              });
                    if (categoryId > 0)
                    {
                        if (includeChildCat)
                        {
                            listCategory = GetAllChildId(categoryId, companyId);
                            listCategory.Add(categoryId);
                            queryCat = queryCat.Where(e => listCategory.Contains(e.CategoryId));
                        }
                        else queryCat = queryCat.Where(e => e.CategoryId == categoryId);
                    }
                    if (top > 0) queryCat = queryCat.Take(top);
                    data = queryCat.ToList();
                    var catIds = data.Select(e => e.ID).ToList();

                    var catLangs = categoryLanguageDAL.GetAll()
                                        .Where(e => e.Category.Item.CompanyId == companyId && catIds.Contains(e.CategoryId))
                                        .ToList();

                    foreach (var cat in data)
                    {
                        var catLang = catLangs.FirstOrDefault(e => e.CategoryId == cat.ID && e.LanguageId == language);
                        if (catLang != null)
                        {
                            cat.Description = catLang.Description;
                            cat.Title = catLang.Title;
                        }
                        else if (defaultLanguage.DefaultLanguageIfNotSet)
                        {
                            catLang = catLangs.FirstOrDefault(e => e.CategoryId == cat.ID && e.LanguageId == defaultLanguage.DefaultLanguage);
                            if (catLang == null) catLang = catLangs.FirstOrDefault(e => e.CategoryId == cat.ID);
                            if (catLang != null)
                            {
                                cat.Description = catLang.Description;
                                cat.Title = catLang.Title;
                            }
                        }
                    }

                    break;
                case "PRO":
                    var queryPRO = articleDAL.GetAll()
                              .Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished && e.Category.TypeId == "PRO")
                              .OrderByDescending(o => o.Id)
                              .Select(e => new DataSimpleModel
                              {
                                  ID = e.Id,
                                  CategoryId = e.CategoryId,
                                  ImagePath = e.Image,
                                  CreateDate = e.Item.ModifyDate,
                                  TargetTag = e.Tag,
                                  URL = e.Product.Code,
                                  Title = "",
                                  Description = ""
                              });
                    if (categoryId > 0)
                    {
                        if (includeChildCat)
                        {
                            listCategory = GetAllChildId(categoryId, companyId);
                            listCategory.Add(categoryId);
                            queryPRO = queryPRO.Where(e => listCategory.Contains(e.CategoryId));
                        }
                        else queryPRO = queryPRO.Where(e => e.CategoryId == categoryId);
                    }


                    switch (order)
                    {
                        case "recent":
                            queryPRO = queryPRO.OrderByDescending(e => e.ID);
                            if (top > 0) queryPRO = queryPRO.Take(top);
                            data = queryPRO.ToList();
                            break;
                        case "random":
                            var random = queryPRO.ToList();
                            if (top == 0) top = random.Count;
                            data = random.GetRandomFromList(top);
                            break;
                    }

                    var proIds = data.Select(e => e.ID).ToList();

                    var proLangs = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && proIds.Contains(e.ArticleId))
                                        .ToList();
                    foreach (var pro in data)
                    {
                        var proLang = proLangs.FirstOrDefault(e => e.ArticleId == pro.ID && e.LanguageId == language);
                        if (proLang != null)
                        {
                            pro.Description = proLang.Brief;
                            pro.Title = proLang.Title;
                        }
                        else if (defaultLanguage.DefaultLanguageIfNotSet)
                        {
                            proLang = proLangs.FirstOrDefault(e => e.ArticleId == pro.ID && e.LanguageId == defaultLanguage.DefaultLanguage);
                            if (proLang == null) proLang = proLangs.FirstOrDefault(e => e.ArticleId == pro.ID);
                            if (proLang != null)
                            {
                                pro.Description = proLang.Brief;
                                pro.Title = proLang.Title;
                            }
                        }
                    }
                    break;
                case "ART":
                    var queryArt = articleDAL.GetAll()
                              .Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished && e.Category.TypeId == "ART")
                              .OrderByDescending(o => o.Id)
                              .Select(e => new DataSimpleModel
                              {
                                  ID = e.Id,
                                  CategoryId = e.CategoryId,
                                  ImagePath = e.Image,
                                  CreateDate = e.Item.ModifyDate,
                                  TargetTag = e.Tag,
                                  URL = e.Product.Code,
                                  Title = "",
                                  Description = ""
                              });
                    if (categoryId > 0)
                    {
                        if (includeChildCat)
                        {
                            listCategory = GetAllChildId(categoryId, companyId);
                            listCategory.Add(categoryId);
                            queryArt = queryArt.Where(e => listCategory.Contains(e.CategoryId));
                        }
                        else queryArt = queryArt.Where(e => e.CategoryId == categoryId);
                    }

                    switch (order)
                    {
                        case "recent":
                            queryArt = queryArt.OrderByDescending(e => e.ID);
                            if (top > 0) queryArt = queryArt.Take(top);
                            data = queryArt.ToList();
                            break;
                        case "random":
                            var random = queryArt.ToList();
                            if (top == 0) top = random.Count;
                            data = random.GetRandomFromList(top);
                            break;
                    }
                    
                    var artIds = data.Select(e => e.ID).ToList();

                    var artLangs = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && artIds.Contains(e.ArticleId))
                                        .ToList();
                    foreach (var art in data)
                    {
                        var artLang = artLangs.FirstOrDefault(e => e.ArticleId == art.ID && e.LanguageId == language);
                        if (artLang != null)
                        {
                            art.Description = artLang.Brief;
                            art.Title = artLang.Title;
                        }
                        else if (defaultLanguage.DefaultLanguageIfNotSet)
                        {
                            artLang = artLangs.FirstOrDefault(e => e.ArticleId == art.ID && e.LanguageId == defaultLanguage.DefaultLanguage);
                            if (artLang == null) artLang = artLangs.FirstOrDefault(e => e.ArticleId == art.ID);
                            if (artLang != null)
                            {
                                art.Description = artLang.Brief;
                                art.Title = artLang.Title;
                            }
                        }
                    }
                    break;
                case "LIN":
                    var queryLIN = articleDAL.GetAll()
                              .Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished && e.Category.TypeId == "LIN")
                              .OrderByDescending(o => o.Id)
                              .Select(e => new DataSimpleModel
                              {
                                  ID = e.Id,
                                  CategoryId = e.CategoryId,
                                  ImagePath = e.Image,
                                  CreateDate = e.Item.ModifyDate,
                                  TargetTag = e.ArticleLink.Target,
                                  URL = e.ArticleLink.URL,
                                  Title = "",
                                  Description = ""
                              });
                    if (categoryId > 0)
                    {
                        if (includeChildCat)
                        {
                            listCategory = GetAllChildId(categoryId, companyId);
                            listCategory.Add(categoryId);
                            queryLIN = queryLIN.Where(e => listCategory.Contains(e.CategoryId));
                        }
                        else queryLIN = queryLIN.Where(e => e.CategoryId == categoryId);
                    }
                    if (top > 0) queryLIN = queryLIN.Take(top);
                    data = queryLIN.ToList();
                    var linIds = data.Select(e => e.ID).ToList();

                    var linLangs = articleLanguageDAL.GetAll()
                                       .Where(e => e.Article.Item.CompanyId == companyId && linIds.Contains(e.ArticleId))
                                       .ToList();

                    var seos = seoDAL.GetAll().Where(e => e.CompanyId == companyId).ToList();
                    foreach (var lin in data)
                    {
                        var linLang = linLangs.FirstOrDefault(e => e.ArticleId == lin.ID && e.LanguageId == language);
                        if (linLang != null)
                        {
                            lin.Description = linLang.Brief;
                            lin.Title = linLang.Title;
                        }
                        else if (defaultLanguage.DefaultLanguageIfNotSet)
                        {
                            linLang = linLangs.FirstOrDefault(e => e.ArticleId == lin.ID && e.LanguageId == defaultLanguage.DefaultLanguage);
                            if (linLang == null) linLang = linLangs.FirstOrDefault(e => e.ArticleId == lin.ID);
                            if (linLang != null)
                            {
                                lin.Description = linLang.Brief;
                                lin.Title = linLang.Title;
                            }
                        }

                        if (lin.URL.Contains("-vit-s"))
                        {
                            var seo = seos.FirstOrDefault(e => e.URL == lin.URL);
                            if (seo != null) lin.URL = seo.SEOURL;
                        }
                    }
                    break;

                case "MID":
                    var queryMID = articleDAL.GetAll()
                             .Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished && e.Category.TypeId == "MID")
                             .Select(e => new DataSimpleModel
                             {
                                 ID = e.Id,
                                 CategoryId = e.CategoryId,
                                 ImagePath = e.File.FileName,
                                 CreateDate = e.Item.ModifyDate,
                                 TargetTag = e.File.FileName,
                                 URL = e.File.FileUrl,
                                 Description = e.File.Embed,
                                 Title = ""
                             });
                    if (categoryId > 0)
                    {
                        if (includeChildCat)
                        {
                            listCategory = GetAllChildId(categoryId, companyId);
                            listCategory.Add(categoryId);
                            queryMID = queryMID.Where(e => listCategory.Contains(e.CategoryId));
                        }
                        else queryMID = queryMID.Where(e => e.CategoryId == categoryId);
                    }

                    switch (order)
                    {
                        case "recent":
                            queryMID = queryMID.OrderByDescending(e => e.ID);
                            if (top > 0) queryMID = queryMID.Take(top);
                            data = queryMID.ToList();
                            break;
                        case "random":
                            var random = queryMID.ToList();
                            if (top == 0) top = random.Count;
                            data = random.GetRandomFromList(top);
                            break;
                    }
                    
                    var midIds = data.Select(e => e.ID).ToList();

                    var midLangs = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && midIds.Contains(e.ArticleId))
                                        .ToList();
                    foreach (var art in data)
                    {
                        var artLang = midLangs.FirstOrDefault(e => e.ArticleId == art.ID && e.LanguageId == language);
                        if (artLang != null)
                        {
                            art.Title = artLang.Title;
                        }
                        else if (defaultLanguage.DefaultLanguageIfNotSet)
                        {
                            artLang = midLangs.FirstOrDefault(e => e.ArticleId == art.ID && e.LanguageId == defaultLanguage.DefaultLanguage);
                            if (artLang == null) artLang = midLangs.FirstOrDefault(e => e.ArticleId == art.ID);
                            if (artLang != null)
                            {
                                art.Title = artLang.Title;
                            }
                        }
                    }
                    break;
                case "DOC":
                    var queryDOC = articleDAL.GetAll()
                             .Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished && e.Category.TypeId == "DOC")
                             .Select(e => new DataSimpleModel
                             {
                                 ID = e.Id,
                                 CategoryId = e.CategoryId,
                                 ImagePath = e.Image,
                                 CreateDate = e.Item.ModifyDate,
                                 TargetTag = e.File.FileName,
                                 URL = e.File.FileUrl,
                                 Description = "",
                                 Title = ""
                             });
                    if (categoryId > 0)
                    {
                        if (includeChildCat)
                        {
                            listCategory = GetAllChildId(categoryId, companyId);
                            listCategory.Add(categoryId);
                            queryMID = queryDOC.Where(e => listCategory.Contains(e.CategoryId));
                        }
                        else queryMID = queryDOC.Where(e => e.CategoryId == categoryId);
                    }

                    switch (order)
                    {
                        case "recent":
                            queryMID = queryDOC.OrderByDescending(e => e.ID);
                            if (top > 0) queryMID = queryMID.Take(top);
                            data = queryMID.ToList();
                            break;
                        case "random":
                            var random = queryDOC.ToList();
                            if (top == 0) top = random.Count;
                            data = random.GetRandomFromList(top);
                            break;
                    }

                    var docIds = data.Select(e => e.ID).ToList();

                    var docLangs = articleLanguageDAL.GetAll()
                                        .Where(e => e.Article.Item.CompanyId == companyId && docIds.Contains(e.ArticleId))
                                        .ToList();
                    foreach (var art in data)
                    {
                        var artLang = docLangs.FirstOrDefault(e => e.ArticleId == art.ID && e.LanguageId == language);
                        if (artLang != null)
                        {
                            art.Title = artLang.Title;
                            art.Description = artLang.Brief;
                        }
                        else if (defaultLanguage.DefaultLanguageIfNotSet)
                        {
                            artLang = docLangs.FirstOrDefault(e => e.ArticleId == art.ID && e.LanguageId == defaultLanguage.DefaultLanguage);
                            if (artLang == null) artLang = docLangs.FirstOrDefault(e => e.ArticleId == art.ID);
                            if (artLang != null)
                            {
                                art.Title = artLang.Title;
                                art.Description = artLang.Brief;
                            }
                        }
                    }
                    break;
            }
            return data.ToList();
        }

        public IList<DataComplexModel> GetDataComplex(int companyId, string language, List<int> listCateId, List<int> listLoadId, string categoryImagePath, string articleImagePath, string productImagePath)
        {
            var data = new List<DataComplexModel>();
            var ids = new List<int>();
            ids.AddRange(listCateId);
            foreach (var catId in listCateId)
            {
                var childs = GetAllChildId(catId, companyId);
                ids.AddRange(childs);
            }

            var idDatas = new List<int>();
            idDatas.AddRange(listLoadId);
            foreach (var catId in listLoadId)
            {
                var childs = GetAllChildId(catId, companyId);
                idDatas.AddRange(childs);
            }

            data = categoryDAL.GetAll()
                                .Where(e => ids.Contains(e.Id) && e.Item.IsPublished)
                                .OrderBy(e => e.Item.Orders)
                                .Select(e => new DataComplexModel
                                {
                                    ID = e.Id,
                                    ParentID = e.ParentId,
                                    Type = e.TypeId,
                                    CategoryName = "",
                                    CategoryDesc = ""
                                }).ToList();
            var catIds = data.Select(e => e.ID).ToList();

            var catLangs = categoryLanguageDAL.GetAll()
                                .Where(e => e.Category.Item.CompanyId == companyId && catIds.Contains(e.CategoryId))
                                .ToList();
            var defaultLanguage = configDAL.GetAll().Where(e => e.Id == companyId)
                                                        .Select(e => new { e.DefaultLanguage, e.DefaultLanguageIfNotSet })
                                                        .FirstOrDefault();
            foreach (var cat in data)
            {
                var catLang = catLangs.FirstOrDefault(e => e.CategoryId == cat.ID && e.LanguageId == language);
                if (catLang != null)
                {
                    cat.CategoryDesc = catLang.Description;
                    cat.CategoryName = catLang.Title;
                }
                else if (defaultLanguage.DefaultLanguageIfNotSet)
                {
                    catLang = catLangs.FirstOrDefault(e => e.CategoryId == cat.ID && e.LanguageId == defaultLanguage.DefaultLanguage);
                    if (catLang == null) catLang = catLangs.FirstOrDefault(e => e.CategoryId == cat.ID);
                    if (catLang != null)
                    {
                        cat.CategoryDesc = catLang.Description;
                        cat.CategoryName = catLang.Title;
                    }
                }
            }

            var jsonSerialiser = new JavaScriptSerializer();
            
            foreach (var cat1 in data)
            {
                if (!string.IsNullOrEmpty(cat1.CategoryImage)) cat1.CategoryImage = categoryImagePath + cat1.CategoryImage;

                foreach (var cat2 in data)
                {
                    if (cat1.ID == cat2.ParentID) cat1.Childs.Add(cat2);
                }

                if (idDatas.Contains(cat1.ID))
                {
                    cat1.Items = GetDataSimple(companyId, language, cat1.Type, cat1.ID);
                    foreach(var item in cat1.Items)
                    {
                        if (!string.IsNullOrEmpty(item.ImagePath))
                        {
                            switch (cat1.Type)
                            {
                                case "PRO":
                                    item.ImagePath = productImagePath + item.ImagePath;
                                    break;
                                case "ART":
                                case "LIN":
                                    item.ImagePath = articleImagePath + item.ImagePath;
                                    break;
                            }
                        }
                    }
                }

                cat1.JsonItems = jsonSerialiser.Serialize(cat1.Items);
                cat1.JsonChilds = jsonSerialiser.Serialize(cat1.Childs);
            }

            //var root = data.Where(e => listCateId.Contains(e.ID)).ToList();
            var root = new List<DataComplexModel>();
            foreach(var catId in listCateId)
            {
                var cat = data.FirstOrDefault(e => e.ID == catId);
                root.Add(cat);
            }

            root = root.Where(e => e != null).ToList();

            return root;
        }

        public IList<DataSimpleModel> GetAllParentId(int companyId, string language, int categoryId)
        {
            var result = new List<DataSimpleModel>();
            var parent = categoryDAL.GetAll()
                            .Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished && e.Id == categoryId)
                            .Select(e => new DataSimpleModel
                            {
                                ID = e.Id,
                                CategoryId = e.ParentId ?? 0,
                                ImagePath = e.Image,
                                CreateDate = e.Item.ModifyDate,
                                TargetTag = e.TypeId,
                                Title = "",
                                Description = ""
                            }).FirstOrDefault();
            while (parent != null)
            {
                result.Add(parent);
                parent = categoryDAL.GetAll()
                            .Where(e => e.Item.CompanyId == companyId && e.Item.IsPublished && e.Id == parent.CategoryId)
                            .Select(e => new DataSimpleModel
                            {
                                ID = e.Id,
                                CategoryId = e.ParentId ?? 0,
                                ImagePath = e.Image,
                                CreateDate = e.Item.ModifyDate,
                                TargetTag = e.TypeId
                            }).FirstOrDefault();
            }

            var catIds = result.Select(e => e.ID).ToList();

            var catLangs = categoryLanguageDAL.GetAll()
                                .Where(e => e.Category.Item.CompanyId == companyId && catIds.Contains(e.CategoryId))
                                .ToList();

            var defaultLanguage = configDAL.GetAll().Where(e => e.Id == companyId)
                                                       .Select(e => new { e.DefaultLanguage, e.DefaultLanguageIfNotSet })
                                                       .FirstOrDefault();

            foreach (var cat in result)
            {
                var catLang = catLangs.FirstOrDefault(e => e.CategoryId == cat.ID && e.LanguageId == language);
                if (catLang != null)
                {
                    cat.Description = catLang.Description;
                    cat.Title = catLang.Title;
                }
                else if (defaultLanguage.DefaultLanguageIfNotSet)
                {
                    catLang = catLangs.FirstOrDefault(e => e.CategoryId == cat.ID && e.LanguageId == defaultLanguage.DefaultLanguage);
                    if (catLang == null) catLang = catLangs.FirstOrDefault(e => e.CategoryId == cat.ID);
                    if (catLang != null)
                    {
                        cat.Description = catLang.Description;
                        cat.Title = catLang.Title;
                    }
                }
            }

            return result;     
        }

        public IList<int> GetAllChildId(int parentId, int companyId = 0)
        {
            if (companyId == 0)
            {
                companyId = this.categoryDAL.GetAll().Where(e => e.Id == parentId).Select(e => e.Item.CompanyId).FirstOrDefault();
            }

            var categories = this.categoryDAL.GetAll()
                .Where(e => e.Item.CompanyId == companyId)
                .Select(e => new CategoryId { ID = e.Id, ParentId = e.ParentId ?? 0 })
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
