namespace Identity.Core.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AuthorizationErrorException : IdentityServiceException
    {
        public AuthorizationErrorException(string? message) : base(message)
        {
        }
    }
}
