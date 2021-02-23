using System;
using Library.Common.Exceptions;

namespace WebSsh.Terminal.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Common.Exceptions.ScBaseException" />
    public sealed class SshTerminalException : BaseException
    {
        /// <inheritdoc />
        public SshTerminalException(string? message) : base(message)
        {
        }

        /// <inheritdoc />
        public SshTerminalException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
