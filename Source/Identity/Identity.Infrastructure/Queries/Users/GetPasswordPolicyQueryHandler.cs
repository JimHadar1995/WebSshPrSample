using System;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Dto.Users;
using Identity.Application.Queries.Users;
using Identity.Application.Services.Contracts;
using Identity.Core.Exceptions;
using Identity.ResourceManager.Constants;
using Library.Common.Exceptions;
using Library.Common.Localization;
using Library.Logging.Contracts;
using MediatR;

namespace Identity.Infrastructure.Queries.Users
{
    /// <summary>
    /// Обработчик запроса получения настроек парольной политики.
    /// </summary>
    public sealed class GetPasswordPolicyQueryHandler : IRequestHandler<GetPasswordPolicyQuery, PasswordPolicyDto>
    {
        private readonly ISettingsService _settingsService;
        private readonly IOwnSystemLocalizer<UsersConstants> _localizer;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPasswordPolicyQueryHandler"/> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="localizer"></param>
        /// <param name="logger"></param>
        public GetPasswordPolicyQueryHandler(
            ISettingsService settingsService,
            IOwnSystemLocalizer<UsersConstants> localizer,
            ILogger logger)
        {
            _settingsService = settingsService;
            _logger = logger;
            _localizer = localizer;
        }
        /// <inheritdoc />
        public async Task<PasswordPolicyDto> Handle(GetPasswordPolicyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _settingsService.GetPasswordPolicyAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                string message = _localizer[UsersConstants.GettingPasswordPolicyError].Value;
                _logger.Error(ex, message);
                throw new IdentityServiceException(message);
            }
        }
    }
}
