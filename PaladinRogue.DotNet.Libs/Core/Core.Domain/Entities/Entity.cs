using System;

namespace PaladinRogue.Library.Core.Domain.Entities
{
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; protected set; }
    }
}
