using System;
using System.Collections.Generic;

namespace Library.Common.Types.Paging
{
    /// <summary>
    /// Результат запроса с постраничной разбивкой.
    /// </summary>
    public sealed class PagedList<TDtoModel> : PagedList
        where TDtoModel : class, new()
    {
        /// <summary>
        /// Данные для выборки
        /// </summary>
        public IReadOnlyList<TDtoModel> Data { get; set; }
            = new List<TDtoModel>();
    }

    /// <summary>
    /// Результат запроса с постраничной разбивкой.
    /// </summary>
    public class PagedList
    {
        /// <summary>
        /// Всего элементов
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Номер страницы
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Размер страницы, сколько элементов на странице
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Всего страниц
        /// </summary>
        public int TotalPages =>
            (int)Math.Ceiling(this.TotalItems / (double)this.PageSize);

        /// <summary>
        /// Есть ли предыдущая страница
        /// </summary>
        public bool HasPreviousPage => this.PageNumber > 0;

        /// <summary>
        /// Есть ли следующая страница
        /// </summary>
        public bool HasNextPage => this.PageNumber < this.TotalPages - 1;

    }
}
