using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSsh.BlazorClient.Internal.Exceptions
{
    public class BadRequestApiException : ApiException
    {
        public BadRequestApiException()
        {

        }
        public BadRequestApiException(string? message, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
