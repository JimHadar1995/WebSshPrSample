using System.Threading.Tasks;
using WebSsh.Shared.Dto.Users;

namespace WebSsh.BlazorClient.Internal.Services.Contracts
{
    /// <summary>
    /// Сервис авторизации
    /// </summary>
    internal interface IAuthService
    {
        /// <summary>
        /// Аутентификация пользователя на основе логина и пароля
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        Task<bool> LoginByPass(LoginByPassCredentials credentials);

        /// <summary>
        /// Выход из системы
        /// </summary>
        /// <returns></returns>
        Task Logout();
    }
}
