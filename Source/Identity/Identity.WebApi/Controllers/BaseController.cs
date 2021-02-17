using System.Collections.Generic;
using Identity.Core.Entities;
using Library.Common.Authentication;
using Library.Common.Authentication.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class BaseController : ControllerBase
    {

        /// <summary>
        /// 
        /// </summary>
        protected readonly IMediator _mediator;

        /// <summary>
        /// 
        /// </summary>
        protected BaseController(
            IHttpContextAccessor accessor,
            IMediator mediator)
        {
            _mediator = mediator;
        }


        #region [ Информация о пользователе из токена доступа ]

        private JsonWebTokenPayload? _tokenInfo;

        /// <summary>
        /// Информация о пользователе из заголовка Authorization
        /// </summary>
        protected JsonWebTokenPayload? TokenInfo
        {
            get
            {
                if (User.Identity!.IsAuthenticated)
                {
                    _tokenInfo = HttpContext.ParseJwtToken();
                }
                return _tokenInfo;
            }
        }

        /// <summary>
        /// Имя залогиненного пользователя.
        /// </summary>
        protected string? UserName => TokenInfo?.UserName;

        /// <summary>
        /// Является ли залогиненный пользователь администратором.
        /// </summary>
        protected bool IsAdmin
            => User.IsInRole(Role.FullAdministratorRole);

        /// <summary>
        /// Идентификатор залогиненного пользователя.
        /// </summary>
        protected int? UserId => TokenInfo?.UserId;

        /// <summary>
        /// 
        /// </summary>
        protected IDictionary<string, string>? CustomClaims
            => TokenInfo?.Claims;

        #endregion  
    }
}
