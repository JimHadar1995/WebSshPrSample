using System;
using System.Collections.Generic;
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
    /// Обработчик команды обновления пользователя
    /// </summary>
    public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IUnitOfWork _ufw;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly IKafkaService _kafkaService;
        private readonly IOwnSystemLocalizer<UsersConstants> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommandHandler"/> class.
        /// </summary>
        public UpdateUserCommandHandler(
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

                //await _kafkaService.SendInfo(
                //        nameof(KeyMessageKafka.User),
                //        KeyMessageKafka.User.UpdateUser,
                //        new List<UserDto> { userUpdated })
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
                _logger.Error(ex, _localizer[UsersConstants.UserUpdateError, model.UserName]);
                throw new IdentityServiceException(_localizer[UsersConstants.UserUpdateError, model.UserName], ex);
            }

            return Unit.Value;
        }
    }
}
