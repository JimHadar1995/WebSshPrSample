using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Library.Common.Database;
using Library.Logging.Contracts;
using Library.Common.Localization;
using Library.Common.Exceptions;
using WebSsh.Application.Services.Contracts;
using WebSsh.ResourceManager.Constants;
using WebSsh.Application.Commands.Users;
using WebSsh.Core.Exceptions;

namespace WebSsh.Infrastructure.Handlers.Commands.Users
{
    /// <summary>
    /// Обработчик команды разблокировки пользователя.
    /// </summary>
    public sealed class UnLockUserCommandHandler : IRequestHandler<UnLockUserCommand, Unit>
    {
        private readonly IUnitOfWork _ufw;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly IOwnSystemLocalizer<UsersConstants> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
        /// </summary>
        public UnLockUserCommandHandler(
            IUnitOfWork ufw,
            IUserService userService,
            ILoggerFactory loggerFactory,
            IOwnSystemLocalizer<UsersConstants> localizer)
        {
            _ufw = ufw;
            _userService = userService;
            _logger = loggerFactory.GetLogger(ILogger.DefaultLoggerName);
            _localizer = localizer;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(UnLockUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _ufw.BeginTransaction();
                await _userService.UnLockAsync(request.UserId, cancellationToken)
                    .ConfigureAwait(false);
                _ufw.CommitTransaction();

                var user = await _userService.GetAsync(request.UserId, cancellationToken);

                _logger.Info(_localizer[UsersConstants.UnLockSuccess, user.UserName]);

            }
            catch (BaseException)
            {
                _ufw.RollbackTransaction();
                throw;
            }
            catch (Exception ex)
            {
                _ufw.RollbackTransaction();
                throw new IdentityServiceException(_localizer[UsersConstants.UnLockError, request.UserId], ex);
            }
            return Unit.Value;
        }
    }
}
