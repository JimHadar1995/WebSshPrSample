using Identity.Application.Dto.Users;
using MediatR;

namespace Identity.Application.Commands.Users
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
