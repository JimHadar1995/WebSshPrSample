using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Identity.Application.Dto.Roles;

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
    }
}
