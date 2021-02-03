using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Identity.Application.Services.Implementations
{
    /// <inheritdoc />
    public class LogService : ILogService
    {
        private readonly IUnitOfWork _ufw;
        private readonly ICacheWrapper _cache;
        private readonly IMapper _mapper;
        public LogService(
           IUnitOfWork ufw,
            ICacheWrapper cache,
            IMapper mapper)
        {
            _mapper = mapper;
            _ufw = ufw;
            _cache = cache;
        }
        /// <inheritdoc />
        public async Task<PagedList<LogDto>> GetLogs(PagedQuery<LogFilter> filter, CancellationToken token = default)
        {
            throw new NotImplementedException();
            //filter.Filter.ThrowIfInvalidSortField();
            //var logsQuery = _ufw.Repository<LogEntity>()
            //    .Query
            //    .Where(_ => _.Level == "Info")
            //    .FullTextSearch(filter.Filter.Search, _cache)
            //    .OrderBy($"{filter.Filter.SortField} {filter.Filter.OrderBy}");

            //return await logsQuery.ToPagedList(MappingFunc, filter);
        }

        private async Task<List<LogDto>> MappingFunc(IQueryable<LogEntity> records)
        {
            throw new NotImplementedException();
            //return await records.AsNoTracking().ProjectTo<LogDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
