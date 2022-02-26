namespace URM.Business
{
    using System;

    using URM.Data;
    using URM.Data.Infrastructure;

    using log4net;

    public abstract class BLLBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private string _connectionString;
        private string ConnectionString
        {
            get { return this._connectionString ?? System.Configuration.ConfigurationManager.ConnectionStrings["URMConnection"].ConnectionString; }
            set { this._connectionString = value; }
        }

        protected static readonly ILog log = LogManager.GetLogger(typeof(BLLBase));

        protected IDatabaseFactory<URMEntities> DatabaseFactory { get; private set; }

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

        protected BLLBase(IUnitOfWork unitOfWork)
        {
            this.DatabaseFactory = unitOfWork.DatabaseFactory as IDatabaseFactory<URMEntities>;
            this._unitOfWork = unitOfWork;
        }

        protected BLLBase(string connectionString)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                this.ConnectionString = connectionString;
            }

            this.ConnectionString = string.Format("metadata=res://*/URMData.csdl|res://*/URMData.ssdl|res://*/URMData.msl;provider=System.Data.SqlClient;provider connection string='{0};MultipleActiveResultSets=True;App=EntityFramework;'", this.ConnectionString);
            this.DatabaseFactory = new DatabaseFactory<URMEntities>(this.ConnectionString);
            this.DatabaseFactory.Get().Database.Log = i => log.Info(i);
            this._unitOfWork = new UnitOfWork(this.DatabaseFactory);
        }

        protected BLLBase()
            : this((string)null)
        {
        }

        ~BLLBase()
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

        public void SaveChanges()
        {
            try
            {
                this._unitOfWork.Commit();
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

        public enum Order
        {
            /// <summary>
            /// Order Up
            /// </summary>
            Up,

            /// <summary>
            /// Order Down
            /// </summary>
            Down
        }
    }
}
