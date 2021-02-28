using System.Threading;
using System.Threading.Tasks;
using Library.Common.Authentication.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSsh.Application.Commands.Auth;
using WebSsh.Shared.Dto.Users;

namespace WebSsh.WebApi.Controllers
{
    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        public AuthController(
            IHttpContextAccessor accessor,
            IMediator mediator)
            : base(accessor, mediator)
        {
        }

        /// <summary>
        /// Получение авторизационного token'а доступа на основе проверки логина и пароля пользователя
        /// </summary>
        /// <param name="credentials">Данные пользователя для проверки</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(JsonWebToken), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(
            [FromBody] LoginByPassCredentials credentials,
            CancellationToken token)
            => Ok(await _mediator.Send(new AuthCommand(credentials), token));
    }
}
