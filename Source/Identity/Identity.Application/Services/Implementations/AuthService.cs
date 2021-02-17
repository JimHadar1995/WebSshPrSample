using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Dto.Users;
using Identity.Application.Services.Contracts;
using Identity.Core.Entities;
using Identity.Core.Enums;
using Identity.Core.Exceptions;
using Identity.Core.Services;
using Identity.ResourceManager.Constants;
using Identity.ResourceManager.Validation;
using Library.Common.Authentication;
using Library.Common.Authentication.Models;
using Library.Common.Database;
using Library.Common.Extensions;
using Library.Common.Localization;
using Library.Logging.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Application.Services.Implementations
{
    public sealed class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly JwtOptions _jwtOptions;
        private readonly IValidationLocalizer _validationLocalizer;
        private readonly IIdentityAppSettings _settings;
        private readonly ILogger _logger;
        private readonly IOwnSystemLocalizer<UsersConstants> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            IOptionsSnapshot<JwtOptions> jwtOptions,
            IValidationLocalizer validationLocalizer,
            IIdentityAppSettings settings,
            ILogger logger,
            IOwnSystemLocalizer<UsersConstants> localizer)
        {
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _validationLocalizer = validationLocalizer;
            _settings = settings;
            _logger = logger;
            _localizer = localizer;
        }

        /// <inheritdoc />
        public async Task<JsonWebToken> LoginByPassAsync(LoginByPassCredentials credentials)
        {
            var user = await CheckUser(credentials);
            var result = await GetFullJwtToken(user);
            await UpdateLastLogin(user);
            _logger.Info(_localizer[UsersConstants.LoggedSuccess, user.UserName]);
            return result;
        }

        #region [ Help methods ]

        private async Task<JsonWebToken> GetFullJwtToken(User user)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.SecretKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = await GetUserClaims(user);

            var expires = _jwtOptions.NotBefore.Add(TimeSpan.FromMinutes(_jwtOptions.ExpiryMinutes));

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.ValidAudience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: expires,
                signingCredentials: signingCredentials);

            var authToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new JsonWebToken
            {
                AccessToken = authToken,
                Expires = expires.DatetimeToUnixTimeStamp(),
                UserId = user.Id,
                UserName = user.UserName,
                Roles = user.Roles.Select(_ => _.Name).ToArray(),
                NeedResetPassword = await IsNeedResetPassword(user.UserName),
                TokenId = jwt.Claims.First(_ => _.Type == CustomClaims.TokenIdClaim).Value
            };

            foreach (var jwtClaim in jwt.Claims)
            {
                response.Claims.Add(jwtClaim.Type, jwtClaim.Value);
            }

            return response;
        }

        /// <summary>
        /// Checks the user.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        /// <exception cref="AuthorizationErrorException"></exception>
        private async Task<User> CheckUser(LoginByPassCredentials credentials)
        {
            var user = await GetUser(credentials.UserName);
            if (user == null)
            {
                //_logger.Error(_validationLocalizer.Message(ValidationConstants.InvalidLoginOrPassword), LoggerEntityType.AuthProfileEntity, null);
                throw new AuthorizationErrorException(_validationLocalizer.Message(ValidationConstants.InvalidLoginOrPassword));
            }
            if (user.Status == UserStatus.Locked)
            {
                //_logger.Error(_validationLocalizer.Message(ValidationConstants.UserLocked), LoggerEntityType.AuthProfileEntity, user.UserName);
                throw new AuthorizationErrorException(_validationLocalizer.Message(ValidationConstants.UserLocked));
            }
            await CheckPasswordAsync(user, credentials.Password);
            return user;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        private async Task<User?> GetUser(string userName)
            => await _userManager
                .FindByNameAsync(userName)
                .ConfigureAwait(false);

        /// <summary>
        /// Checks the password asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <exception cref="AuthorizationErrorException">
        /// </exception>
        private async Task CheckPasswordAsync(User user, string password)
        {
            var checkPass = await _signInManager
                .PasswordSignInAsync(user, password, false, true);

            var userInner = await _userManager.FindByIdAsync(user.Id.ToString());

            if (checkPass.IsLockedOut)
            {
                throw new AuthorizationErrorException(
                    _validationLocalizer.Message(ValidationConstants.AdminLockedNumberAttempts,
                        user.UserName,
                        _signInManager.Options.Lockout.DefaultLockoutTimeSpan.Minutes));
            }

            if (!checkPass.Succeeded)
            {
                throw new AuthorizationErrorException(_validationLocalizer.Message(ValidationConstants.InvalidLoginOrPassword));
            }

            await _userManager.UpdateAsync(userInner);
        }

        private async Task<List<Claim>> GetUserClaims(User user)
        {
            var (roles, privileges) = GetPrivileges(user);

            var needResetPassword = await IsNeedResetPassword(user.UserName);

            List<Claim> foundClaims = new List<Claim>();
            foreach (var role in roles)
            {
                var r = await _roleManager.FindByIdAsync(role.Id.ToString());
                foundClaims.AddRange(await _roleManager.GetClaimsAsync(r));
            }

            var rolesString = roles.Select(_ => _.Name).ToList();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    _jwtOptions.IssuedAt.DatetimeToUnixTimeStamp().ToString(CultureInfo.CurrentCulture),
                    ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault()!.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(CultureInfo.CurrentCulture)),
                new Claim(CustomClaims.RolesClaim, string.Join(';', rolesString)),
                new Claim(CustomClaims.PrivilegesClaim, string.Join(';', privileges)),
                new Claim(nameof(JsonWebTokenPayload.NeedResetPassword), JsonSerializer.Serialize(needResetPassword)),
                new Claim(CustomClaims.TokenIdClaim, Guid.NewGuid().ToString())
            };
            claims.AddRange(foundClaims);

            return claims;
        }

        private (List<Role> roles, List<string> privileges) GetPrivileges(User user)
        {
            var roles = user.Roles.ToList();

            List<string> privileges = new List<string>(roles.Count * 3);

            foreach (var role in roles)
            {
                foreach (var rolePrivilege in role.Privileges)
                {
                    privileges.Add(rolePrivilege.Name);
                }
            }

            return (roles, privileges);
        }

        private async Task<bool> IsNeedResetPassword(string userName)
        {

            var admin = await _userManager.FindByNameAsync(userName);
            var passwordPolicy = _settings.PasswordPolicy;
            int maxExpireTime = passwordPolicy.RequiredMaxExpireTime;
            var datePasswordChanged = admin.DatePasswordChanged;

            var maxExpireDate = datePasswordChanged.AddDays(maxExpireTime);

            return
                // срок действия пароля больше максимального значения, указанного в настройках
                maxExpireDate <= DateTimeOffset.UtcNow
                ||
                !admin.HashHistory.Any();
        }

        private async Task UpdateLastLogin(User user)
        {
            user.LastLogin = DateTimeOffset.UtcNow;
            await _userManager.UpdateAsync(user);
        }
        #endregion
    }
}
