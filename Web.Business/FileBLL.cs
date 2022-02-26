
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Web.Data;
using Web.Data.DataAccess;
using Web.Model;

namespace Web.Business
{
    public class FileBLL : BaseBLL
    {
        private ArticleDAL articleDAL;
        private ArticleLanguageDAL articleLanguageDAL;
        private ArticleLinkDAL linkDAL;
        private ItemDAL itemDAL;
        private ItemCommentDAL commentDAL;
        private SEODAL seoDAL;
        private FileDAL fileDAL;

        public FileBLL(string connectionString = "")
            : base(connectionString)
        {
            articleDAL = new ArticleDAL(this.DatabaseFactory);
            articleLanguageDAL = new ArticleLanguageDAL(this.DatabaseFactory);
            linkDAL = new ArticleLinkDAL(this.DatabaseFactory);
            itemDAL = new ItemDAL(this.DatabaseFactory);
            commentDAL = new ItemCommentDAL(this.DatabaseFactory);
            seoDAL = new SEODAL(this.DatabaseFactory);
            fileDAL = new FileDAL(this.DatabaseFactory);
        }

        #region media
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
                    e.Article.File.FileUrl,
                    e.Article.File.FileName,
                    e.Article.File.Embed,
                    e.Article.File.Type
                })
                .ToList();
            var query = list.Where(e => e.Title.Trim().ConvertToUnSign().Replace("-", "").Replace(" ", "").ToLower().Contains(key) || e.Brief.Trim().ConvertToUnSign().Replace("-", "").Replace(" ", "").ToLower().Contains(key));

            var articles = query.OrderBy(e => e.Orders)
                .ThenByDescending(e => e.ModifyDate)
                .Select(a => new DataSimpleModel
                {
                    ID = a.ArticleId,
                    CategoryId = a.CategoryId,
                    Title = a.Title,
                    Description = a.Brief,
                    ImagePath = a.Image,
                    URL = !string.IsNullOrEmpty(a.Embed) ? a.Embed : !string.IsNullOrEmpty(a.FileUrl) ? a.FileUrl : a.FileName,
                    TargetTag = a.Type
                });

            totalPage = articles.Count();
            if (Skip > 0) articles = articles.Skip(Skip);
            if (Take > 0) articles = articles.Take(Take);

            var data = articles.ToList();

            return data;
        }

        public IQueryable<MediaModel> GetMedias(int companyId, string language, int categoryId)
        {
            var query = articleDAL.GetAll().Where(e => e.Item.CompanyId == companyId && e.Category.TypeId == "MID");

            if (categoryId > 0) query = query.Where(e => e.CategoryId == categoryId);

            var files = query.Select(e => new MediaModel
            {
                ID = e.Id,
                CategoryId = e.CategoryId,
                Views = e.Item.ItemView == null ? 0 : e.Item.ItemView.Views,
                Comments = e.Item.ItemComments.Count,
                FileName = e.File.FileName,
                FileUrl = e.File.FileUrl,
                Embed = e.File.Embed,
                Size = e.File.Size,
                Type = e.File.Type,
                Poster = e.Image
            })
            .OrderByDescending(e => e.ID)
            .ToList();

            var ids = files.Select(e => e.ID).ToList();
            var langs = articleLanguageDAL.GetAll()
                                .Where(e => ids.Contains(e.ArticleId))
                                .Select(e => new MediaModel
                                {
                                    ID = e.ArticleId,
                                    LANGUAGEID = e.LanguageId,
                                    TITLE = e.Title,
                                    BRIEF = e.Brief
                                }).ToList();

            foreach (var file in files)
            {
                file.FilePath = file.FileUrl;

                var lang = langs.FirstOrDefault(e => e.ID == file.ID && e.LANGUAGEID == language);
                if (lang != null)
                {
                    file.LANGUAGEID = lang.LANGUAGEID;
                    file.TITLE = lang.TITLE;
                    file.BRIEF = lang.BRIEF;
                }

                file.Languages = string.Join(", ", langs.Where(e => e.ID == file.ID).Select(e => e.LANGUAGEID));
            }

            return files.AsQueryable();
        }

        public MediaModel GetMedia(int companyId, string language, int id)
        {
            var query = articleDAL.GetAll().Where(e => e.Item.CompanyId == companyId && e.Id == id);
            
            var file = query.Select(e => new MediaModel
            {
                ID = e.Id,
                Views = e.Item.ItemView == null ? 0 : e.Item.ItemView.Views,
                Comments = e.Item.ItemComments.Count,
                FileName = e.File.FileName,
                FileUrl = e.File.FileUrl,
                Embed = e.File.Embed,
                Size = e.File.Size,
                Type = e.File.Type,
                Poster = e.Image
            }).FirstOrDefault();

            if (file == null) return null;
            file.FilePath = file.FileUrl;

            var lang = articleLanguageDAL.GetAll()
                                .Where(e => e.ArticleId == id && e.LanguageId == language)
                                .Select(e => new MediaModel
                                {
                                    ID = e.ArticleId,
                                    LANGUAGEID = e.LanguageId,
                                    TITLE = e.Title,
                                    BRIEF = e.Brief
                                }).FirstOrDefault();

                if (lang != null)
                {
                    file.LANGUAGEID = lang.LANGUAGEID;
                    file.TITLE = lang.TITLE;
                    file.BRIEF = lang.BRIEF;
                }
                
            return file;
        }

        public void AddMedia(MediaModel model, int companyId, int userId)
        {
            var articleLanguage = new ArticleLanguage();

            articleLanguage.LanguageId = model.LANGUAGEID;
            articleLanguage.Brief = model.BRIEF;
            articleLanguage.Title = model.TITLE;
            articleLanguage.Article = new Article();
            articleLanguage.Article.CategoryId = model.CategoryId;
            articleLanguage.Article.Image = model.Poster;
            articleLanguage.Article.Item = new Item();
            articleLanguage.Article.Item.CompanyId = companyId;
            articleLanguage.Article.Item.ModifyDate = DateTime.Now;
            articleLanguage.Article.Item.IsPublished = true;
            articleLanguage.Article.Item.Orders = 0;
            articleLanguage.Article.Item.ModifyByUser = userId;
            articleLanguage.Article.File = new File();
            articleLanguage.Article.File.FileName = model.FileName;
            articleLanguage.Article.File.FileUrl = model.FileUrl;
            articleLanguage.Article.File.Size = model.Size;
            articleLanguage.Article.File.Type = model.Type;
            articleLanguage.Article.File.Embed = model.Embed;

            articleLanguageDAL.Add(articleLanguage);
            //this.SaveChanges();
            //model.ID = articleLanguage.ArticleId;
            //return model.ID;
        }

        public int UpdateMedia(MediaModel model, int companyId, int userId)
        {
            var articleLanguage = this.articleLanguageDAL.AllIncludes(e => e.Article, e => e.Article.Item, e => e.Article.File)
                                                    .FirstOrDefault(e => e.ArticleId == model.ID && e.Article.Item.CompanyId == companyId && e.LanguageId == model.LANGUAGEID);
            if (articleLanguage == null)
            {
                var article = this.articleDAL.AllIncludes(e => e.Item, e => e.File).FirstOrDefault(e => e.Id == model.ID && e.Item.CompanyId == companyId);
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
            articleLanguage.Article.CategoryId = model.CategoryId;
            articleLanguage.Article.Image = model.Poster;
            articleLanguage.Article.Item.ModifyDate = DateTime.Now;
            articleLanguage.Article.Item.ModifyByUser = userId;
            articleLanguage.Article.Item.IsPublished = true;
            articleLanguage.Article.File.FileName = model.FileName;
            articleLanguage.Article.File.FileUrl = model.FileUrl;
            articleLanguage.Article.File.Size = model.Size;
            articleLanguage.Article.File.Type = model.Type;
            articleLanguage.Article.File.Embed = model.Embed;

            articleDAL.Update(articleLanguage.Article);
            itemDAL.Update(articleLanguage.Article.Item);
            fileDAL.Update(articleLanguage.Article.File);

            this.SaveChanges();
            return model.ID;
        }
        #endregion

    }
}
