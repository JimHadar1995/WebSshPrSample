using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebSsh.BlazorClient.Internal.Models.Ssh;
using WebSsh.Shared.Dto.Ssh;

namespace WebSsh.BlazorClient.Internal.Services.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISshTerminalService
    {
        /// <summary>
        /// Проверка существующего соединения соединения ssh.
        /// </summary>
        /// <param name="sshSessionModel"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> CheckConnection(SshSessionModel sshSessionModel, CancellationToken token = default);

        /// <summary>
        /// Подключение к устройству по протоколу ssh
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<SshOperationResult> ConnectToSource(ConnectionsInfoDto model, CancellationToken token = default);

        /// <summary>
        /// Выполнение команды 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<SshOperationResult> ExecuteCommand(ExecuteCommandModel model, CancellationToken token = default);

        /// <summary>
        /// Отключение от устройства
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task DisconnectFromSource(SshSessionModel model, CancellationToken token = default);

        /// <summary>
        /// Получение списка активных сессий для текущего залогиненного пользователя
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IReadOnlyList<UserSessionDto>> GetSessions(CancellationToken token = default);
    }
}
