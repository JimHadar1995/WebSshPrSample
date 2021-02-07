using System;
using System.Collections.Generic;
using Identity.Core.PostgreSql.Contexts;
using Library.Common.Database;
using Library.Common.Types.Wrappers;

namespace Identity.Core.PostgreSql.Implementations
{
    /// <inheritdoc/>
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityContext _dbContext;

        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private readonly ICacheWrapper _cache;

        /// <summary>
        /// 
        /// </summary>
        public UnitOfWork(IdentityContext dbContext, ICacheWrapper cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }

        /// <inheritdoc/>
        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        /// <inheritdoc/>
        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        /// <inheritdoc/>
        public IRepository<T> Repository<T>() where T : class, IAggregateRoot
        {
            var type = typeof(T);
            if (_repositories.TryGetValue(type, out var value))
            {
                return value as IRepository<T> ?? throw new InvalidOperationException();
            }

            IRepository<T> repo = new Repository<T>(_dbContext, _cache);
            _repositories.Add(type, repo);
            return repo;
        }

        /// <inheritdoc/>
        public void RollbackTransaction()
        {
            _dbContext.Database.RollbackTransaction();
        }
    }
}
