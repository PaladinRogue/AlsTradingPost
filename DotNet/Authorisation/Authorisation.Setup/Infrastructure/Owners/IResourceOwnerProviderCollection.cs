using System;

namespace PaladinRogue.Libray.Authorisation.Setup.Infrastructure.Owners
{
    public interface IResourceOwnerProviderCollection
    {
        IResourceOwnerProvider Get(Type resourceType);
    }
}
