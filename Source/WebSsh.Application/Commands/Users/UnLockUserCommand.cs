using MediatR;

namespace WebSsh.Application.Commands.Users
{
    /// <summary>
    /// Команда разблокирвоки пользвоателя
    /// </summary>
    /// <seealso cref="IRequest{Unit}" />
    public sealed class UnLockUserCommand : IRequest<Unit>
    {
        /// <summary>
        /// The user identifier
        /// </summary>
        public readonly int UserId;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnLockUserCommand"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public UnLockUserCommand(int userId)
            => UserId = userId;
    }
}
