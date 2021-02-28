using System.Threading;
using System.Threading.Tasks;
using Library.Common.Authentication;
using Library.Common.Types.Paging;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSsh.Application.Dto.Filters;
using WebSsh.Application.Queries.Logs;
using WebSsh.Shared.Dto;

namespace WebSsh.WebApi.Controllers
{
    /// <summary>
    /// Логи модуля
    /// </summary>
    [Route("api/[controller]")]
    [JwtBase]
    public sealed class LogsController : BaseController
    {
        public LogsController(IHttpContextAccessor accessor, IMediator mediator)
            : base(accessor, mediator)
        {
        }

        /// <summary>
        /// Получение логов с постраничной разбивкой
        /// </summary>
        /// <param name="pagedQuery"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<LogDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllLogs(
            [FromQuery] PagedQuery<LogFilter> pagedQuery,
            CancellationToken token)
            => Ok(await _mediator.Send(new GetLogsQuery(pagedQuery), token));
    }
}
