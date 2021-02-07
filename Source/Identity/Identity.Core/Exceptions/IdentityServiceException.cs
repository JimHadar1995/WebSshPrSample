using Library.Common.Exceptions;

namespace Identity.Core.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class IdentityServiceException : BaseException
    {
        public IdentityServiceException(string? message) : base(message)
        {
        }
    }
}
