using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebSsh.BlazorClient.Internal.Auth;
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
        private readonly IMessageService _message;
        private readonly NavigationManager _navigationManager;
        public AuthService(
            IHttpClientFactory factory,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage,
            IMessageService message,
            NavigationManager navigationManager)
        {
            _factory = factory;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _message = message;
            _navigationManager = navigationManager;
        }

        /// <inheritdoc/>
        public async Task<bool> LoginByPass(LoginByPassCredentials credentials)
        {
            using var client = _factory.CreateClient(HttpClients.WebSshClient);

            foreach(var acc in client.DefaultRequestHeaders.AcceptLanguage)
            {
                string val = acc.Value;
            }

            using var response = await client.PostAsync(UrlConstants.Login, credentials.AsStringContent());

            if (!response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();

                _message.Error(res);

                return false;
            }

            AuthData result = (await response.Content.ReadFromJsonAsync<AuthData>(JsonExtensions.JsonSerializerOptions))!;

            await _localStorage.SetItem(LocalStorageConstants.AuthToken, result.AccessToken);

            await _localStorage.SetItem(LocalStorageConstants.AuthData, result);

            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(result.UserName);

            _navigationManager.NavigateTo("/");

            return true;
        }

        /// <inheritdoc/>
        public async Task Logout()
        {
            await _localStorage.RemoveItem(LocalStorageConstants.AuthData);
            await _localStorage.RemoveItem(LocalStorageConstants.AuthToken);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        }
    }
}
