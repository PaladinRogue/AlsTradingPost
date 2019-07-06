using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public interface IResourceBuilder
    {
        BuiltResource Build<T>(T resource) where T: IResource;

        BuiltCollectionResource BuildCollection<T>(ICollectionResource<T> collectionResource) where T : IResource;

        BuiltCollectionResource BuildCollection<T>(ICollectionResource<T> collectionResource, ITemplate template) where T : IResource;

        BuiltCollectionResource BuildCollection<T>(IPagedCollectionResource<T> collectionResource, IPaginationTemplate paginationTemplate) where T : IResource;
    }
}
