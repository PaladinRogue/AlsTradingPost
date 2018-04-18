namespace Common.Api.Pagination.Interfaces
{
    public interface IPaginationTemplate
    {
        int PageOffset { get; set; }
        int PageSize { get; set; }
    }
}