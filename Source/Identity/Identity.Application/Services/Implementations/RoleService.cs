using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Dto.Roles;
using Identity.Application.Services.Contracts;
using Identity.Application.Specifications.Roles;
using Identity.Core.Entities;
using Identity.Core.Exceptions;
using Identity.ResourceManager.Validation;
using Library.Common.Database;
using Library.Common.Exceptions;
using Library.Common.Localization;
using Library.Logging.Common;
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

        /// <inheritdoc />
        public async Task<int> CreateAsync(RoleAddDto model, CancellationToken token)
        {
            var role = _mapper.Map<Role>(model);

            await _roleManager.CreateAsync(role);

            await UpdatePrivilegesForRole(role, model, token);

            return role.Id;
        }


        /// <inheritdoc />
        public async Task UpdateAsync(RoleUpdateDto model, CancellationToken token)
        {
            var role = await _roleManager.FindByIdAsync(model.Id.ToString()).ConfigureAwait(false);
            if (role == null)
                throw new EntityNotFoundException();

            role = _mapper.Map(model, role);

            role.Privileges.Clear();

            await _roleManager.UpdateAsync(role);

            await UpdatePrivilegesForRole(role, model, token);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id, CancellationToken token)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
                return;

            if (role.IsDefaultRole)
            {
                throw new IdentityServiceException(_localizer.Message(ValidationConstants.RemovingDefaultRoleIsProhibited));
            }

            await _roleManager.DeleteAsync(role);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<PrivilegeDto>> GetAllPrivilegesAsync(CancellationToken token)
        {
            var privileges = await _ufw.Repository<Privilege>().GetAllAsync(token);
            return _mapper.Map<List<PrivilegeDto>>(privileges);
        }

        /// <inheritdoc />
        public async Task LoadPrivilegesFromFile(string filePath, CancellationToken token)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            if (!File.Exists(filePath))
                return;

            List<PrivilegesLoadDto> privileges = JsonSerializer.Deserialize<List<PrivilegesLoadDto>>(
                await File.ReadAllTextAsync(filePath, token), jsonOptions)!;

            try
            {
                _ufw.BeginTransaction(); //TODO: при тестировании не разрешает транзацкции

                foreach (var privilege in privileges)
                {
                    await SavePrivilege(privilege);
                }

                _ufw.CommitTransaction();
            }
            catch (Exception ex)
            {
                _ufw.RollbackTransaction();
                LoggingHelper.Default.Error(ex, ex.Message);
            }
        }

        #region [ Help methods ]

        private async Task UpdatePrivilegesForRole(
            Role role,
            RoleAddDto model,
            CancellationToken token)
        {
            var privileges = await _ufw.Repository<Privilege>().GetAllAsync(token);

            var privilegesDto = model.Privileges.Distinct(EqualityComparer<RoleAddDto.PrivilegeForRoleDto>.Default);

            foreach (var privilegeDto in privilegesDto)
            {
                var privilege = privileges.FirstOrDefault(_ => _.Id == privilegeDto.PrivilegeId);

                if (privilege == null)
                    continue;

                role.Privileges.Add(privilege);
            }

            await _roleManager.UpdateAsync(role);
        }

        private async Task SavePrivilege(PrivilegesLoadDto privilegeLoadDto)
        {
            var privilegeRepo = _ufw.Repository<Privilege>();

            var spec = new FindPrivilegeByNameSpec(privilegeLoadDto.Name);

            var dbPrivilege = await privilegeRepo
                .FirstOrDefaultAsync(spec);

            if (dbPrivilege == null)
            {
                dbPrivilege = new Privilege
                {
                    Name = privilegeLoadDto.Name.ToLowerInvariant(),
                    Description = privilegeLoadDto.Description
                };
                await privilegeRepo.CreateAsync(dbPrivilege);
            }
            else
            {
                dbPrivilege.Description = privilegeLoadDto.Description;
                await privilegeRepo.UpdateAsync(dbPrivilege);
            }
        }

        #endregion
    }
}
