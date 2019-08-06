using System;

namespace Common.Authorisation
{
    public interface IResourceOwnerProviderCollection
    {
        IResourceOwnerProvider Get(Type resourceType);
    }
}
