using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Dto.Users;
using Identity.Application.Services.Contracts;
using Identity.Core.Entities;
using Identity.Core.Enums;
using Identity.Core.Exceptions;
using Identity.Core.Services;
using Identity.ResourceManager.Validation;
using Library.Common.Database;
using Library.Common.Exceptions;
using Library.Common.Localization;
using Library.Common.Types.Wrappers;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Services.Implementations
{
    /// <inheritdoc/>
    public sealed class UserService : IUserService
    {
        private readonly IUnitOfWork _ufw;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;
        private readonly IValidationLocalizer _localizer;
        private readonly IIdentityAppSettings _settings;
        private readonly ICacheWrapper _cache;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        public UserService(
            IUnitOfWork ufw,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IHttpContextAccessor accessor,
            IMapper mapper,
            IValidationLocalizer localizer,
            IIdentityAppSettings settings,
            ICacheWrapper cache)
        {
            _ufw = ufw;
            _userManager = userManager;
            _roleManager = roleManager;
            _accessor = accessor;
            _mapper = mapper;
            _localizer = localizer;
            _settings = settings;
            _cache = cache;
        }

        /// <inheritdoc />
        public async Task<int> CreateAsync(UserAddDto model, CancellationToken token = default)
        {
            var user = _mapper.Map<User>(model);

            user.CreatedAt = DateTimeOffset.UtcNow;
            user.UpdatedAt = DateTimeOffset.UtcNow;
            user.DatePasswordChanged = DateTimeOffset.UtcNow;
            user.PasswordResetedByAdministrator = false;

            var roleIds = model.RoleIds.Distinct().ToList();
            var allRoles = await _roleManager.Roles.ToListAsync(token);

            foreach (var roleId in roleIds)
            {
                if (allRoles.Any(_ => roleIds.Contains(_.Id)))
                {
                    var role = await _roleManager.FindByIdAsync(roleId.ToString());
                    if (role != null)
                    {
                        await _userManager.AddToRoleAsync(user, role.Name);
                    }
                }
            }

            return user.Id;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(UserUpdateDto model, CancellationToken token = default)
        {
            var curUser = await _userManager.FindByIdAsync(model.Id.ToString()).ConfigureAwait(false);
            if (curUser == null)
                throw new EntityNotFoundException();

            curUser.UpdatedAt = DateTimeOffset.UtcNow;

            await _userManager.UpdateAsync(curUser);

            await _userManager.RemoveFromRolesAsync(curUser, curUser.Roles.Select(_ => _.Name).ToList());
            var roleIds = model.RoleIds.Distinct().ToList();
            var allRoles = await _roleManager.Roles.ToListAsync(token);
            foreach (var roleId in roleIds)
            {
                if (allRoles.Any(_ => roleIds.Contains(_.Id)))
                {
                    var role = await _roleManager.FindByIdAsync(roleId.ToString());
                    await _userManager.AddToRoleAsync(curUser, role.Name);
                }
            }
        }

        /// <inheritdoc />
        public async Task ChangePasswordAsync(
            string userName,
            string newPassword,
            bool passwordForReset = false,
            CancellationToken token = default)
        {
            var curUser = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);
            if (curUser == null)
                throw new EntityNotFoundException();

            if (!passwordForReset)
            {
                throw new IdentityServiceException(_localizer.Message(ValidationConstants.NewPasswordHasBeenUsedBefore));
            }

            string oldHash = curUser.PasswordHash;

            await _userManager.RemovePasswordAsync(curUser).ConfigureAwait(false);
            await _userManager.AddPasswordAsync(curUser, newPassword).ConfigureAwait(false);

            curUser.DatePasswordChanged = DateTimeOffset.UtcNow;

            curUser.PasswordResetedByAdministrator = passwordForReset;

            await _userManager.UpdateAsync(curUser);

            var hashHistoryRepo = _ufw.Repository<UserPasswordHashHistory>();
            var hashHistory = new UserPasswordHashHistory
            {
                DateChanged = DateTime.Now,
                Hash = oldHash,
                UserId = curUser.Id,
            };
            await hashHistoryRepo.CreateAsync(hashHistory, token);
        }

        /// <inheritdoc />
        public async Task LockAsync(int userId, CancellationToken token = default)
        {
            var curUser = await _userManager.FindByIdAsync(userId.ToString());
            if (curUser == null)
                throw new EntityNotFoundException();

            if (curUser.IsDefaultUser)
                throw new IdentityServiceException(_localizer.Message(ValidationConstants.DefaultUserLockIsProhibited));

            if (curUser.Status != UserStatus.Locked)
            {
                curUser.Status = UserStatus.Locked;
                await _userManager.UpdateAsync(curUser).ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public async Task UnLockAsync(int userId, CancellationToken token = default)
        {
            var curUser = await _userManager.FindByIdAsync(userId.ToString());
            if (curUser == null)
                throw new EntityNotFoundException();

            if (curUser.Status != UserStatus.Active)
            {
                curUser.Status = UserStatus.Active;
                await _userManager.UpdateAsync(curUser).ConfigureAwait(false);
            }
        }

        /// <inheritdoc />
        public async Task<UserDto> GetAsync(int id, CancellationToken token = default)
        {
            var curUser = await _userManager.FindByIdAsync(id.ToString());
            if (curUser == null)
                throw new EntityNotFoundException();

            var userDto = _mapper.Map<UserDto>(curUser);

            return userDto;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<UserDto>> GetAllAsync(CancellationToken token = default)
        {
            var users = await _userManager
                .Users
                .OrderBy(_ => _.UserName)

                .ToListAsync(token);

            return _mapper.Map<List<UserDto>>(users);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id, CancellationToken token = default)
        {
            var curUser = await _userManager.FindByIdAsync(id.ToString()).ConfigureAwait(false);
            if (curUser == null)
                return;

            if (curUser.IsDefaultUser)
            {
                throw new IdentityServiceException(_localizer.Message(ValidationConstants.RemovingDefaultUserIsProhibited));
            }

            await _userManager.DeleteAsync(curUser).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(List<int> ids, CancellationToken token = default)
        {
            foreach (var id in ids)
            {
                await DeleteAsync(id, token).ConfigureAwait(false);
            }
        }

        #region [ Help methods ]

        #endregion
    }
}
