using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Library.Common.Database;
using Library.Logging.Contracts;
using Library.Common.Localization;
using Library.Common.Exceptions;
using WebSsh.Application.Commands.Users;
using WebSsh.ResourceManager.Constants;
using WebSsh.Core.Exceptions;
using WebSsh.Shared.Contracts;

namespace WebSsh.Infrastructure.Handlers.Commands.Users
{
    /// <summary>
    /// Обработчик команды удаления пользователя
    /// </summary>
    public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUnitOfWork _ufw;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly IOwnSystemLocalizer<UsersConstants> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserCommandHandler"/> class.
        /// </summary>
        public DeleteUserCommandHandler(
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
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userService.GetAsync(request.UserId, cancellationToken);
                _ufw.BeginTransaction();
                await _userService.DeleteAsync(request.UserId, cancellationToken)
                    .ConfigureAwait(false);
                _ufw.CommitTransaction();

                _logger.Info(_localizer[UsersConstants.UserDeleteSuccess, user.UserName]);

            }
            catch (EntityNotFoundException)
            {
                _ufw.RollbackTransaction();
                return Unit.Value;
            }
            catch (BaseException)
            {
                _ufw.RollbackTransaction();
                throw;
            }
            catch (Exception ex)
            {
                _ufw.RollbackTransaction();
                throw new WebSshServiceException(_localizer[UsersConstants.UserDeleteError, request.UserId], ex);
            }

            return Unit.Value;
        }
    }
}
