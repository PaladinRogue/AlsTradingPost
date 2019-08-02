using System;
using Common.Domain.Exceptions;

namespace Authentication.Domain.Identities.RegisterClientCredential
{
    public class ClientCredentialIdentityExistsDomainException : DomainException
    {
        public ClientCredentialIdentityExistsDomainException()
        {
        }

        public ClientCredentialIdentityExistsDomainException(string message) : base(message)
        {
        }

        public ClientCredentialIdentityExistsDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}