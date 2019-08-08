using System;

namespace Authorisation.Application
{
    public class ResourceOwnerProviderNotDefinedException : Exception
    {
        public ResourceOwnerProviderNotDefinedException(string message) : base(message)
        {
        }
    }
}