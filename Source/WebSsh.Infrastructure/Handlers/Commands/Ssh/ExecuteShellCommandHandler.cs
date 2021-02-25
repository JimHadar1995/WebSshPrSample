using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Exceptions;
using Library.Common.Localization;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebSsh.Application.Commands.Ssh;
using WebSsh.ResourceManager.Constants;
using WebSsh.Terminal.Common;
using WebSsh.Terminal.Exceptions;
using WebSsh.Terminal.Models;

namespace Voltron.Business.Handlers.CommandHandlers.Ssh
{
    /// <summary>
    /// Обработчик команды выполненя команды на устройстве
    /// </summary>
    public sealed class ExecuteShellCommandHandler : IRequestHandler<ExecuteShellCommand, OperationResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ServerShellPoll _sshPoll;
        private readonly IOwnSystemLocalizer<SshTerminalConstants> _localizer;

        public ExecuteShellCommandHandler(
            IHttpContextAccessor httpContextAccessor,
            ServerShellPoll sshPoll, 
            IOwnSystemLocalizer<SshTerminalConstants> localizer)
        {
            _httpContextAccessor = httpContextAccessor;
            _sshPoll = sshPoll;
            _localizer = localizer;
        }

        /// <inheritdoc />
        public Task<OperationResult> Handle(ExecuteShellCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_sshPoll.RunShellCommand(
                    _httpContextAccessor.HttpContext?.User.Identity?.Name ?? string.Empty,
                    request.Model.UniqueSessionId,
                    request.Model.Command));
            }
            catch (SshTerminalException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SshTerminalException(_localizer[SshTerminalConstants.ExecuteCommandError], ex);
            }
        }
    }
}
