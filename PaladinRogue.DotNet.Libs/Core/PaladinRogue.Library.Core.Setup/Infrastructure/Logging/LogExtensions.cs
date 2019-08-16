using Microsoft.Extensions.Logging;
using PaladinRogue.Library.Core.Setup.Infrastructure.Logging.Log4Net;

namespace PaladinRogue.Library.Core.Setup.Infrastructure.Logging
{
    public static class LogExtensions
    {
        public static ILoggerFactory AddLogging(this ILoggerFactory factory)
        {
            factory.AddProvider(new Log4NetProvider("log4net.config"));
            return factory;
        }
    }
}
