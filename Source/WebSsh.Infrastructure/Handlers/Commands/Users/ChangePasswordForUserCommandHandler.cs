using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Database;
using Library.Common.Exceptions;
using Library.Common.Localization;
using Library.Logging.Contracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using WebSsh.Application.Commands.Users;
using WebSsh.Application.Services.Contracts;
using WebSsh.Core.Entities;
using WebSsh.Core.Exceptions;
using WebSsh.ResourceManager.Constants;

namespace WebSsh.Infrastructure.Handlers.Commands.Users
{
    /// <summary>
    /// Обработчик команды сброса пароля пользователя
    /// </summary>
    public sealed class ChangePasswordForUserCommandHandler : IRequestHandler<ChangePasswordForUserCommand, Unit>
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _ufw;
        private readonly IOwnSystemLocalizer<UsersConstants> _localizer;
        private readonly ILogger _logger;
        private readonly UserManager<User> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangePasswordForUserCommandHandler"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="ufw">The ufw.</param>
        /// <param name="localizer"></param>
        /// <param name="logger"></param>
        /// <param name="userManager"></param>
        public ChangePasswordForUserCommandHandler(
            IUserService userService,
            IUnitOfWork ufw,
            IOwnSystemLocalizer<UsersConstants> localizer,
            ILogger logger,
            UserManager<User> userManager)
        {
            _userService = userService;
            _ufw = ufw;
            _localizer = localizer;
            _logger = logger;
            _userManager = userManager;
        }


        /// <inheritdoc />
        public async Task<Unit> Handle(ChangePasswordForUserCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId.ToString());

                if (user == null)
                    throw new EntityNotFoundException();

                _ufw.BeginTransaction();

                await _userService
                    .ChangePasswordAsync(user.UserName, model.NewPassword, true, cancellationToken);

                _ufw.CommitTransaction();

                _logger.Info(_localizer[UsersConstants.ResetPasswordSuccess, user.UserName]);
            }
            catch (BaseException)
            {
                _ufw.RollbackTransaction();
                throw;
            }
            catch (Exception ex)
            {
                var message = _localizer[UsersConstants.ChangePasswordError, model.UserId].Value;
                _ufw.RollbackTransaction();
                throw new IdentityServiceException(message, ex);
            }
            return Unit.Value;
        }
    }
}
