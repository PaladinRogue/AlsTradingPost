using System;
using Common.Domain.Exceptions;

namespace ApplicationManager.Domain.Identities.ResendConfirmIdentity
{
    public class IdentityAlreadyConfirmedDomainException : DomainException
    {
        public IdentityAlreadyConfirmedDomainException()
        {
        }

        public IdentityAlreadyConfirmedDomainException(string message) : base(message)
        {
        }

        public IdentityAlreadyConfirmedDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}