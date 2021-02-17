using System.Collections.Generic;
using Identity.Application.Dto.Roles;
using MediatR;

namespace Identity.Application.Queries.Roles
{
    /// <summary>
    /// Запрос получения списка привилегий
    /// </summary>
    public sealed class GetAllPrivilegesQuery : IRequest<IReadOnlyList<PrivilegeDto>>
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly GetAllPrivilegesQuery Instance = new GetAllPrivilegesQuery();
    }
}
