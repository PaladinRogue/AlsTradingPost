using PaladinRogue.Library.Core.Api.Sorting;

namespace PaladinRogue.Library.Core.Api.Pagination.Interfaces
{
    public interface IPaginationTemplate : ISortTemplate
    {
        int PageOffset { get; set; }

        int PageSize { get; set; }
    }
}