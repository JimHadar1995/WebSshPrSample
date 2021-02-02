using System.Threading;
using System.Threading.Tasks;

namespace Library.Common.Database
{
    /// <summary>
    /// Модуль для работы с множеством репозиториев
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Репозиторий сущности
        /// </summary>
        /// <typeparam name="T">Тип сущности</typeparam>
        /// <returns>Набор базовых операций над сущностями</returns>
        IRepository<T> Repository<T>() 
            where T : class;

        /// <summary>
        /// Подтверждение в БД
        /// </summary>
        /// <returns></returns>
        Task<int> Commit(CancellationToken token = default);

        /// <summary>
        /// Начать транзакцию в БД.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Завершить транзакцию и применить изменения в БД.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Откатить транзакцию
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// Проверка наличия таблицы в БД
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        bool IsTableExists(string tableName);
    }
}
