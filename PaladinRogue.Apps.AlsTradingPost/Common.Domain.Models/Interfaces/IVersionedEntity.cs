namespace Common.Domain.Models.Interfaces
{
    public interface IVersionedEntity : IEntity
    {
        byte[] Version { get; set; }
    }
}
