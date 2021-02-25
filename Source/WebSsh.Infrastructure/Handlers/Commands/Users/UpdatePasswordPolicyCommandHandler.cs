using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Library.Common.Localization;
using Library.Logging.Contracts;
using Library.Common.Database;
using Library.Common.Exceptions;
using WebSsh.Application.Commands.Users;
using WebSsh.Application.Services.Contracts;
using WebSsh.ResourceManager.Constants;
using WebSsh.Core.Exceptions;

namespace WebSsh.Infrastructure.Handlers.Commands.Users
{
    /// <summary>
    /// Обработчик команды обновления настроек парольной политики.
    /// </summary>
    public sealed class UpdatePasswordPolicyCommandHandler : IRequestHandler<UpdatePasswordPolicyCommand, Unit>
    {
        private readonly ISettingsService _settingsService;
        private readonly IUnitOfWork _ufw;
        private readonly ILogger _logger;
        private readonly IOwnSystemLocalizer<UsersConstants> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePasswordPolicyCommandHandler"/> class.
        /// </summary>
        public UpdatePasswordPolicyCommandHandler(
            ISettingsService settingsService,
            IUnitOfWork ufw,
            ILogger logger,
            IOwnSystemLocalizer<UsersConstants> localizer)
        {
            _settingsService = settingsService;
            _ufw = ufw;
            _logger = logger;
            _localizer = localizer;
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

            }
            catch (BaseException)
            {
                _ufw.RollbackTransaction();
                throw;
            }
            catch (Exception ex)
            {
                _ufw.RollbackTransaction();
                throw new WebSshServiceException(_localizer[UsersConstants.UpdatePasswordPolicyError], ex);
            }

            return Unit.Value;
        }
    }
}
