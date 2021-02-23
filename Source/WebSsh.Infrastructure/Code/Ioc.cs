using System.Reflection;
using Identity.Infrastructure.Handlers.Commands.Users;
using Library.Common.Database;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebSsh.Application.Services;
using WebSsh.Core.Entities;
using WebSsh.Core.PostgreSql.Contexts;
using WebSsh.Core.PostgreSql.Implementations;
using WebSsh.Terminal.Common;

namespace WebSsh.Infrastructure.Code
{
    /// <summary>
    /// 
    /// </summary>
    public static class Ioc
    {
        public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebSshDbContext>(options =>
            {
                var connString = configuration.GetConnectionString("DefaultConnection");
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
                .AddEntityFrameworkStores<WebSshDbContext>()
                .AddUserManager<IdentityUserManager>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.ConfigureMediatR();

            services.AddSingleton<ServerShellPoll>();
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
