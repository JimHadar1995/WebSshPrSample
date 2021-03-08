using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebSsh.BlazorClient.Internal.Constants;
using WebSsh.BlazorClient.Internal.Models;
using WebSsh.BlazorClient.Internal.Services.Contracts;
using WebSsh.Shared.Contracts;
using WebSsh.Shared.Dto.Roles;

namespace WebSsh.BlazorClient.Internal.Services.Implementations.Api
{
    /// <inheritdoc cref="IRoleService"/>
    public sealed class RoleService : BaseApiService, IRoleService
    {
        private readonly AppState _appState;
        public RoleService(
            IHttpClientFactory factory,
            IMessageService message,
            NavigationManager navigationManager,
            AppState appState)
            : base(factory, message, navigationManager)
        {
            _appState = appState;
        }

        /// <inheritdoc cref="IRoleService"/>
        public async Task<IReadOnlyList<RoleDto>> GetAllAsync(CancellationToken token)
        {
            if (!_appState.Roles.Any())
                _appState.Roles = await GetAsync<IReadOnlyList<RoleDto>>(UrlConstants.Roles, token);

            return _appState.Roles;
        }

        /// <inheritdoc cref="IRoleService"/>
        public async Task<RoleDto> GetByIdAsync(int id, CancellationToken token)
        {
            var roles = await GetAllAsync(token);

            return roles.First(_ => _.Id == id);
        }
    }
}
