using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library.Common.Kafka
{
    /// <summary>
    /// Сервис для работы с Kafka
    /// </summary>
    public interface IKafkaService
    {
        /// <summary>
        /// Отправка данных в Kafka
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="topicName">Название топика.</param>
        /// <param name="key">Ключ.</param>
        /// <param name="data">Данные.</param>
        /// <returns></returns>
        Task SendInfo<T>(
            string topicName,
            string key,
            List<T> data);

        /// <summary>
        /// Отправка данных в Kafka
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="topicName">Название топика.</param>
        /// <param name="keyExpr">Выражение, позволяющее определить, значение ключа.</param>
        /// <param name="data">Данные.</param>
        /// <returns></returns>
        Task SendInfo<T>(
            string topicName,
            Expression<Func<T, string>> keyExpr,
            T data);

        /// <summary>
        /// Отправка данных в Kafka
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="topicName">Название топика.</param>
        /// <param name="key">Ключ.</param>
        /// <param name="data">Данные.</param>
        /// <returns></returns>
        Task SendInfo<T>(
            string topicName,
            string key,
            T data);
    }
}
