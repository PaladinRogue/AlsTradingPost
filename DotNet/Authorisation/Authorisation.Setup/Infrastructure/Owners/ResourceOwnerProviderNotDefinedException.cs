using System;

namespace PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Owners
{
    public class ResourceOwnerProviderNotDefinedException : Exception
    {
        public ResourceOwnerProviderNotDefinedException(string message) : base(message)
        {
        }
    }
}