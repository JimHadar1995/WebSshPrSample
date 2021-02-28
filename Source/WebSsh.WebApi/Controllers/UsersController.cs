using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSsh.Application.Commands.Users;
using WebSsh.Application.Queries.Users;
using WebSsh.Shared.Dto.Users;

namespace WebSsh.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователями
    /// </summary>
    [JwtBase]
    [Route("api/[controller]")]
    public sealed class UsersController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        public UsersController(IHttpContextAccessor accessor, IMediator mediator)
            : base(accessor, mediator)
        {
        }

        /// <summary>
        /// Получение списка всех пользователей
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll(CancellationToken token)
            => Ok(await _mediator.Send(GetAllUsersQuery.Instance, token));

        /// <summary>
        /// Получение пользователя по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(
            int id,
            CancellationToken token)
            => Ok(await _mediator.Send(new GetUserByIdQuery(id), token));

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody] UserAddDto model,
            CancellationToken token)
            => Ok(await _mediator.Send(new CreateUserCommand(model), token));

        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            [FromBody] UserUpdateDto model,
            CancellationToken token)
        {
            await _mediator.Send(new UpdateUserCommand(model), token);
            return Ok();
        }

        /// <summary>
        /// Удаления пользователя по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя для удаления</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken token)
        {
            await _mediator.Send(new DeleteUserCommand(id), token);
            return NoContent();
        }

        /// <summary>
        /// Обновление пароля текущему залогиненному пользователю
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("current/change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangePasswordDto model,
            CancellationToken token)
        {
            await _mediator.Send(new ChangePasswordCommand(model), token);
            return Ok();
        }

        /// <summary>
        /// Обновление пароля для пользователями с указанным в модели идентификатором
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangePasswordForUser(
            [FromBody] ChangePasswordForUserDto model,
            CancellationToken token)
        {
            await _mediator.Send(new ChangePasswordForUserCommand(model), token);
            return Ok();
        }

        /// <summary>
        /// Блокировка пользователя по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("{id:int}/lock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LockUser(
            int id,
            CancellationToken token)
        {
            await _mediator.Send(new LockUserCommand(id), token);
            return Ok();
        }

        /// <summary>
        /// Разблокировка пользователя по его идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("{id:int}/unlock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnLockUser(
            int id,
            CancellationToken token)
        {
            await _mediator.Send(new UnLockUserCommand(id), token);
            return Ok();
        }
    }
}
