using Microsoft.Extensions.Logging;

namespace Common.Resources.Logging
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
