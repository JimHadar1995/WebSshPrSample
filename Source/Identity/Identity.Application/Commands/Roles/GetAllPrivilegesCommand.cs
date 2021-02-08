using System.Collections.Generic;
using Identity.Application.Dto.Roles;
using MediatR;

namespace Identity.Application.Commands.Roles
{
    /// <summary>
    /// Команда получения списка привилегий пользователя.
    /// </summary>
    public sealed class GetAllPrivilegesCommand : IRequest<IReadOnlyList<PrivilegeDto>>
    {
    }
}
