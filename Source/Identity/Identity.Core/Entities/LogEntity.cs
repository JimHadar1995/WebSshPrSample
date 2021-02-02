using System;
using System.ComponentModel.DataAnnotations;
using Library.Common.Types.Attributes;

namespace Identity.Core.Entities
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// 
    /// </summary>
    public class LogEntity
    {
        /// <summary>
        /// Идентификатор события.
        /// </summary>
        [Key]
        public long Id { get; init; }

        /// <summary>
        /// Дата события.
        /// </summary>
        public DateTime Date { get; init; }

        /// <summary>
        /// Текст исключения
        /// </summary>
        [FullTextSearchProperty]
        public string? Exception { get; init; }

        /// <summary>
        /// Уровень события
        /// </summary>
        public string Level { get; init; }

        /// <summary>
        /// Тип логера.
        /// </summary>

        public string Logger { get; init; }

        /// <summary>
        /// Сообщение.
        /// </summary>
        [FullTextSearchProperty]
        public string? Message { get; init; }

        /// <summary>
        /// Трассировка исключения.
        /// </summary>
        public string? Stacktrace { get; init; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        [FullTextSearchProperty]
        public string? Username { get; init; }

        /// <summary>
        /// Тип сущности, на которой произошло исключение.
        /// </summary>
        public string? EntityType { get; init; }

        /// <summary>
        /// ThreadId
        /// </summary>
        public string? Thread { get; init; }
    }
}
