using MediatR;

namespace Identity.Application.Commands.Roles
{
    /// <summary>
    /// Команда удаления роли по идентификатору.
    /// </summary>
    public sealed class DeleteRoleCommand : IRequest<Unit>
    {
        /// <summary>
        /// The role identifier
        /// </summary>
        public readonly int RoleId;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRoleCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public DeleteRoleCommand(int id) => RoleId = id;
    }
}
