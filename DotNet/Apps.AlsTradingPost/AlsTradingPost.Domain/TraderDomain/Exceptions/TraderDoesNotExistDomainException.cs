using System;
using Common.Domain.Exceptions;

namespace AlsTradingPost.Domain.TraderDomain.Exceptions
{
    [Serializable]
    public class TraderDoesNotExistDomainException : DomainException
    {
        public TraderDoesNotExistDomainException()
        {
        }

        public TraderDoesNotExistDomainException(string message) : base(message)
        {
        }

        public TraderDoesNotExistDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
