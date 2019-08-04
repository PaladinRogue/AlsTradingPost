using System;

namespace Common.Authorisation
{
    public class MultipleResourceOwnerProviderDefinedException : Exception
    {
        public MultipleResourceOwnerProviderDefinedException(string message) : base(message)
        {
        }
    }
}