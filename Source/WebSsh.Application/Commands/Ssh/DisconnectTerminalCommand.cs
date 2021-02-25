using System;
using MediatR;

namespace WebSsh.Application.Commands.Ssh
{
    /// <summary>
    /// Команда отразрыва ssh соединения
    /// </summary>
    public sealed class DisconnectTerminalCommand : IRequest<Unit>
    {
        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        public readonly Guid SessionId;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisconnectTerminalCommand"/> class.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        public DisconnectTerminalCommand(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}
