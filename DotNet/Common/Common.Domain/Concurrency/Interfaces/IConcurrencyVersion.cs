
namespace Common.Domain.Concurrency.Interfaces
{
    public interface IConcurrencyVersion
    {
        int Version { get; set; }

        string ToString();
    }
}
