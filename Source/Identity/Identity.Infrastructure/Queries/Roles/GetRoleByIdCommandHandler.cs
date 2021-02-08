using System;
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
    /// 
    /// </summary>
    public sealed class GetRoleByIdCommandHandler : IRequestHandler<GetRoleByIdCommand, RoleDto>
    {
        private readonly IRoleService _roleService;
        private readonly IOwnSystemLocalizer<RolesConstants> _localizer;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRoleByIdCommandHandler"/> class.
        /// </summary>
        /// <param name="roleService">The role service.</param>
        /// <param name="localizer"></param>
        /// <param name="logger"></param>
        public GetRoleByIdCommandHandler(
            IRoleService roleService,
            IOwnSystemLocalizer<RolesConstants> localizer,
            ILogger logger)
        {
            _roleService = roleService;
            _localizer = localizer;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<RoleDto> Handle(GetRoleByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _roleService.GetByIdAsync(request.RoleId, cancellationToken);
            }
            catch (BaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var message = _localizer[RolesConstants.GettingRoleByIdError, request.RoleId].Value;
                _logger.Error(ex, message);
                throw new IdentityServiceException(message);
            }
        }
    }
}
