using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebSsh.BlazorClient.Internal.Constants;
using WebSsh.BlazorClient.Internal.Services.Contracts;
using WebSsh.Shared.Contracts;
using WebSsh.Shared.Dto.Users;

namespace WebSsh.BlazorClient.Internal.Services.Implementations.Api
{
    /// <inheritdoc/>
    public class UserService : BaseApiService, IUserService
    {
        public UserService(
            IHttpClientFactory factory, 
            IMessageService message, 
            NavigationManager navigationManager) 
            : base(factory, message, navigationManager)
        {

        }

        /// <inheritdoc/>
        public async Task<int> CreateAsync(UserAddDto model, CancellationToken token = default)
            => await PostAsync<UserAddDto, int>(UrlConstants.Users, model, token);

        /// <inheritdoc/>
        public async Task UpdateAsync(UserUpdateDto model, CancellationToken token = default)
        {
            await PutAsync(UrlConstants.Users, model, token);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(int id, CancellationToken token = default)
        {
            await DeleteAsync($"{UrlConstants.Users}/{id}", token);
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<UserDto>> GetAllAsync(CancellationToken token = default)
            => await GetAsync<List<UserDto>>(UrlConstants.Users, token);

        /// <inheritdoc/>
        public async Task<UserDto> GetAsync(int id, CancellationToken token = default)
            => await GetAsync<UserDto>($"{UrlConstants.Users}/{id}", token);

        /// <inheritdoc/>
        public Task ChangePasswordAsync(string userName, string newPassword, bool passwordForReset = false, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task LockAsync(int userId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task UnLockAsync(int userId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

    }
}
