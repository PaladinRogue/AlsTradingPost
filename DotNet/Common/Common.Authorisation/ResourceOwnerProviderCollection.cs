using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Authorisation
{
    public class ResourceOwnerProviderCollection : IResourceOwnerProviderCollection
    {
        private readonly IEnumerable<IResourceOwnerProvider> _resourceOwnerProviders;

        public ResourceOwnerProviderCollection(IEnumerable<IResourceOwnerProvider> resourceOwnerProviders)
        {
            _resourceOwnerProviders = resourceOwnerProviders;
        }

        public IResourceOwnerProvider Get(Type resourceType)
        {
            try
            {
                return _resourceOwnerProviders.SingleOrDefault(provider => provider.Type == resourceType)
                       ?? throw new ResourceOwnerProviderNotDefinedException($"Resource owner not defined for type: {nameof(resourceType)}");
            }
            catch (InvalidOperationException)
            {
                throw new MultipleResourceOwnerProviderDefinedException($"Multiple resource owner providers defined for type: {nameof(resourceType)}");
            }
        }
    }
}
