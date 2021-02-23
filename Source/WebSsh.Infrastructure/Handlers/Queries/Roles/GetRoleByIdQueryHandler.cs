using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Exceptions;
using Library.Common.Localization;
using Library.Logging.Contracts;
using MediatR;
using WebSsh.Application.Dto.Roles;
using WebSsh.Application.Queries.Roles;
using WebSsh.Application.Services.Contracts;
using WebSsh.Core.Exceptions;
using WebSsh.ResourceManager.Constants;

namespace WebSsh.Infrastructure.Handlers.Queries.Roles
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
    {
        private readonly IRoleService _roleService;
        private readonly IOwnSystemLocalizer<RolesConstants> _localizer;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRoleByIdQueryHandler"/> class.
        /// </summary>
        public GetRoleByIdQueryHandler(
            IRoleService roleService,
            IOwnSystemLocalizer<RolesConstants> localizer,
            ILogger logger)
        {
            _roleService = roleService;
            _localizer = localizer;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
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
                throw new IdentityServiceException(_localizer[RolesConstants.GettingRoleByIdError, request.RoleId].Value, ex);
            }
        }
    }
}
