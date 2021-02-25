using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Localization;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebSsh.Application.Commands.Ssh;
using WebSsh.ResourceManager.Constants;
using WebSsh.Terminal.Common;
using WebSsh.Terminal.Exceptions;

namespace WebSsh.Infrastructure.Handlers.Commands.Ssh
{
    /// <summary>
    /// Обработчик команды разрыва соединения с терминалом
    /// </summary>
    public sealed class DisconnectTerminalCommandHandler : IRequestHandler<DisconnectTerminalCommand, Unit>
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ServerShellPoll _sshPoll;
        private readonly IOwnSystemLocalizer<SshTerminalConstants> _localizer;

        /// <summary>
        /// 
        /// </summary>
        public DisconnectTerminalCommandHandler(
            IHttpContextAccessor accessor,
            ServerShellPoll sshPoll, 
            IOwnSystemLocalizer<SshTerminalConstants> localizer)
        {
            _accessor = accessor;
            _sshPoll = sshPoll;
            _localizer = localizer;
        }

        /// <inheritdoc />
        public Task<Unit> Handle(DisconnectTerminalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _sshPoll.Disconnect(_accessor.HttpContext?.User.Identity?.Name ?? string.Empty, request.SessionId);
            }
            catch (Exception ex)
            {
                throw new SshTerminalException(_localizer[SshTerminalConstants.DisconnectSourceError], ex);
            }

            return Task.FromResult(Unit.Value);
        }
    }
}
