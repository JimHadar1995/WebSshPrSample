using System;

namespace WebSsh.BlazorClient.Internal.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiException : BaseClientException
    {
        public ApiException()
        {

        }
        public ApiException(string? message, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
