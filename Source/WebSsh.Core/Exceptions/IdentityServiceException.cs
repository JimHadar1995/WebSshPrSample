using System;
using Library.Common.Exceptions;

namespace WebSsh.Core.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class IdentityServiceException : BaseException
    {
        /// <inheritdoc />
        public IdentityServiceException(string? message) : base(message)
        {
        }

        /// <inheritdoc />
        public IdentityServiceException(string? message, Exception? innerException)
            : base(message, innerException)
        {

        }
    }
}
