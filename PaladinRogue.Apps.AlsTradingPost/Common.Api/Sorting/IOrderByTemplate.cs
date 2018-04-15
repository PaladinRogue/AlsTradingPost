namespace Common.Api.Sorting
{
    public interface IOrdernByTemplate
    {
        string OrderBy { get; set; }
        bool OrderByAscending { get; set; }
    }
}