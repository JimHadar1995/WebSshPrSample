using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebSsh.BlazorClient.Internal.Constants;
using WebSsh.BlazorClient.Internal.Exceptions;
using WebSsh.BlazorClient.Internal.Extensions;
using WebSsh.BlazorClient.Internal.Services.Contracts;

namespace WebSsh.BlazorClient.Internal.Services.Implementations.Api
{

    public abstract class BaseApiService
    {
        private protected readonly IHttpClientFactory _factory;
        private protected readonly IMessageService _message;
        private protected readonly NavigationManager _navigationManager;

        public BaseApiService(
            IHttpClientFactory factory,
            IMessageService message,
            NavigationManager navigationManager)
        {
            _factory = factory;
            _message = message;
            _navigationManager = navigationManager;
        }

        private protected async Task<TResult> GetAsync<TResult>(string url, CancellationToken token = default)
        {
            using var client = _factory.CreateClient(HttpClients.WebSshClient);

            try
            {
                var result = await client.GetAsync(url, token);
                await CheckApiResponse(result);

                return (await result.Content.ReadFromJsonAsync<TResult>(JsonExtensions.JsonSerializerOptions, token))!;
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
                _message.Error("Unknown error");
                throw new ApiException("Unknown error", ex);
            }
        }

        private protected async Task<TResult> PostAsync<TRequest, TResult>(string url, TRequest data, CancellationToken token = default)
        {
            using var client = _factory.CreateClient(HttpClients.WebSshClient);

            try
            {
                var result = await client.PostAsync(url, data.AsStringContent(), token);
                await CheckApiResponse(result);

                return (await result.Content.ReadFromJsonAsync<TResult>(JsonExtensions.JsonSerializerOptions, token))!;
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
                _message.Error("Unknown error");
                throw new ApiException("Unknown error", ex);
            }
        }

        private protected async Task PostEmptyResponseAsync<TRequest>(string url, TRequest data, CancellationToken token = default)
        {
            using var client = _factory.CreateClient(HttpClients.WebSshClient);

            try
            {
                var result = await client.PostAsync(url, data.AsStringContent(), token);
                await CheckApiResponse(result);
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
                _message.Error("Unknown error");
                throw new ApiException("Unknown error", ex);
            }
        }

        private protected async Task PutAsync<TRequest>(string url, TRequest data, CancellationToken token = default)
        {
            using var client = _factory.CreateClient(HttpClients.WebSshClient);

            try
            {
                var result = await client.PutAsync(url, data.AsStringContent(), token);
                await CheckApiResponse(result);
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
                _message.Error("Unknown error");
                throw new ApiException("Unknown error", ex);
            }
        }

        private protected async Task DeleteAsync(string url, CancellationToken token)
        {
            using var client = _factory.CreateClient(HttpClients.WebSshClient);

            try
            {
                var result = await client.DeleteAsync(url, token);
                await CheckApiResponse(result);
            }
            catch (ApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.Message);
#endif
                _message.Error("Unknown error");
                throw new ApiException("Unknown error", ex);
            }
        }

        private protected async Task CheckApiResponse(HttpResponseMessage? httpResponseMessage)
        {
            if (httpResponseMessage == null)
            {
                throw new ApiException("Empty response");
            }

            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    {
                        var stringMessage = await httpResponseMessage.Content.ReadAsStringAsync();
                        _message.Error(stringMessage);
                        throw new BadRequestApiException(stringMessage);
                    }
                case HttpStatusCode.NotFound:
                    {
                        throw new NotFoundException();
                    }
                case HttpStatusCode.Unauthorized:
                    {
                        _navigationManager.NavigateTo(PageUrlConstants.Login);
                        break;
                    }
                case HttpStatusCode.OK:
                    {
                        return;
                    }
                case HttpStatusCode.NoContent:
                    return;
                default:
                    {
                        throw new ApiException("Unknown error");
                    }
            }
        }
    }
}
