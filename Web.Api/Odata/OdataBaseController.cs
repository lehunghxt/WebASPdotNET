namespace Web.Api.Odata
{
    using log4net;
    using System;
    using System.Linq;
    using System.Web.Http.Controllers;
    using System.Web.Http.OData;
    using Web.Business;
    using Web.Model;

    public class OdataBaseController<TEntity, TKey> : EntitySetController<TEntity, TKey> where TEntity : class
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(OdataBaseController<TEntity, TKey>));
        private CompanyBLL companyBLL;
        public CompanyConfigModel Web { get; set; }
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            companyBLL = new CompanyBLL();

            var param = this.GetParameter();
            if (param.ContainsKey("CompanyId"))
            {
                var companyId = Convert.ToInt32(param["CompanyId"]);
                Web = companyBLL.GetCompanyById(companyId); 
            }
            else
            {
                var a = this.Request.Headers.Referrer;
                var domain = this.Request.RequestUri.Authority;
                Web = companyBLL.GetCompanyByDomain(domain);
            }

            if (param.ContainsKey("LanguageId"))
            {
                Web.Language = param["LanguageId"];
            }
        }

        #region Default Method
        [EnableQuery]
        public override IQueryable<TEntity> Get()
        {
            throw new NotSupportedException();
        }

        protected override TEntity GetEntityByKey(TKey id)
        {
            throw new NotSupportedException();
        }

        protected override TEntity UpdateEntity(TKey key, TEntity model)
        {
            throw new NotSupportedException();
        }

        protected override TEntity PatchEntity(TKey key, Delta<TEntity> patch)
        {
            var model = this.GetEntityByKey(key);
            patch.Patch(model);
            this.UpdateEntity(key, model);
            return model;
        }

        protected override TKey GetKey(TEntity dto)
        {
            var type = typeof(TEntity);
            var key = type.GetProperty("ID").GetValue(dto);
            return (TKey)key;
        }

        protected override TEntity CreateEntity(TEntity model)
        {
            throw new NotSupportedException();
        }

        public override void Delete(TKey key)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}