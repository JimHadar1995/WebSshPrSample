using System;
using System.Collections.Generic;
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
    /// Обработчик запроса получения всех ролей.
    /// </summary>
    public sealed class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IReadOnlyList<RoleDto>>
    {
        private readonly IRoleService _roleService;
        private readonly IOwnLocalizer<RolesConstants> _localizer;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllRolesQueryHandler"/> class.
        /// </summary>
        /// <param name="roleService">The role service.</param>
        /// <param name="localizer"></param>
        /// <param name="logger"></param>
        public GetAllRolesQueryHandler(
            IRoleService roleService,
            IOwnLocalizer<RolesConstants> localizer,
            ILogger logger)
        {
            _roleService = roleService;
            _logger = logger;
            _localizer = localizer;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
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
                throw new IdentityServiceException(_localizer[RolesConstants.GettingRolesError].Value, ex);
            }
        }
    }
}
