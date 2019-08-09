namespace Common.Application.Concurrency
{
    public interface IVersionAdto<T>
    {
        T Version { get; set; }
    }
}
