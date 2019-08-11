using System;

namespace PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Owners
{
    public class MultipleResourceOwnerProviderDefinedException : Exception
    {
        public MultipleResourceOwnerProviderDefinedException(string message) : base(message)
        {
        }
    }
}