using PaladinRogue.Library.Core.Api.Pagination.Interfaces;
using PaladinRogue.Library.Core.Api.Resources;

namespace PaladinRogue.Library.Core.Api.Builders.Resource
{
    public interface IResourceBuilder
    {
        BuiltResource Build<T>(T resource) where T: IResource;

        BuiltCollectionResource BuildCollection<T>(ICollectionResource<T> collectionResource) where T : IResource;

        BuiltCollectionResource BuildCollection<T>(ICollectionResource<T> collectionResource, ITemplate template) where T : IResource;

        BuiltCollectionResource BuildCollection<T>(IPagedCollectionResource<T> collectionResource, IPaginationTemplate paginationTemplate) where T : IResource;
    }
}
