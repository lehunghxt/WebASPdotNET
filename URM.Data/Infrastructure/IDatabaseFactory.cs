﻿using System;
using System.Data.Entity;

namespace URM.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        DbContext Get();
    }

    public interface IDatabaseFactory<T> : IDatabaseFactory where T : DbContext
    {
        new T Get();
        TSource Get<TSource>(string connectionString) where TSource : class;
    }
}
