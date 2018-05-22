using System;
using Common.Domain.Exceptions;

namespace Common.Authentication.Domain.SessionDomain.Exceptions
{
    [Serializable]
    public class SessionRevokedDomainException : DomainException
    {
        public SessionRevokedDomainException()
        {
        }
        
        public SessionRevokedDomainException(string message) : base(message)
        {
        }

        public SessionRevokedDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}