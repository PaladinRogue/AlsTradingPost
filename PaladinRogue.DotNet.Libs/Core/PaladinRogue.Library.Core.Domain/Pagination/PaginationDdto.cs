using PaladinRogue.Library.Core.Domain.Pagination.Interfaces;

namespace PaladinRogue.Library.Core.Domain.Pagination
{
    public class PaginationDdto : IPaginationDdto
    {
        public int PageSize { get; set; }
        public int PageOffset { get; set; }
    }
}
