using System.Linq;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using WebSsh.Application.Dto.Roles;
using WebSsh.Application.Dto.Users;
using WebSsh.Core.Entities;
using WebSsh.Core.Entities.Settings;

namespace WebSsh.Application.Internal
{
    internal static class MapsterConfiguration
    {
        public static void ConfigureMapster(this IServiceCollection services)
        {
            services.AddSingleton(GetConfiguredMappingConfig());
            services.AddScoped<IMapper, ServiceMapper>();
        }

        internal static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<PasswordPolicy, PasswordPolicyDto>();
            config.NewConfig<PasswordPolicyDto, PasswordPolicy>();

            config.NewConfig<User, UserDto>()
                .Map(_ => _.Role, o => o.Roles.First().Role);
            config.NewConfig<Role, RoleDto>();

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
