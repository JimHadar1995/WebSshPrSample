using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Database;
using Library.Common.Types.Paging;
using MapsterMapper;
using WebSsh.Application.Dto.Filters;
using WebSsh.Application.Dto;
using WebSsh.Application.Services.Contracts;
using WebSsh.Core.Entities;
using WebSsh.Application.Specifications.Logs;

namespace WebSsh.Application.Services.Implementations
{
    /// <inheritdoc />
    public class LogService : ILogService
    {
        private readonly IUnitOfWork _ufw;
        private readonly IMapper _mapper;
        public LogService(
           IUnitOfWork ufw,
            IMapper mapper)
        {
            _mapper = mapper;
            _ufw = ufw;
        }
        /// <inheritdoc />
        public async Task<PagedList<LogDto>> GetLogs(PagedQuery<LogFilter> filter, CancellationToken token = default)
        {
            filter.Filter.ThrowIfInvalidSortField();

            var (data, count) = await _ufw.Repository<LogEntity>()
                .GetPaged(filter, new LogsInfoFilterSpec("Info"), token);

            var dataDto = _mapper.Map<List<LogDto>>(data);

            return new PagedList<LogDto>
            {
                Data = dataDto,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalItems = (int)count
            };
        }
    }
}
