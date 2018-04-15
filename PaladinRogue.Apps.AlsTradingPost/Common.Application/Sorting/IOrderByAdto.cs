namespace Common.Application.Sorting
{
    public interface IOrderByAdto
    {
        string OrderBy { get; set; }
        bool OrderByAscending { get; set; }
    }
}
