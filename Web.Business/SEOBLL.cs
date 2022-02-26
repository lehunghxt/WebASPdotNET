namespace Web.Business
{
    using Library;
    using Library.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Web.Data;
    using Web.Data.DataAccess;
    using Web.Model;

    public class SEOBLL : BaseBLL
    {
        private readonly SEODAL seoDal;
        private readonly ItemDAL itemDal;
        private readonly CategoryLanguageDAL categoryDAL;

        #region Constructor

        public SEOBLL(string connectionString = "")
            : base(connectionString)
        {
            this.seoDal = new SEODAL(this.DatabaseFactory);
            itemDal = new ItemDAL(this.DatabaseFactory);
            this.categoryDAL = new CategoryLanguageDAL(this.DatabaseFactory);
        }
        #endregion

        #region SEO
        public IList<SEOLinkModel> GetAll(int companyId)
        {
            var ids = itemDal.GetAll()
                    .Where(e => e.CompanyId == companyId && e.IsPublished == true)
                    .Select(e => e.Id)
                    .ToList();

            var lst = this.seoDal.GetAll()
                .Where(e => e.CompanyId == companyId && (e.RefItem == null || (e.RefItem > 0 && ids.Contains(e.RefItem ?? 0))))
                .Select(o => new SEOLinkModel
                {
                    SeoUrl = o.SEOURL,
                    Url = o.URL,
                    Title = o.Title,
                    RefItem = o.RefItem,
                    MetaKeyWork = o.MetaKeyWork,
                    MetaDescription = o.MetaDescription,
                    LanguageId = o.LanguageId
                });

            return lst.ToList();
        }

        public SEOLinkModel GetById(int id, int companyId)
        {
            var seoInfoDto = this.seoDal.GetMany(o => o.RefItem == id && o.CompanyId == companyId)
                .Select(o => new SEOLinkModel
                {
                    SeoUrl = o.SEOURL,
                    Url = o.URL,
                    Title = o.Title,
                    MetaKeyWork = o.MetaKeyWork,
                    MetaDescription = o.MetaDescription,
                    LanguageId = o.LanguageId
                }).FirstOrDefault();

            return seoInfoDto;
        }

        public SEOLinkModel GetByUrl(string url, int companyId)
        {
            var seoInfoDto = this.seoDal.GetMany(o => (o.SEOURL == url || o.URL == url) && o.CompanyId == companyId)
                .Select(o => new SEOLinkModel
                {
                        SeoUrl = o.SEOURL,
                        Url = o.URL,
                        Title = o.Title,
                        MetaKeyWork = o.MetaKeyWork,
                        MetaDescription = o.MetaDescription
                    }).FirstOrDefault();

            return seoInfoDto;
        }

        public void Deletes(string[] ids)
        {
            try
            {
                foreach (var url in ids)
                {
                    var seo = this.seoDal.Get(o => o.SEOURL == url);
                    if (seo != null)
                    {
                        this.seoDal.Delete(seo);
                    }
                }

                this.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public void Delete(int refId)
        {
            try
            {
                    var seo = this.seoDal.Get(o => o.RefItem == refId);
                    if (seo != null)
                    {
                        this.seoDal.Delete(seo);
                    }

                this.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public int Save(SEOLinkModel seoLink, int companyId, string languageId)
        {
            try
            {
                var seo = this.seoDal.Get(o => (o.RefItem == seoLink.RefItem || (o.RefItem == null && o.SEOURL == seoLink.SeoUrl)) && o.CompanyId == companyId && o.LanguageId == languageId);
                if (seo == null) //thêm
                {
                    seo = new SEO();
                    seo.CompanyId = companyId;
                    seo.LanguageId = languageId;
                    this.seoDal.Add(seo);
                }
                else this.seoDal.Update(seo);

                var checkExist = this.seoDal.GetAll().Any(o => (o.RefItem != null && o.RefItem != seoLink.RefItem && o.SEOURL == seoLink.SeoUrl) && o.CompanyId == companyId && o.LanguageId == languageId);
                if (checkExist) seoLink.SeoUrl += "-" + seoLink.RefItem;

                seo.SEOURL = seoLink.SeoUrl;
                seo.Title = seoLink.Title;
                seo.URL = seoLink.Url;
                seo.MetaKeyWork = seoLink.MetaKeyWork;
                seo.MetaDescription = seoLink.MetaDescription;
                if(seoLink.RefItem > 0) seo.RefItem = seoLink.RefItem;

                this.SaveChanges();

                return seo.Id;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
        #endregion
    }
}