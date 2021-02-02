#nullable disable warnings
namespace Library.Common.Authentication.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonWebToken : JsonWebTokenPayload
    {
        /// <summary>
        /// Токен доступа к ресурсам
        /// </summary>
        public string AccessToken { get; init; }
    }
}
