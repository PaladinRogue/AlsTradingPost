using System;
using System.ComponentModel.DataAnnotations;
using Common.Domain.Models.Interfaces;

namespace Common.Domain.Models
{
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        public int GetConcurrencyVersion()
        {
            var version = Version;
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(version);
            }
            return BitConverter.ToInt32(version, 0);
        }
    }
}
