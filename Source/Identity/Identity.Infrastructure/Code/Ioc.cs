using System.Reflection;
using Identity.Application.Services;
using Identity.Core.Entities;
using Identity.Core.PostgreSql.Contexts;
using Identity.Core.PostgreSql.Implementations;
using Identity.Infrastructure.Handlers.Commands.Users;
using Library.Common.Database;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Code
{
    /// <summary>
    /// 
    /// </summary>
    public static class Ioc
    {
        public static void ConfigureInfrastructureServices(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetRequiredService<IConfiguration>();
            }
            services.AddDbContext<IdentityContext>(options =>
            {
                string connString = configuration.GetConnectionString("DefaultConnection");
                options.UseLazyLoadingProxies()
                    .UseNpgsql(connString);
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 1;
            })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddUserManager<IdentityUserManager>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.ConfigureMediatR();
        }

        /// <summary>
        /// Configures the mediat r.
        /// </summary>
        /// <param name="services">The services.</param>
        private static void ConfigureMediatR(this IServiceCollection services)
        {
            var assmebly = Assembly.GetAssembly(typeof(UpdateUserCommandHandler));
            services.AddMediatR(assmebly);
        }
    }
}
