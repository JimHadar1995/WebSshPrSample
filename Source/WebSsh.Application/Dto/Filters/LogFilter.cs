using System.Collections.Generic;
using Library.Common.Types.Paging;

namespace WebSsh.Application.Dto.Filters
{
    /// <summary>
    /// Параметры фильтров для Логов.
    /// </summary>
    public class LogFilter : DefaultFilter<LogDto>
    {
        /// <inheritdoc />
        protected override HashSet<string> AllowedSortingFields => new HashSet<string>
        {
            nameof(LogDto.Level),
            nameof(LogDto.Date),
            nameof(LogDto.Username),
            nameof(LogDto.Id)
        };
    }
}
