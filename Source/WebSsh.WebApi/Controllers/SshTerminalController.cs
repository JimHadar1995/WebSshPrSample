using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSsh.Application.Commands.Ssh;
using WebSsh.Application.Dto.Ssh;
using WebSsh.Application.Queries.Ssh;
using WebSsh.Terminal.Models;

namespace WebSsh.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [JwtBase]
    [Route("api/v1/[controller]")]
    public class SshTerminalController : BaseController
    {
        /// <inheritdoc />
        public SshTerminalController(
            IHttpContextAccessor httpContextAccessor,
            IMediator mediator)
            : base(httpContextAccessor, mediator)
        {
        }

        /// <summary>
        /// Проверка соединения с терминалом на основе данных текущего пользователя и идентификатора сессии
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CheckConnection(
            [FromQuery] SshSessionModel model,
            CancellationToken token)
            => Ok(await _mediator.Send(new CheckTerminalConnectionCommand(model.SessionId), token)
                .ConfigureAwait(false));

        /// <summary>
        /// Подключение к устройству с указанными параметрами
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        [HttpPost("connect")]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConnectToSource(
            [FromBody] ConnectionsInfoDto model,
            CancellationToken token)
            => Ok(await _mediator.Send(new ConnectToSourceCommand(model), token));

        /// <summary>
        /// Выполнение команды на устройстве
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        [HttpPost("execute")]
        [ProducesResponseType(typeof(OperationResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExecuteCommand(
            [FromBody] ExecuteCommandModel model,
            CancellationToken token)
            => Ok(await _mediator.Send(new ExecuteShellCommand(model), token));

        /// <summary>
        /// Разрыв соединения с терминалом.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        [HttpPost("disconnect")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Disconnect(
            [FromBody] SshSessionModel model,
            CancellationToken token)
        {
            await _mediator.Send(new DisconnectTerminalCommand(model.SessionId), token);
            return Ok();
        }

        /// <summary>
        /// Получение списка сессий пользователя
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        [HttpGet("sessions")]
        [ProducesResponseType(typeof(List<UserSessionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSessions(
            CancellationToken token)
            => Ok(await _mediator.Send(GetUserSessionsQuery.Instance, token));
    }
}
