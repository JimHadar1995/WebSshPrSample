using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebSsh.Terminal.Exceptions;
using WebSsh.Terminal.Hubs;
using WebSsh.Terminal.Models;

namespace WebSsh.Terminal.Common
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ServerShellPoll
    {
        private readonly IHubContext<SshTerminalHub> _hubContext;
        private readonly ConcurrentDictionary<string, UserSessions> _shellPoolDictionary = new ConcurrentDictionary<string, UserSessions>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerShellPoll"/> class.
        /// </summary>
        public ServerShellPoll(IHubContext<SshTerminalHub> hubContext)
        {
            _hubContext = hubContext;
        }

        /// <summary>
        /// Старт выполнения сервиса
        /// </summary>
        /// <param name="stoppingToken">The stopping token.</param>
        /// <returns></returns>
        public void Start(CancellationToken stoppingToken)
        {
            Task.Factory.StartNew(async() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        foreach (var shell in _shellPoolDictionary.ToArray())
                        {
                            shell.Value.RemoveInActiveSessions();

                            if (shell.Value.Sessions.Count == 0)
                            {
                                _shellPoolDictionary.TryRemove(shell.Key, out _);
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {

                    }
                    catch
                    {
                        //
                    }

                    await Task.Delay(TimeSpan.FromMinutes(3), stoppingToken);
                }
            }, stoppingToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        /// <summary>
        /// Добавление сессии и соединения к указанному пользователю
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="connectionInfo">The connection information.</param>
        public OperationResult AddShellToPool(string userId, ClientConnectionInfo connectionInfo)
        {
            if (!_shellPoolDictionary.TryGetValue(userId, out var sessionsModel))
            {
                sessionsModel = new UserSessions(userId, _hubContext);
                var connectResult = sessionsModel.TryAddSessionUserAndConnect(connectionInfo);
                if (connectResult.Success)
                {
                    _shellPoolDictionary.TryAdd(userId, sessionsModel);
                }

                return connectResult;
            }

            return sessionsModel.TryAddSessionUserAndConnect(connectionInfo);
        }

        /// <summary>
        /// Запуск команды для выполнения
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="uniqueId">Уникальный идентификатор сессии.</param>
        /// <param name="command">команда для выполнения.</param>
        /// <exception cref="SshTerminalException">No available shell connected</exception>
        public OperationResult RunShellCommand(string userId, Guid uniqueId, string command)
        {
            if (_shellPoolDictionary.TryGetValue(userId, out var serverActiveSessionsModel) && serverActiveSessionsModel.Sessions.TryGetValue(uniqueId, out var serverActiveSessionModel))
            {
                serverActiveSessionModel.ShellStream?.WriteLine(command);
                return new OperationResult(OperationResultStatus.NoError, string.Empty, serverActiveSessionModel.UniqueKey);
            }

            return new OperationResult(OperationResultStatus.ConnectionError, "No available shell connected", uniqueId);
        }

        /// <summary>
        /// Закрытие ssh сессии
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="uniqueId">уникальный идентификатор сессии, которую необходимо закрыть.</param>
        public bool Disconnect(string userId, Guid uniqueId)
        {
            if (_shellPoolDictionary.TryGetValue(userId, out var serverActiveSessionsModel))
            {
                serverActiveSessionsModel.RemoveSession(uniqueId);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Проверка соединения
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="uniqueId">The unique identifier.</param>
        public bool IsConnected(string userId, Guid uniqueId)
        {
            if (_shellPoolDictionary.TryGetValue(userId, out var serverActiveSessionsModel) && serverActiveSessionsModel.Sessions.TryGetValue(uniqueId, out var serverActiveSessionModel))
            {
                return serverActiveSessionModel.SshClient != null &&
                       serverActiveSessionModel.SshClient.IsConnected;
            }

            return false;
        }

        /// <summary>
        /// Получение списка открытых сессий конкретного пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, для которого необходимо получить список сессий.</param>
        /// <returns></returns>
        public IReadOnlyList<Session> GetUserSessions(string userId)
        {
            if (_shellPoolDictionary.TryGetValue(userId, out var sessions))
            {
                return sessions.Sessions.Select(_ => _.Value).ToList();
            }

            return new List<Session>();
        }
    }
}
