using System;
using System.Runtime.Serialization;

namespace Library.Common.Exceptions
{
    /// <summary>
    /// Общее исключение для всех компонентов
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class BaseException : Exception
    {
        /// <inheritdoc />
        public BaseException(string? message)
            : base(message)
        {
            
        }

        /// <inheritdoc />
        public BaseException(string? message, Exception? innerException)
            : base(message, innerException)
        {
            
        }

        /// <inheritdoc />
        protected BaseException(SerializationInfo info, StreamingContext context)
            :base(info, context)
        {
            
        }
    }
}
