using System;
using Microsoft.AspNetCore.Mvc;

namespace Library.Common.Types.Paging
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TFilter">The type of the filter.</typeparam>
    [ModelBinder(typeof(PagingModelBinder))]
    public class PagedQuery<TFilter> : PagedQuery
        where TFilter : class, IFilter
    {
        /// <summary>
        /// Фильтр.
        /// </summary>
        public TFilter Filter { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedQuery{TFilter}"/> class.
        /// </summary>
        public PagedQuery()
        {
            Filter = Activator.CreateInstance<TFilter>();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public abstract class PagedQuery
    {
        /// <summary>
        /// Номер страницы
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Размер страницы
        /// </summary>
        public int PageSize { get; set; }
    }
}
