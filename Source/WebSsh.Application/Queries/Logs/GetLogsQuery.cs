using Library.Common.Types.Paging;
using MediatR;
using WebSsh.Application.Dto;
using WebSsh.Application.Dto.Filters;

namespace WebSsh.Application.Queries.Logs
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
