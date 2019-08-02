using System;
using Common.Domain.Exceptions;

namespace Authentication.Domain.Identities.Login
{
    public class InvalidLoginDomainException : DomainException
    {
        public InvalidLoginDomainException()
        {
        }

        public InvalidLoginDomainException(string message) : base(message)
        {
        }

        public InvalidLoginDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}