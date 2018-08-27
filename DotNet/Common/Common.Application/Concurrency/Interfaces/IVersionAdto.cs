namespace Common.Application.Concurrency.Interfaces
{
    public interface IVersionAdto<T>
    {
        T Version { get; set; }
    }
}
