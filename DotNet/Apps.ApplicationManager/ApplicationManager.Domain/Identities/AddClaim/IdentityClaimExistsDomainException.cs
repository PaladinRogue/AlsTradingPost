using System;
using Common.Domain.Exceptions;

namespace ApplicationManager.Domain.Identities.AddClaim
{
    public class IdentityClaimExistsDomainException : DomainException
    {
        public IdentityClaimExistsDomainException()
        {
        }

        public IdentityClaimExistsDomainException(string message) : base(message)
        {
        }

        public IdentityClaimExistsDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}