using System.Text.Json;

namespace WebSsh.BlazorClient.Internal.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class JsonExtensions
    {
        public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string AsJson<T>(this T obj)
            => JsonSerializer.Serialize(obj, JsonSerializerOptions);        
    }
}
