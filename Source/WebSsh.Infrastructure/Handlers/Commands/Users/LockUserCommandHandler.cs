using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Database;
using Library.Common.Exceptions;
using Library.Common.Localization;
using Library.Logging.Contracts;
using MediatR;
using WebSsh.Application.Commands.Users;
using WebSsh.Application.Services.Contracts;
using WebSsh.Core.Exceptions;
using WebSsh.Infrastructure.Handlers.Commands.Users;
using WebSsh.ResourceManager.Constants;

namespace Identity.Infrastructure.Handlers.Commands.Users
{
    /// <summary>
    /// Обработчик команды блокировки пользователя.
    /// </summary>
    public sealed class LockUserCommandHandler : IRequestHandler<LockUserCommand, Unit>
    {
        private readonly IUnitOfWork _ufw;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly IOwnSystemLocalizer<UsersConstants> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
        /// </summary>
        public LockUserCommandHandler(
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
        public async Task<Unit> Handle(LockUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _ufw.BeginTransaction();
                await _userService.LockAsync(request.UserId, cancellationToken)
                    .ConfigureAwait(false);
                _ufw.CommitTransaction();

                var user = await _userService.GetAsync(request.UserId, cancellationToken);

                _logger.Info(_localizer[UsersConstants.LockSuccess, user.UserName]);

            }
            catch (BaseException)
            {
                _ufw.RollbackTransaction();
                throw;
            }
            catch (Exception ex)
            {
                _ufw.RollbackTransaction();
                throw new WebSshServiceException(_localizer[UsersConstants.LockError, request.UserId], ex);
            }
            return Unit.Value;
        }
    }
}
