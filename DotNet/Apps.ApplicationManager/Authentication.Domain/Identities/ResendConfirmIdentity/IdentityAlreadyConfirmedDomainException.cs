using System;
using PaladinRogue.Libray.Core.Domain.Exceptions;

namespace PaladinRogue.Authentication.Domain.Identities.ResendConfirmIdentity
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