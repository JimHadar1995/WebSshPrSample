using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSsh.Application.Dto.Roles;
using WebSsh.Application.Queries.Roles;

namespace WebSsh.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с ролями
    /// </summary>
    [Route("api/[controller]")]
    [JwtBase]
    public sealed class RolesController : BaseController
    {
        public RolesController(IHttpContextAccessor accessor, IMediator mediator)
            : base(accessor, mediator)
        {
        }

        /// <summary>
        /// Получение списка всех ролей
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IReadOnlyList<RoleDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRoles(
            CancellationToken token)
            => Ok(await _mediator.Send(GetAllRolesQuery.Instance, token));

        /// <summary>
        /// Получение роли по ее идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken token)
            => Ok(await _mediator.Send(new GetRoleByIdQuery(id), token));
    }
}
