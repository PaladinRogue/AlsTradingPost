namespace Common.Api.Sorting
{
    public interface IThenByTemplate : IOrderByTemplate
    {
        string ThenBy { get; set; }
        bool? ThenByAscending { get; set; }
    }
}