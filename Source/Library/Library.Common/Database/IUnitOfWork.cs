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
            where T : class, IAggregateRoot;

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
    }
}
