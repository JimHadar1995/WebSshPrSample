using Identity.Application.Dto.Roles;
using MediatR;

namespace Identity.Application.Commands.Roles
{
    /// <summary>
    /// Команда создания роли.
    /// </summary>
    public sealed class CreateRoleCommand : IRequest<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly RoleAddDto Model;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public CreateRoleCommand(RoleAddDto model)
        {
            Model = model;
        }
    }
}
