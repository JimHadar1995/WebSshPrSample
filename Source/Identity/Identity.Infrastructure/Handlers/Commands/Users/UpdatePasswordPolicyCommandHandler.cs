using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Identity.Application.Commands.Users;
using Library.Common.Localization;
using Identity.ResourceManager.Constants;
using Library.Common.Kafka;
using Library.Logging.Contracts;
using Library.Common.Database;
using Identity.Application.Services.Contracts;
using Library.Common.Exceptions;
using Identity.Core.Exceptions;

namespace Identity.Infrastructure.Handlers.Commands.Users
{
    /// <summary>
    /// Обработчик команды обновления настроек парольной политики.
    /// </summary>
    public sealed class UpdatePasswordPolicyCommandHandler : IRequestHandler<UpdatePasswordPolicyCommand, Unit>
    {
        private readonly ISettingsService _settingsService;
        private readonly IUnitOfWork _ufw;
        private readonly ILogger _logger;
        private readonly IKafkaService _kafkaService;
        private readonly IOwnSystemLocalizer<UsersConstants> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePasswordPolicyCommandHandler"/> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="ufw"></param>
        /// <param name="logger"></param>
        /// <param name="kafkaService"></param>
        public UpdatePasswordPolicyCommandHandler(
            ISettingsService settingsService,
            IUnitOfWork ufw,
            ILogger logger,
            IKafkaService kafkaService,
            IOwnSystemLocalizer<UsersConstants> localizer)
        {
            _settingsService = settingsService;
            _ufw = ufw;
            _logger = logger;
            _localizer = localizer;
            _kafkaService = kafkaService;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(UpdatePasswordPolicyCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            try
            {
                _ufw.BeginTransaction();
                await _settingsService.SavePasswordPolicyAsync(model, cancellationToken).ConfigureAwait(false);
                _ufw.CommitTransaction();

                var settingPasswordPolicy = await _settingsService.GetPasswordPolicyAsync(cancellationToken);

                _logger.Info(_localizer[UsersConstants.UpdatePasswordPolicySuccess]);

                //await _kafkaService.SendInfo(
                //        nameof(KeyMessageKafka.User),
                //        KeyMessageKafka.User.PasswordPolicy,
                //        new List<PasswordPolicyDto> { settingPasswordPolicy })
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
                throw new IdentityServiceException(_localizer[UsersConstants.UpdatePasswordPolicyError], ex);
            }

            return Unit.Value;
        }
    }
}
