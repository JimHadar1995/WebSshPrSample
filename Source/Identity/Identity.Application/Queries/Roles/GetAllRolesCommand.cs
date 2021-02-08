using System.Collections.Generic;
using Identity.Application.Dto.Roles;
using MediatR;

namespace Identity.Application.Queries.Roles
{
    /// <summary>
    /// Получение списка всех системы
    /// </summary>
    public sealed class GetAllRolesCommand : IRequest<IReadOnlyList<RoleDto>>
    {
    }
}
