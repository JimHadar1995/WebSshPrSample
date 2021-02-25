using System;
using Library.Common.Exceptions;

namespace WebSsh.Core.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class WebSshServiceException : BaseException
    {
        /// <inheritdoc />
        public WebSshServiceException(string? message) : base(message)
        {
        }

        /// <inheritdoc />
        public WebSshServiceException(string? message, Exception? innerException)
            : base(message, innerException)
        {

        }
    }
}
