using System;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Dto;
using Identity.Application.Queries.Logs;
using Identity.Application.Services.Contracts;
using Identity.Core.Exceptions;
using Identity.ResourceManager.Constants;
using Library.Common.Database;
using Library.Common.Types.Paging;
using Library.Logging.Contracts;
using MediatR;

namespace Identity.Infrastructure.Handlers.Queries.Logs
{
    /// <summary>
    /// Обработчик запроса получения логов
    /// </summary>
    public sealed class GetLogsQueryHandler : IRequestHandler<GetLogsQuery, PagedList<LogDto>>
    {
        private readonly IUnitOfWork _ufw;
        private readonly ILogService _logService;
        private readonly ILogger _logger;

        public GetLogsQueryHandler(
            IUnitOfWork ufw,
            ILogService logService,
            ILoggerFactory loggerFactory)
        {
            _ufw = ufw;
            _logService = logService;
            _logger = loggerFactory.DefaultLogger();
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
                var message = LogsConstants.GettingLogsError;
                _logger.Error(ex, message);
                throw new IdentityServiceException(message);
            }
        }
    }
}
