using Common.Api.Resources;

namespace Common.Api.Pagination.Interfaces
{
    public interface IPagedCollectionResource<T>: ICollectionResource<T>, IPagedResource where T : IResource
    {
    }
}
