using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public interface IResourceBuilder
    {
        BuiltResource Build<TResource>(TResource resource) where TResource: IResource;

        BuiltCollectionResource Build<T>(ICollectionResource<T> collectionResource, IResource resource) where T : IResource;

        BuiltCollectionResource Build<T>(IPagedCollectionResource<T> collectionResource, IPaginationTemplate paginationTemplate) where T : IResource;
    }
}
