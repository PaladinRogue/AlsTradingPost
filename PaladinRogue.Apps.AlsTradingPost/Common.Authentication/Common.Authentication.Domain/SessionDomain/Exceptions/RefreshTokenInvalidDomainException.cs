using System;
using Common.Domain.Exceptions;

namespace Common.Authentication.Domain.SessionDomain.Exceptions
{
    [Serializable]
    public class RefreshTokenInvalidDomainException : DomainException
    {
        public RefreshTokenInvalidDomainException()
        {
        }
        
        public RefreshTokenInvalidDomainException(string message) : base(message)
        {
        }

        public RefreshTokenInvalidDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}