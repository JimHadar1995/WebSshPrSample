using Library.Logging.Contracts;
using Microsoft.AspNetCore.Http;

namespace Library.Logging.Implementations
{
    /// <inheritdoc />
    public sealed class LoggerFactorySC : ILoggerFactory
    {
        private readonly IHttpContextAccessor _accessor;
        /// <summary>
        /// Initializes a new instance of the <see cref="NlogLoggerFactory"/> class.
        /// </summary>
        /// <param name="accessor">The accessor.</param>
        public LoggerFactorySC(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        /// <inheritdoc />
        public ILogger GetLogger(string loggerName)
            => new LoggerSC(loggerName, _accessor);
    }
}
