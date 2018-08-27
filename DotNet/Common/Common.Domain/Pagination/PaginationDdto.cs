using Common.Domain.Pagination.Interfaces;

namespace Common.Domain.Pagination
{
    public class PaginationDdto : IPaginationDdto
    {
        public int PageSize { get; set; }
        public int PageOffset { get; set; }
    }
}
