namespace PaladinRogue.Library.Core.Application.Concurrency
{
    public interface IVersionAdto<T>
    {
        T Version { get; set; }
    }
}
