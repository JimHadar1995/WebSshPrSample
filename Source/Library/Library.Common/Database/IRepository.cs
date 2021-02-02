using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using Library.Common.Exceptions;

namespace Library.Common.Database
{
    /// <summary>
    /// Generic репозиторий для работы с сущностями БД
    /// </summary>
    /// <typeparam name="T">Тип сущности БД</typeparam>
    public interface IRepository<T>
        where T : class, IAggregateRoot
    {

        /// <summary>
        /// Возвращает список сущностей, удовлетворяющих условиям поиска
        /// </summary>
        /// <param name="match">Условие поиска сущностей.</param>
        /// <param name="token"></param>
        /// <returns>Список найденных сущностей</returns>
        Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec, CancellationToken token = default);

        /// <summary>
        /// Возвращает список всех сущностей Типа
        /// </summary>
        /// 
        /// <returns>Список сущностей</returns>
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken token = default);

        /// <summary>
        /// Возвращает  сущность c Id или Null, если ничего не найдено
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        T FindByIdAsync(int id, CancellationToken token = default);

        /// <summary>
        /// Создание сущности с вызовом SaveChangesAsync
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<T> CreateAsync(T entity, CancellationToken token = default);

        /// <summary>
        /// Обновление сущности с вызовом SaveChangesAsync
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity, CancellationToken token = default);

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="token"></param>
        /// <returns>Количество удаленных сущностей</returns>
        Task<int> DeleteAsync(T entity, CancellationToken token = default);

        /// <summary>
        /// Удаление сущностей, удовлетворяющих условиям <paramref name="predicate"></paramref>
        /// </summary>
        /// <param name="predicate">Условие поиска.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken token = default);

        /// <summary>
        /// Получение количества записей на основе спецификации
        /// </summary>
        /// <param name="spec">Спецификация</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получение первого элемента БД, удовлетворяющего условию спецификации. Если элемента не найдено, то выдается исключение <seealso cref="EntityNotFoundException"/>
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        Task<T> FirstAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получение первого элемента или null, удовлетворяющего условию спецификации
        /// </summary>
        /// <param name="spec"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(ISpecification<T> spec, CancellationToken cancellationToken = default);

    }
}
