using System.Collections.Generic;
using System.Threading.Tasks;
using WebSsh.BlazorClient.Internal.Models;

namespace WebSsh.BlazorClient.Internal.Services.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMenuService
    {
        /// <summary>
        /// Возвращает список пунктов меню для роли ткущего пользователя
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyList<MenuItem>> GetItems();
    }
}
