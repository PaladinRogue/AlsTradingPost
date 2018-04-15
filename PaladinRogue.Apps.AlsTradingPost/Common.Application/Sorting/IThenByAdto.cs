namespace Common.Application.Sorting
{
    public interface IThenByAdto : IOrderByAdto
    {
        string ThenBy { get; set; }
        bool ThenByAscending { get; set; }
    }
}
