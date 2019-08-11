using System;

namespace PaladinRogue.Libray.Core.Domain.Exceptions
{
    [Serializable]
    public class DomainException : Exception {
        public DomainException()
        { }

        public DomainException(string message)
            : base(message) { }

        public DomainException(string message, Exception innerException)
            : base(message, innerException) { }

    }
}
