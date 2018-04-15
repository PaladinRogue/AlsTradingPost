namespace Common.Application.Pagination.Interfaces
{
    public interface IPagintation
    {
        int PageSize { get; set; }
        int PageOffset { get; set; }
    }
}
