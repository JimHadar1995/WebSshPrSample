using System.Linq;
using Identity.Application.Dto.Roles;
using Identity.Application.Dto.Users;
using Identity.Core.Entities;
using Identity.Core.Entities.Settings;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application.Internal
{
    internal static class MapsterConfiguration
    {
        public static void ConfigureMapster(this IServiceCollection services)
        {
            services.AddSingleton(GetConfiguredMappingConfig());
        }

        internal static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<PasswordPolicy, PasswordPolicyDto>();
            config.NewConfig<PasswordPolicyDto, PasswordPolicy>();

            config.NewConfig<User, UserDto>()
                .Map(_ => _.Roles, o => o.Roles);
            config.NewConfig<Role, RoleDto>();
            config.NewConfig<Privilege, PrivilegeDto>();

            config.NewConfig<UserAddDto, User>()
                .Map(_ => _.UserName, _ => _.UserName)
                .Map(_ => _.Description, _ => _.Description)
                .Map(_ => _.Email, _ => _.Email)
                .IgnoreNonMapped(true);

            config.NewConfig<UserUpdateDto, User>()
                .Map(_ => _.UserName, _ => _.UserName)
                .Map(_ => _.Description, _ => _.Description)
                .Map(_ => _.Email, _ => _.Email)
                .IgnoreNonMapped(true);

            return config;
        }
    }
}
