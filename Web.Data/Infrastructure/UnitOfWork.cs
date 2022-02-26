namespace Web.Data.Infrastructure
{
    using System.Data;
    using System.Data.Entity;

    public class UnitOfWork : Disposable, IUnitOfWork
    {
        private readonly IDatabaseFactory _databaseFactory;
        private DbContext _dataContext;
        private System.Data.Common.DbTransaction _dbTrans;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this._databaseFactory = databaseFactory;
        }

        protected override void DisposeCore()
        {
            this._databaseFactory.Dispose();
            base.DisposeCore();
        }

        protected DbContext DataContext
        {
            get { return this._dataContext ?? (this._dataContext = this._databaseFactory.Get()); }
        }

        public IDatabaseFactory DatabaseFactory
        {
            get { return this._databaseFactory; }
        }

        public IDbConnection Connection
        {
            get
            {
                return this.DataContext.Database.Connection;
            }
        }

        public void BeginTransaction()
        {
            if (this.DataContext.Database.Connection.State == ConnectionState.Closed) this.DataContext.Database.Connection.Open();
            this._dbTrans = this.DataContext.Database.Connection.BeginTransaction();
        }

        public void Commit()
        {
            this.DataContext.SaveChanges();
            if (this._dbTrans != null)
            {
                var conn = this._dbTrans.Connection;
                this._dbTrans.Commit();
                conn.Close();
            }
        }

        public void RollBackTransaction()
        {
            if (this._dbTrans != null)
            {
                var conn = this._dbTrans.Connection;
                if (conn != null)
                {
                    this._dbTrans.Rollback();
                    conn.Close();
                }
            }
        }
    }
}
