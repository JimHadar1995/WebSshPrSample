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
    /// Обработчик команды обновления пользователя
    /// </summary>
    public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUnitOfWork _ufw;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly IOwnSystemLocalizer<UsersConstants> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
        /// </summary>
        public UpdateUserCommandHandler(
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
        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            try
            {
                _ufw.BeginTransaction();
                await _userService.UpdateAsync(model, cancellationToken)
                    .ConfigureAwait(false);
                _ufw.CommitTransaction();

                _logger.Info(_localizer[UsersConstants.UserUpdateSuccess, model.UserName]);

                var userUpdated = await _userService.GetAsync(model.Id, cancellationToken).ConfigureAwait(false);

            }
            catch (BaseException)
            {
                _ufw.RollbackTransaction();
                throw;
            }
            catch (Exception ex)
            {
                _ufw.RollbackTransaction();
                throw new WebSshServiceException(_localizer[UsersConstants.UserUpdateError, model.UserName], ex);
            }

            return Unit.Value;
        }
    }
}
