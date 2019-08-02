namespace Common.ApplicationServices.Concurrency
{
    public interface IVersionAdto<T>
    {
        T Version { get; set; }
    }
}
