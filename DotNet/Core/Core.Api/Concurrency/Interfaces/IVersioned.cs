namespace PaladinRogue.Library.Core.Api.Concurrency.Interfaces
{
    public interface IVersioned<T>
    {
        T Version { get; set; }
    }
}
