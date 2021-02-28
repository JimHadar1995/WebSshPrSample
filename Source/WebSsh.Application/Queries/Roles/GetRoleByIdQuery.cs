using MediatR;
using WebSsh.Shared.Dto.Roles;

namespace WebSsh.Application.Queries.Roles
{
    /// <summary>
    /// Запрос получения списка ролей системы
    /// </summary>
    public sealed class GetRoleByIdQuery : IRequest<RoleDto>
    {
        /// <summary>
        /// The role identifier
        /// </summary>
        public readonly int RoleId;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRoleByIdQuery"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public GetRoleByIdQuery(int id) => RoleId = id;
    }
}
