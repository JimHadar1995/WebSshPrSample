using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebSsh.BlazorClient.Internal.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class JsonExtensions
    {
        static JsonExtensions()
        {
            JsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public static readonly JsonSerializerOptions JsonSerializerOptions;

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
