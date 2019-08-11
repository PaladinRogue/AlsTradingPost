using Microsoft.Extensions.Logging;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Logging.Log4Net;

namespace PaladinRogue.Libray.Core.Setup.Infrastructure.Logging
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
