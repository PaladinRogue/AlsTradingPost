using System.Reflection;
using log4net;

namespace Common.Application.Logging
{
    public abstract class Logger
    {
        protected static readonly ILog Log = LogManager.GetLogger("Application", MethodBase.GetCurrentMethod().DeclaringType);
    }
}
