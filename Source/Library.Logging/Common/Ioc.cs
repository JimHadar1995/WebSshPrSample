using Library.Logging.Contracts;
using Library.Logging.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Logging.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class Ioc
    {
        /// <summary>
        /// Configures the voltron logger.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureSCLogger(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<ILoggerFactory, LoggerFactorySC>();
            services.AddScoped<ILoggerFactory, LoggerFactorySC>();
            services.AddTransient<ILogger>((s) =>
            {
                var factory = s.GetRequiredService<ILoggerFactory>();
                return factory.DefaultLogger();
            });
        }
    }
}
