using System.Threading.Tasks;
using WebSsh.BlazorClient.Internal.Models.Auth;
using WebSsh.Shared.Dto.Users;

namespace WebSsh.BlazorClient.Internal.Services.Contracts
{
    /// <summary>
    /// Сервис авторизации
    /// </summary>
    public interface IAuthService
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

        /// <summary>
        /// Возвращает true, если роль есть у пользователя
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task<bool> IsInRoles(string[] roles);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<AuthData?> GetAuthData();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<bool> IsAuthenticated();
    }
}
