using Identity.Application.Dto.Users;
using MediatR;

namespace Identity.Application.Commands.Users
{
    /// <summary>
    /// Команда создания пользователя
    /// </summary>
    /// <seealso cref="UserAddDto" />
    /// <seealso cref="IRequest{String}" />
    public sealed class CreateUserCommand : IRequest<string>
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
