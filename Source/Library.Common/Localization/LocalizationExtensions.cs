using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;

namespace Library.Common.Localization
{
    /// <summary>
    /// 
    /// </summary>
    public static class LocalizationExtensions
    {
        /// <summary>
        /// Здесь происходит инцииалиацзия всех зависимостей локализации, кроме интерфеса <see cref="IValidationLocalizer"/>
        /// </summary>
        /// <param name="services">The services.</param>
        public static void InitializeBaseLocaleDi(this IServiceCollection services)
        {
            services.AddOptions();
            services.TryAddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();

            services.TryAddTransient(typeof(IOwnLocalizer<>), typeof(OwnLocalizer<>));
            services.TryAddTransient(typeof(IOwnSystemLocalizer<>), typeof(OwnSystemLocalizer<>));

            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    /* your configurations*/
                    var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("ru")
                    };

                    opts.DefaultRequestCulture = new RequestCulture("en");
                    // Formatting numbers, dates, etc.
                    opts.SupportedCultures = supportedCultures;
                    // UI strings that we have localized.
                    opts.SupportedUICultures = supportedCultures;
                });
        }
    }
}
