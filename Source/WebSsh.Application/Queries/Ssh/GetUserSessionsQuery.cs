using System.Collections.Generic;
using MediatR;
using WebSsh.Application.Dto.Ssh;

namespace WebSsh.Application.Queries.Ssh
{
    /// <summary>
    /// Запрос получения списка сессий залогиненного пользователя.
    /// </summary>
    public sealed class GetUserSessionsQuery : IRequest<IReadOnlyList<UserSessionDto>>
    {
        /// <summary>
        /// The instance
        /// </summary>
        public static readonly GetUserSessionsQuery Instance = new();
    }
}
