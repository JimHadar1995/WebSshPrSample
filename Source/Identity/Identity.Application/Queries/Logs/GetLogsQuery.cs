using Identity.Application.Dto;
using Identity.Application.Dto.Filters;
using Library.Common.Types.Paging;
using MediatR;

namespace Identity.Application.Queries.Logs
{
    /// <summary>
    /// Запрос получения списка логов с постраничной разбивкой.
    /// </summary>
    public sealed class GetLogsQuery : IRequest<PagedList<LogDto>>
    {
        /// <summary>
        /// The filter
        /// </summary>
        public readonly PagedQuery<LogFilter> Filter;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetLogsQuery"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public GetLogsQuery(PagedQuery<LogFilter> model) => Filter = model;
    }
}
