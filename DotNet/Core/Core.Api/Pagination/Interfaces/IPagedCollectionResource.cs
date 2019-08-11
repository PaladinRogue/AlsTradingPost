using PaladinRogue.Libray.Core.Api.Resources;

namespace PaladinRogue.Libray.Core.Api.Pagination.Interfaces
{
    public interface IPagedCollectionResource<T>: ICollectionResource<T>, IPagedResource where T : IResource
    {
    }
}
