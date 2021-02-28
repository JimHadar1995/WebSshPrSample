using System.Threading;
using System.Threading.Tasks;
using Library.Common.Types.Paging;
using WebSsh.Application.Dto.Filters;
using WebSsh.Shared.Dto;

namespace WebSsh.Application.Services.Contracts
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
