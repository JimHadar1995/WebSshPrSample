using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebSsh.Core.Entities;
using WebSsh.Core.Services;

namespace WebSsh.Application.Services
{
    public sealed class IdentityUserManager : UserManager<User>
    {
        /// <inheritdoc />
        public IdentityUserManager(
            IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger,
            IIdentityAppSettings settings)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            try
            {
                Options.Lockout = settings.PasswordPolicy;
            }
            catch
            {
                //
            }
        }
    }
}
