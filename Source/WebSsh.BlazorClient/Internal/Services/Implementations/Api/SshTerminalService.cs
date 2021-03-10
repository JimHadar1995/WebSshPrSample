using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebSsh.BlazorClient.Internal.Constants;
using WebSsh.BlazorClient.Internal.Models.Ssh;
using WebSsh.BlazorClient.Internal.Services.Contracts;
using WebSsh.Shared.Dto.Ssh;

namespace WebSsh.BlazorClient.Internal.Services.Implementations.Api
{
    /// <inheritdoc cref="ISshTerminalService"/>
    public class SshTerminalService : BaseApiService, ISshTerminalService
    {
        /// <inheritdoc cref="BaseApiService"/>
        public SshTerminalService(
            IHttpClientFactory factory,
            IMessageService message,
            NavigationManager navigationManager)
            : base(factory, message, navigationManager)
        {
        }

        /// <inheritdoc />
        public async Task<bool> CheckConnection(SshSessionModel sshSessionModel, CancellationToken token = default)
            => await GetAsync<bool>(UrlConstants.SshTerminal + "?SessionId=" + sshSessionModel.SessionId.ToString(), token);

        /// <inheritdoc />
        public async Task<SshOperationResult> ConnectToSource(ConnectionsInfoDto model, CancellationToken token = default)
            => await PostAsync<ConnectionsInfoDto, SshOperationResult>(UrlConstants.SshTerminal + "/connect", model, token);

        /// <inheritdoc />
        public async Task DisconnectFromSource(SshSessionModel model, CancellationToken token = default)
        {
            await PostEmptyResponseAsync(UrlConstants.SshTerminal + "/disconnect", model, token);
        }

        /// <inheritdoc />
        public async Task<SshOperationResult> ExecuteCommand(ExecuteCommandModel model, CancellationToken token = default)
            => await PostAsync<ExecuteCommandModel, SshOperationResult>(UrlConstants.SshTerminal + "/execute", model, token);

        /// <inheritdoc />
        public async Task<IReadOnlyList<UserSessionDto>> GetSessions(CancellationToken token = default)
            => await GetAsync<IReadOnlyList<UserSessionDto>>(UrlConstants.SshTerminal + "/sessions", token);
    }
}
