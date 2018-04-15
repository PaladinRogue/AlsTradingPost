namespace Common.Api.Sorting
{
    public interface IThenByTemplate : IOrdernByTemplate
    {
        string ThenBy { get; set; }
        bool ThenByAscending { get; set; }
    }
}