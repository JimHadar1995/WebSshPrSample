using Identity.Application.Dto.Users;
using MediatR;

namespace Identity.Application.Queries.Users
{
    /// <summary>
    /// Команда получения пользователя по его идентфиикатору
    /// </summary>
    public sealed class GetUserByIdQuery : IRequest<UserDto>
    {
        /// <summary>
        /// The user identifier
        /// </summary>
        public readonly int UserId;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByIdQuery"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public GetUserByIdQuery(int userId)
            => UserId = userId;
    }
}
