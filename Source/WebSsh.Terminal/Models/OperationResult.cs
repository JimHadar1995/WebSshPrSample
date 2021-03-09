using System;
using WebSsh.Enums.Enums;

namespace WebSsh.Terminal.Models
{
    /// <summary>
    /// 
    /// </summary>
    public readonly struct OperationResult
    {
        public bool Success => Status == OperationResultStatus.NoError;
        /// <summary>
        /// Gets the status.
        /// </summary>
        public OperationResultStatus Status { get; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Уникальный идентификатор сессии
        /// </summary>
        public Guid SessionKey { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult"/> struct.
        /// </summary>
        public OperationResult(OperationResultStatus status, string message, Guid sessionKey)
        {
            Status = status;
            Message = message;
            SessionKey = sessionKey;
        }
    }
}
