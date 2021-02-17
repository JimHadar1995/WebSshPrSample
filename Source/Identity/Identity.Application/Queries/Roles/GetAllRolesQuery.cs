using System.Collections.Generic;
using Identity.Application.Dto.Roles;
using MediatR;

namespace Identity.Application.Queries.Roles
{
    /// <summary>
    /// Получение списка всех системы
    /// </summary>
    public sealed class GetAllRolesQuery : IRequest<IReadOnlyList<RoleDto>>
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly GetAllRolesQuery Instance = new GetAllRolesQuery();
    }
}
