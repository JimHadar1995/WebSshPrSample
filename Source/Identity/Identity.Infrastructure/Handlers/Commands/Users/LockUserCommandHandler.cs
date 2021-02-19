using System;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Commands.Users;
using Identity.Application.Services.Contracts;
using Identity.Core.Exceptions;
using Identity.ResourceManager.Constants;
using Library.Common.Database;
using Library.Common.Exceptions;
using Library.Common.Kafka;
using Library.Common.Localization;
using Library.Logging.Contracts;
using MediatR;

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
        private readonly IKafkaService _kafkaService;
        private readonly IOwnSystemLocalizer<UsersConstants> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
        /// </summary>
        /// <param name="ufw">The ufw.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="loggerFactory"></param>
        /// <param name="kafkaService"></param>
        public LockUserCommandHandler(
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

                //await _kafkaService.SendInfo(
                //    nameof(KeyMessageKafka.User),
                //    KeyMessageKafka.User.LockUser,
                //    new List<UserDto> { user })
                //    .ConfigureAwait(false);
            }
            catch (BaseException)
            {
                _ufw.RollbackTransaction();
                throw;
            }
            catch (Exception ex)
            {
                _ufw.RollbackTransaction();
                throw new IdentityServiceException(_localizer[UsersConstants.LockError, request.UserId], ex);
            }
            return Unit.Value;
        }
    }
}
