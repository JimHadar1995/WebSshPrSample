using MediatR;

namespace WebSsh.Application.Commands.Users
{
    /// <summary>
    /// Команда удаления пользователя
    /// </summary>
    /// <seealso cref="IRequest{Unit}" />
    public sealed class DeleteUserCommand : IRequest<Unit>
    {
        /// <summary>
        /// The user identifier
        /// </summary>
        public readonly int UserId;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserCommand"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public DeleteUserCommand(int userId)
            => UserId = userId;
    }
}
