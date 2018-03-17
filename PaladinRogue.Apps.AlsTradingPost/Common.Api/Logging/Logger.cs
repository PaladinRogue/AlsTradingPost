using System.Reflection;
using log4net;

namespace Common.Api.Logging
{
    public abstract class Logger
    {
        protected static readonly ILog Log = LogManager.GetLogger("Api", MethodBase.GetCurrentMethod().DeclaringType);
    }
}
