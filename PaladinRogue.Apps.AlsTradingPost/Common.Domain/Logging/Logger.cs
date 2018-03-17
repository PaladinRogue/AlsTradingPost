using System.Reflection;
using log4net;

namespace Common.Domain.Logging
{
    public abstract class Logger
    {
        protected static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    }
}
