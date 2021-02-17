using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Dto.Roles;
using Identity.Application.Queries.Roles;
using Identity.Application.Services.Contracts;
using Identity.Core.Exceptions;
using Identity.ResourceManager.Constants;
using Library.Common.Exceptions;
using Library.Common.Localization;
using Library.Logging.Contracts;
using MediatR;

namespace Identity.Infrastructure.Handlers.Queries.Roles
{
    /// <summary>
    /// Обработчик команды получения всех привилегий.
    /// </summary>
    public sealed class GetAllPrivilegesQueryHandler : IRequestHandler<GetAllPrivilegesQuery, IReadOnlyList<PrivilegeDto>>
    {
        private readonly IRoleService _roleService;
        private readonly IOwnSystemLocalizer<RolesConstants> _localizer;
        private readonly ILogger _logger;
        public GetAllPrivilegesQueryHandler(
            IRoleService roleService,
            IOwnSystemLocalizer<RolesConstants> localizer,
            ILogger logger)
        {
            _roleService = roleService;
            _localizer = localizer;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<PrivilegeDto>> Handle(GetAllPrivilegesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _roleService.GetAllPrivilegesAsync(cancellationToken);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var message = _localizer[RolesConstants.GettingPrivilegesError].Value;
                _logger.Error(ex, message);
                throw new IdentityServiceException(message);
            }
        }
    }
}
