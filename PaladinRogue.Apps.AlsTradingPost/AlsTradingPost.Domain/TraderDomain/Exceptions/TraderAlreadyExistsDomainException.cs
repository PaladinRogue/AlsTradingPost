using System;
using Common.Domain.Exceptions;

namespace AlsTradingPost.Domain.TraderDomain.Exceptions
{
    [Serializable]
    public class TraderAlreadyExistsDomainException : DomainException
    {
        public TraderAlreadyExistsDomainException()
        {
        }

        public TraderAlreadyExistsDomainException(string message) : base(message)
        {
        }

        public TraderAlreadyExistsDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
