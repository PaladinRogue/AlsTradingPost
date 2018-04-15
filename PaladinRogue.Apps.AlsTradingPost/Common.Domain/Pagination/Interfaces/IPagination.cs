namespace Common.Domain.Pagination.Interfaces
{
    public interface IPagination
    {
        int PageSize { get; set; }
        int PageOffset { get; set; }
    }
}
