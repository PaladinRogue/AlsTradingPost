using PaladinRogue.Libray.Core.Api.Resources;

namespace PaladinRogue.Libray.Core.Api.Links
{
    public interface ILinkBuilder
    {
        Links BuildLinks<TResource>(TResource resource) where TResource : IResource;

        Links BuildLinks<TResource, TTemplate>(TResource resource, TTemplate template) where TResource : IResource where TTemplate : IResource;
    }
}