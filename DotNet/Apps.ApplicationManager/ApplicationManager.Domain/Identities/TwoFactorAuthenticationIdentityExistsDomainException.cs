using System;
using Common.Domain.Exceptions;

namespace ApplicationManager.Domain.Identities
{
    public class TwoFactorAuthenticationIdentityExistsDomainException : DomainException
    {
        public TwoFactorAuthenticationIdentityExistsDomainException()
        {
        }

        public TwoFactorAuthenticationIdentityExistsDomainException(string message) : base(message)
        {
        }

        public TwoFactorAuthenticationIdentityExistsDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}