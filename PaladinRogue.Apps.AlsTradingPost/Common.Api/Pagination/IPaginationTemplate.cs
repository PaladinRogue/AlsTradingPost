namespace Common.Api.Pagination
{
    public interface IPaginationTemplate
    {
        int PageOffset { get; set; }
        int PageSize { get; set; }
    }
}