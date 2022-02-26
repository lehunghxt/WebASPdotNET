namespace Web.Data.Infrastructure
{
    using System.Collections.Generic;
    using System.Data.Entity;

    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private readonly string _connectionString;
        private DbContext _dataContext;

        protected override void DisposeCore()
        {
            this._dataContext.Dispose();
            base.DisposeCore();
        }

        public DatabaseFactory(string connectionString)
        {
            this._connectionString = connectionString;
        }
        
        public DbContext Get()
        {
            return this._dataContext ?? (this._dataContext = new DbContext(this._connectionString));
        }
    }

    public class DatabaseFactory<T> : Disposable, IDatabaseFactory<T> where T : DbContext
    {
        private readonly string _connectionString;
        private T _dataContext;
        private readonly Dictionary<string, object> _dicDataContext;

        protected override void DisposeCore()
        {
            this._dataContext.Dispose();
            base.DisposeCore();
        }

        public DatabaseFactory(string connectionString)
        {
            this._connectionString = connectionString;
            this._dicDataContext = new Dictionary<string, object>();
        }

        public T Get()
        {
            if (this._dataContext == null)
            {
                var type = typeof(T);
                this._dataContext = (T)type.InvokeMember(type.Name, System.Reflection.BindingFlags.CreateInstance, null, null, new object[] { this._connectionString });
                this._dicDataContext[this._connectionString] = this._dataContext;
            }

            return this._dataContext;
        }

        public TSource Get<TSource>(string connectionString) where TSource : class
        {
            if (!this._dicDataContext.ContainsKey(connectionString))
            {
                var type = typeof(TSource);
                this._dicDataContext.Add(connectionString, type.InvokeMember(type.Name, System.Reflection.BindingFlags.CreateInstance, null, null, new object[] { connectionString }));
            }
            return this._dicDataContext[connectionString] as TSource;
        }

        DbContext IDatabaseFactory.Get()
        {
            return this.Get();
        }
    }
}
