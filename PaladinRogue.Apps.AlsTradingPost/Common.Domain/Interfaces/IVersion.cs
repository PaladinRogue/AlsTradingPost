namespace Common.Domain.Interfaces
{
    public interface IVersion<T>
    {
        T Version { get; set; }
    }
}
