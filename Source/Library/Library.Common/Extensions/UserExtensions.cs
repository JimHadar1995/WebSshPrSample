using Microsoft.AspNetCore.Http;

namespace Library.Common.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Users the identifier.
        /// </summary>
        /// <param name="accessor">The accessor.</param>
        /// <returns></returns>
        public static string? UserName(this IHttpContextAccessor accessor)
            => accessor.HttpContext?.User?.Identity?.Name;
    }
}
