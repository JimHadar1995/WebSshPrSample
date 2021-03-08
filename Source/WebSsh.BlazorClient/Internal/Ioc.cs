using System;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebSsh.BlazorClient.Internal.Auth;
using WebSsh.BlazorClient.Internal.Constants;
using WebSsh.BlazorClient.Internal.Models;
using WebSsh.BlazorClient.Internal.Services.Contracts;
using WebSsh.BlazorClient.Internal.Services.Implementations;
using WebSsh.BlazorClient.Internal.Services.Implementations.Api;
using WebSsh.Shared.Contracts;

namespace WebSsh.BlazorClient.Internal
{
    /// <summary>
    /// 
    /// </summary>
    internal static class Ioc
    {
        public static void InitServices(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddAntDesign();

            builder.Services.AddHttpClient(HttpClients.WebSshClient, async (sp, client) =>
            {
                if (builder.HostEnvironment.Environment == "Development")
                {
                    client.BaseAddress = new Uri("http://localhost:5000");
                }
                else
                {
                    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
                }
            })
                .AddHttpMessageHandler<WebSshDelegatingHandler>();

            builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
            builder.Services.AddScoped<IMessageService, MessageService>();

            builder.Services.AddAuthorizationCore(config =>
            {
                config.AddPolicy(Policies.IsAdmin, Policies.IsAdminPolicy());
                config.AddPolicy(Policies.IsReadonly, Policies.IsReadonlyPolicy());
            });

            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

            builder.Services.AddTransient<WebSshDelegatingHandler>();

            builder.Services.AddScoped<IMenuService, MenuService>();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();

            builder.Services.AddSingleton<AppState>();

            builder.Services.ConfigureMapster();
        }
    }
}
