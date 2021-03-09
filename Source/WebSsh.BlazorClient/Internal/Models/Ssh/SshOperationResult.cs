using System;
using WebSsh.Enums.Enums;

namespace WebSsh.BlazorClient.Internal.Models.Ssh
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    /// <summary>
    /// Результа выполнения операции
    /// </summary>
    public sealed record SshOperationResult
    {
        /// <summary>
        /// Успешность выполнения операции
        /// </summary>
        public bool Success { get; init; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        public OperationResultStatus Status { get; init; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; init; }

        /// <summary>
        /// Уникальный идентификатор сессии
        /// </summary>
        public Guid SessionKey { get; init; }
    }
}
