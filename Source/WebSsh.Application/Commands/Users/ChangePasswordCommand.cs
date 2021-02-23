using MediatR;
using WebSsh.Application.Dto.Users;

namespace WebSsh.Application.Commands.Users
{
    /// <summary>
    /// Команда изменения пароля пользователя
    /// </summary>
    public sealed class ChangePasswordCommand : IRequest<Unit>
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly ChangePasswordDto Model;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public ChangePasswordCommand(ChangePasswordDto model)
        {
            Model = model;
        }
    }
}
