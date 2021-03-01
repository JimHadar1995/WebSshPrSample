using System;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebSsh.BlazorClient.Internal.Auth;
using WebSsh.BlazorClient.Internal.Constants;
using WebSsh.BlazorClient.Internal.Services.Contracts;
using WebSsh.BlazorClient.Internal.Services.Implementations;

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

                var localStorage = sp.GetRequiredService<ILocalStorageService>();

                var authToken = await localStorage.GetItem<string>(LocalStorageConstants.AuthToken);

                if (!string.IsNullOrWhiteSpace(authToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                }
            });

            builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();

            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();
        }
    }
}
