using System;
using PaladinRogue.Library.Core.Domain.Exceptions;

namespace PaladinRogue.Authentication.Domain.Identities.RegisterPassword
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