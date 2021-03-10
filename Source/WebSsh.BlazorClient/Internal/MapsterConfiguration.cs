using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using WebSsh.BlazorClient.Internal.Models.Ssh;
using WebSsh.BlazorClient.Internal.Models.ViewModels.Users;
using WebSsh.Shared.Dto.Ssh;
using WebSsh.Shared.Dto.Users;

namespace WebSsh.BlazorClient.Internal
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

            config.NewConfig<UserDto, UserViewModel>()
                .Map(_ => _.RoleId, o => o.Role.Id)
                .Map(_ => _.UserName, _ => _.UserName)
                .Map(_ => _.Description, _ => _.Description)
                .Map(_ => _.Email, _ => _.Email)
                .Map(_ => _.Id, _ => _.Id)
                .IgnoreNonMapped(true);

            config.NewConfig<UserViewModel, UserAddDto>();
            config.NewConfig<UserViewModel, UserUpdateDto>();
            config.NewConfig<ConnectionViewModel, ConnectionsInfoDto>();

            return config;
        }
    }
}
