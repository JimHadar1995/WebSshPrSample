using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Commands.Users;
using Identity.Application.Dto.Users;
using Identity.Application.Queries.Users;
using Library.Common.Authentication;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для работы с настройками модуля
    /// </summary>
    [Route("api/[controller]")]
    [JwtBasePrivilege]
    public sealed class SettingsController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        public SettingsController(IHttpContextAccessor accessor, IMediator mediator) 
            : base(accessor, mediator)
        {
        }

        /// <summary>
        /// Получение настроек парольных политик
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("password-policy")]
        [ProducesResponseType(typeof(PasswordPolicyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPasswordPolicy(CancellationToken token)
            => Ok(await _mediator.Send(GetPasswordPolicyQuery.Instance, token));

        /// <summary>
        /// Сохранение настроек парольных политик
        /// </summary>
        /// <param name="model">Модель для обновления</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("password-policy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SavePasswordPolicy([FromBody]PasswordPolicyDto model,
            CancellationToken token)
        {
            await _mediator.Send(new UpdatePasswordPolicyCommand(model), token);
            return Ok();
        }
    }
}
