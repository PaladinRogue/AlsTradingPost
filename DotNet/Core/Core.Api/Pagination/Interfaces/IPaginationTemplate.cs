using PaladinRogue.Libray.Core.Api.Sorting;

namespace PaladinRogue.Libray.Core.Api.Pagination.Interfaces
{
    public interface IPaginationTemplate : ISortTemplate
    {
        int PageOffset { get; set; }

        int PageSize { get; set; }
    }
}