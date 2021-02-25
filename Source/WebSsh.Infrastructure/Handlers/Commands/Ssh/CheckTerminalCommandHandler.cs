using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Localization;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebSsh.ResourceManager.Constants;
using WebSsh.Terminal.Common;
using WebSsh.Terminal.Exceptions;

namespace WebSsh.Application.Commands.Ssh
{
    /// <summary>
    /// Обработчик команды проверки соединения с терминалом ssh.
    /// </summary>
    public sealed class CheckTerminalCommandHandler : IRequestHandler<CheckTerminalConnectionCommand, bool>
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ServerShellPoll _sshPoll;
        private readonly IOwnSystemLocalizer<SshTerminalConstants> _localizer;

        public CheckTerminalCommandHandler(
            IHttpContextAccessor accessor,
            ServerShellPoll sshPoll,
            IOwnSystemLocalizer<SshTerminalConstants> localizer)
        {
            _accessor = accessor;
            _sshPoll = sshPoll;
            _localizer = localizer;
        }

        /// <inheritdoc />
        public Task<bool> Handle(CheckTerminalConnectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(
                    _sshPoll.IsConnected(_accessor.HttpContext?.User.Identity?.Name ?? string.Empty, request.SessionId));
            }
            catch(Exception ex)
            {
                throw new SshTerminalException(_localizer[SshTerminalConstants.CheckTerminalError], ex);
            }
        }
    }
}
