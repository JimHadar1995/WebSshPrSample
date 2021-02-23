using MediatR;
using WebSsh.Application.Dto.Users;

namespace WebSsh.Application.Queries.Users
{
    /// <summary>
    /// Запрос получения настроек парольной политики
    /// </summary>
    public sealed class GetPasswordPolicyQuery : IRequest<PasswordPolicyDto>
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly GetPasswordPolicyQuery Instance = new GetPasswordPolicyQuery();
    }
}
