using System;

namespace WebSsh.BlazorClient.Internal.Exceptions
{
    /// <summary>
    /// Базовое клиентское исключение
    /// </summary>
    public class BaseClientException : Exception
    {
        public BaseClientException()
        {

        }
        /// <inheritdoc/>
        public BaseClientException(string? message, Exception? innerException = null)
            : base(message, innerException)
        {
        }
    }
}
