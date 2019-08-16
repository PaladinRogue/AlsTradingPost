using System;

namespace PaladinRogue.Library.Authorisation.Setup.Infrastructure.Owners
{
    public interface IResourceOwnerProviderCollection
    {
        IResourceOwnerProvider Get(Type resourceType);
    }
}
