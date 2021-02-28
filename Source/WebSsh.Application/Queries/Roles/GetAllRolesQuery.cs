using System.Collections.Generic;
using MediatR;
using WebSsh.Shared.Dto.Roles;

namespace WebSsh.Application.Queries.Roles
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
