using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Database;
using Library.Common.Exceptions;
using Library.Common.Extensions;
using Library.Common.Localization;
using Library.Logging.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WebSsh.Application.Commands.Users;
using WebSsh.Application.Services.Contracts;
using WebSsh.Core.Entities;
using WebSsh.Core.Exceptions;
using WebSsh.ResourceManager.Constants;

namespace WebSsh.Infrastructure.Handlers.Commands.Users
{
    /// <summary>
    /// Обработчик команды смены пароля пользователя
    /// </summary>
    /// <seealso cref="IRequestHandler{ChangeUserPasswordCommand, Unit}" />
    public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUnitOfWork _ufw;
        private readonly ILogger _logger;
        private readonly IOwnSystemLocalizer<UsersConstants> _localizer;
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordCommandHandler"/> class.
        /// </summary>
        public ChangePasswordCommandHandler(
            IUserService userService,
            IUnitOfWork ufw,
            IHttpContextAccessor accessor,
            ILoggerFactory loggerFactory,
            IOwnSystemLocalizer<UsersConstants> localizer,
            UserManager<User> userManager)
        {
            _userService = userService;
            _accessor = accessor;
            _ufw = ufw;
            _logger = loggerFactory.GetLogger(ILogger.DefaultLoggerName);
            _localizer = localizer;
            _userManager = userManager;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            try
            {
                _ufw.BeginTransaction();
                await _userService
                    .ChangePasswordAsync(
                        _accessor.UserName()!,
                        model.NewPassword,
                        token: cancellationToken);
                _ufw.CommitTransaction();
                var user = await _userManager.FindByNameAsync(_accessor.UserName());
                _logger.Info(_localizer[UsersConstants.ChangePasswordSuccess, user.UserName]);
            }
            catch (BaseException)
            {
                _ufw.RollbackTransaction();
                throw;
            }
            catch (Exception ex)
            {
                _ufw.RollbackTransaction();
                throw new IdentityServiceException(_localizer[UsersConstants.ChangePasswordError, _accessor.UserName()!], ex);
            }

            return Unit.Value;
        }
    }
}
