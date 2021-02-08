using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Identity.Core.PostgreSql.Contexts;
using Library.Common.Database;
using Library.Common.Database.Specifications;
using Library.Common.Exceptions;
using Library.Common.Types.Attributes;
using Library.Common.Types.Paging;
using Library.Common.Types.Wrappers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Identity.Core.PostgreSql.Implementations
{
    /// <inheritdoc/>
    public sealed class Repository<T> : IRepository<T>
        where T : class, IAggregateRoot
    {
        private readonly IdentityContext _dbContext;
        private readonly ICacheWrapper _cache;

        private IQueryable<T> Set => _dbContext.Set<T>();

        public IQueryable<T> Query => _dbContext.Set<T>();

        /// <summary>
        /// 
        /// </summary>
        public Repository(IdentityContext context, ICacheWrapper cache)
        {
            _dbContext = context;
            _cache = cache;
        }

        /// <inheritdoc/>
        public async Task<int> CountAsync(ISpecification<T>? spec = null, CancellationToken cancellationToken = default)
        {
            if (spec == null)
            {
                return await Set.CountAsync(cancellationToken);
            }

            var query = ApplySpecification(spec);

            return await query.CountAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<(IReadOnlyList<T> data, long count)> GetPaged<TFilter>(PagedQuery<TFilter> paging, ISpecification<T>? spec = null, CancellationToken token = default)
            where TFilter : class, IFilter
        {
            var filter = paging.Filter;
            IQueryable<T> query = Set.FullTextSearch(filter.Search, _cache);

            if (spec != null)
            {
                query = query.Where(spec.ToExpression());
            }

            query = query.OrderBy($"{filter.SortField} {filter.OrderBy}");

            var count = query.Count();

            var pagedQuery = await query.Skip(paging.PageNumber * paging.PageSize)
                .Take(paging.PageSize).ToListAsync(token);

            return (pagedQuery, count);
        }

        /// <inheritdoc/>
        public async Task<T> CreateAsync(T entity, CancellationToken token = default)
        {
            await _dbContext.Set<T>().AddAsync(entity, token);
            await _dbContext.SaveChangesAsync(token);

            return entity;
        }

        /// <inheritdoc/>
        public async Task<int> DeleteAsync(T entity, CancellationToken token = default)
        {
            _dbContext.Set<T>().Remove(entity);
            return await _dbContext.SaveChangesAsync(token);
        }

        /// <inheritdoc/>
        public async Task<T> FindByIdAsync(int id, CancellationToken token = default)
        {
            var keyValues = new object[] { id };
            return await _dbContext.Set<T>().FindAsync(keyValues, token);
        }

        /// <inheritdoc/>
        public async Task<T> FirstAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var query = ApplySpecification(spec);

            var entity = await query.FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
                throw new EntityNotFoundException();

            return entity;
        }

        /// <inheritdoc/>
        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default)
        {
            var query = ApplySpecification(spec);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken token = default)
        {
            return await Set.ToListAsync(token);
        }

        /// <inheritdoc/>
        public IReadOnlyList<T> GetAll()
            => Set.ToList();

        /// <inheritdoc/>
        public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T>? spec, CancellationToken token = default)
        {
            var query = ApplySpecification(spec);

            return await query.ToListAsync(token);
        }

        /// <inheritdoc/>
        public async Task<T> UpdateAsync(T entity, CancellationToken token = default)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync(token);
            return entity;
        }

        /// <inheritdoc/>
        public bool Any(ISpecification<T> spec)
        {
            var query = ApplySpecification(spec);

            return query.Any();
        }

        #region [ Help methods ]

        private IQueryable<T> ApplySpecification(ISpecification<T>? spec)
        {
            if (spec == null)
                return Set;

            return Set.Where(spec.ToExpression());
        }

        #endregion
    }
}
