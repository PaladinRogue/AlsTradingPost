using System;
using Common.Domain.Exceptions;

namespace ApplicationManager.Domain.Identities
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