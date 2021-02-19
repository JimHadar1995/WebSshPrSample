using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Commands.Users;
using Identity.Application.Services.Contracts;
using Identity.Core.Exceptions;
using Identity.ResourceManager.Constants;
using Library.Common.Database;
using Library.Common.Kafka;
using Library.Common.Localization;
using Library.Logging.Contracts;
using MediatR;

namespace Identity.Infrastructure.Handlers.Commands.Users
{
    /// <summary>
    /// Обработчик команды создания пользователя
    /// </summary>
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUnitOfWork _ufw;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly IKafkaService _kafkaService;
        private readonly IOwnSystemLocalizer<UsersConstants> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
        /// </summary>
        public CreateUserCommandHandler(
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
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            try
            {
                _ufw.BeginTransaction();
                var userId = await _userService.CreateAsync(model, cancellationToken)
                    .ConfigureAwait(false);
                _ufw.CommitTransaction();

                _logger.Info(_localizer[UsersConstants.UserCreatedSuccess, model.UserName]);

                var userCreated = await _userService.GetAsync(userId, cancellationToken).ConfigureAwait(false);

                //await _kafkaService.SendInfo(
                //        nameof(KeyMessageKafka.User),
                //        KeyMessageKafka.User.CreateUser,
                //        new List<UserDto> { userCreated })
                //    .ConfigureAwait(false);

                return userId;
            }
            catch (Exception ex)
            {
                _ufw.RollbackTransaction();
                throw new IdentityServiceException(_localizer[UsersConstants.UserCreatedError, model.UserName], ex);
            }
        }
    }
}
