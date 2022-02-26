namespace Web.Business
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using log4net;
    using Web.Data;
    using Web.Data.DataAccess;
    using Web.Data.Infrastructure;

    public abstract class BaseBLL
    {
        private readonly IUnitOfWork _unitOfWork;

        private string _connectionString;
        private string ConnectionString
        {
            get { return this._connectionString ?? System.Configuration.ConfigurationManager.ConnectionStrings["WebConnection"].ConnectionString; }
            set { this._connectionString = value; }
        }

        protected static readonly ILog log = LogManager.GetLogger(typeof(BaseBLL));

        protected IDatabaseFactory<WebEntities> DatabaseFactory { get; private set; }

        protected void DisposeCore()
        {
            if (this._unitOfWork != null) this._unitOfWork.Dispose();
            //base.DisposeCore();
        }

        protected bool CatchError(Exception ex)
        {
            this.RollBack();
            throw ex;
        }

        protected BaseBLL(IUnitOfWork unitOfWork)
        {
            this.DatabaseFactory = unitOfWork.DatabaseFactory as IDatabaseFactory<WebEntities>;
            this._unitOfWork = unitOfWork;
        }

        protected BaseBLL(string connectionString)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                this.ConnectionString = connectionString;
            }

            this.ConnectionString = string.Format("metadata=res://*/WebData.csdl|res://*/WebData.ssdl|res://*/WebData.msl;provider=System.Data.SqlClient;provider connection string='{0};MultipleActiveResultSets=True;App=EntityFramework;'", this.ConnectionString);
            this.DatabaseFactory = new DatabaseFactory<WebEntities>(this.ConnectionString);
            this._unitOfWork = new UnitOfWork(this.DatabaseFactory);
        }

        protected BaseBLL()
            : this((string)null)
        {
        }

        ~BaseBLL()
        {
            DisposeCore();
        }

        public IUnitOfWork UnitOfWork
        {
            get { return this._unitOfWork; }
        }

        public void BeginWork()
        {
            this._unitOfWork.BeginTransaction();
        }

        public void SaveChanges(int companyId = 0, bool checkTryUsing = true, bool clearCacheProvider = true)
        {
            try
            {
                this._unitOfWork.Commit();

                if (companyId > 0)
                {
                    if (checkTryUsing) this.CheckTryUsingBeforeSave(companyId);
                    if (clearCacheProvider) ClearCache("AllDataCache" + companyId);
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var error = string.Empty;
                foreach (var eve in ex.EntityValidationErrors)
                {
                    error = string.Format(
                        "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name,
                        eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        error += string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }

                    log.Error(error);
                }

                throw new BusinessException(error);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message, ex);
            }
        }

        public void RollBack()
        {
            this._unitOfWork.RollBackTransaction();
        }

        public void CheckTryUsingBeforeSave(int companyId)
        {
            var companyDAL = new CompanyDAL(this.DatabaseFactory);
            var demo = companyDAL.GetAll()
                .Where(c => c.Id == companyId)
                .Select(e => e.IsDemo)
                .FirstOrDefault();

            if (demo == null) throw new BusinessException(string.Format("Không tồn tại CompanyId {0}", companyId));
            else if (!demo)
            {
                var itemDAL = new ItemDAL(this.DatabaseFactory);
                var webConfigDAL = new WebConfigDAL(this.DatabaseFactory);
                var maxItem = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MaxItem"]);
                var freeDay = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FreeDay"]);

                var dtoQuery = webConfigDAL.GetAll()
                    .Where(c => c.Id == companyId)
                    .Select(o => new { o.CreateDate, o.RegisDate, o.ExperDate, o.Keys });

                var dto = dtoQuery.FirstOrDefault();
                if (dto != null)
                {
                    if (!dto.ExperDate.HasValue || !dto.RegisDate.HasValue || string.IsNullOrEmpty(dto.Keys))
                    {
                        // neu dang dung thu
                        if ((DateTime.Now.Date - dto.CreateDate.Date).Days > freeDay)
                        {
                            var countItem = itemDAL.GetAll().Count(e => e.CompanyId == companyId && e.IsPublished);
                            if (countItem > maxItem)
                            {
                                log.Warn(string.Format("Vượt quá giới hạn tối đa:"));
                                log.Warn(string.Format("Company: {0}", companyId));
                                log.Warn(string.Format("Max: {0}", maxItem));
                                throw new BusinessException(string.Format("Bạn chỉ được lưu tối đa {0} Item trong quát trình dùng thử, vui lòng đăng ký để sử dụng hết chức năng", maxItem));
                            }
                        }
                    }
                }
            }
        }

        public void ClearCache(string key)
        {
            var th = new System.Threading.Thread(delegate ()
            {
                Post(System.Configuration.ConfigurationManager.AppSettings["DomainPublic"] + "Clear/vit/sCld/" + key, "");
                Post(System.Configuration.ConfigurationManager.AppSettings["DomainPublic"], "");
            });
            th.Start();
        }

        public string Post(string url, string data)
        {
            string vystup = null;
            try
            {
                //Our postvars
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                //Initialisation, we use localhost, change if appliable
                var WebReq = (HttpWebRequest)WebRequest.Create(url);
                //Our method is post, otherwise the buffer (postvars) would be useless
                WebReq.Method = "POST";
                //We use form contentType, for the postvars.
                WebReq.ContentType = "application/x-www-form-urlencoded";
                //The length of the buffer (postvars) is used as contentlength.
                WebReq.ContentLength = buffer.Length;
                //We open a stream for writing the postvars
                var PostData = WebReq.GetRequestStream();
                //Now we write, and afterwards, we close. Closing is always important!
                PostData.Write(buffer, 0, buffer.Length);
                PostData.Close();
                //Get the response handle, we have no true response yet!
                var WebResp = (HttpWebResponse)WebReq.GetResponse();
                //Let's show some information about the response
                Console.WriteLine(WebResp.StatusCode);
                Console.WriteLine(WebResp.Server);

                //Now, we read the response (the string), and output it.
                var Answer = WebResp.GetResponseStream();
                if (Answer != null)
                {
                    var _Answer = new StreamReader(Answer);
                    vystup = _Answer.ReadToEnd();
                }

                //Congratulations, you just requested your first POST page, you
                //can now start logging into most login forms, with your application
                //Or other examples.
            }
            catch (Exception)
            {
            }
            return (vystup ?? string.Empty).Trim() + "\n";
        }
    }
}
