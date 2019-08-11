namespace PaladinRogue.Libray.Core.Domain.Entities
{
    public interface IVersionedEntity : IEntity
    {
        int Version { get; }

        void UpdateVersion();
    }
}
