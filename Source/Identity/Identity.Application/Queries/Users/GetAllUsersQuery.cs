using System.Collections.Generic;
using Identity.Application.Dto.Users;
using MediatR;

namespace Identity.Application.Queries.Users
{
    /// <summary>
    /// Запрос получения списка всех пользователей.
    /// </summary>
    public sealed class GetAllUsersQuery : IRequest<IReadOnlyList<UserDto>>
    {

    }
}
