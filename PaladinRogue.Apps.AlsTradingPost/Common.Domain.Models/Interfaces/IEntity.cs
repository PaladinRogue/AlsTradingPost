
namespace Common.Domain.Models.Interfaces
{
    public interface IEntity
    {
        byte[] Version { get; set; }
        int GetHashCode();
    }
}
