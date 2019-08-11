namespace PaladinRogue.Libray.Core.Application.Concurrency
{
    public interface IVersionAdto<T>
    {
        T Version { get; set; }
    }
}
