using Common.Api.Resources;

namespace Common.Api.Pagination.Interfaces
{
    public interface IPagedCollectionResource<T>: ICollectionResource<T> where T : ISummaryResource
    {
        int TotalResults { get; set; }
    }
}
