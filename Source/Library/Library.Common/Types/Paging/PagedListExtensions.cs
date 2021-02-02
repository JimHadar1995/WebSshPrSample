using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Common.Types.Paging
{
    /// <summary>
    /// 
    /// </summary>
    public static class PagedListExtensions
    {
        /// <summary>
        /// Converts to pagedlist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="pagedQuery">The paging parameters.</param>
        /// <param name="allCount">All count.</param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this List<T> data, PagedQuery pagedQuery, int allCount)
            where T : class, new()
        {
            return new PagedList<T>
            {
                Data = data,
                PageNumber = pagedQuery.PageNumber,
                PageSize = pagedQuery.PageSize,
                TotalItems = allCount
            };
        }
        /// <summary>
        /// Converts to pagedlist.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="mappingFunc">The mapping function.</param>
        /// <param name="pagedQuery">The paging parameters.</param>
        /// <returns></returns>
        public static async Task<PagedList<TDto>> ToPagedList<TDto, TEntity>(
            this IQueryable<TEntity> data,
            Func<IQueryable<TEntity>, Task<List<TDto>>> mappingFunc,
            PagedQuery pagedQuery)
            where TDto : class, new()
        {
            var totalItems = await Task.Run(data.Count);

            var entityItems = data
                .Skip(pagedQuery.PageNumber * pagedQuery.PageSize)
                .Take(pagedQuery.PageSize);

            var items = await mappingFunc(entityItems);

            return new PagedList<TDto>
            {
                TotalItems = totalItems,
                Data = items,
                PageSize = pagedQuery.PageSize,
                PageNumber = pagedQuery.PageNumber
            };
        }

        /// <summary>
        /// Converts to pagedlist.
        /// </summary>
        /// <typeparam name="TDto">The type of the dto.</typeparam>
        /// <param name="data">The data.</param>
        /// <param name="pagedQuery">The paging parameters.</param>
        /// <returns></returns>
        public static async Task<PagedList<TDto>> ToPagedList<TDto>(
            this IQueryable<TDto> data,
            PagedQuery pagedQuery)
            where TDto : class, new()
        {
            var totalItems = await Task.Run(data.Count);
            var items = await Task.Run(() => data
                    .Skip(pagedQuery.PageNumber * pagedQuery.PageSize)
                    .Take(pagedQuery.PageSize)
                    .ToList())
                .ConfigureAwait(false);
            return new PagedList<TDto>
            {
                TotalItems = totalItems,
                Data = items,
                PageSize = pagedQuery.PageSize,
                PageNumber = pagedQuery.PageNumber
            };
        }
    }
}
