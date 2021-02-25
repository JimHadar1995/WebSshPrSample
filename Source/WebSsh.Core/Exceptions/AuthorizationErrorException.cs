namespace WebSsh.Core.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AuthorizationErrorException : WebSshServiceException
    {
        public AuthorizationErrorException(string? message) : base(message)
        {
        }
    }
}
