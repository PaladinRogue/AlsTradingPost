namespace Common.Api.Interfaces
{
    public interface IVersioned<T>
    {
        T Version { get; set; }
    }
}
