namespace PaladinRogue.Library.Core.Domain.Pagination.Interfaces
{
    public interface IPaginationDdto
    {
        int PageSize { get; set; }
        int PageOffset { get; set; }
    }
}
