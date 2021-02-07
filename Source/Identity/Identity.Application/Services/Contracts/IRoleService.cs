using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Dto;

namespace Identity.Application.Services.Contracts
{
    /// <summary>
    /// Сервис работы с ролями
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Получение всех ролей
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<IReadOnlyList<RoleDto>> GetAllAsync(CancellationToken token);

        /// <summary>
        /// Получение роли по ее идентификатору
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        Task<RoleDto> GetByIdAsync(int id, CancellationToken token);

        /// <summary>
        /// Создание роли
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<int> CreateAsync(RoleAddDto model, CancellationToken token);

        /// <summary>
        /// Обновление роли
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        Task UpdateAsync(RoleUpdateDto model, CancellationToken token);

        /// <summary>
        /// Удаление роли
        /// </summary>
        /// <param name="id">Идентификатор роли для удаления.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task DeleteAsync(int id, CancellationToken token);


        /// <summary>
        /// Получение всех привилегий.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<IReadOnlyList<PrivilegeDto>> GetAllPrivilegesAsync(CancellationToken token);

        /// <summary>
        /// Loads the privileges from file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task LoadPrivilegesFromFile(string filePath, CancellationToken token);
    }
}
