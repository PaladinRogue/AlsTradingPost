namespace Common.Api.Sorting
{
    public interface IOrderByTemplate
    {
        string OrderBy { get; set; }
        bool OrderByAscending { get; set; }
    }
}