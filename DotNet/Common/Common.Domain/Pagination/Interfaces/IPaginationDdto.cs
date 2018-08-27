namespace Common.Domain.Pagination.Interfaces
{
    public interface IPaginationDdto
    {
        int PageSize { get; set; }
        int PageOffset { get; set; }
    }
}
