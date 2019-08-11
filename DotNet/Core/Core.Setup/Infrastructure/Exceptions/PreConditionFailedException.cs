using System;

namespace PaladinRogue.Libray.Core.Setup.Infrastructure.Exceptions
{
    public class PreConditionFailedException : Exception
    {
        public PreConditionFailedException() : this(string.Empty)
        {
        }

        public PreConditionFailedException(string message) : this(message, null)
        {
        }

        public PreConditionFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
