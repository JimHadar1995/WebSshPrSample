using System;
using System.Linq;
using System.Threading.Tasks;
using Library.Common.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebSsh.Core.Entities;
using WebSsh.Core.PostgreSql.Contexts;

namespace WebSsh.Infrastructure.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationDbIntializer
    {
        /// <summary>
        /// Применение миграций
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task MigrateAsync(IServiceProvider serviceProvider)
        {
            try
            {
                var context = serviceProvider.GetRequiredService<WebSshDbContext>();
                var migrations = (await context.Database.GetPendingMigrationsAsync()
                        .ConfigureAwait(false))
                    .ToList();
                if (migrations.Any())
                {
                    await context.Database.MigrateAsync();
                }

                Console.WriteLine($"Db deployed successfull");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Initialize migrations error: {ex.Message}");
            }
        }

        /// <summary>
        /// Инициализация БД начальными данными
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static async Task InitializeData(this IServiceCollection serviceCollection)
        {
            try
            {
                var sp = serviceCollection.BuildServiceProvider();
                using var scope = sp.CreateScope();

                await MigrateAsync(scope.ServiceProvider).ConfigureAwait(false);

                var ufw = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roles = roleManager.Roles.ToList();

                if (!roles.Any())
                {
                    var adminRole = new Role { Name = Role.Administrator, Description = "Администратор", };
                    var userRole = new Role { Name = Role.Readonly, Description = "Пользователь" };
                    await roleManager.CreateAsync(adminRole);
                    await roleManager.CreateAsync(userRole);
                }

                if (!ufw.Repository<User>().GetAll().Any())
                {
                    var user = new User
                    {
                        UserName = User.DefaultAdmin,
                        Description = "Администратор по умолчанию",
                        IsDefaultUser = true
                    };
                    var result = await userManager.CreateAsync(user, "Qwerty7").ConfigureAwait(false);
                    if (result == IdentityResult.Success)
                    {
                        await userManager.AddToRoleAsync(user, Role.Administrator);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
