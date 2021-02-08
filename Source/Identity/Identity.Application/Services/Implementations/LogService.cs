using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Dto;
using Identity.Application.Dto.Filters;
using Identity.Application.Services.Contracts;
using Identity.Core.Entities;
using Library.Common.Database;
using Library.Common.Types.Paging;
using Library.Common.Types.Wrappers;
using MapsterMapper;
using Identity.Application.Specifications.Logs;

namespace Identity.Application.Services.Implementations
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
