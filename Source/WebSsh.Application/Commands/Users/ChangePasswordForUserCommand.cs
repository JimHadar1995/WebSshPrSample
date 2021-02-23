using MediatR;
using WebSsh.Application.Dto.Users;

namespace WebSsh.Application.Commands.Users
{
    /// <summary>
    /// Команда сброса пароля для указанного пользователя.
    /// </summary>
    public sealed class ChangePasswordForUserCommand : IRequest<Unit>
    {
        public readonly ChangePasswordForUserDto Model;

        public ChangePasswordForUserCommand(ChangePasswordForUserDto model)
        {
            Model = model;
        }
    }
}
