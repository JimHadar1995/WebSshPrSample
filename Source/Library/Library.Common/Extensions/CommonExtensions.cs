using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Library.Common.Extensions
{
    public static class CommonExtensions
    {
        /// <summary>
        /// Underscores the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string Underscore(this string value)
            => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="section">The section.</param>
        /// <returns></returns>
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);
            
            return model;
        }

        /// <summary>
        /// Запуск ожидания, если время <paramref name="delay"/> превышает Int.MaxValue
        /// </summary>
        /// <param name="delay">Задержка</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static async Task LongDelay(TimeSpan delay, CancellationToken cancellationToken = default)
        {
            var start = DateTime.UtcNow;
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var remaining = (delay - (DateTime.UtcNow - start)).TotalMilliseconds;
                if (remaining <= 0)
                    break;
                if (remaining > Int16.MaxValue)
                    remaining = Int16.MaxValue;
                await Task.Delay(TimeSpan.FromMilliseconds(remaining), cancellationToken);
            }
        }
    }
}
