using Identity.Application.Dto.Users;
using MediatR;

namespace Identity.Application.Commands.Users
{
    /// <summary>
    /// Команда обновления настроек парольных политик
    /// </summary>
    public sealed class UpdatePasswordPolicyCommand : IRequest<Unit>
    {
        /// <summary>
        /// Модель
        /// </summary>
        public readonly PasswordPolicyDto Model;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public UpdatePasswordPolicyCommand(PasswordPolicyDto model)
        {
            Model = model;
        }
    }
}
