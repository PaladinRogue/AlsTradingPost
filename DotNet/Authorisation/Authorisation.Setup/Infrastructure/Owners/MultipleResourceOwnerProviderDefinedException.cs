using System;

namespace PaladinRogue.Library.Authorisation.Setup.Infrastructure.Owners
{
    public class MultipleResourceOwnerProviderDefinedException : Exception
    {
        public MultipleResourceOwnerProviderDefinedException(string message) : base(message)
        {
        }
    }
}