using Common.Api.Pagination.Interfaces;

namespace Common.Api.Pagination
{
    public class PaginationTemplate : IPaginationTemplate
    {
        public int PageOffset { get; set; }
        public int PageSize { get; set; }
    }
}