using Identity.Application.Dto.Roles;
using MediatR;

namespace Identity.Application.Commands.Roles
{
    /// <summary>
    /// Команда обновления роли
    /// </summary>
    public sealed class UpdateRoleCommand : IRequest<Unit>
    {
        public readonly RoleUpdateDto Model;

        public UpdateRoleCommand(RoleUpdateDto model)
        {
            Model = model;
        }
    }
}
