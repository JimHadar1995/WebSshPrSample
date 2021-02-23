using Library.Common.Kafka;
using Library.Common.Localization;
using Library.Common.Types.Wrappers;
using Library.Logging.Contracts;
using Library.Logging.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebSsh.Application.Services.Contracts;
using WebSsh.Application.Services.Implementations;
using WebSsh.Core.Services;
using WebSsh.ResourceManager.Implementations;

namespace WebSsh.Application.Internal
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
        public static void ConfigureAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            services.AddSingleton<ICacheWrapper, MemoryCacheWrapper>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILogService, LogService>();

            services.AddScoped<ILoggerFactory, LoggerFactorySC>();
            services.AddScoped<IIdentityAppSettings, IdentityAppSettings>();
            services.AddScoped<ISettingsService, SettingsService>();

            services.ConfigureLocalization();

            services.ConfigureMapster();
        }

        private static void ConfigureLocalization(this IServiceCollection services)
        {
            services.InitializeBaseLocaleDi();
            services.TryAddTransient<IValidationLocalizer, ValidationLocalizer>();
        }
    }
}
