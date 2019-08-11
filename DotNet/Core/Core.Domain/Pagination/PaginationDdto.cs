using PaladinRogue.Libray.Core.Domain.Pagination.Interfaces;

namespace PaladinRogue.Libray.Core.Domain.Pagination
{
    public class PaginationDdto : IPaginationDdto
    {
        public int PageSize { get; set; }
        public int PageOffset { get; set; }
    }
}
