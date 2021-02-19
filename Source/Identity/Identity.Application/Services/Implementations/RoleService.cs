using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Dto.Roles;
using Identity.Application.Services.Contracts;
using Identity.Core.Entities;
using Library.Common.Database;
using Library.Common.Exceptions;
using Library.Common.Localization;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Services.Implementations
{
    /// <inheritdoc/>
    public sealed class RoleService : IRoleService
    {
        private readonly IUnitOfWork _ufw;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly IValidationLocalizer _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleService"/> class.
        /// </summary>
        public RoleService(
            IUnitOfWork ufw,
            IMapper mapper,
            RoleManager<Role> roleManager,
            IValidationLocalizer localizer)
        {
            _ufw = ufw;
            _mapper = mapper;
            _roleManager = roleManager;
            _localizer = localizer;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<RoleDto>> GetAllAsync(CancellationToken token)
        {
            var roles = await _roleManager.Roles.OrderBy(_ => _.Name)
                .ProjectToType<RoleDto>(_mapper.Config)
                .ToListAsync(token);
            return roles;
        }

        /// <inheritdoc />
        public async Task<RoleDto> GetByIdAsync(int id, CancellationToken token)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString()).ConfigureAwait(false);
            if (role == null)
                throw new EntityNotFoundException();

            return _mapper.Map<RoleDto>(role);
        }
    }
}
