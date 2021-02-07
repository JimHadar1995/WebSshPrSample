using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Logging.Implementations;
using NLog;
using ILogger = Library.Logging.Contracts.ILogger;

namespace Library.Logging.Common
{
    /// <summary>
    /// Helper for logger
    /// </summary>
    public static class LoggingHelper
    {
        /// <summary>
        /// Initializes the logger.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="logBaseDir">The log base dir.</param>
        public static void InitLogger(string dbConnection, string logBaseDir = "")
        {
            if (!string.IsNullOrWhiteSpace(dbConnection))
            {
                GlobalDiagnosticsContext.Set("DefaultConnection", dbConnection);
            }

            if (!string.IsNullOrWhiteSpace(logBaseDir))
            {
                LogManager.Configuration.Variables["basedir"] = logBaseDir;
            }

            LogManager.AutoShutdown = true;
        }

        /// <summary>
        /// Shuts down this instance.
        /// </summary>
        public static void Shutdown()
        {
            if (LogManager.Configuration != null)
                LogManager.Shutdown();
        }

        /// <summary>
        /// Get default logger.
        /// </summary>
        public static ILogger Default => new LoggerSC("SC", null);

        /// <summary>
        /// Возвращает логгер, соответствуюий имени логгера <paramref name="loggerName"/>
        /// </summary>
        /// <param name="loggerName">Название логгера для возврата.</param>
        /// <returns></returns>
        public static ILogger GetLogger(string loggerName)
        {
            return new LoggerSC(loggerName, null);
        }

    }
}
