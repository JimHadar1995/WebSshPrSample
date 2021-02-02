using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using Library.Common.Database;

namespace Identity.Core.PostgreSql.Implementations
{
    /// <inheritdoc/>
    public sealed class Repository<T> : IRepository<T>
        where T : class, IAggregateRoot
    {
        /// <inheritdoc/>
        public Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<T> CreateAsync(T entity, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<int> DeleteAsync(T entity, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<int> DeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public T FindByIdAsync(int id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<T> FirstAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IReadOnlyList<T>> GetAllAsync(CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<T> UpdateAsync(T entity, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
