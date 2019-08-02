using Common.Api.Sorting;

namespace Common.Api.Pagination.Interfaces
{
    public interface IPaginationTemplate : ISortTemplate
    {
        int PageOffset { get; set; }

        int PageSize { get; set; }
    }
}