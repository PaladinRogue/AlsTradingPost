using Common.Setup.Infrastructure.Logging.Log4Net;
using Microsoft.Extensions.Logging;

namespace Common.Setup.Infrastructure.Logging
{
    public static class LogExtensions
    {
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory)
        {
            factory.AddProvider(new Log4NetProvider("log4net.config"));
            return factory;
        }
    }
}
