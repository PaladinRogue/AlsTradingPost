
namespace Common.Domain.Concurrency.Interfaces
{
    public interface IConcurrencyVersion
    {
        byte[] Version { get; set; }
    }
}
