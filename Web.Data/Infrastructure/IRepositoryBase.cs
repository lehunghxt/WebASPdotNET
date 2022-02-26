namespace Web.Data.Infrastructure
{
    using System;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepositoryBase<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T GetDetachedById(long id);
        T GetById(long id);
        T GetById(string id);
        T GetById(params object[] keyValues);
        T Get(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll();
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);
        ObjectResult<TSource> ExecuteFunction<TSource>(string functionName, params ObjectParameter[] parameters);
        bool ExecuteFunctionNonReturn(string functionName, params ObjectParameter[] parameters);
    }
}
