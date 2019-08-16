using System;

namespace PaladinRogue.Library.Authorisation.Setup.Infrastructure.Owners
{
    public class ResourceOwnerProviderNotDefinedException : Exception
    {
        public ResourceOwnerProviderNotDefinedException(string message) : base(message)
        {
        }
    }
}