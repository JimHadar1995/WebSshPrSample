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
using WebSsh.Terminal.Models;

namespace WebSsh.Infrastructure.Handlers.Commands.Ssh
{
    /// <summary>
    /// Обработчик команды подключения к устройству
    /// </summary>
    public sealed class ConnectToSourceCommandHandler : IRequestHandler<ConnectToSourceCommand, OperationResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ServerShellPoll _shellPoll;
        private readonly IOwnSystemLocalizer<SshTerminalConstants> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectToSourceCommandHandler"/> class.
        /// </summary>
        public ConnectToSourceCommandHandler(
            IHttpContextAccessor httpContextAccessor,
            ServerShellPoll shellPoll, 
            IOwnSystemLocalizer<SshTerminalConstants> localizer)
        {
            _httpContextAccessor = httpContextAccessor;
            _shellPoll = shellPoll;
            _localizer = localizer;
        }

        /// <inheritdoc />
        public Task<OperationResult> Handle(ConnectToSourceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var model = request.Model;
                var connectionInfo = new ClientConnectionInfo
                {
                    Host = model.Host,
                    Password = model.Password,
                    Port = model.Port,
                    UserName = model.UserName
                };
                return Task.FromResult(_shellPoll.AddShellToPool(_httpContextAccessor.HttpContext?.User.Identity?.Name ?? string.Empty, connectionInfo));
            }
            catch(Exception ex)
            {
                throw new SshTerminalException(_localizer[SshTerminalConstants.ConnectToSourceError], ex);
            }
        }
    }
}
