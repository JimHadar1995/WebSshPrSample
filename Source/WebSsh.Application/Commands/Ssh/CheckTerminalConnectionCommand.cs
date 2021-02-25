using System;
using MediatR;

namespace WebSsh.Application.Commands.Ssh
{
    /// <summary>
    /// Команда проверки  соединения и наличии сессии с указанным идентификатором
    /// </summary>
    public sealed class CheckTerminalConnectionCommand : IRequest<bool>
    {
        /// <summary>
        /// The session identifier
        /// </summary>
        public readonly Guid SessionId;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckTerminalConnectionCommand"/> class.
        /// </summary>
        public CheckTerminalConnectionCommand(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}
