using System.Threading;
using System.Threading.Tasks;
using WebSsh.Application.Dto.Users;

namespace WebSsh.Application.Services.Contracts
{
    /// <summary>
    /// Работа с настройками
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Настройки парольных политик.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        ValueTask<PasswordPolicyDto> GetPasswordPolicyAsync(CancellationToken token);

        /// <summary>
        /// Сохранение настроек парольных политик 
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task SavePasswordPolicyAsync(PasswordPolicyDto model, CancellationToken token);
    }
}
