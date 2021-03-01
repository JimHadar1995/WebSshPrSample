using System.Net.Http;
using System.Text;

namespace WebSsh.BlazorClient.Internal.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpContentExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static StringContent AsStringContent<T>(this T obj)
            => new StringContent(obj.AsJson(), Encoding.UTF8, "application/json");
    }
}
