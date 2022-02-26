namespace URM.Website.Odata
{
    using Business;
    using log4net;
    using Security;
    using System;
    using System.Linq;
    using System.Net;
    using System.Security.Principal;
    using System.Threading;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.OData;
    using System.Web.Security;

    public class OdataBaseController<TEntity, TKey> : EntitySetController<TEntity, TKey> where TEntity : class
    {
        public new UserPrincipal User;

        protected static readonly ILog log = LogManager.GetLogger(typeof(OdataBaseController<TEntity, TKey>));

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            var request = controllerContext.Request;
            var authHeaderVal = request.Headers.Authorization;

            // RFC 2617 sec 1.2, "scheme" name is case-insensitive
            if (authHeaderVal.Scheme.Equals("token", StringComparison.OrdinalIgnoreCase) && authHeaderVal.Parameter != null)
            {
                var tokens = authHeaderVal.Parameter.Split('|');
                var token = tokens[0];
                var appIdInToken = Convert.ToInt32(tokens[2]);

                try
                {
                    var ticket = FormsAuthentication.Decrypt(token);
                    if (ticket != null && !ticket.Expired)
                    {
                        var userName = ticket.Name;
                        var userBLL = new UserBLL();
                        var user = userBLL.GetProfileByUserName(userName, appIdInToken);
                        if (user != null)
                        {
                            var identity = new GenericIdentity(userName, "token");
                            
                            var roles = new string[] { };
                            if (!string.IsNullOrEmpty(user.Roles)) roles = user.Roles.Split('|');

                            var principal = new UserPrincipal(identity, roles);
                            principal.FullName = user.FullName;
                            principal.UserName = userName;
                            principal.Roles = roles;
                            principal.UserId = user.ID;
                            principal.AppId = user.AppId;

                            Thread.CurrentPrincipal = principal;
                            HttpContext.Current.User = principal;
                            this.User = principal;
                        }
                        else throw new HttpResponseException(HttpStatusCode.Unauthorized);
                    }
                    else new HttpResponseException(HttpStatusCode.Unauthorized);
                }
                catch
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }
            }
            else throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        protected virtual void CheckRole(string role)
        {
            if(this.User == null) throw new HttpResponseException(HttpStatusCode.Unauthorized);
            else if(!this.User.Roles.Contains(role)) throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        #region Default Method
        [EnableQuery]
        public override IQueryable<TEntity> Get()
        {
            try
            {
                return Select();
            }
            catch (BusinessException ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }

        protected override TEntity GetEntityByKey(TKey id)
        {
            throw new NotSupportedException();
        }

        protected override TEntity UpdateEntity(TKey key, TEntity model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var error = ModelState.Values.FirstOrDefault(e => e.Errors.Count > 0);
                    if(error != null) log.Error(ModelState.Values.FirstOrDefault().Errors[0].ErrorMessage);
                }

                return Update(key, model);
            }
            catch (BusinessException ex)
            {
                log.Error(ex.Message, ex);
                return model;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
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
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.FirstOrDefault(e => e.Errors.Count > 0);
                if (error != null) log.Error(ModelState.Values.FirstOrDefault().Errors[0].ErrorMessage);
            }

            try
            {
                return Insert(model);
            }
            catch (BusinessException ex)
            {
                log.Error(ex.Message, ex);
                return model;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }

            return model;
        }

        public override void Delete(TKey key)
        {
            try
            {
                Drop(key);
            }
            catch (BusinessException ex)
            {
                log.Error(ex.Message, ex);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
        }
        #endregion

        #region Public Method
        [EnableQuery]
        public virtual IQueryable<TEntity> Select()
        {
            throw new NotSupportedException();
        }

        protected virtual TEntity Update(TKey key, TEntity model)
        {
            throw new NotSupportedException();
        }

        protected virtual TEntity Insert(TEntity model)
        {
            throw new NotSupportedException();
        }

        public virtual void Drop(TKey key)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}