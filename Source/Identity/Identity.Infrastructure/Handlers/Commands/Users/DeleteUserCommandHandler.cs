using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Identity.Application.Commands.Users;
using Library.Common.Database;
using Identity.Application.Services.Contracts;
using Library.Logging.Contracts;
using Library.Common.Kafka;
using Library.Common.Localization;
using Identity.ResourceManager.Constants;
using Library.Common.Exceptions;
using Identity.Core.Exceptions;

namespace Identity.Infrastructure.Handlers.Commands.Users
{
    /// <summary>
    /// Обработчик команды удаления пользователя
    /// </summary>
    public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUnitOfWork _ufw;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly IKafkaService _kafkaService;
        private readonly IOwnSystemLocalizer<UsersConstants> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserCommandHandler"/> class.
        /// </summary>
        /// <param name="ufw">The ufw.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="kafkaService">The kafka service.</param>
        /// <param name="localizer">The localizer.</param>
        public DeleteUserCommandHandler(
            IUnitOfWork ufw,
            IUserService userService,
            ILoggerFactory loggerFactory,
            IKafkaService kafkaService,
            IOwnSystemLocalizer<UsersConstants> localizer)
        {
            _ufw = ufw;
            _userService = userService;
            _logger = loggerFactory.GetLogger(ILogger.DefaultLoggerName);
            _kafkaService = kafkaService;
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

                //await _kafkaService.SendInfo(
                //        nameof(KeyMessageKafka.User),
                //        KeyMessageKafka.User.DeleteUser,
                //        new List<string> { request.UserId })
                //    .ConfigureAwait(false);
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
                throw new IdentityServiceException(_localizer[UsersConstants.UserDeleteError, request.UserId], ex);
            }

            return Unit.Value;
        }
    }
}
