namespace Common.Domain.Models.Interfaces
{
    public interface IVersionedEntity : IEntity
    {
        int Version { get; }

        void UpdateVersion();
    }
}
