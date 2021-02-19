using Identity.Application.Dto.Users;
using Library.Common.Authentication.Models;
using MediatR;

namespace Identity.Application.Commands.Auth
{
    /// <summary>
    /// Конмады авторизации пользователя
    /// </summary>
    public sealed class AuthCommand : IRequest<JsonWebToken>
    {
        public readonly LoginByPassCredentials Model;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public AuthCommand(LoginByPassCredentials model)
        {
            Model = model;
        }
    }
}
