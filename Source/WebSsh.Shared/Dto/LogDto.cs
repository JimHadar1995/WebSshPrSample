using System;

namespace WebSsh.Shared.Dto
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// Модель для логов
    /// </summary>
    public record LogDto
    {
        /// <summary>
        /// Идентификатор события.
        /// </summary>
        public long Id { get; init; }

        /// <summary>
        /// Дата события.
        /// </summary>
        public DateTime Date { get; init; }

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
        public string? Message { get; init; }

        /// <summary>
        /// Пользователь.
        /// </summary>
        public string Username { get; init; }

        /// <summary>
        /// Тип сущности, на которой произошло исключение.
        /// </summary>
        public string EntityType { get; init; }
    }
}
