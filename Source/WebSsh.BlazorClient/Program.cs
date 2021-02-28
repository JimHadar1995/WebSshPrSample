using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebSsh.Shared.Dto.Users;

namespace WebSsh.BlazorClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp =>
            {
                if (builder.HostEnvironment.Environment == "Development")
                {
                    return new HttpClient { BaseAddress = new Uri("http://localhost:5000") };
                }
                return new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
            });

            await builder.Build().RunAsync();
        }
    }
}
