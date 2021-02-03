using Identity.Application.Dto;
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

            return config;
        }
    }
}
