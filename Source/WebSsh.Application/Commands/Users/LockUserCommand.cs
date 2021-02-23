using MediatR;

namespace WebSsh.Application.Commands.Users
{
    /// <summary>
    /// Команда блокировки пользвоателя.
    /// </summary>
    /// <seealso cref="IRequest{Unit}" />
    public sealed class LockUserCommand : IRequest<Unit>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LockUserCommand"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public LockUserCommand(int userId)
            => UserId = userId;
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public readonly int UserId;
    }
}
