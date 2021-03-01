using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using WebSsh.BlazorClient.Internal.Exceptions;
using WebSsh.BlazorClient.Internal.Services.Contracts;

namespace WebSsh.BlazorClient.Internal.Services.Implementations
{
    /// <inheritdoc/>
    public class LocalStorageService : ILocalStorageService
    {
        private IJSRuntime _jsRuntime;

        /// <summary>
        /// 
        /// </summary>
        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        /// <inheritdoc/>
        public async Task<T?> GetItem<T>(string key)
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

            if (json == null)
                return default;

            return JsonSerializer.Deserialize<T>(json) ?? default;
        }

        /// <inheritdoc/>
        public async Task SetItem<T>(string key, T value)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));
        }

        /// <inheritdoc/>
        public async Task RemoveItem(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }
}
