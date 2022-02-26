namespace Web.Data.Infrastructure
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    using System.Linq.Expressions;

    public abstract class RepositoryBase<TDb, T>
        where TDb : DbContext where T : class
    {
        private TDb _dataContext;
        private readonly IDbSet<T> _dbset;

        protected IDatabaseFactory<TDb> DatabaseFactory { get; private set; }
        protected TDb DataContext
        {
            get { return this._dataContext ?? (this._dataContext = this.DatabaseFactory.Get()); }
        }

        protected RepositoryBase(IDatabaseFactory<TDb> databaseFactory)
        {
            this.DatabaseFactory = databaseFactory;
            this._dbset = this.DataContext.Set<T>();
        }

        ~RepositoryBase()
        {
            //DatabaseFactory.Dispose();
        }

        public virtual void Add(T entity)
        {
            this._dbset.Add(entity);
        }

        public virtual void UpdateAttach(T entity)
        {            
            this._dbset.Attach(entity);
            this.DataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Update(T entity)
        {
            this.DataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            this._dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            var objects = this._dbset.Where(where).AsEnumerable();
            foreach (T obj in objects)
                this._dbset.Remove(obj);
        }

        public virtual T GetDetachedById(long id)
        {
            var entity = this._dbset.Find(id);
            if (entity != null) this.DataContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual T GetById(long id)
        {
            var entity = this._dbset.Find(id);
            return entity;
        }

        public T GetById(string id)
        {
            return this._dbset.Find(id); 
        }

        public T GetById(params object[] keyValues)
        {
            return this._dbset.Find(keyValues);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return this._dbset.Where(where).FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return this._dbset;
        }

        /// <summary>
        /// The all includes.
        /// </summary>
        /// <param name="includes">
        /// The includes.
        /// </param>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public IQueryable<T> AllIncludes(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = this._dbset;

            foreach (var expression in includes)
            {
                query = query.Include(expression);
            }

            return query;
        }

        public IQueryable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return this._dbset.Where(where);
        }

        public ObjectResult<TSource> ExecuteFunction<TSource>(string functionName, params ObjectParameter[] parameters)
        {
            try
            {
                var objectContext = (this._dataContext as IObjectContextAdapter).ObjectContext;
                return objectContext.ExecuteFunction<TSource>(objectContext.DefaultContainerName + "." + functionName, parameters);
            }
            catch
            {
                return null;
            }
        }

        public bool ExecuteFunctionNonReturn(string functionName, params ObjectParameter[] parameters)
        {
            try
            {
                var objectContext = (this._dataContext as IObjectContextAdapter).ObjectContext;
                objectContext.ExecuteFunction(objectContext.DefaultContainerName + "." + functionName, parameters);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
