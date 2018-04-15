using Common.Application.Pagination.Interfaces;

namespace Common.Application.Pagination
{
    public class PaginationAdto : IPagintationAdto
    {
        public int PageSize { get; set; }
        public int PageOffset { get; set; }
    }
}
