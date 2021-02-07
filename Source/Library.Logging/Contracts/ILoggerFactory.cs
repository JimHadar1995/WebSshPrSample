using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logging.Contracts
{
    public interface ILoggerFactory
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="loggerName">Name of the logger.</param>
        /// <returns></returns>
        ILogger GetLogger(string loggerName);

        /// <summary>
        /// Логгер для Voltron
        /// </summary>
        /// <returns></returns>
        ILogger DefaultLogger() => GetLogger(ILogger.DefaultLoggerName);
    }
}
