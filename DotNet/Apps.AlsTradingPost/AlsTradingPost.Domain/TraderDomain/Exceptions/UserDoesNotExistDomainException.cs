using System;
using Common.Domain.Exceptions;

namespace AlsTradingPost.Domain.TraderDomain.Exceptions
{
    [Serializable]
    public class UserDoesNotExistDomainException : DomainException
    {
        public UserDoesNotExistDomainException()
        {
        }

        public UserDoesNotExistDomainException(string message) : base(message)
        {
        }

        public UserDoesNotExistDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
