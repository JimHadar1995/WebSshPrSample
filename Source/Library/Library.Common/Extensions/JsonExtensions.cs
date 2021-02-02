using System;
using System.Text.Json;

namespace Library.Common.Extensions
{
    /// <summary>
    /// Json расширения 
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// Gets the  json serializer options.
        /// </summary>
        /// <value>
        /// The  json serializer options.
        /// </value>
        public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true
        };

        /// <summary>
        /// Сериализация объекта в формате snake_case
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string AsJson(this object? obj)
        {
            if (obj == null)
                return string.Empty;
            return JsonSerializer.Serialize(obj, JsonSerializerOptions);
        }

        /// <summary>
        /// Десериализация объекта в формате snake_case
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TResult FromJson<TResult>(this string? obj)
        {
            if (string.IsNullOrWhiteSpace(obj))
                return (TResult)(object)string.Empty;
            //костыль для строковых данных
            try
            {
                return JsonSerializer.Deserialize<TResult>(obj, JsonSerializerOptions)!;
            }
            catch (JsonException)
            {
                return (TResult)(object)obj;
            }
        }
    }
}
