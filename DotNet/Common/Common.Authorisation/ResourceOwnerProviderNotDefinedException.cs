using System;

namespace Common.Authorisation
{
    public class ResourceOwnerProviderNotDefinedException : Exception
    {
        public ResourceOwnerProviderNotDefinedException(string message) : base(message)
        {
        }
    }
}