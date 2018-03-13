using System;
using System.ComponentModel.DataAnnotations;
using Common.Domain.Models.Interfaces;

namespace Common.Domain.Models
{
    public abstract class Entity : IEntity
    {
        [Timestamp]
        public byte[] Version { get; set; }

        public override int GetHashCode()
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
