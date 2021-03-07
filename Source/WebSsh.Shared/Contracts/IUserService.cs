using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebSsh.Shared.Dto.Users;

namespace WebSsh.Shared.Contracts
{
    /// <summary>
    /// Сервис работы с пользователями
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Создание пользователя.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<int> CreateAsync(UserAddDto model, CancellationToken token = default);

        /// <summary>
        /// Обновление информации о пользователе
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="token"></param>
        /// <returns>Асинхронная операция</returns>
        Task UpdateAsync(UserUpdateDto model, CancellationToken token = default);

        /// <summary>
        /// Изменение пароля пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя, для которого необходимо сменить пароль.</param>
        /// <param name="newPassword">Новый пароль</param>
        /// <param name="passwordForReset">Осуществляется сброс пароля. В таком случае нет проверок по истории паролей.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task ChangePasswordAsync(
            string userName,
            string newPassword,
            bool passwordForReset = false,
            CancellationToken token = default);

        /// <summary>
        /// Блокировка пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя для блокировки.</param>
        /// <param name="token"></param>
        /// <returns>Асинхронная операция.</returns>
        Task LockAsync(int userId, CancellationToken token = default);

        /// <summary>
        /// Разблокировка пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя для разблокировки.</param>
        /// <param name="token"></param>
        /// <returns>Асинхронная операция.</returns>
        Task UnLockAsync(int userId, CancellationToken token = default);

        /// <summary>
        /// Получение пользователя по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="token"></param>
        /// <returns>Пользователь.</returns>
        Task<UserDto> GetAsync(int id, CancellationToken token = default);

        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<UserDto>> GetAllAsync(CancellationToken token = default);

        /// <summary>
        /// Удаление пользователя по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя для удаления.</param>
        /// <param name="token"></param>
        /// <returns>Асинхронная операция.</returns>
        Task DeleteAsync(int id, CancellationToken token = default);

    }
}
