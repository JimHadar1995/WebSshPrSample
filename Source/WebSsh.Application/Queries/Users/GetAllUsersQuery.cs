using System.Collections.Generic;
using MediatR;
using WebSsh.Shared.Dto.Users;

namespace WebSsh.Application.Queries.Users
{
    /// <summary>
    /// Запрос получения списка всех пользователей.
    /// </summary>
    public sealed class GetAllUsersQuery : IRequest<IReadOnlyList<UserDto>>
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly GetAllUsersQuery Instance = new GetAllUsersQuery();
    }
}
