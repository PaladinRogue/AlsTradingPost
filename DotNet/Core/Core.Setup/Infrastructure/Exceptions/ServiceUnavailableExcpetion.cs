using System;

namespace PaladinRogue.Libray.Core.Setup.Infrastructure.Exceptions
{
    [Serializable]
    public class ServiceUnavailableExcpetion : Exception
    {
        public ServiceUnavailableExcpetion() : this(string.Empty)
        {
        }

        public ServiceUnavailableExcpetion(string message) : this(message, null)
        {
        }

        public ServiceUnavailableExcpetion(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
