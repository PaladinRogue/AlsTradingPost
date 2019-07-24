using System;
using Common.Domain.Exceptions;

namespace Authentication.Domain.Identities.RegisterPassword
{
    public class PasswordIdentityExistsDomainException : DomainException
    {
        public PasswordIdentityExistsDomainException()
        {
        }

        public PasswordIdentityExistsDomainException(string message) : base(message)
        {
        }

        public PasswordIdentityExistsDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}