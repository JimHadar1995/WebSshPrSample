using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Localization;
using Library.Common.Types.Paging;
using Library.Logging.Contracts;
using MediatR;
using WebSsh.Application.Queries.Logs;
using WebSsh.Application.Services.Contracts;
using WebSsh.Core.Exceptions;
using WebSsh.ResourceManager.Constants;
using WebSsh.Shared.Dto;

namespace WebSsh.Infrastructure.Handlers.Queries.Logs
{
    /// <summary>
    /// Обработчик запроса получения логов
    /// </summary>
    public sealed class GetLogsQueryHandler : IRequestHandler<GetLogsQuery, PagedList<LogDto>>
    {
        private readonly ILogService _logService;
        private readonly IOwnSystemLocalizer<LogsConstants> _localizer;

        public GetLogsQueryHandler(
            ILogService logService,
            IOwnSystemLocalizer<LogsConstants> localizer)
        {
            _logService = logService;
            _localizer = localizer;
        }

        /// <inheritdoc />
        public async Task<PagedList<LogDto>> Handle(GetLogsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _logService.GetLogs(request.Filter, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new WebSshServiceException(_localizer[LogsConstants.GettingLogsError], ex);
            }
        }
    }
}
