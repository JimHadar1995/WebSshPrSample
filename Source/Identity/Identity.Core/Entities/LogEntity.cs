using System;
using System.ComponentModel.DataAnnotations;
using Library.Common.Database;
using Library.Common.Types.Attributes;

namespace Identity.Core.Entities
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// 
    /// </summary>
    public class LogEntity : IAggregateRoot
    {
        /// <summary>
        /// Идентификатор события.
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Дата события.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Текст исключения
        /// </summary>
        [FullTextSearchProperty]
        public string? Exception { get; set; }

        /// <summary>
        /// Уровень события
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// Тип логера.
        /// </summary>

        public string Logger { get; set; }

        /// <summary>
        /// Сообщение.
        /// </summary>
        [FullTextSearchProperty]
        public string? Message { get; set; }

        /// <summary>
        /// Трассировка исключения.
        /// </summary>
        public string? Stacktrace { get; set; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        [FullTextSearchProperty]
        public string? Username { get; set; }

        /// <summary>
        /// Тип сущности, на которой произошло исключение.
        /// </summary>
        public string? EntityType { get; set; }

        /// <summary>
        /// ThreadId
        /// </summary>
        public string? Thread { get; set; }
    }
}
