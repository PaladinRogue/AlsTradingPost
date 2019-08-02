using System;
using Common.Domain.Exceptions;

namespace Authentication.Domain.Identities.ValidateToken
{
    public class InvalidTwoFactorTokenDomainException : DomainException
    {
        public InvalidTwoFactorTokenDomainException()
        {
        }

        public InvalidTwoFactorTokenDomainException(string message) : base(message)
        {
        }

        public InvalidTwoFactorTokenDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}