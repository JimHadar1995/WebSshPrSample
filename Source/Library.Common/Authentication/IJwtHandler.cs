using Library.Common.Authentication.Models;

namespace Library.Common.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public interface IJwtHandler
    {
        /// <summary>
        /// Gets the token payload.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <returns></returns>
        JsonWebTokenPayload? GetTokenPayload(string accessToken);
    }
}
