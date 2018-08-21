namespace Common.Api.Pagination
{
    public interface IPagedResource
    {
        int TotalResults { get; set; }
    }
}
