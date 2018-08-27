namespace Common.Domain.Concurrency.Interfaces
{
    public interface IVersion<T>
    {
        T Version { get; set; }
    }
}
