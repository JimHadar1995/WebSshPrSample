using MediatR;
using WebSsh.Shared.Dto.Users;

namespace WebSsh.Application.Commands.Users
{
    /// <summary>
    /// Команда создания пользователя
    /// </summary>
    /// <seealso cref="UserAddDto" />
    /// <seealso cref="IRequest{String}" />
    public sealed class CreateUserCommand : IRequest<int>
    {
        public readonly UserAddDto Model;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public CreateUserCommand(UserAddDto model)
        {
            Model = model;
        }
    }
}
