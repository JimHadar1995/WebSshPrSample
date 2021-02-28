using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Library.Common.Exceptions;
using WebSsh.Shared.Dto.Users;

namespace WebSsh.Application.Services.Contracts
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
        Task<int> CreateAsync(UserAddDto model, CancellationToken token);

        /// <summary>
        /// Обновление информации о пользователе
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="token"></param>
        /// <returns>Асинхронная операция</returns>
        /// <exception cref="EntityNotFoundException"></exception>
        Task UpdateAsync(UserUpdateDto model, CancellationToken token);

        /// <summary>
        /// Изменение пароля пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя, для которого необходимо сменить пароль.</param>
        /// <param name="newPassword">Новый пароль</param>
        /// <param name="passwordForReset">Осуществляется сброс пароля. В таком случае нет проверок по истории паролей.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
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
        /// <exception cref="EntityNotFoundException"></exception>
        Task LockAsync(int userId, CancellationToken token);

        /// <summary>
        /// Разблокировка пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя для разблокировки.</param>
        /// <param name="token"></param>
        /// <returns>Асинхронная операция.</returns>
        /// <exception cref="EntityNotFoundException"></exception>
        Task UnLockAsync(int userId, CancellationToken token);

        /// <summary>
        /// Получение пользователя по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="token"></param>
        /// <returns>Пользователь.</returns>
        /// <exception cref="EntityNotFoundException"></exception>
        Task<UserDto> GetAsync(int id, CancellationToken token);

        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<UserDto>> GetAllAsync(CancellationToken token);

        /// <summary>
        /// Удаление пользователя по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя для удаления.</param>
        /// <param name="token"></param>
        /// <returns>Асинхронная операция.</returns>
        Task DeleteAsync(int id, CancellationToken token);

    }
}
