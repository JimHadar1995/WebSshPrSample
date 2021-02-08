using Identity.Application.Dto.Users;
using MediatR;

namespace Identity.Application.Commands.Users
{
    /// <summary>
    /// Команда обновления пользователя
    /// </summary>
    /// <seealso cref="UserUpdateDto" />
    /// <seealso cref="IRequest{Unit}" />
    public sealed class UpdateUserCommand : IRequest<Unit>
    {
        /// <summary>
        /// Модель
        /// </summary>
        public readonly UserUpdateDto Model;

        public UpdateUserCommand(UserUpdateDto model)
        {
            Model = model;
        }
    }
}
