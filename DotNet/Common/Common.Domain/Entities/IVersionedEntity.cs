namespace Common.Domain.Entities
{
    public interface IVersionedEntity : IEntity
    {
        int Version { get; }

        void UpdateVersion();
    }
}
