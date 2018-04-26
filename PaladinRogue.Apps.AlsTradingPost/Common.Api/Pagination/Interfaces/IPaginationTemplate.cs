using Common.Api.Resources;

namespace Common.Api.Pagination.Interfaces
{
    public interface IPaginationTemplate : ITemplate
    {
        int PageOffset { get; set; }
        int PageSize { get; set; }
    }
}