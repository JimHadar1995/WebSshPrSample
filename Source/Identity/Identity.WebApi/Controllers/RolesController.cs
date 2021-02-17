using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Commands.Roles;
using Identity.Application.Dto.Roles;
using Identity.Application.Queries.Roles;
using Library.Common.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с ролями
    /// </summary>
    [Route("api/[controller]")]
    [JwtBasePrivilege]
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

        /// <summary>
        /// Создание роли
        /// </summary>
        /// <param name="model">Модель роли для создания</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] RoleAddDto model,
            CancellationToken token)
            => Ok(await _mediator.Send(new CreateRoleCommand(model), token));

        /// <summary>
        /// Обновление роли
        /// </summary>
        /// <param name="model">Модель роли для обновления</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] RoleUpdateDto model,
            CancellationToken token)
        {
            await _mediator.Send(new UpdateRoleCommand(model), token);
            return Ok();
        }

        /// <summary>
        /// Удаление роли по ее идентификатору
        /// </summary>
        /// <param name="id">Идентификатор роли для удаления</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id, CancellationToken token)
        {
            await _mediator.Send(new DeleteRoleCommand(id), token);
            return NoContent();
        }

        /// <summary>
        /// Получение списка всех привилегий системы
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("privileges")]
        [ProducesResponseType(typeof(IReadOnlyList<PrivilegeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllPrivileges(
            CancellationToken token)
            => Ok(await _mediator.Send(GetAllPrivilegesQuery.Instance, token));
    }
}
