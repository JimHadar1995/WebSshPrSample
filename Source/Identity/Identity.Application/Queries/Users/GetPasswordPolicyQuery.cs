using Identity.Application.Dto.Users;
using MediatR;

namespace Identity.Application.Queries.Users
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
