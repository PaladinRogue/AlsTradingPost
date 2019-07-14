using System;
using Common.Domain.Exceptions;

namespace ApplicationManager.Domain.Identities.ChangeClaim
{
    public class IdentityClaimDoesNotExistDomainException : DomainException
    {
        public IdentityClaimDoesNotExistDomainException()
        {
        }

        public IdentityClaimDoesNotExistDomainException(string message) : base(message)
        {
        }

        public IdentityClaimDoesNotExistDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}