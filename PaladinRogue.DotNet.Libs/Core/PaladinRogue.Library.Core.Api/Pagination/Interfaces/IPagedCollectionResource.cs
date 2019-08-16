using PaladinRogue.Library.Core.Api.Resources;

namespace PaladinRogue.Library.Core.Api.Pagination.Interfaces
{
    public interface IPagedCollectionResource<T>: ICollectionResource<T>, IPagedResource where T : IResource
    {
    }
}
