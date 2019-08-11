using System;
using System.Reflection;
using System.Xml;
using log4net;
using log4net.Repository;
using Microsoft.Extensions.Logging;

namespace PaladinRogue.Libray.Core.Setup.Infrastructure.Logging.Log4Net
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _log;

        public Log4NetLogger(string name, XmlElement xmlElement)
        {
            ILoggerRepository loggerRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            _log = LogManager.GetLogger(loggerRepository.Name, name);
            log4net.Config.XmlConfigurator.Configure(loggerRepository, xmlElement);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    return _log.IsFatalEnabled;
                case LogLevel.Debug:
                case LogLevel.Trace:
                    return _log.IsDebugEnabled;
                case LogLevel.Error:
                    return _log.IsErrorEnabled;
                case LogLevel.Information:
                    return _log.IsInfoEnabled;
                case LogLevel.Warning:
                    return _log.IsWarnEnabled;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            string message =  formatter(state, exception);
            if (string.IsNullOrEmpty(message) && exception == null) return;

            switch (logLevel)
            {
                case LogLevel.Critical:
                    _log.Fatal(message, exception);
                    break;
                case LogLevel.Debug:
                case LogLevel.Trace:
                    _log.Debug(message, exception);
                    break;
                case LogLevel.Error:
                    _log.Error(message, exception);
                    break;
                case LogLevel.Information:
                    _log.Info(message, exception);
                    break;
                case LogLevel.Warning:
                    _log.Warn(message, exception);
                    break;
                default:
                    _log.Warn($"Encountered unknown log level {logLevel}, writing out as Info.");
                    _log.Info(message, exception);
                    break;
            }
        }
    }
}
