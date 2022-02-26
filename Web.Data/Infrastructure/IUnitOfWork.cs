namespace Web.Data.Infrastructure
{
    using System;
    using System.Data;

    public interface IUnitOfWork : IDisposable
    {
        IDatabaseFactory DatabaseFactory { get; }
        IDbConnection Connection { get; }

        void BeginTransaction();
        void Commit();
        void RollBackTransaction();
    }
}
