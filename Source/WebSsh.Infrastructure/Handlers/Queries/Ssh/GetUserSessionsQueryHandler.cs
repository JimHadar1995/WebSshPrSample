using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Localization;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebSsh.ResourceManager.Constants;
using WebSsh.Shared.Dto.Ssh;
using WebSsh.Terminal.Common;
using WebSsh.Terminal.Exceptions;

namespace WebSsh.Application.Queries.Ssh
{
    /// <summary>
    /// Обработчик запроса получения всех сессий ssh 
    /// </summary>
    public sealed class GetUserSessionsQueryHandler : IRequestHandler<GetUserSessionsQuery, IReadOnlyList<UserSessionDto>>
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ServerShellPoll _sshPoll;
        private readonly IOwnSystemLocalizer<SshTerminalConstants> _localizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserSessionsQueryHandler"/> class.
        /// </summary>
        public GetUserSessionsQueryHandler(
            IHttpContextAccessor accessor,
            ServerShellPoll sshPoll, 
            IOwnSystemLocalizer<SshTerminalConstants> localizer)
        {
            _accessor = accessor;
            _sshPoll = sshPoll;
            _localizer = localizer;
        }
        /// <inheritdoc />
        public Task<IReadOnlyList<UserSessionDto>> Handle(GetUserSessionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _sshPoll.GetUserSessions(
                    _accessor.HttpContext?.User.Identity?.Name ?? string.Empty)
                    .Select(_ => new UserSessionDto()
                    {
                        UniqueKey = _.UniqueKey,
                        StartSessionDate = _.StartSessionDate,
                        LastAccessSessionDate = _.LastAccessSessionDate,
                        ConnectionInfo = new ConnectionsInfoDto
                        {
                            Host = _.StoredSessionModel.Host,
                            Port = _.StoredSessionModel.Port,
                            UserName = _.StoredSessionModel.UserName,
                        }
                    }).ToList().AsReadOnly();
                return Task.FromResult((IReadOnlyList<UserSessionDto>)result);
            }
            catch (Exception ex)
            {
                throw new SshTerminalException(_localizer[SshTerminalConstants.GettingUserSessionsError], ex);
            }
        }
    }
}
