using Identity.Application.Services.Contracts;
using Identity.Application.Services.Implementations;
using Identity.Core.Services;
using Identity.ResourceManager.Implementations;
using Library.Common.Kafka;
using Library.Common.Localization;
using Library.Common.Types.Wrappers;
using Library.Logging.Contracts;
using Library.Logging.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Identity.Application.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public static class Ioc
    {
        /// <summary>
        /// Configures the application services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureAppServices(this IServiceCollection services)
        {
            services.AddMemoryCache();
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetRequiredService<IConfiguration>();
            }

            services.AddSingleton<ICacheWrapper, MemoryCacheWrapper>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILogService, LogService>();

            services.AddScoped<ILoggerFactory, LoggerFactorySC>();
            services.AddScoped<IIdentityAppSettings, IdentityAppSettings>();
            services.AddScoped<ISettingsService, SettingsService>();

            services.ConfigureLocalization();
            services.InitKafkaServices(configuration);

            services.ConfigureMapster();
        }

        private static void ConfigureLocalization(this IServiceCollection services)
        {
            services.InitializeBaseLocaleDi();
            services.TryAddTransient<IValidationLocalizer, ValidationLocalizer>();
        }
    }
}
