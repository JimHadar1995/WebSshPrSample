using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Dto;
using Identity.Application.Dto.Filters;
using Library.Common.Types.Paging;

namespace Identity.Application.Services.Contracts
{
    /// <summary>
    /// Сервис для работы с логами
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Получение списка логов
        /// </summary>
        /// <param name="filter">Параметры фильтра.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<PagedList<LogDto>> GetLogs(PagedQuery<LogFilter> filter, CancellationToken token);
    }
}
