using System.Threading.Tasks;
using Library.Common.Authentication.Models;
using WebSsh.Application.Dto.Users;

namespace WebSsh.Application.Services.Contracts
{
    /// <summary>
    /// Интерфейс работы с аутентификацией пользователя.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Авторизация пользователя по логину и паролю.
        /// </summary>
        /// <returns></returns>
        Task<JsonWebToken> LoginByPassAsync(LoginByPassCredentials credentials);
    }
}
