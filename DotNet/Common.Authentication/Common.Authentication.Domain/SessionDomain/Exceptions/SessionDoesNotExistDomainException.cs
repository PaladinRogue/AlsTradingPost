using System;
using Common.Domain.Exceptions;

namespace Common.Authentication.Domain.SessionDomain.Exceptions
{
    [Serializable]
    public class SessionDoesNotExistDomainException : DomainException
    {
        public SessionDoesNotExistDomainException()
        {
        }
        
        public SessionDoesNotExistDomainException(string message) : base(message)
        {
        }

        public SessionDoesNotExistDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}