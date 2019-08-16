using System;

namespace PaladinRogue.Library.Core.Setup.Infrastructure.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public BadRequestException() : this(string.Empty)
        {
        }

        public BadRequestException(string message) : this(message, null)
        {
        }

        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
