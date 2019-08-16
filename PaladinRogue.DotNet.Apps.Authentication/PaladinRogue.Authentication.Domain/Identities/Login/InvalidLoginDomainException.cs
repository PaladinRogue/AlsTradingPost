using System;
using PaladinRogue.Library.Core.Domain.Exceptions;

namespace PaladinRogue.Authentication.Domain.Identities.Login
{
    public class InvalidLoginDomainException : DomainException
    {
        public InvalidLoginDomainException()
        {
        }

        public InvalidLoginDomainException(string message) : base(message)
        {
        }

        public InvalidLoginDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}