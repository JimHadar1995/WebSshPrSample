using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using Renci.SshNet;
using WebSsh.Enums.Enums;
using WebSsh.Terminal.Hubs;
using WebSsh.Terminal.Models;
using Session = WebSsh.Terminal.Models.Session;

namespace WebSsh.Terminal.Common
{
    /// <summary>
    /// Активные сессии пользователя
    /// </summary>
    public sealed class UserSessions
    {
        private readonly IHubContext<SshTerminalHub> _hubContext;

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// Список сессий
        /// </summary>
        public ConcurrentDictionary<Guid, Session> Sessions { get; } = new ConcurrentDictionary<Guid, Session>();

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSessions"/> class.
        /// </summary>
        public UserSessions(string userId, IHubContext<SshTerminalHub> hubContext)
        {
            _hubContext = hubContext;
            UserId = userId;
        }

        /// <summary>
        /// Добавление сессии к пользователю и поппытка присоединения
        /// </summary>
        /// <param name="connectionInfo">Информация дл подключения к устройству.</param>
        /// <returns>Успешность выполнения операции</returns>
        public OperationResult TryAddSessionUserAndConnect(ClientConnectionInfo connectionInfo)
        {
            var sshClient = new SshClient(connectionInfo.Host, connectionInfo.Port, connectionInfo.UserName, connectionInfo.Password); ;
            ShellStream? shellStream = null;
            try
            {
                sshClient.ConnectionInfo.Timeout = TimeSpan.FromMinutes(10);
                sshClient.Connect();
                shellStream = sshClient.CreateShellStream("Terminal", 80, 30, 800, 400, 1000);

                var sessionModel = new Session
                {
                    Status = "Connected",
                    UniqueKey = connectionInfo.UniqueKey,
                    ShellStream = shellStream,
                    SshClient = sshClient,
                    StartSessionDate = DateTime.Now,
                    LastAccessSessionDate = DateTime.Now,
                    StoredSessionModel = connectionInfo,
                    OnLineReceive = OnLineReceived,
                    OnTerminalError = OnTerminalError
                };

                string? result;
                while ((result = sessionModel.ShellStream.ReadLine(TimeSpan.FromSeconds(0.3))) != null)
                {
                    sessionModel.OnLineReceive?.Invoke(
                        new OperationResult(
                            OperationResultStatus.NoError,
                            result + Environment.NewLine,
                            sessionModel.UniqueKey));
                }

                //sessionModel.Lines.TryAdd(sessionModel.ShellStream.Read());

                shellStream.DataReceived += (obj, e) =>
                {
                    try
                    {
                        //sessionModel.Lines.TryAdd(Encoding.UTF8.GetString(e.Data));
                        sessionModel.OnLineReceive?.Invoke(
                            new OperationResult(OperationResultStatus.NoError,
                                Encoding.UTF8.GetString(e.Data),
                                sessionModel.UniqueKey));
                    }
                    catch (Exception ex)
                    {
                        //sessionModel.Lines.TryAdd($"An error occurred while receiving data: {ex.Message}");
                        sessionModel.OnTerminalError?.Invoke(
                            new OperationResult(
                                OperationResultStatus.ReceiveDataError,
                                $"An error occurred while receiving data: {ex.Message}", sessionModel.UniqueKey));
                    }
                };

                AddActiveSession(sessionModel);

                sshClient.ErrorOccurred += (obj, e) =>
                {
                    if (obj is SshClient sshClientInner)
                    {
                        if (sshClientInner.IsConnected)
                        {
                            sessionModel.OnTerminalError?.Invoke(new OperationResult(
                                OperationResultStatus.SessionError, $"Session error: {e.Exception.Message}",
                                sessionModel.UniqueKey));
                        }
                    }

                    RemoveSession(sessionModel.UniqueKey);
                };

                var connectResult = new OperationResult(
                    OperationResultStatus.NoError, string.Empty, sessionModel.UniqueKey);
                return connectResult;
            }
            catch (Exception ex)
            {
                shellStream?.Dispose();
                sshClient?.Dispose();
                return new OperationResult(
                    OperationResultStatus.ConnectionError, ex.Message, Guid.Empty);
            }
        }

        /// <summary>
        /// Удаление сессии с указанным идентфиикатором
        /// </summary>
        /// <param name="uniqueKey">The unique key.</param>
        public void RemoveSession(Guid uniqueKey)
        {
            if (Sessions.TryRemove(uniqueKey, out var activeSession))
            {
                activeSession.OnTerminalError?.Invoke(new OperationResult(
                    OperationResultStatus.SessionClosed, $"Session closed", uniqueKey));
                activeSession.Dispose();
            }
        }

        /// <summary>
        /// Удаление неактивных сессий
        /// </summary>
        public void RemoveInActiveSessions()
        {
            var inActiveSessions = Sessions.Where(_ => _.Value.LastAccessSessionDate < DateTime.Now.AddMinutes(-10) ||
                                                       _.Value.SshClient != null && !_.Value.SshClient.IsConnected)
                .ToList();
            inActiveSessions.ForEach(s => RemoveSession(s.Key));
        }

        #region [ Help methods ]

        void AddActiveSession(Session session)
        {
            Sessions.TryAdd(session.UniqueKey, session);
        }

        private async void OnLineReceived(OperationResult operationResult)
        {
            try
            {
                await _hubContext.Clients.Group(UserId).SendAsync(SshTerminalHub.SendTerminalResult, operationResult);
            }
            catch
            {
                //
            }
        }

        private async void OnTerminalError(OperationResult operationResult)
        {
            try
            {
                await _hubContext.Clients.Group(UserId).SendAsync(SshTerminalHub.SendTerminalResult, operationResult);
            }
            catch
            {
                //
            }
        }

        #endregion
    }
}
