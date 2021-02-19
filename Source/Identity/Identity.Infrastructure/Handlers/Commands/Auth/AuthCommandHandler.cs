using System;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Commands.Auth;
using Identity.Application.Services.Contracts;
using Identity.Core.Exceptions;
using Identity.ResourceManager.Constants;
using Library.Common.Authentication.Models;
using Library.Common.Exceptions;
using Library.Common.Localization;
using MediatR;

namespace Identity.Infrastructure.Handlers.Commands.Auth
{
    /// <summary>
    /// Обработчик команды авторизации пользователя
    /// </summary>
    public sealed class AuthCommandHandler : IRequestHandler<AuthCommand, JsonWebToken>
    {
        private readonly IAuthService _authService;
        IOwnSystemLocalizer<UsersConstants> _localizer;

        public AuthCommandHandler(
            IAuthService authService,
            IOwnSystemLocalizer<UsersConstants> localizer)
        {
            _authService = authService;
            _localizer = localizer;
        }
        /// <inheritdoc/>
        public async Task<JsonWebToken> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _authService.LoginByPassAsync(request.Model);
            }
            catch (BaseException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new IdentityServiceException(_localizer[UsersConstants.LoginByPassError], ex);
            }
        }
    }
}
