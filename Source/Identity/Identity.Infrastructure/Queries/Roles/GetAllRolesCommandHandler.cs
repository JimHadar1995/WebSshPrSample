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

namespace Identity.Infrastructure.Queries.Roles
{
    /// <summary>
    /// Обработчик запроса получения всех ролей.
    /// </summary>
    public sealed class GetAllRolesCommandHandler : IRequestHandler<GetAllRolesCommand, IReadOnlyList<RoleDto>>
    {
        private readonly IRoleService _roleService;
        private readonly IOwnLocalizer<RolesConstants> _localizer;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllRolesCommandHandler"/> class.
        /// </summary>
        /// <param name="roleService">The role service.</param>
        /// <param name="localizer"></param>
        /// <param name="logger"></param>
        public GetAllRolesCommandHandler(
            IRoleService roleService,
            IOwnLocalizer<RolesConstants> localizer,
            ILogger logger)
        {
            _roleService = roleService;
            _logger = logger;
            _localizer = localizer;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<RoleDto>> Handle(GetAllRolesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _roleService.GetAllAsync(cancellationToken);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                string message = _localizer[RolesConstants.GettingRolesError].Value;
                _logger.Error(ex, message);
                throw new IdentityServiceException(message);
            }
        }
    }
}
