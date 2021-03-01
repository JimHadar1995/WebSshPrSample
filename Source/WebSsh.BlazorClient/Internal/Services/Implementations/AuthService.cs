using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using WebSsh.BlazorClient.Internal.Constants;
using WebSsh.BlazorClient.Internal.Extensions;
using WebSsh.BlazorClient.Internal.Models.Auth;
using WebSsh.BlazorClient.Internal.Services.Contracts;
using WebSsh.Shared.Dto.Users;

namespace WebSsh.BlazorClient.Internal.Services.Implementations
{
    /// <inheritdoc/>
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _factory;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        public AuthService(
            IHttpClientFactory factory,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage)
        {
            _factory = factory;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        /// <inheritdoc/>
        public async Task<bool> LoginByPass(LoginByPassCredentials credentials)
        {
            using var client = _factory.CreateClient(HttpClients.WebSshClient);            

            using var response = await client.PostAsync(UrlConstants.Login, credentials.AsStringContent());

            if (!response.IsSuccessStatusCode)
            {
                //TODO: что-то делаем, если косяк
                return false;
            }

            var result = await response.Content.ReadFromJsonAsync<AuthSuccessResult>(JsonExtensions.JsonSerializerOptions);

            Console.WriteLine(result);

            return true;
        }
    }
}
