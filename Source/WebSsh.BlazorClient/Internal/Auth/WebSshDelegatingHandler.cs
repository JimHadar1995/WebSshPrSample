using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using WebSsh.BlazorClient.Internal.Constants;
using WebSsh.BlazorClient.Internal.Services.Contracts;

namespace WebSsh.BlazorClient.Internal.Auth
{
    public class WebSshDelegatingHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="localStorage"></param>
        public WebSshDelegatingHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authToken = await _localStorage.GetItem<string>(LocalStorageConstants.AuthToken);

            if (!string.IsNullOrWhiteSpace(authToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }
            request.Headers.AcceptLanguage.Clear();
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
