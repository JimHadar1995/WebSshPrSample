using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Library.Logging.Contracts;
using Microsoft.AspNetCore.Http;
using Npgsql;

namespace Library.Logging.Implementations
{
    /// <summary>
    /// Implementation of <see cref="ILogger"/>
    /// </summary>
    /// <seealso cref="ILogger" />
    public sealed class LoggerSC : ILogger
    {
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        /// <summary>
        /// NLog logger.
        /// </summary>
        public readonly NLog.Logger _log;

        /// <summary>
        /// The accessor
        /// </summary>
        private readonly IHttpContextAccessor? _accessor;
        public LoggerSC() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="NLogLogger"/> class.
        /// </summary>
        /// <param name="loggerName">Name of the logger.</param>
        /// <param name="accessor">The accessor.</param>
        public LoggerSC(string loggerName, IHttpContextAccessor? accessor)
        {
            _accessor = accessor;
            _log = NLog.LogManager.GetLogger(loggerName);
        }

        /// <inheritdoc />
        public void Debug(string? message)
        {
            Log(NLog.LogLevel.Debug, null, message);
        }

        /// <inheritdoc />
        public void Debug(string? format, params object[] args)
        {
            Log(NLog.LogLevel.Debug, null, format, args);
        }

        /// <inheritdoc />
        public void Debug(Exception exception, string? format, params object[] args)
        {
            Log(NLog.LogLevel.Debug, exception, format, args);
        }

        /// <inheritdoc />
        public void Info(string? message)
        {
            Log(NLog.LogLevel.Info, null, message);
        }

        /// <inheritdoc />
        public void Info(string? format, params object[] args)
        {
            Log(NLog.LogLevel.Info, null, format, args);
        }

        /// <inheritdoc />
        public void Info(Exception exception, string? format, params object[] args)
        {
            Log(NLog.LogLevel.Info, exception, format, args);
        }

        /// <inheritdoc />
        public void Trace(string? message)
        {
            Log(NLog.LogLevel.Trace, null, message);
        }

        /// <inheritdoc />
        public void Trace(string? format, params object[] args)
        {
            Log(NLog.LogLevel.Trace, null, format, args);
        }

        /// <inheritdoc />
        public void Trace(Exception exception, string? format, params object[] args)
        {
            Log(NLog.LogLevel.Trace, exception, format, args);
        }

        /// <inheritdoc />
        public void Warn(string? message)
        {
            Log(NLog.LogLevel.Warn, null, message);
        }

        /// <inheritdoc />
        public void Warn(string? format, params object[] args)
        {
            Log(NLog.LogLevel.Warn, null, format, args);
        }

        /// <inheritdoc />
        public void Warn(Exception exception, string? format, params object[] args)
        {
            Log(NLog.LogLevel.Warn, exception, format, args);
        }

        /// <inheritdoc />
        public void Error(string? message)
        {
            Log(NLog.LogLevel.Error, null, message);
        }

        /// <inheritdoc />
        public void Error(string? format, params object[] args)
        {
            Log(NLog.LogLevel.Error, null, format, args);
        }

        /// <inheritdoc />
        public void Error(Exception exception, string? format, params object[] args)
        {
            Log(NLog.LogLevel.Error, exception, format, args);
        }

        /// <inheritdoc />
        public void Fatal(string? message)
        {
            Log(NLog.LogLevel.Fatal, null, message);
        }

        /// <inheritdoc />
        public void Fatal(string format, params object[] args)
        {
            Log(NLog.LogLevel.Fatal, null, format, args);
        }

        /// <inheritdoc />
        public void Fatal(Exception exception, string? format, params object[] args)
        {
            Log(NLog.LogLevel.Fatal, exception, format, null, args);
        }

        #region [ Help methods ]

        private void Log(
            NLog.LogLevel level,
            Exception? exception,
            string? format,
            params object[] args)
        {
            try
            {
                var props = new AdditionalEventProperties(_accessor?.HttpContext?.User?.Identity?.Name ?? "System");
                _log.WithProperty("additional", props)
                    .Log(level, exception, format ?? string.Empty, args);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Logging argument exception error: {ex.Message}");
            }
            catch (PostgresException ex)
            {
                Console.WriteLine($"Logging database exception error: {ex.Message}");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Logging socket exception error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logging unhandled error: {ex.Message}");
            }
        }

        #endregion
    }
}
