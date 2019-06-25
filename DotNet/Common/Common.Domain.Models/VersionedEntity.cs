using System;
using Common.Domain.Models.Interfaces;

namespace Common.Domain.Models
{
    public abstract class VersionedEntity : IVersionedEntity
    {
        protected VersionedEntity()
        {
            Id = Guid.NewGuid();
            Version = 0;
        }

        public Guid Id { get; protected set; }

        public int Version { get; protected set; }

        public void UpdateVersion()
        {
            Version++;
        }
    }
}
