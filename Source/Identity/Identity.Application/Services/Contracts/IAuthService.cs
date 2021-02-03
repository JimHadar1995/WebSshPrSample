using System.Threading.Tasks;
using Identity.Application.Dto;
using Library.Common.Authentication.Models;

namespace Identity.Application.Services.Contracts
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

        /// <summary>
        /// Creates the access token asynchronous.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<JsonWebToken> LoginByRefreshToken(string token);
    }
}
