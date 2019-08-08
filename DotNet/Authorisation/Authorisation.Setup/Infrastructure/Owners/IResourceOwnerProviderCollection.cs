using System;

namespace Authorisation.Application
{
    public interface IResourceOwnerProviderCollection
    {
        IResourceOwnerProvider Get(Type resourceType);
    }
}
