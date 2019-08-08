using System;

namespace Authorisation.Application
{
    public class MultipleResourceOwnerProviderDefinedException : Exception
    {
        public MultipleResourceOwnerProviderDefinedException(string message) : base(message)
        {
        }
    }
}