using System.Threading.Tasks;

namespace WebSsh.BlazorClient.Internal.Services.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILocalStorageService
    {
        /// <summary>
        /// Получение объекта из storage по ключу
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="key">Ключ</param>
        /// <returns></returns>
        Task<T?> GetItem<T>(string key);
        
        /// <summary>
        /// Запись в storage данных
        /// </summary>
        /// <typeparam name="T">Тип оъекта</typeparam>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        /// <returns></returns>
        Task SetItem<T>(string key, T value);

        /// <summary>
        /// Удаление объекта по ключу
        /// </summary>
        /// <param name="key">Ключ объекта для удаления</param>
        /// <returns></returns>
        Task RemoveItem(string key);
    }
}
