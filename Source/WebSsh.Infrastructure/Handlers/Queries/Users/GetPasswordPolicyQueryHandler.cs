using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Exceptions;
using Library.Common.Localization;
using Library.Logging.Contracts;
using MediatR;
using WebSsh.Application.Dto.Users;
using WebSsh.Application.Queries.Users;
using WebSsh.Application.Services.Contracts;
using WebSsh.Core.Exceptions;
using WebSsh.ResourceManager.Constants;

namespace Identity.Infrastructure.Handlers.Queries.Users
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
                throw new IdentityServiceException(_localizer[UsersConstants.GettingPasswordPolicyError].Value, ex);
            }
        }
    }
}
