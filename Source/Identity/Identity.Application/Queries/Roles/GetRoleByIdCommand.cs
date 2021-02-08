using Identity.Application.Dto.Roles;
using MediatR;

namespace Identity.Application.Queries.Roles
{
    /// <summary>
    /// Запрос получения списка ролей системы
    /// </summary>
    public sealed class GetRoleByIdCommand : IRequest<RoleDto>
    {
        /// <summary>
        /// The role identifier
        /// </summary>
        public readonly int RoleId;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRoleByIdCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public GetRoleByIdCommand(int id) => RoleId = id;
    }
}
