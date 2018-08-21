using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public interface IResourceBuilder
    {
        BuiltResource Build(IResource resource);

        BuiltCollectionResource Build<T>(ICollectionResource<T> collectionResource, ITemplate template) where T : IResource;

        BuiltCollectionResource Build<T>(IPagedCollectionResource<T> collectionResource, IPaginationTemplate paginationTemplate) where T : IResource;
    }
}
